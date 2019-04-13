using MVPConf.CheckIn.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MVPConf.CheckIn.Repositories
{
    public interface ISpeakSessionRepository
    {
        SpeakSession GetSpeakSessionById(int id);
        IEnumerable<SpeakSession> GetSpeakSessionsByRoom(int roomId);
        IEnumerable<Room> GetRooms();

        Task Refresh();
    }
}
