using System.Collections.Generic;

namespace KSR
{
    public interface IKeywords
    {
        Dictionary<string, Dictionary<string, int>> _keywordsList { get; }
        IList<string> this[string country] { get; }
    }
}