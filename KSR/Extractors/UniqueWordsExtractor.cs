using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class UniqueWordsExtractor<T> : IMeanExtractor<T>
    {
        public T Extract(IList<string> text, string country) => (T)Convert.ChangeType(text.Distinct().Count(), typeof(T));
    }
}
