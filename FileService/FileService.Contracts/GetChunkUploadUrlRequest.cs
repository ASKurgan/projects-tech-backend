using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts
{
    public record GetChunkUploadUrlRequest(
     string FileId,
     string UploadId,
     int PartNumber,
     string BucketName);
}
