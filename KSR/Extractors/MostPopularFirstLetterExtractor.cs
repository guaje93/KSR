using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class MostPopularFirstLetterExtractor<T> : IMeanExtractor<T>
    {
        public T Extract(IList<string> text, string country) => (T)Convert.ChangeType(text.Select(p => p.Substring(0, 1))
                                                                                          .Where(p => char.IsUpper(p.First()))
                                                                                          .GroupBy(p => p)
                                                                                          .Select(group => new
                                                                                          {
                                                                                              Key = group.Key,
                                                                                              Count = group.Count()
                                                                                          })
                                                                                          .Max(p => p.Count), typeof(T));
    }
}
