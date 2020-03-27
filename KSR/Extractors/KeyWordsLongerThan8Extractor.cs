using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class KeyWordsLongerThan8Extractor<T> : IMeanExtractor<T>
    {
        public KeyWordsLongerThan8Extractor(IKeywords keywords)
        {
            Keywords = keywords;
        }

        public IKeywords Keywords { get; }


        public T Extract(IList<string> text, string country) =>
            (T)Convert.ChangeType(text.Where(p => Keywords[country].Where(kw => kw.Length >8).Contains(p))
                                       .Count(), typeof(T));
    }
}
