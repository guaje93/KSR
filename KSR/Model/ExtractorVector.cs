namespace KSR
{
    public class ExtractorVector
    {
        private double _keyWordsCountExtractor;
        private double _meanKeyWordLengthExtractor;
        private double _shorterThan4Extractor;
        private double _lengthFrom4To6Extractor;
        private double _longerThan8Extractor;
        private double _dashSeparatedKeyWordsExtractor;
        private double _firstKeywordPositionExtractor;
        private bool _containsKeywordExtractor;
        private double _keyWordsWithAllCapitalLettersExtractor;
        private double _keyWordsStartedWithFirstCapitalExtractor;
        private double _keyWordsStartedWithFirstLowerExtractor;
        private double _UniqueWordsExtractor;

        public double KeyWordsCountExtractor { get => _keyWordsCountExtractor; set => _keyWordsCountExtractor = value; }
        public double MeanKeyWordLengthExtractor { get => _meanKeyWordLengthExtractor; set => _meanKeyWordLengthExtractor = value; }
        public double ShorterThan4Extractor { get => _shorterThan4Extractor; set => _shorterThan4Extractor = value; }
        public double LengthFrom4To6Extractor { get => _lengthFrom4To6Extractor; set => _lengthFrom4To6Extractor = value; }
        public double LongerThan8Extractor { get => _longerThan8Extractor; set => _longerThan8Extractor = value; }
        public double DashSeparatedKeyWordsExtractor { get => _dashSeparatedKeyWordsExtractor; set => _dashSeparatedKeyWordsExtractor = value; }
        public double FirstKeywordPositionExtractor { get => _firstKeywordPositionExtractor; set => _firstKeywordPositionExtractor = value; }
        public bool ContainsKeywordExtractor { get => _containsKeywordExtractor; set => _containsKeywordExtractor = value; }
        public double KeyWordsWithAllCapitalLettersExtractor { get => _keyWordsWithAllCapitalLettersExtractor; set => _keyWordsWithAllCapitalLettersExtractor = value; }
        public double KeyWordsStartedWithFirstCapitalExtractor { get => _keyWordsStartedWithFirstCapitalExtractor; set => _keyWordsStartedWithFirstCapitalExtractor = value; }
        public double KeyWordsStartedWithFirstLowerExtractor { get => _keyWordsStartedWithFirstLowerExtractor; set => _keyWordsStartedWithFirstLowerExtractor = value; }
        public double UniqueWordsExtractor { get => _UniqueWordsExtractor; set => _UniqueWordsExtractor = value; }
    }
}
