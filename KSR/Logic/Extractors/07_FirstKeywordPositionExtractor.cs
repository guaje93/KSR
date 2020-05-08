using System.Collections.Generic;
using System.Linq;

namespace KSR.Extractors
{
    class FirstKeywordPositionExtractor : IExtractor
    {
        public double Result = 0;

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            var firstKeyWord = textWords.FirstOrDefault(p => keywords.Contains(p));
            var firstKeywordPosition = textWords.IndexOf(firstKeyWord) + 1;
            Result = firstKeywordPosition * 1.0 / textWords.Count();
        }
    }
}
