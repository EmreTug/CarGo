using PratikKargo.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PratikKargo.ViewModel
{
    public class MasterViewModel
    {
        public MasterViewModel()
        {
            logout = new Command(onLogout);
            profile = new Command(onProfile);

        }
        ICommand logout, profile;
        public ICommand Logout
        {
            get
            {
                return logout;
            }

            set
            {
                logout = value;
            }
        }
        public ICommand Profile
        {
            get
            {
                return profile;
            }

            set
            {
                profile = value;
            }
        }
        private async void onProfile(object param)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Profile());


        }
        private async void onLogout(object param)
        {
            App.MasterDet.IsPresented = false;
            Application.Current.MainPage = new NavigationPage(new LoginPage());


        }

    }
}
