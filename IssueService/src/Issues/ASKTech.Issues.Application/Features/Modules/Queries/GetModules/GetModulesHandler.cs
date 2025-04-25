using ASKTech.Core.Abstractions;
using ASKTech.Core.Database;
using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Contracts.Dtos;
using ASKTech.Issues.Contracts.Module;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Queries.GetModules
{
    public class GetModulesHandler : IQueryHandler<PagedList<ModuleDto>, GetModulesQuery>
    {
        private readonly IIssuesReadDbContext _readDbContext;

        public GetModulesHandler(IIssuesReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<PagedList<ModuleDto>> Handle(
            GetModulesQuery query,
            CancellationToken cancellationToken = default)
        {
            var modulesQuery = _readDbContext.ReadModules;

            if (!string.IsNullOrWhiteSpace(query.Title))
                modulesQuery = modulesQuery.Where(m => EF.Functions.Like(m.Title.Value.ToLower(), $"%{query.Title.ToLower()}%"));

            var modulesPagedList = await modulesQuery.ToPagedList(
                query.Page,
                query.PageSize,
                m => new ModuleDto(
                    m.Id,
                    m.Title.Value,
                    m.Description.Value,
                    m.IssuesPosition.Select(i => new IssuePositionDto(i.IssueId.Value, i.Position.Value)).ToList(),
                    m.LessonsPosition.Select(l => new LessonPositionDto(l.LessonId.Value, l.Position.Value)).ToList()),
                cancellationToken);

            return modulesPagedList;
        }
    }
}
