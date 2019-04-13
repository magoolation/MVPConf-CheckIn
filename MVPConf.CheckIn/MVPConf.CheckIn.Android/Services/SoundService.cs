using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MVPConf.CheckIn.Services;

namespace MVPConf.CheckIn.Droid.Services
{
    class SoundService : ISoundService
    {

        private readonly MediaPlayer successPlayer;
        private readonly MediaPlayer errorPlayer;

        public SoundService()
        {
            successPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.Success);
            errorPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.Error);
        }


        public void PlaySound(Sounds sound)
        {
            switch (sound)
            {
                case Sounds.Success:
                    successPlayer.Start();
                    break;
                case Sounds.Error:
                    errorPlayer.Start();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}