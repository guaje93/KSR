using KSR.Model;
using Logic.Metrics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KSR.Logic
{
    class KnnProcessor
    {
        private IMetric _metric;
        private string _outputPath = $@"C:\Users\{Environment.UserName}\Desktop\raw\";
        private string _latexTablePath = $@"C:\Users\{Environment.UserName}\Desktop\latex\";

        public void Calculate(string metric, List<Article> trainingArticles, List<Article> testArticles, Settings settings)
        {
            SetMetric(metric);
            _metric.Calculate(trainingArticles, testArticles, settings.Neighbours);
            CheckMatch(testArticles, settings);
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


        private void CheckMatch(IEnumerable<Article> testArticles, Settings settings)
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

            using var file = SaveFile(classificationInfos, Values, classInfo, settings);
            using var latexTable = GenerateLatexFormatTable(Values, classInfo, settings);
            using var pData = GeneratePrecisionData(Values, classInfo, settings);
            using var rData = GenerateRecallData(Values, classInfo, settings);
            using var aData = GenerateAccuracyData(Values, classInfo, settings);

        }

        private System.IO.StreamWriter GeneratePrecisionData(List<(string country, int trues, int falses, double precision)> values, Dictionary<string, Dictionary<string, double>> classInfo, Settings settings)
        {
            var path = $"{_outputPath}{settings.Metric}_{settings.TrainingSet}_Precision.csv";
            if (File.Exists(path))
            {
                string line = "";
                string newText = "";
                using (var readFile = new System.IO.StreamReader(path))
                {
                    line = readFile.ReadLine() + "," + settings.Neighbours;
                    newText += line + "\n";
                    foreach (var item in values)
                    {
                        line = readFile.ReadLine() + "," + item.precision;
                        newText += line + "\n";

                    }
                }
                var file = new System.IO.StreamWriter(path);
                file.Write(newText);
                return file;

            }
            else
            {
                var file = new System.IO.StreamWriter(path);

                file.WriteLine($"country,{settings.Neighbours}");
                foreach (var item in values)
                {
                    file.WriteLine($"{item.country},{item.precision}");
                }

                return file;
            }
        }

        private System.IO.StreamWriter GenerateRecallData(List<(string country, int trues, int falses, double precision)> values, Dictionary<string, Dictionary<string, double>> classInfo, Settings settings)
        {
            var path = $"{_outputPath}{settings.Metric}_{settings.TrainingSet}_Recall.csv";

            if (File.Exists(path))
            {
                string line = "";
                string newText = "";
                using (var readFile = new System.IO.StreamReader(path))
                {
                    line = readFile.ReadLine() + "," + settings.Neighbours;
                    newText += line + "\n";
                    foreach (var item in values)
                    {
                        line = readFile.ReadLine() + "," + classInfo[item.country]["Recall"];
                        newText += line + "\n";

                    }
                }
                var file = new System.IO.StreamWriter(path);
                file.Write(newText);
                return file;

            }
            else
            {
                var file = new System.IO.StreamWriter(path);
                file.WriteLine($"country,{settings.Neighbours}");
                foreach (var item in values)
                {
                    file.WriteLine($"{item.country},{classInfo[item.country]["Recall"]}");
                }

                return file;
            }
        }

        private System.IO.StreamWriter GenerateAccuracyData(List<(string country, int trues, int falses, double precision)> values, Dictionary<string, Dictionary<string, double>> classInfo, Settings settings)
        {
            var path = $"{_outputPath}{settings.Metric}_{settings.TrainingSet}_Accuracy.csv";
            var allArticles = 0.0;
            foreach (var item in classInfo)
            {
                foreach (var val in item.Value)
                {
                    if (val.Key != "FN" && val.Key != "Recall")
                        allArticles += val.Value;
                }
            }
            if (File.Exists(path))
            {
                string line = "";
                string newText = "";
                using (var readFile = new System.IO.StreamReader(path))
                {
                    line = readFile.ReadLine() + "," + settings.Neighbours;
                    newText += line + "\n";
                    foreach (var item in values)
                    {
                        line = readFile.ReadLine() + "," + (allArticles - classInfo[item.country]["FN"] - item.falses) / (allArticles);
                        newText += line + "\n";

                    }
                }
                var file = new System.IO.StreamWriter(path);
                file.Write(newText);
                return file;

            }
            else
            {

                var file = new System.IO.StreamWriter(path);
                file.WriteLine($"country,{settings.Neighbours}");



                foreach (var item in values)
                {
                    file.WriteLine($"{item.country},{(allArticles - classInfo[item.country]["FN"] - item.falses) / (allArticles)}");
                }

                return file;
            }
        }

        private System.IO.StreamWriter GenerateLatexFormatTable(List<(string country, int trues, int falses, double prescision)> values, Dictionary<string, Dictionary<string, double>> clasinfo, Settings settings)
        {
            var path = $"{_latexTablePath}{settings.Metric}_{settings.TrainingSet}_{settings.Neighbours}.txt";
            var file = new System.IO.StreamWriter(path);
            var tableHeader = $"\\subsubsection{{Metryka {settings.Metric} dla {settings.Neighbours} sąsiadów, zbiór treningowy {settings.TrainingSet * 100}\\%}} \n\n " +
                $"Zbiór treningowy - {settings.TrainingSet * 100} [\\%] \n Zbiór testowy - { 100 - settings.TrainingSet * 100} [\\%]\n \\begin{{table}}[H] \n " +
                $"\\centering \n\\begin{{tabular}}{{|| c c c c c c c c c||}} \n \\hline \n-";
            var resultString = tableHeader;
            //var resultString = "\\begin{tabular}{||c c c c c c c c c||} \n \\hline \n-";
            foreach (var item in clasinfo)
            {
                resultString += $" & {item.Key}";
            }
            resultString += " & FN & Recall\\\\ [0.5ex] \n \\hline\\hline";
            foreach (var item in clasinfo)
            {
                resultString += "\n";
                resultString += $"{item.Key}";
                foreach (var elem in item.Value)
                {
                    resultString += $" &{Math.Round(elem.Value, 2)}";
                }
                resultString += "\\\\";
            }
            resultString += "\n";

            resultString += "False Positive";

            foreach (var elem in values.OrderBy(x => x.country))
            {
                resultString += $" & {elem.falses}";
            }
            resultString += " & - & -\\\\";
            resultString += "\n";

            resultString += "Precision";
            foreach (var elem in values.OrderBy(x => x.country))
            {
                resultString += $" & {Math.Round(elem.prescision, 2)}";
            }
            resultString += " & - & -\\\\[1ex]";
            resultString += $"\n \\hline\n \\end{{tabular}} \n \\caption{{Trafność klasyfikacji dla metryki {settings.Metric}, {settings.Neighbours} sąsiadów oraz kategorii places, zbiór treningowy {settings.TrainingSet * 100}\\%}}";


            double properlyClassified = 0;
            foreach (var item in clasinfo)
            {
                properlyClassified += item.Value.Where(x => x.Key == item.Key).FirstOrDefault().Value;
            }


            double allArticles = 0;
            foreach (var item in clasinfo)
            {
                foreach (var elem in item.Value)
                {
                    if (elem.Key == "FN" || elem.Key == "Recall")
                        continue;
                    allArticles += elem.Value;
                }

            }

            var accuracyString = $"$$\nAccuracy = \\frac{{Q}}{{N}} = \\frac{{{properlyClassified}}}{{{allArticles}}} = {Math.Round(properlyClassified / allArticles, 2)}" +
                $"\n $$ \n \\newline\n Q - ilość poprawnianie zaklasyfikowanych artykułów.\n \\newline\n N - ilość wszystkich artykułów w zbiorze testowym. \n \\newline \n\n \\end{{table}}";

            resultString += $"\n {accuracyString}";

            file.WriteLine(resultString);

            return file;
        }

        private System.IO.StreamWriter SaveFile(List<ClassificationInfo> classificationInfos, List<(string country, int trues, int falses, double prescision)> values, Dictionary<string, Dictionary<string, double>> clasinfo, Settings settings)
        {

            var path = $"{_outputPath}{settings.Metric}_{settings.TrainingSet}_{settings.Neighbours}.txt";
            var file = new System.IO.StreamWriter(path);
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

            return file;
        }
    }
}
