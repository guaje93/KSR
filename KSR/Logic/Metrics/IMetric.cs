using KSR;
using KSR.Model;
using System.Collections.Generic;

namespace KSR.Logic.Metric
{
    public interface IMetric
    {
        //void CalculateMetricForOneTestSet(Article testSet, List<Article> TrainingVectors, int k);
        void Calculate(List<Article> TrainingVectors, List<Article> TestVectors, int kNeighbours);
    }
}
