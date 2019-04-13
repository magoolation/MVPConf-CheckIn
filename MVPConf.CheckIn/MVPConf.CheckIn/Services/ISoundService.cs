using System;
using System.Collections.Generic;
using System.Text;

namespace MVPConf.CheckIn.Services
{
    public enum Sounds
    {
        Success = 1,
        Error = 2
    }

    public interface ISoundService
    {
        void PlaySound(Sounds sound);
    }
}
