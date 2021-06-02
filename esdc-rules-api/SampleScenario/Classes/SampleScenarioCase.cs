using System;

using esdc_rules_api.Lib;

namespace esdc_rules_api.SampleScenario.Classes
{
    /// <summary>
    /// Class representing an encoding of a sample scenario
    /// </summary>
    public class SampleScenarioCase : IRule
    {
        public double SomeThreshold { get; set; }
        public bool SomeToggle { get; set; }
        public int SomeFactor { get; set; }
    }
}