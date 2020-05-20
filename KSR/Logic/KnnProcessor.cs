using KSR.Logic.Helpers;
using KSR.Logic.Metric;
using KSR.Model;
using System.Collections.Generic;
using System.Linq;

namespace KSR.Logic.Knn
{
    public class KnnProcessor
    {
        #region Fields

        private IMetric _metric;
        private readonly KnnAlgorithm _knnAlgorithm;

        #endregion

        #region Constructors

        public KnnProcessor()
        {
            _knnAlgorithm = new KnnAlgorithm();
        }

        #endregion

        #region Public Methods

        public void Calculate(string metric, List<Article> trainingArticles, List<Article> testArticles, Settings settings)
        {
            SetMetric(metric);
            for (int i = 0; i < testArticles.Count; i++)
            {
                _metric.Calculate(trainingArticles, testArticles.ElementAt(i), settings.Neighbours);
                _knnAlgorithm.AssignCountry(testArticles.ElementAt(i), trainingArticles, settings.Neighbours);
            }

            SaveMatchData(testArticles, settings);
        }

        #endregion

        #region Private Methods

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


        private void SaveMatchData(IEnumerable<Article> testArticles, Settings settings)
        {
            var classificationInfos = new List<ClassificationInfo>();

            Dictionary<string, int> assignAmounts = new Dictionary<string, int>();
            var grouppedPlaces = testArticles.GroupBy(x => x.Place).OrderBy(x => x.Key);
            var assigned = new Dictionary<string, int>();
            var FalsePositive = new Dictionary<string, int>();
            var TruePositive = new Dictionary<string, int>();
            var classInfo = new Dictionary<string, Dictionary<string, double>>();
            foreach (var group in grouppedPlaces)
            {
                classInfo.Add(group.Key, new Dictionary<string, double>());
                //classInfo[group.Key].Add("classified", group.Where(x => x.AssignedPlace == group.Key.ToLower()).Count());
                classInfo[group.Key].Add("canada", group.Where(p => p.AssignedPlace == "canada").Count());
                classInfo[group.Key].Add("france", group.Where(p => p.AssignedPlace == "france").Count());
                classInfo[group.Key].Add("japan", group.Where(p => p.AssignedPlace == "japan").Count());
                classInfo[group.Key].Add("uk", group.Where(p => p.AssignedPlace == "uk").Count());
                classInfo[group.Key].Add("usa", group.Where(p => p.AssignedPlace == "usa").Count());
                classInfo[group.Key].Add("west-germany", group.Where(p => p.AssignedPlace == "west-germany").Count());
                classInfo[group.Key].Add("FN", group.Count() - group.Where(p => p.AssignedPlace == group.Key).Count());
                classInfo[group.Key].Add("Recall", group.Where(p => p.AssignedPlace == group.Key).Count() * 1.0 / group.Count());


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

            var chartGenerator = new ChartDataGenerator();
            chartGenerator.GenerateChartDataFromBatch(classificationInfos, classInfo, Values, settings);

        }     

        #endregion
    }
}
