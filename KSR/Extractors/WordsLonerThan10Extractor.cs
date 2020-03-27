using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class WordsLonerThan10Extractor<T> : IMeanExtractor<T>
    {
        public T Extract(IList<string> text, string country) => 
            (T)Convert.ChangeType(text.Where(p => p.Length > 10).Count(), typeof(T));
    }
}
