using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class MeanKeyWordLengthExtractor<T> : IMeanExtractor<T>
    {
        public IKeywords Keywords { get; }
        public MeanKeyWordLengthExtractor(IKeywords keywords)
        {
            Keywords = keywords;
        }

        public T Extract(IList<string> text, string country) => (T)Convert.ChangeType(
                                    Math.Round(text.Where(p => Keywords[country].Contains(p))
                                                .Select(p => p.Length)
                                                .Average(),1),typeof(T));

    }
}
