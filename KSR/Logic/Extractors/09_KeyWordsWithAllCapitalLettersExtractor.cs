using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class KeyWordsWithAllCapitalLettersExtractor : IExtractor
    {
        public double Result = 0;

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            Result = textWords.Where(p => !string.IsNullOrWhiteSpace(p))
                                             .Where(p => keywords.Where(kw => kw.All(c => Char.IsUpper(c))).Contains(p))
                                             .Count() * 1.0 / textWords.Count();
        }

    }
}
