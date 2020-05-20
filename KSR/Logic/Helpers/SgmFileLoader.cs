using System.IO;

namespace KSR.Logic.Helpers
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
