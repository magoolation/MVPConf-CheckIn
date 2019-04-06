using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVPConf.CheckIn.ViewModels
{
    public class InitializeSegwayPageViewModel : ViewModelBase
    {
        public InitializeSegwayPageViewModel(INavigationService navigationService): base(navigationService)
        {

        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await NavigationService.NavigateAsync(Pages.MAIN_PAGE);
        }
    }
}
