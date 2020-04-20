using KSR.Extractors;
using System;
using System.Collections.Generic;
using System.Text;

namespace KSR.Model
{
   public class VectorFeatureCreator
    {
        public void CreateVectorFeature(List<Article> articles, KeyWords keyWords, Settings settings)
        {
            foreach (var article in articles)
            {
                var keyWordsCountExtractor = new KeyWordsCountExtractor(keyWords, article.FilteredWords, article.Place);
                if(settings.Measures.KeyWordsCount)
                    keyWordsCountExtractor.Extract();

                var meanKeyWordLengthExtractor = new MeanKeyWordLengthExtractor(keyWords, article.FilteredWords, article.Place);
                if(settings.Measures.MeanKeyWordLength)
                    meanKeyWordLengthExtractor.Extract();

                var shorterThan4Extractor = new ShorterThan4Extractor(keyWords, article.FilteredWords, article.Place);
                if(settings.Measures.FirstCapitalShorterThan4)
                shorterThan4Extractor.Extract();

                var lengthFrom4To6Extractor = new LengthFrom4To6Extractor(keyWords, article.FilteredWords, article.Place);
                if (settings.Measures.FirstCapitalLengthFrom4To6)
                    lengthFrom4To6Extractor.Extract();

                var longerThan8Extractor = new LongerThan8Extractor(keyWords, article.FilteredWords, article.Place);
                if (settings.Measures.KeyWordsLongerThan8)
                    longerThan8Extractor.Extract();

                var dashSeparatedKeyWordsExtractor = new DashSeparatedKeyWordsExtractor(keyWords, article.FilteredWords, article.Place);
                if (settings.Measures.DashSeparatedKeyWords)
                    dashSeparatedKeyWordsExtractor.Extract();

                var firstKeywordPositionExtractor = new FirstKeywordPositionExtractor(keyWords, article.FilteredWords, article.Place);
                if (settings.Measures.FirstKeywordPosition)
                    firstKeywordPositionExtractor.Extract();

                var containsKeywordExtractor = new ContainsKeywordExtractor(keyWords, article.FilteredWords, article.Place);
                if (settings.Measures.ContainsKeyWord)
                    containsKeywordExtractor.Extract();

                var keyWordsWithAllCapitalLettersExtractor = new KeyWordsWithAllCapitalLettersExtractor(keyWords, article.FilteredWords, article.Place);
                if (settings.Measures.KeyWordsWithAllCapitalLetters)
                    keyWordsWithAllCapitalLettersExtractor.Extract();

                var keyWordsStartedWithFirstCapitalExtractor = new KeyWordsStartedWithFirstCapitalExtractor(keyWords, article.FilteredWords, article.Place);
                if (settings.Measures.KeyWordsStartedWithFirstCapital)
                    keyWordsStartedWithFirstCapitalExtractor.Extract();

                var keyWordsStartedWithFirstLowerExtractor = new KeyWordsStartedWithFirstLowerExtractor(keyWords, article.FilteredWords, article.Place);
                if (settings.Measures.KeyWordsStartedWithFirstLower)
                    keyWordsStartedWithFirstLowerExtractor.Extract();

                var uniqueWordsExtractor = new UniqueWordsExtractor(keyWords, article.FilteredWords, article.Place);
                if (settings.Measures.UniqueWords)
                    uniqueWordsExtractor.Extract();

                article.VectorFeatures = new ExtractorVector()
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

                };
            }
        }
    }
}
