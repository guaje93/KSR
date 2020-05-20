﻿using KSR;
using KSR.Logic.Knn;
using KSR.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KSR.Logic.Metric
{
    public class Euclidean : IMetric
    {
        #region Public Methods

        public void Calculate(List<Article> TrainingVectors, Article TestVector, int kNeighbours)
        {
                CalculateMetricForOneTestSet(TestVector, TrainingVectors, kNeighbours);
        }

        #endregion

        #region Private Methods

        private void CalculateMetricForOneTestSet(Article testArticle, List<Article> TrainingVectors, int kNeighbours)
        {
            double distance = 0;

            for (int i = 0; i < TrainingVectors.Count; i++)
            {
                var resultList = new List<double>() {
                   Math.Pow(((testArticle.VectorFeatures.ContainsKeywordExtractor == true ? 1 : 0) - (TrainingVectors.ElementAt(i).VectorFeatures.ContainsKeywordExtractor == true ? 1 : 0)),2),
                   Math.Pow((testArticle.VectorFeatures.DashSeparatedKeyWordsExtractor - TrainingVectors.ElementAt(i).VectorFeatures.DashSeparatedKeyWordsExtractor),2),
                   Math.Pow((testArticle.VectorFeatures.FirstKeywordPositionExtractor - TrainingVectors.ElementAt(i).VectorFeatures.FirstKeywordPositionExtractor),2),
                   Math.Pow((testArticle.VectorFeatures.KeyWordsCountExtractor - TrainingVectors.ElementAt(i).VectorFeatures.KeyWordsCountExtractor),2),
                   Math.Pow((testArticle.VectorFeatures.KeyWordsStartedWithFirstCapitalExtractor - TrainingVectors.ElementAt(i).VectorFeatures.KeyWordsStartedWithFirstCapitalExtractor),2),
                   Math.Pow((testArticle.VectorFeatures.KeyWordsStartedWithFirstLowerExtractor - TrainingVectors.ElementAt(i).VectorFeatures.KeyWordsStartedWithFirstLowerExtractor),2),
                   Math.Pow((testArticle.VectorFeatures.KeyWordsWithAllCapitalLettersExtractor - TrainingVectors.ElementAt(i).VectorFeatures.KeyWordsWithAllCapitalLettersExtractor),2),
                   Math.Pow((testArticle.VectorFeatures.LengthFrom4To6Extractor - TrainingVectors.ElementAt(i).VectorFeatures.LengthFrom4To6Extractor),2),
                   Math.Pow((testArticle.VectorFeatures.LongerThan8Extractor - TrainingVectors.ElementAt(i).VectorFeatures.LongerThan8Extractor),2),
                   Math.Pow((testArticle.VectorFeatures.MeanKeyWordLengthExtractor - TrainingVectors.ElementAt(i).VectorFeatures.MeanKeyWordLengthExtractor),2),
                   Math.Pow((testArticle.VectorFeatures.ShorterThan4Extractor - TrainingVectors.ElementAt(i).VectorFeatures.ShorterThan4Extractor),2),
                   Math.Pow((testArticle.VectorFeatures.UniqueWordsExtractor - TrainingVectors.ElementAt(i).VectorFeatures.UniqueWordsExtractor),2),
            };

                distance = resultList.Sum();

                TrainingVectors.ElementAt(i).Distance = Math.Sqrt(distance);
            }

        }

        #endregion
    }
}