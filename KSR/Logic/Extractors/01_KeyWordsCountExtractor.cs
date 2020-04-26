using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR
{
    class KeyWordsCountExtractor : IExtractor
    {
        public double Result = 0;

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            var foundKeyWords = (textWords.Where(p => !string.IsNullOrWhiteSpace(p))
                                      .Where(p => keywords.Contains(p))
                                      .Distinct()
                                      .Count());

            if (foundKeyWords == 0)
                return;

            Result = foundKeyWords * 1.0 / keywords.Distinct().Count();
        }
    }
}
