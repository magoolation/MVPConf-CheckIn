using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVPConf.CheckIn.Models
{
    public class SpeakSession
    {
        public int Id { get; set; }        
        [JsonProperty(PropertyName = "title")]
        public string Name { get; set; }
        public string SessionCode { get; set; }
        [JsonProperty(PropertyName = "trilha")]
        public Track Track { get; set; }
        [JsonProperty(PropertyName = "trilha#Id")]
        public int TrackId { get; set; }
        [JsonProperty(PropertyName = "palestrante01")]
        public Speaker Speaker { get; set; }
        [JsonProperty(PropertyName = "palestrante01#Id")]
        public int SpeakerId { get; set; }
        public string HoraInicioCodigo { get; set; }
        public bool Visible { get; set; }
        public string Descricao { get; set; }
        public float RegisteredAttendees { get; set; }
        public float Attendees { get; set; }
        public bool CreateSessionCode { get; set; }
        [JsonProperty(PropertyName = "sala")]
        public Room Room{ get; set; }
        [JsonProperty(PropertyName = "sala#Id")]
        public int RoomId { get; set; }
        public DateTime Modified { get; set; }
    }
}
