using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KSR.Logic.Helpers
{
    public class AppSettingsGenerator
    {
        #region Fields

        private string path = $@"C:\Users\{Environment.UserName}\Desktop\appset\";

        #endregion

        #region Public Methods

        public int GetAppSettingsFilesAmount()
        {
            return Directory.GetFiles(path,"*.json").Length;
        }
        public void Generate()
        {
            var metrics = new List<string>() { "Chebyshev", "Euclidean", "Manhattan" };
            var neighbours = new List<int>() { 1, 3, 5, 7, 9, 12, 15, 17, 20, 25 };
            var trainingsets = new List<double>() { 0.2, 0.4, 0.5, 0.6, 0.8 };


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
        #endregion
    }
}