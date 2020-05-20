using System.Collections.Generic;
using System.Linq;

namespace KSR.Logic.Extractors
{
    public class KeyWordsCountExtractor : IExtractor
    {
        #region Properties

        public double Result { get; private set; } = 0;

        #endregion

        #region Public Methods

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            var foundKeyWords = (textWords.Where(p => !string.IsNullOrWhiteSpace(p))
                                      .Where(p => keywords.Contains(p))
                                      .Distinct()
                                      .Count());

            if (foundKeyWords == 0)
                return;

            Result = foundKeyWords * 1.0 / keywords.Distinct().Count();
        }

        #endregion
    }
}
