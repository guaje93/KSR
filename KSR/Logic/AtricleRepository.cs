using StopWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KSR
{
    public class AtricleRepository
    {
        private List<Article> _articles;
        public List<Article> Articles
        {
            get => _articles;
            set => _articles = value;
        }

        public bool CompleteRepository(string directoryPath)
        {
            var fileLoader = new FileLoader();
            var fileParser = new FilesContentParser();
            
            
            var readSuccess = fileLoader.ReadFiles(directoryPath);
            if (!readSuccess)
                return false;

            var parseSuccess = fileParser.ReadArticlesFromFiles(fileLoader.Files);
            if (!parseSuccess)
                return false;

            Articles = fileParser.Articles.ToList();

            var filterSuccess = ExtractAndFilterWords();
            if (!filterSuccess)
                return false;

            return true;
        }

        private bool ExtractAndFilterWords()
        {
            foreach (var article in Articles)
            {
                article.AllWords = article.Text.Split('\n', '\t', ' ').Where(p => !string.IsNullOrEmpty(p)).ToList();
                article.FilteredWords = FilterWordsFromText(article.Text);
            }

            if (Articles.Any(p => p.AllWords == null || p.AllWords.Count == 0 || p.FilteredWords == null || p.FilteredWords.Count == 0))
                return false;
            return true;
        }
        

        private List<string> FilterWordsFromText(string text)
        {
            var removedPuntuationMarks = Regex.Replace(text, @"[^a-zA-Z-\s]+", "");
            var removeWhiteSpaces = Regex.Replace(removedPuntuationMarks, @"[\n\t]+", " ");
            //stopWords
            var removedStopWordsText = removeWhiteSpaces.ToLower().RemoveStopWords("en");
            var removeReuterFromTextEnd = removedStopWordsText.Replace("Reuter", "", StringComparison.InvariantCultureIgnoreCase);
            var words = removeReuterFromTextEnd.Split(' ', '\n', '\t').Where(p => !string.IsNullOrEmpty(p)).Distinct();
            return words.ToList();
        }
    }
}
