using MVPConf.CheckIn.Models;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVPConf.CheckIn.Services
{
    [Headers("Content-Type: application/json; charset=UTF-8")]
    interface IApiService
    {
        [Post("/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=Nn92d7m-cMhP9ihR_nhexN3g8mpzodH_NrPSC6y8Sgw")]
        Task<AttendeeResult> GetAttendees(Request request);
        [Post("/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=Nn92d7m-cMhP9ihR_nhexN3g8mpzodH_NrPSC6y8Sgw")]
        Task<SpeakSessionResult> GetSpeakSessions(Request request);
    }

    class Request
    {
        public string Key { get; set; }
        public string Action { get; set; }
    }

    public class AttendeeResult
    {
        [JsonProperty(PropertyName = "value")]
        public Attendee[] Attendees { get; set; }
    }

    public class SpeakSessionResult
    {
        [JsonProperty(PropertyName = "value")]
        public SpeakSession[] SpeakSessions { get; set; }
    }
}
