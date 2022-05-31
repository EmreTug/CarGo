using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static PratikKargo.MainPage;

namespace PratikKargo.Model
{
    public class StaticClass : INotifyPropertyChanged
    {

       

        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");

            }
        }


        private bool _isBusy1;

        public bool IsBusy1
        {
            get
            {
                return _isBusy1;
            }

            set
            {
                _isBusy1 = value;
                OnPropertyChanged("IsBusy1");

            }
        }

        private bool _isBusy2;

        public bool IsBusy2
        {
            get
            {
                return _isBusy2;
            }

            set
            {
                _isBusy2 = value;
                OnPropertyChanged("IsBusy2");

            }
        }

        private bool _isBusy3;

        public bool IsBusy3
        {
            get
            {
                return _isBusy3;
            }

            set
            {
                _isBusy3 = value;
                OnPropertyChanged("IsBusy3");

            }
        }

        private static int _nokta;

        public static int Nokta
        {
            get
            {
                return _nokta;
            }

            set
            {
                _nokta = value;

            }
        }
        static private ObservableCollection<CargoMain> _AllCargo;
        public static ObservableCollection<CargoMain> AllCargo
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
        private static ObservableCollection<CargoMain> _completed;
        public static ObservableCollection<CargoMain> Completed
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

        //private ctor so you need to use the Instance prop
        private StaticClass()
        {

        }

        private static StaticClass _instance;

        public static StaticClass Instance
        {
            get { return _instance ?? (_instance = new StaticClass()); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }



}
