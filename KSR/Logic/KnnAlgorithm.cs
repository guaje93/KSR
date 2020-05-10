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
                                  .Select(x => new { Country = x.Key, Count = x.Count() })
                                  .OrderByDescending(n => n.Count);
            var results = new List<Result>();
            foreach (var grp in result)
            {
                var item = new Result { Country = grp.Country, Occurence = grp.Count };
                results.Add(item);
            }

            testArticle.AssignedPlace = results.FirstOrDefault().Country;
        }

        class Result
        {
            public string Country { get; set; }
            public int Occurence { get; set; }
        }
    }
}
