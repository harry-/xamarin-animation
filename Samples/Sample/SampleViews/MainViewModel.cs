using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Sample.SampleViews
{

    public class MainViewModel : INotifyPropertyChanged
    {
        #region Members
        private double _X;
        private bool _Gravity;
        #endregion Members

        #region Properties
        public double X
        {
            get
            {
                return _X;
            }
            set
            {
                _X = value;
                Debug.WriteLine("new X:" + _X);
                NotifyPropertyChanged("X");
            }
        }
        public bool Gravity
        {
            get { return _Gravity; }
            set
            {
                _Gravity = value;
                NotifyPropertyChanged("Gravity");
            }
        }

        #endregion Properties

        public MainViewModel()
        {
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}