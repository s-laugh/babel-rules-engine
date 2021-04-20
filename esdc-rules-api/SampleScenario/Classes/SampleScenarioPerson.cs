using System;

using esdc_rules_api.Lib;

namespace esdc_rules_api.SampleScenario.Classes
{
    public class SampleScenarioPerson : IRulePerson
    {
        public Guid Id { get; set; }

        public SampleScenarioPerson() {
            Id = new Guid();
        }
    }
}