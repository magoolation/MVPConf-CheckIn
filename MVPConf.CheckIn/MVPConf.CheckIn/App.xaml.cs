using Prism;
using Prism.Ioc;
using MVPConf.CheckIn.ViewModels;
using MVPConf.CheckIn.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MVPConf.CheckIn.Services;
using Xamarin.Essentials;
using MVPConf.CheckIn.Repositories;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Prism.AppModel;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MVPConf.CheckIn
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var applicationStore = Container.Resolve<IApplicationStore>();

            if (Connectivity.NetworkAccess == NetworkAccess.Internet && !applicationStore.IsDataLoaded())
            {
                await NavigationService.NavigateAsync(Pages.INITIALIZE_SEGWAY_PAGE);
            }
            else
            {
                await NavigationService.NavigateAsync(Pages.SELECT_ROOM_PAGE);
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<InitializeSegwayPage, InitializeSegwayPageViewModel>();
            containerRegistry.RegisterForNavigation<SelectRoomPage, SelectRoomPageViewModel>();

            containerRegistry.RegisterSingleton<ITextToSpeechService, TextToSpeechService>();

            containerRegistry.RegisterSingleton<IBackendService, BackendService>();
            containerRegistry.RegisterSingleton<IAttendeeRepository, AttendeeRepository>();
            containerRegistry.RegisterSingleton<ISpeakSessionRepository, SpeakSessionRepository>();

            containerRegistry.RegisterInstance(GetClient());
            containerRegistry.RegisterForNavigation<SelectSessionPage, SelectSessionPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
        }

        private HttpClient GetClient()
        {
            var handler = new HttpClientHandler()
            {
                UseProxy = true,
                UseDefaultCredentials = true
            };

            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            }

            var client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromMinutes(5),
                BaseAddress = new Uri(Constants.URL),
        };

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            

        return client;
        }
    }
}
