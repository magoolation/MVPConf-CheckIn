using MVPConf.CheckIn.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVPConf.CheckIn.Repositories
{
    interface ISpeakSessionRepository
    {
        SpeakSession GetSpeakSessionById(int id);
        IEnumerable<SpeakSession> GetSpeakSessionsByRoom(int roomId);

        void CreateSpeakSession(SpeakSession session);
    }
}
