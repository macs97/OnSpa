using Prism;
using Prism.Ioc;
using OnSpa.Prism.ViewModels;
using OnSpa.Prism.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Syncfusion.Licensing;
using OnSpa.Common.Services;
using Concierto.Prism.Helpers;
using OnSpa.Common.Helpers;

namespace OnSpa.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MzM2MjIyQDMxMzgyZTMzMmUzMG1aSkc4dEdSNFRPWmcxRnh2d21GWUFVNHV4ZEJUWThZcVFIM3hCMTd6eUU9");
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(OnSpaMasterDetailPage)}/NavigationPage/{nameof(MainPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.Register<IFilesHelper, FilesHelper>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<OnSpaMasterDetailPage, OnSpaMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ServicePage, ServicePageViewModel>();
            containerRegistry.RegisterForNavigation<RecoverPasswordPage, RecoverPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ReservePage, ReservePageViewModel>();
            containerRegistry.RegisterForNavigation<RecordPage, RecordPageViewModel>();
        }
    }
}
