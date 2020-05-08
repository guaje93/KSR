using System;
using System.Collections.Generic;
using System.Linq;

namespace KSR.Extractors
{
    class MeanKeyWordLengthExtractor : IExtractor
    {
        public double Result = 0;

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            var filteredWords = textWords.Where(p => keywords.Contains(p)).ToList();
            if (filteredWords.Count == 0)
                return;
            Result = Math.Round(filteredWords.Select(p => p.Length).Average(), 1) * 1.0 / textWords.Count();
        }
    }
}
