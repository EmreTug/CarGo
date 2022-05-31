using PratikKargo.Model;
using PratikKargo.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Essentials;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace PratikKargo.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public MainViewModel()
        {
            LoadProductsAsync();

        }
        private void LoadProductsAsync()
        {

            AllCargo = StaticClass.AllCargo;

            Completed = StaticClass.Completed;

              

          
        }

        private ObservableCollection<CargoMain> _AllCargo;
        public ObservableCollection<CargoMain> AllCargo
        {
            get
            {
                if (_AllCargo == null)
                {
                    _AllCargo = new ObservableCollection<CargoMain>();

                }
                return _AllCargo;
            }
            set
            {
                _AllCargo = value;

            }
        }

        private ObservableCollection<CargoMain> _completed;
        public  ObservableCollection<CargoMain> Completed
        {
            get
            {
                if (_completed == null)
                {
                    _completed = new ObservableCollection<CargoMain>();

                }
                return _completed;
            }
            set
            {
                _completed = value;

            }
        }
        //    public static Repo[] repos;


        //    public string Title => "CarGo";




        //    public static Item[] Items { get; } =
        //    {
        //        new Item
        //        {
        //            Title = "Task",
        //            Repos =repos },
        //        new Item
        //        {
        //            Title = "Completed",
        //            Repos =repos
        //        }
        //    };
        //}

        //public sealed class Item : BindableObject
        //{
        //    public string Title { get; set; }
        //    public Repo[] Repos { get; set; }

        //    private FontAttributes _fontAttributes;
        //    public FontAttributes FontAttributes
        //    {
        //        get => _fontAttributes;
        //        set
        //        {
        //            _fontAttributes = value;
        //            OnPropertyChanged();
        //        }
        //    }

        //    private string _fontFamily = "AvenirNext-DemiBold";
        //    public string FontFamily
        //    {
        //        get => _fontFamily;
        //        set
        //        {
        //            _fontFamily = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //public sealed class Repo : BindableObject
        //{
        //    public string NameSurname { get; set; }
        //    public string phoneNumber { get; set; }
        //    public string address { get; set; }
        //    public string distance { get; set; }
        //    public int number { get; set; }
        //    public string X { get; set; }
        //    public string Y { get; set; }







        //}

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}