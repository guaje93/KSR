using System.Diagnostics;

namespace KSR.Model
{
    [DebuggerDisplay("Country = {Country}, True = {TrueNegative}, Recall = {Recall}")]
    public class ClassificationInfo
    {
        #region Properties

        public string Country { get; set; }
        public double ArticlesAmount { get; set; }
        public double ClassifiedAmount { get; set; }
        public int Japan { get; set; }
        public int France { get; set; }
        public int West_germany { get; set; }
        public int Usa { get; set; }
        public int Canada { get; set; }
        public int Uk { get; set; }
        public int TrueNegative { get; set; }
        public double Recall { get; set; }

        #endregion
    }
}
