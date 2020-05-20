using KSR.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KSR.Logic.Helpers
{
    public class ChartDataGenerator
    {
        #region Fields

        private string _latexTablePath = $@"C:\Users\{Environment.UserName}\Desktop\latex\";
        private string _outputPath = $@"C:\Users\{Environment.UserName}\Desktop\raw\";

        #endregion

        #region Public Methods

        public void GenerateChartDataFromBatch(List<ClassificationInfo> classificationInfos, Dictionary<string, Dictionary<string, double>> classInfo, List<(string, int, int, double)> values, Settings settings)
        {
            using var file = SaveFile(classificationInfos, values, classInfo, settings);

            string path1 = $"{_outputPath}{settings.Metric}_{settings.TrainingSet}";
            string path2 = $"{_outputPath}M_{settings.Neighbours}_{settings.TrainingSet}";
            string path3 = $"{_outputPath}S_{settings.Metric}_{settings.Neighbours}";
            using var latexTable = GenerateLatexFormatTable(values, classInfo, settings);
            using var pData1 = GeneratePrecisionData(path1, values, classInfo, settings);
            using var rData1 = GenerateRecallData(path1, values, classInfo, settings);
            using var aData1 = GenerateAccuracyData(path1, values, classInfo, settings);
            using var pData2 = GeneratePrecisionData(path2, values, classInfo, settings);
            using var rData2 = GenerateRecallData(path2, values, classInfo, settings);
            using var aData2 = GenerateAccuracyData(path2, values, classInfo, settings);
            using var pData3 = GeneratePrecisionData(path3, values, classInfo, settings);
            using var rData3 = GenerateRecallData(path3, values, classInfo, settings);
            using var aData3 = GenerateAccuracyData(path3, values, classInfo, settings);
        }

        #endregion

        #region Private Methods

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

        private System.IO.StreamWriter GeneratePrecisionData(string path, List<(string country, int trues, int falses, double precision)> values, Dictionary<string, Dictionary<string, double>> classInfo, Settings settings)
        {
            path += "precision.csv";
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

        private System.IO.StreamWriter GenerateRecallData(string path, List<(string country, int trues, int falses, double precision)> values, Dictionary<string, Dictionary<string, double>> classInfo, Settings settings)
        {
            path += "recall.csv";
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

        private System.IO.StreamWriter GenerateAccuracyData(string path, List<(string country, int trues, int falses, double precision)> values, Dictionary<string, Dictionary<string, double>> classInfo, Settings settings)
        {
            path += "accuracy.csv";
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

        #endregion
    }
}
