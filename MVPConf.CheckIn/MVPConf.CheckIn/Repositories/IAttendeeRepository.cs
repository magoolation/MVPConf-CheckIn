using MVPConf.CheckIn.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVPConf.CheckIn.Repositories
{
    interface IAttendeeRepository
    {
        Attendee GetAttendeeByIdAsync(int id);
    }
}
