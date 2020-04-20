﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class KeyWordsStartedWithFirstLowerExtractor : IExtractor
    {
        private readonly IKeywords _keywords;
        private readonly IList<string> _allText;
        private readonly string _country;

        public KeyWordsStartedWithFirstLowerExtractor(IKeywords keywords, IList<string> allText, string country)
        {
            _keywords = keywords;
            _allText = allText;
            _country = country;
        }

        public double Result = 0;

        public bool Extract()
        {
            Result = _allText.Where(p => !string.IsNullOrWhiteSpace(p))
                                      .Where(p => _keywords[_country].Where(kw => Char.IsLower(kw.First())).Contains(p))
                                      .Count() * 1.0 / _allText.Count();
            if (Result >= 0)
                return true;
            return false;
        }
    }
}

