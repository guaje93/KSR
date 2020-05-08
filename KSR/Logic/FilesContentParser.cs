using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace KSR
{
    public class FilesContentParser
    {
        #region Fields

        private readonly string[] places = new string[] { "west-germany", "usa", "france", "uk", "canada", "japan" };

        #endregion

        #region Properties

        public IEnumerable<Article> Articles { get; private set; } = new List<Article>();

        #endregion

        public bool ReadArticlesFromFiles(string[] files)
        {

            foreach (var file in files)
            {
                var fileContent = File.ReadAllText(file);
                var matches = Regex.Matches(fileContent, @"<REUTERS([\s\S]*?)</REUTERS>").Select(p => p.Groups[0].Value);
                Articles = Articles.Concat(matches.Select(p => GetArticleFromXml(p))
                                                   .Where(p => IsArticleValid(p)))
                                                   .ToList();
            }

            if (Articles == null || Articles.Count() == 0)
                return false;
            return true;
        }

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
                Place = xmlDocument.GetElementsByTagName("PLACES")?.Item(0)?.InnerText,
                Text = content
            };
        }

        private bool IsArticleValid(Article p) => p != null && places.Contains(p.Place);
    }
}
