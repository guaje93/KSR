﻿using KSR.Extractors;
using KSR.Logic;
using KSR.Model;
using Logic.Metrics;
using Newtonsoft.Json;
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
            var settings = ReadInitialValues();
            var articlesRepository =  ReadFile();
            articlesRepository.SetAmountOfArticlesInSets(settings);
            var keyWords = new KeyWords(articlesRepository.ArticlesForLearning);
            var vectorFeatureCreator = new VectorFeatureCreator(keyWords, settings);
            vectorFeatureCreator.CreateVectorFeature(articlesRepository.ArticlesForLearning);
            vectorFeatureCreator.CreateVectorFeature(articlesRepository.ArticlesForValidation);

            var knnProcesor = new KnnProcessor();
            knnProcesor.Calculate(settings.Metric, articlesRepository.ArticlesForLearning, articlesRepository.ArticlesForValidation, settings.Neighbours);
            
           // Console.Beep(800, 200);
        }


        public static AtricleRepository ReadFile()
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
            var articlesRepo = new AtricleRepository();
            articlesRepo.CompleteRepository(path + @"/Data");
            return articlesRepo;
        }

        public static Settings ReadInitialValues()
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\AppSettings\Settings.json"));
            using StreamReader file = File.OpenText(path);
            JsonSerializer serializer = new JsonSerializer();
            var settings = (Settings)serializer.Deserialize(file, typeof(Settings));
           
            return settings;
        }

    }
}
