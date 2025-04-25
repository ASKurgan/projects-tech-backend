// using System.Linq.Expressions;
// using Microsoft.EntityFrameworkCore;
// using ASKTech.Core.Abstractions;
// using ASKTech.Core.Database;
// using ASKTech.Issues.Application.DataModels;
// using ASKTech.Issues.Application.Interfaces;
// using ASKTech.Issues.Contracts.IssueReview;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssuesReviews.Queries.GetCommentsWithPagination
{
    // public class GetCommentsWithPaginationHandler
    //     : IQueryHandler<PagedList<CommentResponse>, GetCommentsWithPaginationQuery>
    // {
    //     private readonly IIssuesReadDbContext _readDbContext;
    //
    //     public GetCommentsWithPaginationHandler(IIssuesReadDbContext readDbContext)
    //     {
    //         _readDbContext = readDbContext;
    //     }
    //
    //     public async Task<PagedList<CommentResponse>> Handle(
    //         GetCommentsWithPaginationQuery query,
    //         CancellationToken cancellationToken)
    //     {
    //         var commentsQuery = _readDbContext.ReadComments
    //             .Where(c => c.IssueReviewId == query.IssueReviewId);
    //
    //         int totalCount = await commentsQuery.CountAsync(cancellationToken);
    //
    //         Expression<Func<CommentDataModel, object>> keySelector = query.SortBy?.ToLower() switch
    //         {
    //             "createdat" => (commentDto) => commentDto.CreatedAt,
    //             "message" => (commentDto) => commentDto.Message,
    //             _ => (commentDto) => commentDto.UserId
    //         };
    //
    //         commentsQuery = query.SortDirection?.ToLower() == "desc"
    //             ? commentsQuery.OrderByDescending(keySelector)
    //             : commentsQuery.OrderBy(keySelector);
    //
    //         var comments = commentsQuery.ToList()
    //             .Select(i => new CommentResponse
    //                 {
    //                     Id = i.Id,
    //                     UserId = i.UserId,
    //                     IssueReviewId = i.IssueReviewId,
    //                     Message = i.Message,
    //                     CreatedAt = i.CreatedAt,
    //                 });
    //
    //         return new PagedList<CommentResponse>
    //         {
    //             Items = comments.ToList(), TotalCount = totalCount, PageSize = query.PageSize, Page = query.Page,
    //         };
    //     }
    // }
}
