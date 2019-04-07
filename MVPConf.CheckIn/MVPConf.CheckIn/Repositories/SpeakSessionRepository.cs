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
    class SpeakSessionRepository : ISpeakSessionRepository
    {
        private readonly LiteCollection<SpeakSession> collection;
        private readonly IBackendService backendService;

        public SpeakSessionRepository(IBackendService backendService)
        {
            var db = new LiteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MVPConf.db"));
            collection = db.GetCollection<SpeakSession>();

            var mapper = BsonMapper.Global;

            mapper.Entity<SpeakSession>()
                .Id(x => x.Id);
            this.backendService = backendService;
        }        

        public SpeakSession GetSpeakSessionById(int id)
        {
            return collection.FindById(id);
        }

        public IEnumerable<SpeakSession> GetSpeakSessionsByRoom(int roomId)
        {
            return collection.Find(s => s.RoomId == roomId);
        }

        public async Task Refresh()
        {
            var result = await backendService.GetSpeakSessions();
            foreach(var session in result.SpeakSessions)
                {
                if (collection.FindById(session.Id) == null)
                {
                    collection.Insert(session);
                }
                else
                {
                    collection.Update(session);
                }
            }
        }
    }
}
