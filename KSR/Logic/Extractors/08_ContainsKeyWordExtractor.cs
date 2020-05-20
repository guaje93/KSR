using System.Collections.Generic;
using System.Linq;

namespace KSR.Logic.Extractors
{
    public class ContainsKeywordExtractor : IExtractor
    {
        #region Properties

        public bool Result { get; private set; } = false;

        #endregion

        #region Public Methods

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            Result = textWords.Any(p => keywords.Contains(p));
        }

        #endregion
    }
}
