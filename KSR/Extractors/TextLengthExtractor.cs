using System;
using System.Collections.Generic;
using System.Text;

namespace KSR.Extractors
{
    class TextLengthExtractor<T> : IMeanExtractor<T>
    {
        public T Extract(IList<string> text, string country) => (T)Convert.ChangeType(text.Count, typeof(T));
    }
}
