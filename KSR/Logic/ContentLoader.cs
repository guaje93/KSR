using StopWord;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace KSR
{
    class ContentLoader
    {
        private List<Article> _articles = new List<Article>();
        private readonly string[] places = new string[] { "west-germany", "usa", "france", "uk", "canada", "japan" };

        internal List<Article> Articles { get => _articles; private set => _articles = value; }

        public bool ReadFile(string directoryPath)
        {

            var files = Directory.GetFiles(directoryPath, "*.sgm", SearchOption.TopDirectoryOnly);
            if (files.Length == 0)
                return false;

            foreach (var file in files)
            {
                var fileContent = File.ReadAllText(file);
                var matches = Regex.Matches(fileContent, @"<REUTERS([\s\S]*?)</REUTERS>").Select(p => p.Groups[0].Value);
                Articles = Articles.Concat(matches.AsParallel()
                                                    .Select(p => GetArticleFromXml(p))
                                                    .Where(p => IsArticleValid(p)))
                                                    .ToList();
            }

            return true;
        }

        private bool IsArticleValid(Article p) => p != null && p.FilteredWords.Count !=0 && places.Contains(p.Place);

        private Article GetArticleFromXml(string p)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(p);
            var content = xmlDocument.GetElementsByTagName("BODY")?.Item(0)?.InnerText;
            if (content is null)
                return null;
            
            return new Article()
            {
                Title = xmlDocument.GetElementsByTagName("TITLE")?.Item(0)?.InnerText,
                AllWords = content.Split('\n','\t',' ').Where(p => !string.IsNullOrEmpty(p)).ToList(),
                FilteredWords = FilterWordsFromText(content),
                Place = xmlDocument.GetElementsByTagName("PLACES")?.Item(0)?.InnerText
            };
        }

        private List<string> FilterWordsFromText(string text)
        {
            var removedPuntuationMarks = Regex.Replace(text, @"[^a-zA-Z-\s]+", "");
            var removeWhiteSpaces = Regex.Replace(removedPuntuationMarks, @"[\n\t]+", " ");
            var removedStopWordsText = removeWhiteSpaces.ToLower().RemoveStopWords("en");
            var removeReuterFromTextEnd = removedStopWordsText.Replace("Reuter", "", StringComparison.InvariantCultureIgnoreCase);
            var words = removeReuterFromTextEnd.Split(' ','\n','\t').Where(p=> !string.IsNullOrEmpty(p)).Distinct();
            return words.ToList();
        }
    }
}
