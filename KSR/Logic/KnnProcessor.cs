using KSR.Model;
using Logic.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Logic
{
    class KnnProcessor
    {
        public void Calculate(string metric, List<Article> trainingArticles, List<Article> testArticles, int negihboursAmount)
        {
            if (metric == "Manhattan")
            {
                var manhatanMetric = new Manhattan();
                manhatanMetric.Calculate(trainingArticles, testArticles, negihboursAmount);
                CheckMatch(testArticles);
            }
            else if (metric == "Euclidean")
            {
                var euclideanMetric = new Euclidean();
                euclideanMetric.Calculate(trainingArticles, testArticles, negihboursAmount);
                CheckMatch(testArticles);
            }
            else
            {
                var chebyshevMetric = new Chebyshev();
                chebyshevMetric.Calculate(trainingArticles, testArticles, negihboursAmount);
                CheckMatch(testArticles);
            }
        }

        private void CheckMatch(List<Article> testArticles)
        {
            var classificationInfos = new List<ClassificationInfo>();

            Dictionary<string, int> assignAmounts = new Dictionary<string, int>();
            var grouppedPlaces = testArticles.GroupBy(x => x.Place); ;
            foreach (var group in grouppedPlaces)
            {
                var counter = 0;
                foreach (var item in group)
                {
                    if (item.AssignedPlace == group.Key)
                        counter++;
                }
                classificationInfos.Add(new ClassificationInfo()
                {
                    Country = group.Key,
                    ArticlesAmount = group.Count(),
                    ClassifiedAmount = counter
                });
            }

            using var file = new System.IO.StreamWriter($@"C:\Users\{Environment.UserName}\Desktop\probki\Sample.txt");
            foreach (var item in classificationInfos)
            {
                Console.WriteLine("Done");
                file.WriteLine("-------------------------");
                file.WriteLine($"Country: {item.Country}");
                file.WriteLine($"TotalArticles: {item.ArticlesAmount}");
                file.WriteLine($"Classified Articles: {item.ClassifiedAmount}");
            }
        }
    }
}
