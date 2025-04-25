using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Command.StartUploadVideo
{
    public record StartUploadVideoCommand(
     string FileName,
     string ContentType,
     long Size) : ICommand;
}
