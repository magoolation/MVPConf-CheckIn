using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVPConf.CheckIn.Services
{
    public interface IBackendService
    {
        Task<AttendeeResult> GetAttendees();
    Task<SpeakSessionResult> GetSpeakSessions();
        Task<ScheduleResult> GetSchedules();
    }
}
