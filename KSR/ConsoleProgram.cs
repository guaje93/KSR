using KSR.Logic;
using KSR.Logic.Helpers;
using KSR.Logic.Knn;
using KSR.Logic.Articles;
using KSR.Model;
using Newtonsoft.Json;
using System;
using System.IO;

namespace KSR
{
    public class ConsoleProgram
    {
        #region Public Methods

        static void Main(string[] args)
        {
            var appSettingsGenerator = new AppSettingsGenerator();
            //appSettingsGenerator.Generate();
            
            for (int i = 0; i < appSettingsGenerator.GetAppSettingsFilesAmount(); i++)
            {

                var settings = ReadInitialValues(i);
                var articlesRepository = ReadFile();
                articlesRepository.SetAmountOfArticlesInSets(settings);
                var keyWords = new KeyWordsHandler(articlesRepository.ArticlesForLearning);
                var vectorFeatureCreator = new VectorFeatureCreator(keyWords, settings);
                vectorFeatureCreator.CreateVectorFeature(articlesRepository.ArticlesForLearning);
                vectorFeatureCreator.CreateVectorFeature(articlesRepository.ArticlesForValidation);

                var knnProcesor = new KnnProcessor();
                knnProcesor.Calculate(settings.Metric, articlesRepository.ArticlesForLearning, articlesRepository.ArticlesForValidation, settings);
            }
            Console.Beep(800, 200);
        }

        #endregion

        #region Private Methods

        private static AtricleRepository ReadFile()
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
            var articlesRepo = new AtricleRepository();
            articlesRepo.CompleteRepository(path + @"/Data");
            return articlesRepo;
        }

        private static Settings ReadInitialValues(int interator)
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $@"..\..\..\AppSettings\Settings{interator}.json"));
            using StreamReader file = File.OpenText(path);
            JsonSerializer serializer = new JsonSerializer();
            var settings = (Settings)serializer.Deserialize(file, typeof(Settings));

            return settings;
        }

        #endregion
    }
}
