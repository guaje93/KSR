using System;
using System.Collections.Generic;
using System.Text;

namespace KSR
{
    public class Article
    {
        public string Title { get; internal set; }
        public string Place { get; internal set; }
        public List<string> FilteredWords { get; internal set; }
        public List<string> AllWords { get; internal set; }
        public string Text { get; internal set; }
    }
}
