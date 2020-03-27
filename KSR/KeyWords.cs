using System;
using System.Collections.Generic;
using System.Text;

namespace KSR
{
    public class KeyWords : IKeywords
    {
        private IDictionary<string, IList<string>> _keywordsList = new Dictionary<string, IList<string>>()
        {
            //ToDo uzupenic
            ["usa"] = new List<string>()
            {
                "test", "second", "third"
            },
            ["canada"] = new List<string>()
            {
                "test", "second", "third"
            },
            ["uk"] = new List<string>()
            {
                "test", "second", "third"
            },
            ["japan"] = new List<string>()
            {
                "test", "second", "third"
            },
            ["france"] = new List<string>()
            {
                "test", "second", "third"
            },
            ["west-germany"] = new List<string>()
            {
                "test", "second", "third"
            },

        };

        public IList<string> this[string country] => _keywordsList[country];


    }
}
