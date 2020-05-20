using System.Collections.Generic;
using System.Linq;

namespace KSR.Logic.Extractors
{
    public class UniqueWordsExtractor : IExtractor
    {
        #region Properties

        public double Result = 0;

        #endregion

        #region Public Methods

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            Result = textWords.Distinct().Count() * 1.0 / textWords.Count();
        }

        #endregion
    }
}
