using KSR.Extractors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace KSR
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadFile();
        }


        public static void ReadFile()
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\")); ;
            var contentLoader = new ContentLoader();
            contentLoader.ReadFile(path+@"/Data");


            var vectors = new Dictionary<Article, AnalysisVector>();

            foreach (var article in contentLoader.Articles)
            {
                vectors.Add(article, new AnalysisVector()
                {
                    DashSeparatedWords = new DashSeparatedWordsExtractor<int>().Extract(article.AllWords, article.Place),
                    FirstCapitalLenthFrom4To6 = new FirstCapitalLengthFrom4To6Extractor<int>().Extract(article.AllWords, article.Place),
                    FirstCapitalShorterThan4 = new FirstCapitalShorterThan4Extractor<int>().Extract(article.AllWords, article.Place),
                    KeyWordsAmount = new KeyWordsCountExtractor<int>(new KeyWords()).Extract(article.FilteredWords, article.Place),
                    MeanWordLength = new MeanWordLengthExtractor<double>().Extract(article.AllWords, article.Place),
                    MostPopularFirstLetter = new MostPopularFirstLetterExtractor<string>().Extract(article.AllWords, article.Place),
                    TextLength = new TextLengthExtractor<int>().Extract(article.AllWords, article.Place),
                    UniqueWords = new UniqueWordsExtractor<int>().Extract(article.AllWords, article.Place),
                    WordsLongerThan10 = new WordsLongerThan10Extractor<int>().Extract(article.AllWords, article.Place),

                }); ;
            }
        
        }

    }
}
