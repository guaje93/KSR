using System.Collections.Generic;
using System.Linq;

namespace KSR.Logic.Extractors
{
    public class LengthFrom4To6Extractor : IExtractor
    {
        #region Properties
        public double Result { get; private set; } = 0;

        #endregion

        #region Public Methods

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            Result = textWords.Where(p => !string.IsNullOrWhiteSpace(p))
                             .Where(p => p.Length >= 4 && p.Length < 6)
                             .Count() * 1.0 / textWords.Count();
        }

        #endregion
    }
}
