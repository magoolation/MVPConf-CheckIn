using MVPConf.CheckIn.Models;
using MVPConf.CheckIn.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MVPConf.CheckIn.ViewModels
{
    public class SelectSessionPageViewModel : ViewModelBase
    {
        private readonly ISpeakSessionRepository speakSessionRepository;

        public ICommand SelectCommand { get; }

        public SelectSessionPageViewModel(INavigationService navigationService, ISpeakSessionRepository speakSessionRepository) : base(navigationService)
        {
            this.speakSessionRepository = speakSessionRepository;

            SelectCommand = new DelegateCommand(SelectSession, () => SelectedSession != null).ObservesProperty(() => SelectedSession);
        }

        private async void SelectSession()
        {
            var parameters = new NavigationParameters();
            parameters.Add(NavigationParameterKeys.ROOM, currentRoom);
            parameters.Add(NavigationParameterKeys.SESSION, SelectedSession);
            await NavigationService.GoBackAsync(parameters, useModalNavigation: true);
        }

        private ObservableCollection<SpeakSession> sessions;
        public ObservableCollection<SpeakSession> Sessions
        {
        get => sessions;
            set => SetProperty(ref sessions, value);
    }

        private Room currentRoom;

        private SpeakSession selectedSession;
        public SpeakSession SelectedSession
        {
            get => selectedSession;
            set => SetProperty(ref selectedSession, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            parameters.TryGetValue(NavigationParameterKeys.ROOM, out currentRoom);

            LoadSessions();
        }

        private void LoadSessions()
        {
            var result = speakSessionRepository.GetSpeakSessionsByRoom(currentRoom.Id);
            Sessions = new ObservableCollection<SpeakSession>(result);
        }
    }
}
