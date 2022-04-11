using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PratikKargo.ViewModel
{
    public sealed class MainViewModel
    {
        public string Title => "CarGo";


        private Item _selectedItem;
        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != null)
                {
                    _selectedItem.FontAttributes = FontAttributes.None;
                    _selectedItem.FontFamily = "AvenirNext-DemiBold";
                }
                value.FontAttributes = FontAttributes.Bold;
                value.FontFamily = "AvenirNext-Bold";
                _selectedItem = value;
            }
        }

        public Item[] Items { get; } =
        {
            new Item
            {
                Title = "Task",
                Repos = new Repo[]
                {

                    new Repo
                    {
                        number=1,
                      NameSurname= " isim Soyisim",
                      phoneNumber="03056465234",
                      address="2524 Olympic Dr EOak Harbor, Washington(WA), 98277",
                        distance="800 m"
                    },
                     new Repo
                    {
                                                 number=2,

                      NameSurname= " isim Soyisim",
                      phoneNumber="03056465234",
                      address="2524 Olympic Dr EOak Harbor, Washington(WA), 98277",
                      distance="800 m"
                    },  new Repo
                    {
                                                 number=2,

                      NameSurname= " isim Soyisim",
                      phoneNumber="03056465234",
                      address="2524 Olympic Dr EOak Harbor, Washington(WA), 98277",
                      distance="800 m"
                    },  new Repo
                    {
                                                 number=2,

                      NameSurname= " isim Soyisim",
                      phoneNumber="03056465234",
                      address="2524 Olympic Dr EOak Harbor, Washington(WA), 98277",
                      distance="800 m"
                    },  new Repo
                    {
                                                 number=2,

                      NameSurname= " isim Soyisim",
                      phoneNumber="03056465234",
                      address="2524 Olympic Dr EOak Harbor, Washington(WA), 98277",
                      distance="800 m"
                    },
                      new Repo
                    {
                                                  number=3,

                      NameSurname= " isim Soyisim",
                      phoneNumber="03056465234",
                      address="2524 Olympic Dr EOak Harbor, Washington(WA), 98277",
                      distance="800 m"
                    },
                    new Repo
                    {
                                                number=4,

                      NameSurname= " isim Soyisim",
                      phoneNumber="03056465234",
                      address="mahmutpasa mah. cadde. apt no:3",
                      distance="800 m"
                    },


                }
            },
            new Item
            {
                Title = "Completed",
                Repos = new Repo[]
                {
                      new Repo
                    {
                          number=1,
                      NameSurname= " isim Soyisim",
                      phoneNumber="03056465234",
                      address="mahmutpasa mah. cadde. apt no:3",
                      distance="800 m"
                    },
                    new Repo
                    {
                        number=2,
                      NameSurname= " isim Soyisim",
                      phoneNumber="03056465234",
                      address="mahmutpasa mah. cadde. apt no:3",
                      distance="800 m"
                    },

                }
            }
        };
    }

    public sealed class Item : BindableObject
    {
        public string Title { get; set; }
        public Repo[] Repos { get; set; }

        private FontAttributes _fontAttributes;
        public FontAttributes FontAttributes
        {
            get => _fontAttributes;
            set
            {
                _fontAttributes = value;
                OnPropertyChanged();
            }
        }

        private string _fontFamily = "AvenirNext-DemiBold";
        public string FontFamily
        {
            get => _fontFamily;
            set
            {
                _fontFamily = value;
                OnPropertyChanged();
            }
        }
    }

    public sealed class Repo : BindableObject
    {
        public string NameSurname { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public string distance { get; set; }
        public int number { get; set; }




        private bool _isExpanded = true;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = true;
                OnPropertyChanged();
            }
        }

        public Repo()
        {
            TapCommand = new Command(() =>
            {
                IsExpanded = IsExpanded;
            });

        }

        public ICommand TapCommand { get; }
    }
}
