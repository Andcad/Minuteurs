using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Minuteurs.Models
{
    internal class Event : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _EventTitle;
        private string _Description;
        private TimeSpan _Timespan;
        private string _StateBgColor;
        private string _Seconds;
        private string _Minutes;
        private string _Hours;
        private bool _IsTimerRunning;
        private string _BgColor;
        private DateTime _Date;
        private string _Id;
        private bool _IsReset;
        private string _RememberHours;
        private string _RememberMinutes;

        public string Id
        {
            get { return _Id; }
            set { SetProperty(ref _Id, value); }
        }

        public DateTime Date
        {
            get { return _Date; }
            set { SetProperty(ref _Date, value); }
        }

        public string BgColor
        {
            get { return _BgColor; }
            set { SetProperty(ref _BgColor, value); }
        }

        public bool IsTimerRunning
        {
            get { return _IsTimerRunning; }
            set { SetProperty(ref _IsTimerRunning, value); }
        }

        public string Hours
        {
            get { return _Hours; }
            set { SetProperty(ref _Hours, value); }
        }

        public string Minutes
        {
            get { return _Minutes; }
            set { SetProperty(ref _Minutes, value); }
        }

        public string Seconds
        {
            get { return _Seconds; }
            set { SetProperty(ref _Seconds, value); }
        }

        public string StateBgColor
        {
            get { return _StateBgColor; }
            set { SetProperty(ref _StateBgColor, value); }
        }
        public bool IsReset
        {
            get { return _IsReset; }
            set { SetProperty(ref _IsReset, value); }
        }
        public string RememberHours
        {
            get { return _RememberHours; }
            set { SetProperty(ref _RememberHours, value); }
        }
        public string RememberMinutes
        {
            get { return _RememberMinutes; }
            set { SetProperty(ref _RememberMinutes, value); }
        }

        public TimeSpan Timespan
        {
            get { return _Timespan; }
            set { SetProperty(ref _Timespan, value); }
        }
        public string Description
        {
            get { return _Description; }
            set { SetProperty(ref _Description, value); }
        }

        public string EventTitle
        {
            get { return _EventTitle; }
            set { SetProperty(ref _EventTitle, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
