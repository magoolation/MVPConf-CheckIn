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
    class SpeakSessionRepository : ISpeakSessionRepository
    {
        private readonly LiteCollection<SpeakSession> collection;
        private readonly LiteCollection<Room> rooms;
        private readonly IBackendService backendService;

        public SpeakSessionRepository(IBackendService backendService)
        {
            var db = new LiteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MVPConf.db"));
            collection = db.GetCollection<SpeakSession>();
            collection.EnsureIndex(s => s.Id);
            collection.EnsureIndex(s => s.RoomId);
            rooms = db.GetCollection<Room>();
            rooms.EnsureIndex(s => s.Id);

            var mapper = BsonMapper.Global;

            mapper.Entity<SpeakSession>()
                .Id(x => x.Id);
            mapper.Entity<Room>()
    .Id(x => x.Id);
            this.backendService = backendService;
        }

        public IEnumerable<Room> GetRooms()
        {
            return rooms.FindAll().ToList().OrderBy(r => r.Name);
        }

        public SpeakSession GetSpeakSessionById(int id)
        {
            return collection.FindById(id);
        }

        public IEnumerable<SpeakSession> GetSpeakSessionsByRoom(int roomId)
        {
            return collection.Find(s => s.RoomId == roomId).ToList().OrderBy(s => s.HoraInicioCodigo);
        }

        public async Task Refresh()
        {
            var result = await backendService.GetSpeakSessions().ConfigureAwait(false);

            foreach(var room in result.SpeakSessions.Select(s => s.Room))
            {
                if (rooms.FindById(room.Id) == null)
                {
                    rooms.Insert(room);
                }
                else
                {
                    rooms.Update(room);
                }
            }

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
