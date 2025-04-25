﻿using ASKTech.Core.Abstractions;
using ASKTech.Core.Database;
using ASKTech.Issues.Contracts.Issue;
using CSharpFunctionalExtensions;
using Dapper;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Issue.Queries.GetIssuesByModuleWithPagination
{
    public class GetIssuesByModuleWithPaginationHandler
      : IQueryHandlerWithResult<PagedList<IssueDto>, GetFilteredIssuesByModuleWithPaginationQuery>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetIssuesByModuleWithPaginationHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<PagedList<IssueDto>, ErrorList>> Handle(
            GetFilteredIssuesByModuleWithPaginationQuery query,
            CancellationToken cancellationToken = default)
        {
            using var connection = _sqlConnectionFactory.Create();

            var parameters = new DynamicParameters();
            parameters.Add("@ModuleId", query.ModuleId); // Добавляем ModuleId в параметры

            var sqlBuilder = new StringBuilder(
                """
            SELECT
                i.id AS Id,
                i.module_id AS ModuleId,
                i.lesson_id AS LessonId,
                ip.position AS Position,
                i.files AS Files,
                i.is_deleted AS IsDeleted,
                i.description AS Description,
                i.title AS Title
            FROM issues.issues AS i
                     JOIN issues.issue_positions AS ip
                          ON i.id = ip.issue_id
            WHERE NOT i.is_deleted
              AND i.module_id = @ModuleId
            """);

            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                sqlBuilder.Append("\nAND i.title ILIKE @Title");
                parameters.Add("@Title", $"%{query.Title}%");
            }

            var allowedSortColumns = new List<string>
        {
            "Id",
            "ModuleId",
            "LessonId",
            "Position",
            "Files",
            "IsDeleted",
            "Description",
            "Title",
        };

            string? sortBy = allowedSortColumns.Contains(query.SortBy) ? query.SortBy : "Id";
            string sortDirection = query.SortDirection?.ToUpper() == "DESC" ? "DESC" : "ASC";
            sqlBuilder.ApplySorting(sortBy, sortDirection);
            sqlBuilder.ApplyPagination(parameters, query.Page, query.PageSize);

            var totalCountSql = new StringBuilder(
                """
            SELECT COUNT(*)
            FROM issues.issues AS i
            JOIN issues.issue_positions AS ip ON i.id = ip.issue_id
            WHERE NOT i.is_deleted
            AND i.module_id = @ModuleId
            """);

            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                totalCountSql.Append("\nAND i.title ILIKE @Title");
            }

            long totalCount = await connection.ExecuteScalarAsync<long>(
                totalCountSql.ToString(),
                parameters);

            var issues = await connection.QueryAsync<IssueDto>(
                sqlBuilder.ToString(),
                param: parameters);

            return new PagedList<IssueDto>
            {
                Items = issues.ToList(),
                TotalCount = totalCount,
                PageSize = query.PageSize,
                Page = query.Page,
            };
        }
    }
}
