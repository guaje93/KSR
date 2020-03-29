using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSR.Extractors
{
    class MeanWordLengthExtractor<T> : IMeanExtractor<T>
    {
        public T Extract(IList<string> text, string country) => 
            (T)Convert.ChangeType(Math.Round(text.Select(p => p.Length).Average(),1), typeof(T));
    }
}
