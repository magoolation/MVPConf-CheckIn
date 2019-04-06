using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MVPConf.CheckIn.Services
{
    class TextToSpeechService : ITextToSpeechService
    {
        public async Task SpeakAsync(string text)
        {
            await TextToSpeech.SpeakAsync(text);
        }
    }
}
