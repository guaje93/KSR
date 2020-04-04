using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class ContainsKeywordExtractor : IExtractor
    {

        private readonly IKeywords _keywords;
        private readonly IList<string> _allText;
        private readonly string _country;

        public ContainsKeywordExtractor(IKeywords keywords, IList<string> allText, string country)
        {
            _keywords = keywords;
            _allText = allText;
            _country = country;
        }

        public bool Result = false;

        public bool Extract()
        {
            Result = _allText.Any(p => _keywords[_country].Contains(p));
            return true;
        }
    }
}
