using PratikKargo.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PratikKargo.ViewModel
{


    public class LoginViewModel : INotifyPropertyChanged
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool HidePassword { get; set; }
        public string EyeIcon
        {
            get
            {
                return HidePassword ? "eye" : "eyeHidden";
            }
        }
        public ICommand LoginCommand =>
            new Command(Login);
       
        public ICommand ShowPasswordCommand =>
            new Command(HidePasswordChange);


        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
            HidePassword = true;
        }

        public async void Login()
        {
            string message = "";

            if (string.IsNullOrEmpty(UserName))
            {
                message = "UserName or Email is required.";
                Application.Current.MainPage = new BurgerMenu();

            }

            if (string.IsNullOrEmpty(Password))
            {
                message = $"{message} \nPassword is required.";
                Application.Current.MainPage = new BurgerMenu();

            }

            if (!string.IsNullOrEmpty(message))
            {
                await Application.Current.MainPage.DisplayAlert("Alert!", message, "Ok");
            }
            else
            {
                Application.Current.MainPage = new BurgerMenu();
            }
        }

      

        public void HidePasswordChange()
        {
            HidePassword = !HidePassword;
        }
    }
}
