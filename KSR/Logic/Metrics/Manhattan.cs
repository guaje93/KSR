using KSR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Metrics
{
    public class Manhattan : IMetric
    {

        public void Calculate(List<Article> TrainingVectors, List<Article> TestVectors, int kNeighbours)
        {
            for (int i = 0; i < TestVectors.Count; i++)
            {
                CalculateMetricForOneTestSet(TestVectors.ElementAt(i), TrainingVectors, kNeighbours);
            }

            var test = TrainingVectors.GroupBy(p => p.Distance);
        }

        public void CalculateMetricForOneTestSet(Article testArticle, List<Article> TrainingVectors, int kNeighbours)
        {
            double distance = 0;
            for (int i = 0; i < TrainingVectors.Count; i++)
            {
                var resultList = new List<double>() {
                   //Math.Abs((testArticle.VectorFeatures.ContainsKeywordExtractor == true ? 1 : 0) - (TrainingVectors.ElementAt(i).VectorFeatures.ContainsKeywordExtractor == true ? 1 : 0)),
                   Math.Abs(testArticle.VectorFeatures.DashSeparatedKeyWordsExtractor - TrainingVectors.ElementAt(i).VectorFeatures.DashSeparatedKeyWordsExtractor),
                   Math.Abs(testArticle.VectorFeatures.FirstKeywordPositionExtractor - TrainingVectors.ElementAt(i).VectorFeatures.FirstKeywordPositionExtractor),
                   Math.Abs(testArticle.VectorFeatures.KeyWordsCountExtractor - TrainingVectors.ElementAt(i).VectorFeatures.KeyWordsCountExtractor),
                   Math.Abs(testArticle.VectorFeatures.KeyWordsStartedWithFirstCapitalExtractor - TrainingVectors.ElementAt(i).VectorFeatures.KeyWordsStartedWithFirstCapitalExtractor),
                   Math.Abs(testArticle.VectorFeatures.KeyWordsStartedWithFirstLowerExtractor - TrainingVectors.ElementAt(i).VectorFeatures.KeyWordsStartedWithFirstLowerExtractor),
                   Math.Abs(testArticle.VectorFeatures.KeyWordsWithAllCapitalLettersExtractor - TrainingVectors.ElementAt(i).VectorFeatures.KeyWordsWithAllCapitalLettersExtractor),
                   Math.Abs(testArticle.VectorFeatures.LengthFrom4To6Extractor - TrainingVectors.ElementAt(i).VectorFeatures.LengthFrom4To6Extractor),
                   Math.Abs(testArticle.VectorFeatures.LongerThan8Extractor - TrainingVectors.ElementAt(i).VectorFeatures.LongerThan8Extractor),
                   Math.Abs(testArticle.VectorFeatures.MeanKeyWordLengthExtractor - TrainingVectors.ElementAt(i).VectorFeatures.MeanKeyWordLengthExtractor),
                   Math.Abs(testArticle.VectorFeatures.ShorterThan4Extractor - TrainingVectors.ElementAt(i).VectorFeatures.ShorterThan4Extractor),
                   Math.Abs(testArticle.VectorFeatures.UniqueWordsExtractor - TrainingVectors.ElementAt(i).VectorFeatures.UniqueWordsExtractor)
            };

                distance = resultList.Sum();
                TrainingVectors.ElementAt(i).Distance = Math.Sqrt(distance);
            }
            
            KnnAlgorithm.AssignCountry(testArticle, TrainingVectors, kNeighbours);
        }

    }
}
