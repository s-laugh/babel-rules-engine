using System;
using System.ComponentModel.DataAnnotations;

using esdc_rules_api.Lib;

namespace esdc_rules_api.SampleScenario.Classes
{
    public class SampleScenarioPerson : IRulePerson
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        /// <value></value>
        [Required]
        public Guid Id { get; set; }

        public SampleScenarioPerson() {
            Id = new Guid();
        }
    }
}