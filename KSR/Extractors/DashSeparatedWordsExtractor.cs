using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KSR.Extractors
{
    class DashSeparatedWordsExtractor<T> : IMeanExtractor<T>
    {
        public T Extract(IList<string> text, string country) =>
            (T)Convert.ChangeType(text.Where(p => Regex.IsMatch(@"\w*[-]\w*", p)), typeof(T));
    }
}
