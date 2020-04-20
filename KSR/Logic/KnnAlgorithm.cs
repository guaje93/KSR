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

            //return CheckTag(testArticle, neighbours, tag);
        }

        // sprawdza, czy w państwa z testowych danych znajdują się w podanych przez nas artykułach
        //private static bool CheckTag(Article testArticle, List<Article> neighbours, string tag) 
        //{
        //    Dictionary<string, int> howManyTags = new Dictionary<string, int>();
        //    int howManyTimesOccur = 0;

        //    for (int i = 0; i < neighbours.Count; i++)
        //    {
        //        for (int j = 0; j < neighbours.Count; j++)
        //        {
        //            if (neighbours.ElementAt(i).SelectedTag.FirstOrDefault() == neighbours.ElementAt(j).SelectedTag.FirstOrDefault())
        //            {
        //                howManyTimesOccur++;
        //            }
        //        }

        //        if (neighbours.ElementAt(i).SelectedTag.FirstOrDefault() != null && howManyTags.ContainsKey(neighbours.ElementAt(i).SelectedTag.FirstOrDefault())) //zeruje licznik żeby nie powtarzać juz raz dodanych państsw 
        //        {
        //            howManyTimesOccur = 0;
        //            continue;
        //        }

        //        if(neighbours.ElementAt(i).SelectedTag.FirstOrDefault() != null)
        //            howManyTags.Add(neighbours.ElementAt(i).SelectedTag.FirstOrDefault(), howManyTimesOccur);

        //        howManyTimesOccur = 0;
        //    }

        //    howManyTags = howManyTags.OrderByDescending(x => x.Value)
        //        .ToDictionary(pair => pair.Key, pair => pair.Value);

        //    List<KeyValuePair<string, int>> result = howManyTags.ToList();
        //    testArticle.AssignedTag = result.First().Key;

        //    if (testArticle.SelectedTag.FirstOrDefault() != null && testArticle.SelectedTag.FirstOrDefault().Equals(testArticle.AssignedTag))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        class Result{
            public string Country { get; set; }
            public int Occurence { get; set; }
       }
    }
}
