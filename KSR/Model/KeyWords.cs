﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR
{
    public class KeyWords : IKeywords
    {
        public IList<string> Keywords
        {
            get;
            private set;
        } = new List<string>();

        public KeyWords(List<Article> testArticles)
        {
            Dictionary<string, Dictionary<string, int>> allWords = GetAllWordsPerCountry(testArticles);
            Dictionary<string, Dictionary<string, int>> orderedFilteredKeyWords = GetFilteredKeyWords(testArticles, allWords);
            Dictionary<string, int> keyWordsAmount = GetKeyWordsByAmount(orderedFilteredKeyWords);

            Dictionary<string, int> wordInDifferentCountries = new Dictionary<string, int>();

            foreach (var country in orderedFilteredKeyWords)
            {
                foreach (var word in country.Value)
                {
                    if (wordInDifferentCountries.ContainsKey(word.Key))
                        wordInDifferentCountries[word.Key] += 1;
                    else wordInDifferentCountries.Add(word.Key, 1);

                }
            }


            var keyWordsThatExistInLessThen4Countries = wordInDifferentCountries.Where(p => p.Value < 2 ).Select(p => p.Key).ToList();
            var filtered = orderedFilteredKeyWords.ToDictionary(p => p.Key, q => q.Value.Select(p => p.Key).Where(p => keyWordsThatExistInLessThen4Countries.Contains(p)));
            foreach (var country in filtered) 
            {
                Keywords = Keywords.Concat(country.Value.Take(20)).ToList();
            }
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
                                                         .Where(p => p.Value > item.Value.Count() * 0.02 && p.Value < item.Value.Count * 0.25)
                                                         .OrderByDescending(p => p.Value)
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
