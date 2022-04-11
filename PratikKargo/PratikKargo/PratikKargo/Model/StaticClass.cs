using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
