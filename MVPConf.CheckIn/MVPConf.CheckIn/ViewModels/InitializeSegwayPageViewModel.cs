using MVPConf.CheckIn.Repositories;
using Prism.AppModel;
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
        private readonly IApplicationStore applicationStore;

        public InitializeSegwayPageViewModel(INavigationService navigationService, IApplicationStore applicationStore, IAttendeeRepository attendeeRepository, ISpeakSessionRepository speakSessionRepository) : base(navigationService)
        {
            this.applicationStore = applicationStore;
            this.attendeeRepository = attendeeRepository;
            this.speakSessionRepository = speakSessionRepository;
        }

        private string status = "Carregando ...";
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await LoadData();

            await NavigationService.NavigateAsync(Pages.SELECT_ROOM_PAGE);
        }

        private async Task LoadData()
        {
            Status = "Carregando participantes ...";
            await attendeeRepository.Refresh().ConfigureAwait(false);
            Status = "Carregando palestras ...";
            await speakSessionRepository.Refresh().ConfigureAwait(false);
            Status = "Finalizando ...";

            applicationStore.MarkDataAsLoaded();
        }
    }
}
