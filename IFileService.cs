using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superpad
{
    public interface IFileService
    {
        void CreateFile();
        void OpenFile();
        void SaveFile();
        string FileName { get; set; }
        string FilePath { get; set; }

        string FileContent { get; set; }
    }
}
