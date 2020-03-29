using System;
using System.Collections.Generic;
using System.Text;

namespace KSR
{
    interface IMeanExtractor<T>
    {
        T Extract(IList<string> text, string country);
    }
}
