using KSR.Logic.Metrics;
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
                    _metric = new Euclidean();
                    break;
                case "Euclidean":
                    _metric = new Chebyshev();
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
            var grouppedPlaces = testArticles.GroupBy(x => x.Place);
            var assigned = new Dictionary<string, int>();
            foreach (var group in grouppedPlaces)
            {
                classificationInfos.Add(new ClassificationInfo()
                {
                    Country = group.Key,
                    ArticlesAmount = group.Count(),
                    Canada = group.Where(p => p.AssignedPlace == "canada").Count(),
                    France = group.Where(p => p.AssignedPlace == "france").Count(),
                    Usa = group.Where(p => p.AssignedPlace == "usa").Count(),
                    Japan = group.Where(p => p.AssignedPlace == "japan").Count(),
                    Uk = group.Where(p => p.AssignedPlace == "uk").Count(),
                    West_germany = group.Where(p => p.AssignedPlace == "west-germany").Count(),
                });
            }
            using var file = SaveFile(classificationInfos);

        }

        private System.IO.StreamWriter SaveFile(List<ClassificationInfo> classificationInfos)
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
                file.WriteLine("-------------------------");
            }

            return file;
        }
    }
}
