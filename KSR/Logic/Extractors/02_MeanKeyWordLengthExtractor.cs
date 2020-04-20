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

        public double Result = 0;

        public bool Extract()
        {
            var temp = _allText.Where(p => _keywords[_country].Contains(p)).ToList();
            if (temp.Count == 0)
            {
                Result = 0;
                return true;
            }
            Result = Math.Round(temp.Select(p => p.Length).Average(), 1) *1.0 / _allText.Count();

            if (Result >= 0)
                return true;
            return false;

        }


    }
}
