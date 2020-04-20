using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KSR.Extractors
{
    class DashSeparatedKeyWordsExtractor : IExtractor
    {
        private readonly IKeywords _keywords;
        private readonly IList<string> _allText;
        private readonly string _country;

        public DashSeparatedKeyWordsExtractor(IKeywords keywords, IList<string> allText, string country)
        {
            _keywords = keywords;
            _allText = allText;
            _country = country;
        }

        public double Result = 0;

        public bool Extract()
        {
            Result = _allText.Where(p => !string.IsNullOrWhiteSpace(p))
                                             .Where(p => _keywords[_country].Where(kw => Regex.IsMatch(p, @"\w*[-]\w*")).Contains(p))
                                             .Count() * 1.0 / _allText.Count();
            if (Result >= 0)
                return true;
            return false;
        }

    }
}