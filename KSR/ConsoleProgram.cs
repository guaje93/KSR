using KSR.Extractors;
using KSR.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace KSR
{
    class ConsoleProgram
    {
        static void Main(string[] args)
        {
            ReadFile();
        }


        public static void ReadFile()
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\")); ;
            var contentLoader = new AtricleRepository();
            contentLoader.CompleteRepository(path+@"/Data");


            var vectors = new Dictionary<Article, AnalysisVector>();

            foreach (var article in contentLoader.Articles)
            {
                vectors.Add(article, new AnalysisVector()
                {


                }); ;
            }
        
        }

    }
}
