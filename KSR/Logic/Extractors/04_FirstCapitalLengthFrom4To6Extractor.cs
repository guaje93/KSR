﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class LengthFrom4To6Extractor : IExtractor
    {
        public double Result = 0;

        public void Extract(IList<string> keywords, IList<string> textWords)  {
                            Result = textWords.Where(p => !string.IsNullOrWhiteSpace(p))
                                             .Where(p => p.Length >= 4 && p.Length < 6)
                                             .Count() * 1.0 / textWords.Count();
        } 
    
    }
}
