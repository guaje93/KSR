using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KSR
{
    public class SgmFileLoader
    {
        public string[] Files { get; private set; }

        public bool ReadFiles(string directoryPath)
        {
            Files = Directory.GetFiles(directoryPath, "*.sgm", SearchOption.TopDirectoryOnly);
            if (Files == null || Files.Length == 0)
                return false;
            else return true;
        }
    }
}
