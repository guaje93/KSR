﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KSR.Model
{
   public class Settings
    {
        public string Metric { get; set; }
        public int Neighbours { get; set; }
        public double TrainingSet { get; set; }
        public Measures Measures { get; set; }

    }
}
