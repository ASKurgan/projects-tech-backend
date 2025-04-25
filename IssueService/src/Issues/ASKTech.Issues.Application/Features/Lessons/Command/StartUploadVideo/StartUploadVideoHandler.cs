using ASKTech.Core.Abstractions;
using ASKTech.Issues.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using FileService.Communication;
using FileService.Contracts;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Command.StartUploadVideo
{
    // TODO: добавить валидатор
    public class StartUploadVideoHandler : ICommandHandler<StartMultipartUploadResponse, StartUploadVideoCommand>
    {
        private readonly IFileService _fileService;

        public StartUploadVideoHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<Result<StartMultipartUploadResponse, ErrorList>> Handle(
            StartUploadVideoCommand command,
            CancellationToken cancellationToken = default)
        {
            var validateResult = Video.Validate(
                command.FileName,
                command.ContentType,
                command.Size);

            if (validateResult.IsFailure)
                return validateResult.Error.ToErrorList();

            var startMultipartRequest = new StartMultipartUploadRequest(
                command.FileName,
                Video.LOCATION,
                command.ContentType,
                command.Size);

            var result = await _fileService.StartMultipartUpload(
                startMultipartRequest,
                cancellationToken);

            if (result.IsFailure)
                return result.Error;

            return result.Value;
        }
    }
}
