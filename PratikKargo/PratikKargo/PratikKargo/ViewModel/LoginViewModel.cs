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

            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string PropertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
            }



            public string Username { get; set; }

            public string Password { get; set; }
            private string _note;
            public string Note
            {
                get
                {
                    return _note;
                }

                set
                {
                    _note = value;

                    OnPropertyChanged("Note");
                }
            }


            public ICommand LoginCommand
            {
                get
                {
                    return new Command(async () =>
                    {
                      

                                Application.Current.MainPage = new NavigationPage(new MainPage());

                      

                    });
                }
            }

          

        }
    }
