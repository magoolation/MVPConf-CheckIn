using System;
using System.Collections.Generic;
using System.Text;

namespace MVPConf.CheckIn.Models
{
    public class SessionAttendee
    {
        public int AttendeeId { get; set; }
        public int SpeakSessionId { get; set; }
        public DateTime Date { get; set; }
        public bool IsRegistered { get; set; }
        public int Id { get; internal set; }
    }
}
