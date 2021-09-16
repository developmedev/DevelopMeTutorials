using DevelopMeTutorials.Services;
using DevelopMeTutorials.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace DevelopMeTutorials
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                AskForRelevantPermissionsAsync();
            }
            else if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                //AskForRelevantPermissionsAsync();
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await AskForRelevantPermissionsAsync();
                });
            }
        }
        private async Task AskForRelevantPermissionsAsync()
        {
            await AskForPermissionAsync<Permissions.LocationAlways>();
            await AskForPermissionAsync<Permissions.Camera>();
            await AskForPermissionAsync<Permissions.Media>();
            await AskForPermissionAsync<Permissions.StorageRead>();
            await AskForPermissionAsync<Permissions.StorageWrite>();

        }
        private async Task AskForPermissionAsync<TPermission>()
              where TPermission : BasePermission, new()
        {
            var result = await CheckStatusAsync<TPermission>();
            if (result != PermissionStatus.Granted)
                await RequestAsync<TPermission>();
        }

        public async Task<PermissionStatus> CheckAndRequestPermissionAsync<T>(T permission)
            where T : BasePermission
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }

            return status;
        }
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
