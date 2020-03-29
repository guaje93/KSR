using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR
{
    class KeyWordsCountExtractor<T> : IMeanExtractor<T>
    {
        public KeyWordsCountExtractor(IKeywords keywords)
        {
            Keywords = keywords;
        }

        public IKeywords Keywords { get; }

        public T Extract(IList<string> text, string country) => 
                   (T)Convert.ChangeType(text.Where(p => !string.IsNullOrWhiteSpace(p))
                                             .Where(p => Keywords[country].Contains(p))
                                             .Count(), typeof(T));
    }
}
