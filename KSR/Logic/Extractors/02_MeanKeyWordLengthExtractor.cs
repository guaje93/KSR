using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class MeanKeyWordLengthExtractor : IExtractor
    {
        private readonly IKeywords _keywords;
        private readonly IList<string> _allText;
        private readonly string _country;

        public MeanKeyWordLengthExtractor(IKeywords keywords, IList<string> allText, string country)
        {
            _keywords = keywords;
            _allText = allText;
            _country = country;
        }

        public double Result = -1;

        public bool Extract()
        {
            Result = Math.Round(_allText.Where(p => _keywords[_country].Contains(p))
                                                  .Select(p => p.Length)
                                                  .Average(), 1) / _allText.Count();

            if (Result >= 0)
                return true;
            return false;

        }
                                   

    }
}
