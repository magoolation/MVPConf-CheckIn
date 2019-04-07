using MVPConf.CheckIn.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVPConf.CheckIn.Repositories
{
    public interface IAttendeeRepository
    {
        Attendee GetAttendeeByIdAsync(int id);

        void CreateAttendee(Attendee attendee);

        Task Refresh();
    }
}
