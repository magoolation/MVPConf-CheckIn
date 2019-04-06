using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LiteDB;
using MVPConf.CheckIn.Models;

namespace MVPConf.CheckIn.Repositories
{
    class AttendeRepository : IAttendeeRepository
    {
        private readonly LiteCollection<Attendee> collection;

        public AttendeRepository()
        {
            var db = new LiteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MVPConf.db"));
            collection = db.GetCollection<Attendee>();

            var mapper = BsonMapper.Global;

            mapper.Entity<Attendee>()
                .Id(x => x.Id);
        }

        public Attendee GetAttendeeByIdAsync(int id)
        {
            return collection.FindById(id); throw new NotImplementedException();
        }
    }
}
