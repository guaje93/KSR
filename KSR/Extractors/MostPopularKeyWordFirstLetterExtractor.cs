using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class MostPopularKeyWordFirstLetterExtractor<T> : IMeanExtractor<T>
    {
        public IKeywords Keywords { get; }
        public MostPopularKeyWordFirstLetterExtractor(IKeywords keywords)
        {
            Keywords = keywords;
        }

        public T Extract(IList<string> text, string country) =>(T)Convert.ChangeType(text.Where(p => !string.IsNullOrWhiteSpace(p))
               .Where(p => Keywords[country].Where(kw => Char.IsUpper(kw.First())).Contains(p))
               .Select(p => p.First())
               .GroupBy(p => p)
               .Select(group => new
               {
                   Key = group.Key,
                   Count = group.Count()
               })
               .Max(p => p.Count)
               , typeof(T));

    }
}
