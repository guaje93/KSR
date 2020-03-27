using System;
using System.Collections.Generic;
using System.Text;

namespace KSR
{
    class AnalysisVector
    {
        private int _dashSeparatedWords;
        private int _firstCapitalShorterThan4;
        private int _firstCapitalLenthFrom4To6;
        private int _keyWordsAmount;
        private double _meanWordLength;
        private string _mostPopularFirstLetter;
        private int _textLength;
        private int _uniqueWords;
        private int _wordsLongerThan10;

        public int DashSeparatedWords { get => _dashSeparatedWords; set => _dashSeparatedWords = value; }
        public int FirstCapitalShorterThan4 { get => _firstCapitalShorterThan4; set => _firstCapitalShorterThan4 = value; }
        public int FirstCapitalLenthFrom4To6 { get => _firstCapitalLenthFrom4To6; set => _firstCapitalLenthFrom4To6 = value; }
        public int KeyWordsAmount { get => _keyWordsAmount; set => _keyWordsAmount = value; }
        public double MeanWordLength { get => _meanWordLength; set => _meanWordLength = value; }
        public string MostPopularFirstLetter { get => _mostPopularFirstLetter; set => _mostPopularFirstLetter = value; }
        public int TextLength { get => _textLength; set => _textLength = value; }
        public int UniqueWords { get => _uniqueWords; set => _uniqueWords = value; }
        public int WordsLongerThan10 { get => _wordsLongerThan10; set => _wordsLongerThan10 = value; }
    }
}
