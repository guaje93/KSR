using System;
using System.Collections.Generic;
using System.Linq;

namespace KSR.Logic.Extractors
{
    public class KeyWordsStartedWithFirstLowerExtractor : IExtractor
    {
        #region Properties

        public double Result = 0;

        #endregion

        #region Public Methods

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            Result = textWords.Where(p => !string.IsNullOrWhiteSpace(p))
                                      .Where(p => keywords.Where(kw => Char.IsLower(kw.First())).Contains(p))
                                      .Count() * 1.0 / textWords.Count();
        }

        #endregion
    }
}

