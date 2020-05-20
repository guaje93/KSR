using KSR.Model;
using System.Collections.Generic;
using System.Linq;

namespace KSR.Logic.Keywords
{
    public class KeyWordsHandler : IKeywords
    {
        #region Properties

        public IList<string> Keywords
        {
            get;
            private set;
        } = new List<string>();

        #endregion

        #region Constructors

        public KeyWordsHandler(List<Article> testArticles)
        {
            Dictionary<string, Dictionary<string, double>> allWords = GetAllWordsPerCountry(testArticles);
            Dictionary<string, Dictionary<string, double>> orderedFilteredKeyWords = GetFilteredKeyWordsPerCountry(testArticles, allWords);
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
            
            var keyWordsThatExistInLessThen6Countries = wordInDifferentCountries.Where(p => p.Value < 6).Select(p => p.Key).ToList();
            var filtered = orderedFilteredKeyWords.ToDictionary(p => p.Key, q => q.Value.Select(p => p.Key).Where(p => keyWordsThatExistInLessThen6Countries.Contains(p)));
            foreach (var country in filtered)
            {
                Keywords = Keywords.Concat(country.Value.Take(5)).ToList();
            }
        }

        #endregion

        #region Private Methods

        private Dictionary<string, int> GetKeyWordsByAmount(Dictionary<string, Dictionary<string, double>> orderedFilteredKeyWords)
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

        private Dictionary<string, Dictionary<string, double>> GetFilteredKeyWordsPerCountry(List<Article> testArticles, Dictionary<string, Dictionary<string, double>> allWords)
        {
            var orderedFilteredKeyWords = new Dictionary<string, Dictionary<string, double>>();
            foreach (var item in allWords)
            {
                orderedFilteredKeyWords.Add(item.Key, item.Value
                                                         .Where(p => p.Key.Length > 5)
                                                         .OrderByDescending(p => p.Value)
                                                         .ToDictionary(p => p.Key, g => g.Value));
            }

            return orderedFilteredKeyWords;
        }

        private Dictionary<string, Dictionary<string, double>> GetAllWordsPerCountry(List<Article> testArticles)
        {
            var allWords = new Dictionary<string, Dictionary<string, double>>();

            Dictionary<string, double> keyWordsDict = null;

            foreach (var article in testArticles)
            {
                keyWordsDict = article.FilteredWords.GroupBy(p => p).ToDictionary(p => p.Key, g => 1.0 * g.Count() / article.AllWords.Count());

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

        #endregion
    }
}
