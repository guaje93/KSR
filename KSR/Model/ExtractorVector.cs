namespace KSR.Model
{
    public class ExtractorVector
    {

        #region Properties

        public double KeyWordsCountExtractor { get; set; }
        public double MeanKeyWordLengthExtractor { get; set; }
        public double ShorterThan4Extractor { get; set; }
        public double LengthFrom4To6Extractor { get; set; }
        public double LongerThan8Extractor { get; set; }
        public double DashSeparatedKeyWordsExtractor { get; set; }
        public double FirstKeywordPositionExtractor { get; set; }
        public bool ContainsKeywordExtractor { get; set; }
        public double KeyWordsWithAllCapitalLettersExtractor { get; set; }
        public double KeyWordsStartedWithFirstCapitalExtractor { get; set; }
        public double KeyWordsStartedWithFirstLowerExtractor { get; set; }
        public double UniqueWordsExtractor { get; set; }

        #endregion
    }
}
