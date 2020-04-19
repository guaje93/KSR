using KSR.Extractors;
using KSR.Logic;
using KSR.Model;
using Logic.Metrics;
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
            var chosenMetric = "Euclidean";
            var numberOfNeughbours =20;
           var articlesRepository =  ReadFile();
            var keyWords = new KeyWords(articlesRepository.ArticlesForLearning);
            var vectorFeatureCreator = new VectorFeatureCreator();
            vectorFeatureCreator.CreateVectorFeature(articlesRepository.ArticlesForLearning, keyWords);
            vectorFeatureCreator.CreateVectorFeature(articlesRepository.ArticlesForValidation, keyWords);

            var knnProcesor = new KnnProcessor();
            knnProcesor.Calculate(chosenMetric, articlesRepository.ArticlesForValidation, articlesRepository.ArticlesForLearning, numberOfNeughbours);
            Console.Beep(800, 200);
        }


        public static AtricleRepository ReadFile()
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\")); ;
            var articlesRepo = new AtricleRepository();
            articlesRepo.CompleteRepository(path + @"/Data");
            return articlesRepo;
        }

    }
}
