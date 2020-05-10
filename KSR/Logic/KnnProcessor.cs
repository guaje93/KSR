using KSR.Model;
using Logic.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KSR.Logic
{
    class KnnProcessor
    {
        private IMetric _metric;
        private string _outputPath = $@"C:\Users\{Environment.UserName}\Desktop\probki\Sample_29.txt";

        public void Calculate(string metric, List<Article> trainingArticles, List<Article> testArticles, int negihboursAmount)
        {
            SetMetric(metric);
            _metric.Calculate(trainingArticles, testArticles, negihboursAmount);
            CheckMatch(testArticles);
        }

        private void SetMetric(string metric)
        {

            switch (metric)
            {
                case "Chebyshev":
                    _metric = new Chebyshev();
                    break;
                case "Euclidean":
                    _metric = new Euclidean();
                    break;
                case "Manhattan":
                    _metric = new Manhattan();
                    break;
            }
        }
        

        private void CheckMatch(IEnumerable<Article> testArticles)
        {
            var classificationInfos = new List<ClassificationInfo>();

            Dictionary<string, int> assignAmounts = new Dictionary<string, int>();
            var grouppedPlaces = testArticles.GroupBy(x => x.Place).OrderBy(x=> x.Key);
            var assigned = new Dictionary<string, int>();
            var FalsePositive = new Dictionary<string, int>();
            var TruePositive = new Dictionary<string, int>();
            var classInfo = new Dictionary<string, Dictionary<string, int>>();
            foreach (var group in grouppedPlaces)
            {
                classInfo.Add(group.Key, new Dictionary<string, int>());
                //classInfo[group.Key].Add("classified", group.Where(x => x.AssignedPlace == group.Key.ToLower()).Count());
                classInfo[group.Key].Add("canada", group.Where(p => p.AssignedPlace == "canada").Count());
                classInfo[group.Key].Add("france", group.Where(p => p.AssignedPlace == "france").Count());
                classInfo[group.Key].Add("japan", group.Where(p => p.AssignedPlace == "japan").Count());
                classInfo[group.Key].Add("uk", group.Where(p => p.AssignedPlace == "uk").Count());
                classInfo[group.Key].Add("usa", group.Where(p => p.AssignedPlace == "usa").Count());
                classInfo[group.Key].Add("west-germany", group.Where(p => p.AssignedPlace == "west-germany").Count());


                classificationInfos.Add(new ClassificationInfo()
                {
                    Country = group.Key,
                    ClassifiedAmount = group.Where(x => x.AssignedPlace == group.Key.ToLower()).Count(),
                    ArticlesAmount = group.Count(),
                    Canada = group.Where(p => p.AssignedPlace == "canada").Count(),
                    France = group.Where(p => p.AssignedPlace == "france").Count(),
                    Usa = group.Where(p => p.AssignedPlace == "usa").Count(),
                    Japan = group.Where(p => p.AssignedPlace == "japan").Count(),
                    Uk = group.Where(p => p.AssignedPlace == "uk").Count(),
                    West_germany = group.Where(p => p.AssignedPlace == "west-germany").Count(),
                    TrueNegative = group.Count() - group.Where(p => p.AssignedPlace == group.Key).Count(),
                    Recall = group.Where(p => p.AssignedPlace == group.Key).Count() * 1.0 / group.Count(),
                });
                foreach (var country in grouppedPlaces.Select(p => p.Key).Distinct())
                {
                    if (FalsePositive.ContainsKey(country))
                        FalsePositive[country] += group.Where(p => p.AssignedPlace != group.Key && p.AssignedPlace == country).Count();
                    else FalsePositive.Add(country, group.Where(p => p.AssignedPlace != group.Key && p.AssignedPlace == country).Count());
                }
                foreach (var country in grouppedPlaces.Select(p => p.Key).Distinct())
                {
                    if (TruePositive.ContainsKey(country))
                        TruePositive[country] += group.Where(p => p.AssignedPlace == country && country == group.Key).Count();
                    else TruePositive.Add(country, group.Where(p => p.AssignedPlace == country && country == group.Key).Count());
                }
            }

            List<(string, int, int, double)> Values = new List<(string, int, int, double)>();
            foreach (var item in grouppedPlaces)
            {
                var trues = TruePositive.FirstOrDefault(p => p.Key == item.Key).Value;
                var falses = FalsePositive.FirstOrDefault(p => p.Key == item.Key).Value;
                Values.Add((item.Key, trues, falses, 1.0 * trues / (trues + falses)));
            }



            using var file = SaveFile(classificationInfos, Values, classInfo) ;

        }

        private System.IO.StreamWriter SaveFile(List<ClassificationInfo> classificationInfos, List<(string country, int trues, int falses, double prescision)> values, Dictionary<string, Dictionary<string, int>> clasinfo)
        {
            var file = new System.IO.StreamWriter(_outputPath);
            foreach (var item in classificationInfos)
            {
                file.WriteLine("-------------------------");
                file.WriteLine($"Country: {item.Country}");
                file.WriteLine($"TotalArticles: {item.ArticlesAmount}");
                file.WriteLine($"Canada: {item.Canada}");
                file.WriteLine($"France: {item.France}");
                file.WriteLine($"Usa: {item.Usa}");
                file.WriteLine($"Japan: {item.Japan}");
                file.WriteLine($"Uk: {item.Uk}");
                file.WriteLine($"West_germany: {item.West_germany}");
                file.WriteLine($"TrueNegative : {item.TrueNegative}");
                file.WriteLine($"Recall: {item.Recall}");
                file.WriteLine("-------------------------");
            }

            foreach (var item in values)
            {
                file.WriteLine("-------------------------");
                file.WriteLine("-------------------------");
                file.WriteLine("-------------------------");
                file.WriteLine($"Country: {item.country}");
                file.WriteLine($"TruePositive: {item.trues}");
                file.WriteLine($"FalsePositive: {item.falses}");
                file.WriteLine($"Precision: {item.prescision}");
                file.WriteLine("-------------------------");
            }


            var resultString = string.Empty;
            foreach (var item in clasinfo)
            {
                resultString += "\n";
                resultString += $"{item.Key}";
                foreach (var elem in item.Value)
                {
                    resultString += $" &{elem.Value}";
                }
                resultString += "\\\\";
            }

            file.WriteLine(resultString);



            return file;
        }
    }
}
