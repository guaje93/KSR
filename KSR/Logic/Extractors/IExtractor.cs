using System.Collections.Generic;

namespace KSR
{
    interface IExtractor
    {
        void Extract(IList<string> keywords, IList<string> textWords);
    }
}
