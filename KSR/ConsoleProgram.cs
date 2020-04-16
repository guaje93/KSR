using KSR.Extractors;
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
            ReadFile();
        }


        public static void ReadFile()
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\")); ;
            var articlesRepo = new AtricleRepository();
            articlesRepo.CompleteRepository(path + @"/Data");
            var keyWords = new KeyWords(articlesRepo.ArticlesForLearning);

            var vectors = new List<AnalysisVector>();




            articlesRepo.ArticlesForLearning.AsParallel().ForAll(article =>
            {
                var keyWordsCountExtractor = new KeyWordsCountExtractor(keyWords, article.FilteredWords, article.Place);
                keyWordsCountExtractor.Extract();
                var meanKeyWordLengthExtractor = new MeanKeyWordLengthExtractor(keyWords, article.FilteredWords, article.Place);
                meanKeyWordLengthExtractor.Extract();
                var shorterThan4Extractor = new ShorterThan4Extractor(keyWords, article.FilteredWords, article.Place);
                shorterThan4Extractor.Extract();
                var lengthFrom4To6Extractor = new LengthFrom4To6Extractor(keyWords, article.FilteredWords, article.Place);
                lengthFrom4To6Extractor.Extract();
                var longerThan8Extractor = new LongerThan8Extractor(keyWords, article.FilteredWords, article.Place);
                longerThan8Extractor.Extract();
                var dashSeparatedKeyWordsExtractor = new DashSeparatedKeyWordsExtractor(keyWords, article.FilteredWords, article.Place);
                dashSeparatedKeyWordsExtractor.Extract();
                var firstKeywordPositionExtractor = new FirstKeywordPositionExtractor(keyWords, article.FilteredWords, article.Place);
                firstKeywordPositionExtractor.Extract();
                var containsKeywordExtractor = new ContainsKeywordExtractor(keyWords, article.FilteredWords, article.Place);
                containsKeywordExtractor.Extract();
                var keyWordsWithAllCapitalLettersExtractor = new KeyWordsWithAllCapitalLettersExtractor(keyWords, article.FilteredWords, article.Place);
                keyWordsWithAllCapitalLettersExtractor.Extract();
                var keyWordsStartedWithFirstCapitalExtractor = new KeyWordsStartedWithFirstCapitalExtractor(keyWords, article.FilteredWords, article.Place);
                keyWordsStartedWithFirstCapitalExtractor.Extract();
                var keyWordsStartedWithFirstLowerExtractor = new KeyWordsStartedWithFirstLowerExtractor(keyWords, article.FilteredWords, article.Place);
                keyWordsStartedWithFirstLowerExtractor.Extract();
                var uniqueWordsExtractor = new UniqueWordsExtractor(keyWords, article.FilteredWords, article.Place);
                uniqueWordsExtractor.Extract();

                vectors.Add(new AnalysisVector()
                {
                    ContainsKeywordExtractor = containsKeywordExtractor.Result,
                    DashSeparatedKeyWordsExtractor = dashSeparatedKeyWordsExtractor.Result,
                    FirstKeywordPositionExtractor = firstKeywordPositionExtractor.Result,
                    ShorterThan4Extractor = shorterThan4Extractor.Result,
                    LengthFrom4To6Extractor = lengthFrom4To6Extractor.Result,
                    KeyWordsCountExtractor = keyWordsCountExtractor.Result,
                    KeyWordsStartedWithFirstCapitalExtractor = keyWordsStartedWithFirstCapitalExtractor.Result,
                    KeyWordsStartedWithFirstLowerExtractor = keyWordsStartedWithFirstLowerExtractor.Result,
                    KeyWordsWithAllCapitalLettersExtractor = keyWordsWithAllCapitalLettersExtractor.Result,
                    LongerThan8Extractor = longerThan8Extractor.Result,
                    MeanKeyWordLengthExtractor = meanKeyWordLengthExtractor.Result,
                    UniqueWordsExtractor = uniqueWordsExtractor.Result,

                });


            });

                var dash = vectors.Where(p => p.DashSeparatedKeyWordsExtractor != 0);
                var capital = vectors.Where(p => p.KeyWordsWithAllCapitalLettersExtractor != 0);
                var _4capital = vectors.Where(p => p.ShorterThan4Extractor != 0);
                var _46capital = vectors.Where(p => p.LengthFrom4To6Extractor != 0);
                var _8capital = vectors.Where(p => p.LongerThan8Extractor != 0);


        }

    }
}
