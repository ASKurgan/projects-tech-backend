﻿using FileService.Contracts;
using FileService.Services;
using ASKTech.Framework.Endpoints;
using ASKTech.Core.Database;
using System.Net.WebSockets;
using ASKTech.Framework.Authorization;

namespace FileService.Features
{
    public class CancelMultipartUpload
    {


        public sealed class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapPost("api/files/multipart/cancel", Handler)
                    .RequireAuthorization(Permissions.Files.UPLOAD_FILES);
            }
        }

        private static async Task<IResult> Handler(
            CancelMultipartUploadRequest request,
            IS3Provider s3Provider,
            CancellationToken cancellationToken)
        {
          
            await s3Provider.AbortMultipartUploadAsync(request.FileLocation, request.UploadId, cancellationToken);

            return ResultResponse.Ok();
        }
    }
}
