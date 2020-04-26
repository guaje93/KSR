using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR
{
    public class KeyWords : IKeywords
    {
        public Dictionary<string, Dictionary<string, int>> _keywordsList
        {
            get;
            private set;
        } = new Dictionary<string, Dictionary<string, int>>();

        public IList<string> this[string country] => _keywordsList[country].Select(p => p.Key).ToList();

        public KeyWords(List<Article> testArticles)
        {
            Dictionary<string, Dictionary<string, int>> allWords = GetAllWordsPerCountry(testArticles);
            Dictionary<string, Dictionary<string, int>> orderedFilteredKeyWords = GetFilteredKeyWords(testArticles, allWords);
            Dictionary<string, int> keyWordsAmount = GetKeyWordsByAmount(orderedFilteredKeyWords);

            var keyWordsThatExistInLessThen4CountriesDictionary = new Dictionary<string, Dictionary<string, int>>();
            var keyWordsThatExistInLessThen4Countries = keyWordsAmount.Where(p => p.Value < 4).Select(p => p.Key).ToList();

            foreach (var country in orderedFilteredKeyWords)
            {
                keyWordsThatExistInLessThen4CountriesDictionary.Add(country.Key, country.Value.Where(p => keyWordsThatExistInLessThen4Countries.Contains(p.Key)).ToDictionary(p => p.Key, g => g.Value));
            }

            _keywordsList = keyWordsThatExistInLessThen4CountriesDictionary;
        }

        private static Dictionary<string, int> GetKeyWordsByAmount(Dictionary<string, Dictionary<string, int>> orderedFilteredKeyWords)
        {
            var keyWordsAmount = new Dictionary<string, int>();
            foreach (var item in orderedFilteredKeyWords)
            {
                foreach (var value in item.Value)
                {
                    if (keyWordsAmount.ContainsKey(value.Key))
                        keyWordsAmount[value.Key] += 1;
                    else
                        keyWordsAmount.Add(value.Key, 1);

                }
            }
            keyWordsAmount = keyWordsAmount.OrderBy(p => p.Value).ToDictionary(p => p.Key, g => g.Value);
            return keyWordsAmount;
        }

        private static Dictionary<string, Dictionary<string, int>> GetFilteredKeyWords(List<Article> testArticles, Dictionary<string, Dictionary<string, int>> allWords)
        {
            var orderedFilteredKeyWords = new Dictionary<string, Dictionary<string, int>>();
            foreach (var item in allWords)
            {
                var articlesPerCountry = testArticles.Where(p => p.Place == item.Key).Count();
                orderedFilteredKeyWords.Add(item.Key, item.Value
                                                         .Where(p => p.Value > item.Value.Count() * 0.05 && p.Value < item.Value.Count * 0.20)
                                                         .OrderByDescending(p => p.Value).Take(20)
                                                         .ToDictionary(p => p.Key, g => g.Value));
            }

            return orderedFilteredKeyWords;
        }

        private static Dictionary<string, Dictionary<string, int>> GetAllWordsPerCountry(List<Article> testArticles)
        {
            var allWords = new Dictionary<string, Dictionary<string, int>>();

            Dictionary<string, int> keyWordsDict = null;

            foreach (var article in testArticles)
            {
                keyWordsDict = article.FilteredWords.GroupBy(p => p).ToDictionary(p => p.Key, g => g.Count());

                if (allWords.ContainsKey(article.Place))
                {
                    foreach (var value in keyWordsDict)
                    {
                        if (allWords[article.Place].ContainsKey(value.Key))
                            allWords[article.Place][value.Key] += value.Value;
                        else
                            allWords[article.Place].Add(value.Key, value.Value);
                    }

                }
                else
                    allWords.Add(article.Place, keyWordsDict);

            }

            return allWords;
        }
    }
}
