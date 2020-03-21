using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace KSR
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadFile();
        }


        public static void ReadFile()
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\")); ;
            var contentLoader = new ContentLoader();
            contentLoader.ReadFile(path+@"/reuters21578");

        }

    }
}
