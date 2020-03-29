using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KSR.Extractors
{
    class DashSeparatedKeyWordsExtractor<T> : IMeanExtractor<T>
    {
        public DashSeparatedKeyWordsExtractor(IKeywords keywords)
        {
            Keywords = keywords;
        }

        public IKeywords Keywords { get; }

        public T Extract(IList<string> text, string country) =>
                   (T)Convert.ChangeType(text.Where(p => !string.IsNullOrWhiteSpace(p))
                                             .Where(p => Keywords[country].Where(kw => Regex.IsMatch(p, @"\w*[-]\w*")).Contains(p))
                                             .Count(), typeof(T));
    }
}