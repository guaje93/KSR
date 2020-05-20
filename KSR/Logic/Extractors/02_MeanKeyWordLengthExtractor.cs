using System;
using System.Collections.Generic;
using System.Linq;

namespace KSR.Logic.Extractors
{
    public class MeanKeyWordLengthExtractor : IExtractor
    {
        #region Properties

        public double Result { get; private set; } = 0;

        #endregion

        #region Public Methods

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            var filteredWords = textWords.Where(p => keywords.Contains(p)).ToList();
            if (filteredWords.Count == 0)
                return;
            Result = Math.Round(filteredWords.Select(p => p.Length).Average(), 1) * 1.0 / textWords.Count();
        }

        #endregion
    }
}
