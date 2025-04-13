using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Communication
{
    public class FileServiceOptions
    {
        public const string FILE_SERVICE = "FileService";

        public string Url { get; init; } = string.Empty;
    }
}
