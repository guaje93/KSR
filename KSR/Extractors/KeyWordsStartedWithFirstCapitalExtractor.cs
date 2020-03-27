using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class KeyWordsStartedWithFirstCapitalExtractor<T> : IMeanExtractor<T>
    {
        public KeyWordsStartedWithFirstCapitalExtractor(IKeywords keywords)
        {
            Keywords = keywords;
        }

        public IKeywords Keywords { get; }

        public T Extract(IList<string> text, string country) =>
                   (T)Convert.ChangeType(text.Where(p => !string.IsNullOrWhiteSpace(p))
                                             .Where(p => Keywords[country].Where(kw=>Char.IsUpper(kw.First())).Contains(p))
                                             .Count(), typeof(T));
    }
}
