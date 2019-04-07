using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using MVPConf.CheckIn.Models;
using MVPConf.CheckIn.Services;

namespace MVPConf.CheckIn.Repositories
{
    class AttendeeRepository : IAttendeeRepository
    {
        private readonly LiteCollection<Attendee> collection;
        private readonly IBackendService backendService;

        public AttendeeRepository(IBackendService backendService)
        {
            var db = new LiteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MVPConf.db"));
            collection = db.GetCollection<Attendee>();

            var mapper = BsonMapper.Global;
            mapper.Entity<Attendee>()
                .Id(x => x.Id);

            this.backendService = backendService;
        }

        public void CreateAttendee(Attendee attendee)
        {
            collection.Insert(attendee);
        }

        public Attendee GetAttendeeByIdAsync(int id)
        {
            return collection.FindById(id); throw new NotImplementedException();
        }

        public async Task Refresh()
        {
            var response = await backendService.GetAttendees();

            foreach (var attendee in response.Attendees)
            {
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
    }
}
