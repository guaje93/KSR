using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KSR.Model
{
    [DebuggerDisplay("Country = {Country}, True = {TrueNegative}, Recall = {Recall}")]
    public class ClassificationInfo
    {
        public string Country;
        public double ArticlesAmount;
        public double ClassifiedAmount;
        public int Japan;
        public int France;
        public int West_germany;
        public int Usa;
        public int Canada;
        public int Uk;

        public int TrueNegative { get; internal set; }
        public double Recall { get; internal set; }
    }
}
