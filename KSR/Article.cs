using System;
using System.Collections.Generic;
using System.Text;

namespace KSR
{
    class Article
    {
        public string Title { get; internal set; }
        public string Place { get; internal set; }
        public List<string >Text { get; internal set; }
    }
}
