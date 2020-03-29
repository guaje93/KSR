using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class MostPopularKeyWordExtractor<T> : IMeanExtractor<T>
    {
        public MostPopularKeyWordExtractor(IKeywords keywords)
        {
            Keywords = keywords;
        }

        public IKeywords Keywords { get; }

        public T Extract(IList<string> text, string country) =>
             (T)Convert.ChangeType(text.Where(p => !string.IsNullOrWhiteSpace(p))
                                            .Where(p => Keywords[country].Contains(p))
                                            .GroupBy(p => p)
                                            .Select(group => new {
                                                 Key = group.Key,
                                                 Count = group.Count()})
                                            .Max(p => p.Count), typeof(T));
    }
}
