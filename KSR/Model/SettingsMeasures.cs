using System;
using System.Collections.Generic;
using System.Text;

namespace KSR.Model
{
    public class Measures
    {
        public bool KeyWordsCount { get; set; }
        public bool MeanKeyWordLength { get; set; }
        public bool FirstCapitalShorterThan4 { get; set; }
        public bool FirstCapitalLengthFrom4To6 { get; set; }
        public bool KeyWordsLongerThan8 { get; set; }
        public bool DashSeparatedKeyWords { get; set; }
        public bool FirstKeywordPosition { get; set; }
        public bool ContainsKeyWord { get; set; }
        public bool KeyWordsWithAllCapitalLetters { get; set; }
        public bool KeyWordsStartedWithFirstCapital { get; set; }
        public bool KeyWordsStartedWithFirstLower { get; set; }
        public bool UniqueWords { get; set; }
    }
}
