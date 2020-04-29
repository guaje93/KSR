using KSR.Extractors;
using System;
using System.Collections.Generic;
using System.Text;

namespace KSR.Model
{
    public class VectorFeatureCreator
    {
        private IKeywords _keyWords;
        private Settings _settings;
        public VectorFeatureCreator(KeyWords keyWords, Settings settings)
        {
            this._keyWords = keyWords;
            this._settings = settings;
        }

        public void CreateVectorFeature(List<Article> articles)
        {


            foreach (var article in articles)
            {
                var keyWordsCountExtractor = new KeyWordsCountExtractor();
                if (_settings.Measures.KeyWordsCount)
                    keyWordsCountExtractor.Extract(_keyWords.Keywords, article.FilteredWords);

                var meanKeyWordLengthExtractor = new MeanKeyWordLengthExtractor();
                if (_settings.Measures.MeanKeyWordLength)
                    meanKeyWordLengthExtractor.Extract(_keyWords.Keywords, article.FilteredWords);

                var shorterThan4Extractor = new ShorterThan4Extractor();
                if (_settings.Measures.FirstCapitalShorterThan4)
                    shorterThan4Extractor.Extract(_keyWords.Keywords, article.FilteredWords);

                var lengthFrom4To6Extractor = new LengthFrom4To6Extractor();
                if (_settings.Measures.FirstCapitalLengthFrom4To6)
                    lengthFrom4To6Extractor.Extract(_keyWords.Keywords, article.FilteredWords);

                var longerThan8Extractor = new LongerThan8Extractor();
                if (_settings.Measures.KeyWordsLongerThan8)
                    longerThan8Extractor.Extract(_keyWords.Keywords, article.FilteredWords);

                var dashSeparatedKeyWordsExtractor = new DashSeparatedKeyWordsExtractor();
                if (_settings.Measures.DashSeparatedKeyWords)
                    dashSeparatedKeyWordsExtractor.Extract(_keyWords.Keywords, article.FilteredWords);

                var firstKeywordPositionExtractor = new FirstKeywordPositionExtractor();
                if (_settings.Measures.FirstKeywordPosition)
                    firstKeywordPositionExtractor.Extract(_keyWords.Keywords, article.FilteredWords);

                var containsKeywordExtractor = new ContainsKeywordExtractor();
                if (_settings.Measures.ContainsKeyWord)
                    containsKeywordExtractor.Extract(_keyWords.Keywords, article.FilteredWords);

                var keyWordsWithAllCapitalLettersExtractor = new KeyWordsWithAllCapitalLettersExtractor();
                if (_settings.Measures.KeyWordsWithAllCapitalLetters)
                    keyWordsWithAllCapitalLettersExtractor.Extract(_keyWords.Keywords, article.FilteredWords);

                var keyWordsStartedWithFirstCapitalExtractor = new KeyWordsStartedWithFirstCapitalExtractor();
                if (_settings.Measures.KeyWordsStartedWithFirstCapital)
                    keyWordsStartedWithFirstCapitalExtractor.Extract(_keyWords.Keywords, article.FilteredWords);

                var keyWordsStartedWithFirstLowerExtractor = new KeyWordsStartedWithFirstLowerExtractor();
                if (_settings.Measures.KeyWordsStartedWithFirstLower)
                    keyWordsStartedWithFirstLowerExtractor.Extract(_keyWords.Keywords, article.FilteredWords);

                var uniqueWordsExtractor = new UniqueWordsExtractor();
                if (_settings.Measures.UniqueWords)
                    uniqueWordsExtractor.Extract(_keyWords.Keywords, article.FilteredWords);

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
