using System.Collections.Generic;

namespace KSR
{
    public interface IKeywords
    {
        IList<string> this[string country] { get; }

    }
}