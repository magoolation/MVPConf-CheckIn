using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVPConf.CheckIn.Models
{    
    public class ScheduledSession
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_209")]
        public double Slot1 { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_210")]
        public double                Slot2 { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_211")]
        public double Slot3 { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_213")]
        public double Slot4 { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_214")]
        public double Slot5 { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_216")]
        public double Slot6 { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_217")]
        public double Slot7 { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_309")]
        public double Slot8 { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_310")]
        public double Slot9 { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_311")]
        public double Slot10 { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_313")]
        public double Slot11  { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_314")]
        public double Slot12 { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_316")]
        public double Slot13 { get; set; }
        [JsonProperty(PropertyName = "OData__x0031_317")]
        public double Slot14 { get; set; }
        public string Controle { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
    }
}