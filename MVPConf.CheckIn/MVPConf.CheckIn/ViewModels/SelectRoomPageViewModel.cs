using MVPConf.CheckIn.Models;
using MVPConf.CheckIn.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVPConf.CheckIn.ViewModels
{
    public class SelectRoomPageViewModel : ViewModelBase
    {
        private readonly ISpeakSessionRepository speakSessionRepository;

        public SelectRoomPageViewModel(INavigationService navigationService, ISpeakSessionRepository speakSessionRepository) : base(navigationService)
        {
            this.speakSessionRepository = speakSessionRepository;

            SelectCommand = new DelegateCommand(SelectRoom, IsRoomSelected).ObservesProperty(() => SelectedRoom);

        }

        private bool IsRoomSelected() => selectedRoom != null;

        private async void SelectRoom()
        {
            var parameters = new NavigationParameters();
            parameters.Add(NavigationParameterKeys.ROOM, selectedRoom);
            await NavigationService.NavigateAsync(Pages.MAIN_PAGE, parameters);
        }

        public ICommand SelectCommand { get; }

        private Room selectedRoom;
        public Room SelectedRoom
        {
            get => selectedRoom;
            set => SetProperty(ref selectedRoom, value);
        }

        private ObservableCollection<Room> rooms;
        public ObservableCollection<Room> Rooms
        {
            get => rooms;
            set => SetProperty(ref rooms, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            LoadRooms();
        }

        private void LoadRooms()
        {
            var result = speakSessionRepository.GetRooms();
            Rooms = new ObservableCollection<Room>(result);
        }
    }
}