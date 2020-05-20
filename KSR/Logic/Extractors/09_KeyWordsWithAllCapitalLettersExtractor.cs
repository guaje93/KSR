using System;
using System.Collections.Generic;
using System.Linq;

namespace KSR.Logic.Extractors
{
    public class KeyWordsWithAllCapitalLettersExtractor : IExtractor
    {
        #region Properties

        public double Result { get; private set; } = 0;

        #endregion

        #region Public Methods

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            Result = textWords.Where(p => !string.IsNullOrWhiteSpace(p))
                                             .Where(p => keywords.Where(kw => kw.All(c => Char.IsUpper(c))).Contains(p))
                                             .Count() * 1.0 / textWords.Count();
        }

        #endregion

    }
}
