using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class ContainsKeywordExtractor : IExtractor
    {
        public bool Result = false;

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            Result = textWords.Any(p => keywords.Contains(p));
        }
    }
}
