using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KSR.Extractors
{
    class DashSeparatedKeyWordsExtractor : IExtractor
    {
        private const string dashSeparatedWordPattern = @"\w*[-]\w*";
        public double Result = 0;

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            Result = textWords.Where(p => !string.IsNullOrWhiteSpace(p))
                                             .Where(p => keywords.Where(kw => Regex.IsMatch(p, dashSeparatedWordPattern)).Contains(p))
                                             .Count() * 1.0 / textWords.Count();
        }

    }
}