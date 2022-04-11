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


    public class LoginViewModel
    {



        public string Username { get; set; }

        public string Password { get; set; }



        public ICommand LoginCommand
        {

            get
            {

                
                return new Command(async () =>
                {
                    PratikKargo.Helpers.Settings.Username = Username;
                    PratikKargo.Helpers.Settings.Password = Password;

                });
            }


        }

        public LoginViewModel()
        {
            Username = PratikKargo.Helpers.Settings.Username;
            Password = PratikKargo.Helpers.Settings.Password;
        }

    }
}
