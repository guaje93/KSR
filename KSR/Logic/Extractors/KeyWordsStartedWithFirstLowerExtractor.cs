using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class KeyWordsStartedWithFirstLowerExtractor<T> : IMeanExtractor<T>
    {
        public KeyWordsStartedWithFirstLowerExtractor(IKeywords keywords)
        {
            Keywords = keywords;
        }

        public IKeywords Keywords { get; }

        public T Extract(IList<string> text, string country) =>
                   (T)Convert.ChangeType(text.Where(p => !string.IsNullOrWhiteSpace(p))
                                             .Where(p => Keywords[country].Where(kw => Char.IsLower(kw.First())).Contains(p))
                                             .Count(), typeof(T));
    }
}

