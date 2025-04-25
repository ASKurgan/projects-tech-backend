using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.DataModels
{
    public record CommentDataModel(
     Guid Id,
     Guid UserId,
     string Message,
     DateTime CreatedAt,
     Guid IssueReviewId);
}
