using System;
using System.Collections.Generic;
using System.Text;

namespace KSR
{
    interface IExtractor
    {
        void Extract(IList<string> keywords, IList<string> textWords);
    }
}
