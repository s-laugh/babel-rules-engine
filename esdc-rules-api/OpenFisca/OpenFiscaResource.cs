using System.Collections.Generic;
using System.Linq;

namespace esdc_rules_api.OpenFisca
{
    public class OpenFiscaResource {
        public Dictionary<string, Dictionary<string, Dictionary<string, object>>> persons { get; set; }
    
        public OpenFiscaResource() {
            persons = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();
        }

        public void CreatePerson(string personName) {
            persons.Add(personName, new Dictionary<string, Dictionary<string, object>>());
        }

        public void SetProp(string personName, string key, object value) {
            persons[personName].Add(key, SetValue(value));
        }

        public object GetProp(string personName, string key) {
            return persons[personName][key].First().Value;
        }

        private Dictionary<string, object> SetValue(object val) {
            return new Dictionary<string, object>() {
                { "2020-09", val }// TODO: can't hard-code this - will depend
            };
        }
    }
}