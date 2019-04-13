using MVPConf.CheckIn.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVPConf.CheckIn.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private readonly IAttendeeRepository attendeeRepository;
        private readonly ISpeakSessionRepository speakSessionRepository;

        public SettingsPageViewModel(INavigationService navigationService, IAttendeeRepository attendeeRepository, ISpeakSessionRepository SpeakSessionRepository) : base(navigationService)
        {
            this.attendeeRepository = attendeeRepository;
            speakSessionRepository = SpeakSessionRepository;

            GoBackCommand = new DelegateCommand(GoBack);
            UpdateCommand = new DelegateCommand(Update);
        }

        private async void Update()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                IsBusy = true;
                await Task.WhenAll(attendeeRepository.Refresh(), speakSessionRepository.Refresh()).ConfigureAwait(false);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void GoBack()
        {
            await NavigationService.GoBackAsync(useModalNavigation: true);
        }

        public ICommand GoBackCommand { get; }
        public ICommand UpdateCommand { get; }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }
    }
}
