using MVPConf.CheckIn.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVPConf.CheckIn.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ITextToSpeechService textToSpeechService;
        private readonly IQRScanService qrScanService;

        public ICommand ScanCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService, ITextToSpeechService textToSpeechService, IQRScanService qrScanService)
            : base(navigationService)
        {
            Title = "MVP Conference 2019";
            this.textToSpeechService = textToSpeechService;
            this.qrScanService = qrScanService;

            ScanCommand = new DelegateCommand(Scan);
        }

        private string trackName = "dotnet";
        public string TrackName
        {
            get => trackName;
            set => SetProperty(ref trackName, value);
        }

        private string sessionName = "Inteligencia Artificial a serviço da Acessibilidade";
        public string SessionName
        {
            get => sessionName;
            set => SetProperty(ref sessionName, value);
        }

        private string speakersName = "Alexandre Costa e Angelo Belchior";
        public string SpeakersName
        {
            get => speakersName;
            set => SetProperty(ref speakersName, value);
        }

        private string attendeeName = "Alexandre Santos Costa";
        public string AttendeeName
        {
            get => attendeeName;
            set => SetProperty(ref attendeeName, value);
        }

        private async void Scan()
        {
            await ScanAsync();
        }

        private async Task ScanAsync()
        {
            var code = await qrScanService.Scan();
            if (string.IsNullOrEmpty(code))
            {
                await textToSpeechService.SpeakAsync("Não foi possível ler o código!");
                return;
            }
            await textToSpeechService.SpeakAsync($"Bem-vindo {AttendeeName} a sessão {SessionName} da trilha {TrackName}");
        }
    }
}
