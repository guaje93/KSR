using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class UniqueWordsExtractor : IExtractor
    {

        public double Result = 0;
        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            Result = textWords.Distinct().Count() * 1.0 / textWords.Count();
        }
    }
}
