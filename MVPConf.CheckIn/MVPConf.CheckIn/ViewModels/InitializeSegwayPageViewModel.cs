using MVPConf.CheckIn.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVPConf.CheckIn.ViewModels
{
    public class InitializeSegwayPageViewModel : ViewModelBase
    {
        private readonly IAttendeeRepository attendeeRepository;
        private readonly ISpeakSessionRepository speakSessionRepository;

        public InitializeSegwayPageViewModel(INavigationService navigationService, IAttendeeRepository attendeeRepository, ISpeakSessionRepository speakSessionRepository) : base(navigationService)
        {
            this.attendeeRepository = attendeeRepository;
            this.speakSessionRepository = speakSessionRepository;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await LoadData();

            await NavigationService.NavigateAsync(Pages.MAIN_PAGE);
        }

        private async Task LoadData()
        {
            await Task.WhenAll(attendeeRepository.Refresh(), speakSessionRepository.Refresh());
        }
    }
}
