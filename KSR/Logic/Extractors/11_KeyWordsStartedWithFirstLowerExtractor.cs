﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace KSR.Extractors
{
    class KeyWordsStartedWithFirstLowerExtractor : IExtractor
    {
        public double Result = 0;

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            Result = textWords.Where(p => !string.IsNullOrWhiteSpace(p))
                                      .Where(p => keywords.Where(kw => Char.IsLower(kw.First())).Contains(p))
                                      .Count() * 1.0 / textWords.Count();
        }
    }
}

