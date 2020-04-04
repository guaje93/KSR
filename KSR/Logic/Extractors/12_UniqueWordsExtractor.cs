using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class UniqueWordsExtractor : IExtractor
    {
        private readonly IKeywords _keywords;
        private readonly IList<string> _allText;
        private readonly string _country;

        public UniqueWordsExtractor(IKeywords keywords, IList<string> allText, string country)
        {
            _keywords = keywords;
            _allText = allText;
            _country = country;
        }

        public double Result = -1;
        public bool Extract()
        {
            Result = _allText.Distinct().Count() / _allText.Count();
            if (Result >= 0)
                return true;
            return false;
        }
    }
}
