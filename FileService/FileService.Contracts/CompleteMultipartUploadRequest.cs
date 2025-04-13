using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts
{
    public record CompleteMultipartUploadRequest(
    string FileId,
    string UploadId,
    List<PartETagDto> PartETags,
    string BucketName);
}
