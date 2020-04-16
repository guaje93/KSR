using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class FirstKeywordPositionExtractor : IExtractor
    {

        private readonly IKeywords _keywords;
        private readonly IList<string> _allText;
        private readonly string _country;

        public FirstKeywordPositionExtractor(IKeywords keywords, IList<string> allText, string country)
        {
            _keywords = keywords;
            _allText = allText;
            _country = country;
        }

        public double Result = -1;

        public bool Extract()
        {
            var firstKeyWord = _allText.FirstOrDefault(p => _keywords[_country].Contains(p));
            var firstKeywordPosition = _allText.IndexOf(firstKeyWord) + 1;
            Result = firstKeywordPosition * 1.0 / _allText.Count();

            if (Result >= 0)
                return true;
            return false;


        }
    }
}
