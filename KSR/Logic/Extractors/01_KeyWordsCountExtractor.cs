using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR
{
    class KeyWordsCountExtractor : IExtractor
    {
        private readonly IKeywords _keywords;
        private readonly IList<string> _allText;
        private readonly string _country;

        public KeyWordsCountExtractor(IKeywords keywords, IList<string> allText, string country)
        {
            _keywords = keywords;
            _allText = allText;
            _country = country;
        }

        public double Result = -1;

        public bool Extract()
        {
            Result = (_allText.Where(p => !string.IsNullOrWhiteSpace(p))
                                      .Where(p => _keywords[_country].Contains(p))
                                      .Distinct()
                                      .Count()) / _keywords[_country].Distinct().Count();
            if (Result >= 0)
                return true;
            return false;
        }
    }
}
