﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class FirstCapitalShorterThan4Extractor<T> : IMeanExtractor<T>
    {
        public T Extract(IList<string> text, string country) => (T)Convert.ChangeType(text.Select(p => p.Substring(0, 1))
                                                                                          .Where(p => p.Length < 4 && char.IsUpper(p.First()))
                                                                                          .Count(), typeof(T));

    }
}
