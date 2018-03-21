using System;
using System.Collections.Generic;

namespace TelesBot.Model
{
    public class ImageAnalysisResult
    {
        public string Id { get; set; }

        public string Project { get; set; }

        public string Iteration { get; set; }

        public DateTime Created { get; set; }

        public IEnumerable<Predictions> Predictions { get; set; }
    }

    public class Predictions
    {
        public string TagId { get; set; }

        public string Tag { get; set; }

        public double Probability { get; set; }
    }
}