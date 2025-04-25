using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.Lesson
{
    public record FileMetadataRequest(string FileName, string ContentType, long Size);
}
