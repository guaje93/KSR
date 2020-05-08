using KSR;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public static class KnnAlgorithm
    {
        public static void AssignCountry(Article testArticle, List<Article> TrainingArticles, int neighboursAmount)
        {
            TrainingArticles = TrainingArticles.OrderBy(h => h.Distance).ToList(); //sortuje
            var neighbours = TrainingArticles.Take(neighboursAmount).ToList(); //bierze k sąsiadów

            var result = neighbours.GroupBy(x => x.Place)
                                   .Select(x => new { Country = x.Key, Count = x.Count(), Avg = neighbours.Where(p => p.Place == x.Key).Sum(x => x.Distance) / x.Count() })
                                   .OrderBy(n => n.Avg).FirstOrDefault().Country;

            testArticle.AssignedPlace = result;
        }
    }
}
