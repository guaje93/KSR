using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KSR.Logic.Extractors
{
    public class DashSeparatedKeyWordsExtractor : IExtractor
    {
        #region Fields

        private const string dashSeparatedWordPattern = @"\w*[-]\w*";

        #endregion

        #region Properties
        public double Result { get; private set; } = 0;

        #endregion

        #region Public Methods

        public void Extract(IList<string> keywords, IList<string> textWords)
        {
            Result = textWords.Where(p => !string.IsNullOrWhiteSpace(p))
                                             .Where(p => keywords.Where(kw => Regex.IsMatch(p, dashSeparatedWordPattern)).Contains(p))
                                             .Count() * 1.0 / textWords.Count();
        }

        #endregion
    }
}