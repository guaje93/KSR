using KSR;
using KSR.Model;
using System.Collections.Generic;

namespace KSR.Logic.Metric
{
    public interface IMetric
    {
        void Calculate(List<Article> TrainingVectors, Article TestVector, int kNeighbours);
    }
}
