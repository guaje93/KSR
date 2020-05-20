using System.Collections.Generic;
using System.Linq;

namespace KSR.Logic.Extractors
{
    public class FirstKeywordPositionExtractor : IExtractor
    {
        #region Properties
        public double Result { get; private set; } = 0;

        #endregion

        #region Public Methods

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            var firstKeyWord = textWords.FirstOrDefault(p => keywords.Contains(p));
            var firstKeywordPosition = textWords.IndexOf(firstKeyWord) + 1;
            Result = firstKeywordPosition * 1.0 / textWords.Count();
        }

        #endregion
    }
}
