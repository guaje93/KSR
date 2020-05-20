namespace KSR.Model
{
    public class Settings
    {
        #region Properties

        public string Metric { get; set; }
        public int Neighbours { get; set; }
        public double TrainingSet { get; set; }
        public Measures Measures { get; set; }

        #endregion
    }
}
