using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LiteDB;
using MVPConf.CheckIn.Models;

namespace MVPConf.CheckIn.Repositories
{
    class SpeakSessionRepository : ISpeakSessionRepository
    {
        private readonly LiteCollection<SpeakSession> collection;

        public SpeakSessionRepository()
        {
            var db = new LiteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MVPConf.db"));
            collection = db.GetCollection<SpeakSession>();

            var mapper = BsonMapper.Global;

            mapper.Entity<SpeakSession>()
                .Id(x => x.Id);
        }

        public void CreateSpeakSession(SpeakSession session)
        {
            collection.Insert(session);
        }

        public SpeakSession GetSpeakSessionById(int id)
        {
            return collection.FindById(id);
        }

        public IEnumerable<SpeakSession> GetSpeakSessionsByRoom(int roomId)
        {
            return collection.Find(s => s.RoomId == roomId);
        }        
    }
}
