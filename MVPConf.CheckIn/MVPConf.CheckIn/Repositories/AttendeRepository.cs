using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using MVPConf.CheckIn.Models;
using MVPConf.CheckIn.Services;

namespace MVPConf.CheckIn.Repositories
{
    class AttendeeRepository : IAttendeeRepository
    {
        private readonly ILiteCollection<Attendee> collection;
        private readonly ILiteCollection<SessionAttendee> sessionAttendees;

        private readonly IBackendService backendService;

        public AttendeeRepository(IBackendService backendService)
        {
            var db = new LiteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MVPConf.db"));
            collection = db.GetCollection<Attendee>();

            collection.EnsureIndex(x => x.Id);

            sessionAttendees = db.GetCollection<SessionAttendee>();

            sessionAttendees.EnsureIndex(s => s.AttendeeId);

            var mapper = BsonMapper.Global;
            mapper.Entity<Attendee>()
                .Id(x => x.Id);
            mapper.Entity<SessionAttendee>()
                .Id(x => x.Id, true);

            this.backendService = backendService;
        }

        public void CreateAttendee(Attendee attendee)
        {
            collection.Insert(attendee);
        }

        public Attendee GetAttendeeByIdAsync(int id)
        {
            return collection.FindById(id);
        }

        public async Task Refresh()
        {
            var attendeeTask = backendService.GetAttendees();
            var scheduleTask = backendService.GetSchedules();

            await Task.WhenAll(attendeeTask, scheduleTask).ConfigureAwait(false);

            var attendees = attendeeTask.Result.Attendees;
            var schedules = scheduleTask.Result.ScheduledSessions;

            foreach (var attendee in attendees)
            {
                if (schedules.Any(s => s.Title== attendee.Login))
                {
                    var item = schedules.First(s => s.Title == attendee.Login);
                    attendee.Sessions = new List<double>() { item.Slot1, item.Slot2, item.Slot3, item.Slot4, item.Slot5, item.Slot6, item.Slot7, item.Slot8, item.Slot9, item.Slot10, item.Slot11, item.Slot12, item.Slot13, item.Slot14 };
                }
                if (collection.FindById(attendee.Id) == null)
                {
                    collection.Insert(attendee);
                }
                else
                {
                    collection.Update(attendee);
                }
            }
        }

        public void RegisterSessionAttendence(Attendee attendee, int speakSessionId)
        {
            if (sessionAttendees.FindOne(s => s.AttendeeId == attendee.Id && s.SpeakSessionId == speakSessionId) != null)
            {
                sessionAttendees.Insert(new SessionAttendee()
                {
                    AttendeeId = attendee.Id,
                    SpeakSessionId = speakSessionId,
                    Date = DateTime.Now
                });
            }
        }
    }
}