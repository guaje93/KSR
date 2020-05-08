using KSR;
using System.Collections.Generic;

namespace Logic.Metrics
{
    public interface IMetric
    {
        void CalculateMetricForOneTestSet(Article testSet, List<Article> TrainingVectors, int k);
        void Calculate(List<Article> TrainingVectors, List<Article> TestVectors, int kNeighbours);
    }
}
