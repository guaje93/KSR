using System.Collections.Generic;

namespace KSR.Logic.Extractors
{
    interface IExtractor
    {
        void Extract(IList<string> keywords, IList<string> textWords);
    }
}
