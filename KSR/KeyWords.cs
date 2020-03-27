using System;
using System.Collections.Generic;
using System.Text;

namespace KSR
{
    public class KeyWords : IKeywords
    {
        private IDictionary<string, IList<string>> _keywordsList = new Dictionary<string, IList<string>>()
        {
            ["usa"] = new List<string>()
            {
                "test", "second", "third"
            }

        };

        public IList<string> this[string country] => _keywordsList[country];


    }
}
