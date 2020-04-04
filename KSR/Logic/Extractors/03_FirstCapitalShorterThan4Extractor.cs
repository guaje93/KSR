using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class ShorterThan4Extractor : IExtractor
    {
        private readonly IKeywords _keywords;
        private readonly IList<string> _allText;
        private readonly string _country;

        public ShorterThan4Extractor(IKeywords keywords, IList<string> allText, string country)
        {
            _keywords = keywords;
            _allText = allText;
            _country = country;
        }

        public double Result = -1;

        public bool Extract() {
            Result =  _allText.Select(p => p.Substring(0, 1))
                              .Where(p => p.Length < 4 && char.IsUpper(p.First()))
                              .Count() / _allText.Count();
            
            if (Result >= 0)
                return true;
            return false;
        } 

    }
}
