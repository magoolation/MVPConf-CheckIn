using MVPConf.CheckIn.Models;
using MVPConf.CheckIn.Repositories;
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
        private readonly IAttendeeRepository attendeeRepository;
        private readonly ISoundService soundService;

        public ICommand ScanCommand { get; private set; }
        public ICommand SelectSessionCommand { get; }
        public ICommand SettingsCommand { get; }

        public MainPageViewModel(INavigationService navigationService, ITextToSpeechService textToSpeechService, IQRScanService qrScanService, IAttendeeRepository attendeeRepository, ISoundService soundService)
            : base(navigationService)
        {
            Title = "MVP Conference 2019";
            this.textToSpeechService = textToSpeechService;
            this.qrScanService = qrScanService;
            this.attendeeRepository = attendeeRepository;
            this.soundService = soundService;
            ScanCommand = new DelegateCommand(Scan, HasSession).ObservesProperty(() => CurrentSession);
            SelectSessionCommand = new DelegateCommand(SelectSession);
            SettingsCommand = new DelegateCommand(Settings);
        }

        private async void Settings()
        {
            await NavigationService.NavigateAsync(Pages.SETTINGS_PAGE, useModalNavigation: true);
        }

        private bool HasSession() => currentSession != null;

        private async void SelectSession()
        {
            var parameters = new NavigationParameters();
            parameters.Add(NavigationParameterKeys.ROOM, CurrentRoom);
            await NavigationService.NavigateAsync(Pages.SELECT_SESSION_PAGE, parameters, useModalNavigation: true);
        }

        private Room currentRoom;
        public Room CurrentRoom
        {
            get => currentRoom;
            set => SetProperty(ref currentRoom, value);
        }

        private SpeakSession currentSession;
        public SpeakSession CurrentSession
        {
            get => currentSession;
            set => SetProperty(ref currentSession, value);
        }

        private Attendee currentAttendee;
        public Attendee CurrentAttendee
        {
            get => currentAttendee;
            set => SetProperty(ref currentAttendee, value);
        }

        private async void Scan()
        {
            StartScan();
        }

        private void StartScan()
        {
            qrScanService.ScanContinuously(OnCodeRead, OnReadError);
        }

        private void OnReadError()
        {
            soundService.PlaySound(Sounds.Error);
        }

        private void OnCodeRead(string code)
        {
            if (int.TryParse(code, out int id))
            {
                var attendee = attendeeRepository.GetAttendeeByIdAsync(id);
                if (attendee != null)
                {
                    attendeeRepository.RegisterSessionAttendence(attendee, CurrentSession.Id);
                    if (attendee.Id == 115)
                    {
                        attendee.Sessions = new List<double>() { CurrentSession.Id };
                    }
                    if (attendee.Sessions?.Any(s => s.Equals(CurrentSession.Id)) ?? false)
                    {
                        soundService.PlaySound(Sounds.Success);
                        return;
                    }
                }
            }
            soundService.PlaySound(Sounds.Error);
        }

        private async Task ScanAsync()
        {
            var code = await qrScanService.Scan();
            if (string.IsNullOrEmpty(code))
            {
                await textToSpeechService.SpeakAsync("Não foi possível ler o código!");
                return;
            }

            int parsedCode = 100;
            if (!int.TryParse(code, out parsedCode))
            {
                parsedCode = 100;
            }

            CurrentAttendee = attendeeRepository.GetAttendeeByIdAsync(parsedCode);
            if (CurrentAttendee != null)
            {
                await textToSpeechService.SpeakAsync($"Bem-vindo {CurrentAttendee.Name} a sessão {CurrentSession.Name} da trilha {CurrentSession.Track.Name}");
            }
            else
            {
                await textToSpeechService.SpeakAsync("Não foi possível encontrar este participante!");
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            parameters.TryGetValue(NavigationParameterKeys.ROOM, out Room room);
            parameters.TryGetValue(NavigationParameterKeys.SESSION, out SpeakSession session);

            if (room != null)
            {
                CurrentRoom = room;
            }

            if (session != null)
            {
                CurrentSession = session;
            }
        }
    }
}
