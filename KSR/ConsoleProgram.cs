using KSR.Logic;
using KSR.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace KSR
{
    class ConsoleProgram
    {
        static void Main(string[] args)
        {
            GenerateAppSettingsJson();
            for (int i = 0; i < 10; i+=3)
            {

                var settings = ReadInitialValues(i);
                var articlesRepository = ReadFile();
                articlesRepository.SetAmountOfArticlesInSets(settings);
                var keyWords = new KeyWords(articlesRepository.ArticlesForLearning);
                var vectorFeatureCreator = new VectorFeatureCreator(keyWords, settings);
                vectorFeatureCreator.CreateVectorFeature(articlesRepository.ArticlesForLearning);
                vectorFeatureCreator.CreateVectorFeature(articlesRepository.ArticlesForValidation);

                var knnProcesor = new KnnProcessor();
                knnProcesor.Calculate(settings.Metric, articlesRepository.ArticlesForLearning, articlesRepository.ArticlesForValidation, settings);
            }
            Console.Beep(800, 200);
        }


        public static AtricleRepository ReadFile()
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
            var articlesRepo = new AtricleRepository();
            articlesRepo.CompleteRepository(path + @"/Data");
            return articlesRepo;
        }

        public static Settings ReadInitialValues(int interator)
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $@"..\..\..\AppSettings\Settings{interator}.json"));
            using StreamReader file = File.OpenText(path);
            JsonSerializer serializer = new JsonSerializer();
            var settings = (Settings)serializer.Deserialize(file, typeof(Settings));

            return settings;
        }

        public static void GenerateAppSettingsJson()
        {
            var metrics = new List<string>() { "Chebyshev", "Euclidean", "Manhattan" };
            var neighbours = new List<int>() { 1, 3, 5, 7, 9, 12, 15, 17, 20, 25 };
            var trainingsets = new List<double>() { 0.4, 0.6, 0.8 };

            string path = $@"C:\Users\{Environment.UserName}\Desktop\appset\";

            var counter = 0;
            foreach (var metric in metrics)
            {
                foreach (var neighbour in neighbours)
                {
                    foreach (var trainingset in trainingsets)
                    {
                        using var file = new System.IO.StreamWriter($"{path}Settings{counter}.json");

                        file.WriteLine("{");
                        file.WriteLine($"\"Metric\": \"{metric}\",");
                        file.WriteLine($"\"Neighbours\": \"{neighbour}\",");
                        file.WriteLine($"\"TrainingSet\": {trainingset},");
                        file.WriteLine("\n");
                        file.WriteLine("\"Measures\": {");
                        file.WriteLine("\"KeyWordsCount\": \"True\",");
                        file.WriteLine("\"MeanKeyWordLength\": \"True\",");
                        file.WriteLine("\"FirstCapitalShorterThan4\": \"True\",");
                        file.WriteLine("\"FirstCapitalLengthFrom4To6\": \"True\",");
                        file.WriteLine("\"KeyWordsLongerThan8\": \"True\",");
                        file.WriteLine("\"DashSeparatedKeyWords\": \"True\",");
                        file.WriteLine("\"FirstKeywordPosition\": \"True\",");
                        file.WriteLine("\"ContainsKeyWord\": \"True\",");
                        file.WriteLine("\"KeyWordsWithAllCapitalLetters\": \"True\",");
                        file.WriteLine("\"KeyWordsStartedWithFirstCapital\": \"True\",");
                        file.WriteLine("\"KeyWordsStartedWithFirstLower\": \"True\",");
                        file.WriteLine("\"UniqueWords\": \"True\",");
                        file.WriteLine("}");
                        file.WriteLine("}");

                        counter++;
                    }
                }
            }

        }

    }
}
