using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVPConf.CheckIn.Models
{
    public class Attendee
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Name{ get; set; }
        public string Gender { get; set; }
        public string DocumentID { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public bool PaymentStatus { get; set; }
        public bool Welcome { get; set; }
        public bool Create { get; set; }
        public bool Credenciais { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public List<double> Sessions { get; set; }
    }
}
