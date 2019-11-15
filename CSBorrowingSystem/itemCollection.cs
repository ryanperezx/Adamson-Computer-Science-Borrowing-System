﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CSBorrowingSystem
{
    class itemCollection : INotifyPropertyChanged
    {
        private bool _isReturned;
        public bool isReturned
        {
            get { return _isReturned; }
            set { _isReturned = value; OnPropertyChanged("isReturned"); }
        }
        public string fullName
        {
            get;
            set;
        }
        public string itemName
        {
            get;
            set;
        }
        public string brand
        {
            get;
            set;
        }
        public string subject
        {
            get;
            set;
        }
        public string schedule
        {
            get;
            set;
        }
        public string dateBorrowed
        {
            get;
            set;
        }
        public string status
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
