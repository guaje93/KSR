﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class LongerThan8Extractor : IExtractor
    {
        private readonly IKeywords _keywords;
        private readonly IList<string> _allText;
        private readonly string _country;

        public LongerThan8Extractor(IKeywords keywords, IList<string> allText, string country)
        {
            _keywords = keywords;
            _allText = allText;
            _country = country;
        }

        public double Result = -1;


        public bool Extract()
        {

           Result = _allText.Where(p => !string.IsNullOrWhiteSpace(p))
                            .Where(p => _keywords[_country].Contains(p))
                            .Where(p => p.Length > 8)
                            .Select(p => p.Substring(0, 1))
                            .Where(p => char.IsUpper(p.First()))
                            .Count() * 1.0 / _allText.Count();

            if (Result >= 0)
                return true;
            return false;
        }
    }
}
