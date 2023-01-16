using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesseract_OCR.Core
{
    class Files
    {
        public string? _fileName;
        public string? _fileType;
        public bool _isFileProcessed=false;

        public Files(string? fileName, string? fileType)
        {
            _fileName = fileName;
            _fileType = fileType;
        }
    }
}
