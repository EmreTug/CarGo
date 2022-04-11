using PratikKargo.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PratikKargo
{
    public partial class App : Application
    {
        [Obsolete]
        public static MasterDetailPage MasterDet { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
