using App2.Models;
using App2.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        private Event _SelectedEvent;
        internal static Event _EventToDelete { get; set; }
        internal static Event _EventToAdd { get; set; }

        private ObservableCollection<Event> _Events { get; set; }

        public MainPage()
        {
            InitializeComponent();

            _Events = GetEvents();

            listView.ItemsSource = _Events;

           StartTimer();
        }

        private ObservableCollection<Event> GetEvents()
        {
            return new ObservableCollection<Event>
            {
                new Event{ Id ="1", EventTitle = "Pain Brun", Description="Brun. En harmonie avec la nature...", IsTimerRunning = true, BgColor = "#EB9999", StateBgColor = "#F8F8F8", Date = new DateTime(2021,1,1,1,12,0), RememberHours = "0", RememberMinutes="2", IsReset=false},
                new Event{ Id ="2", EventTitle = "Galettes", Description="Généralement de forme ronde", IsTimerRunning = true, BgColor = "#96338F", StateBgColor = "#F8F8F8", Date = new DateTime(2021,1,1,0,6,0), RememberHours = "1", RememberMinutes="22", IsReset=false},
                new Event{ Id ="3", EventTitle = "Pain George", Description="Blanc. Le meilleur pain en ville!!", IsTimerRunning = false, BgColor = "#8251AE", StateBgColor = "#F8F8F8", Date = new DateTime(2021,1,1,1,44,0), RememberHours = "11", RememberMinutes="15", IsReset=false},
                new Event{ Id ="4", EventTitle = "Pain George", Description="Blanc. Le meilleur pain en ville!!", IsTimerRunning = false, BgColor = "#7241AE", StateBgColor = "#F8F8F8", Date = new DateTime(2021,1,1,1,44,0), RememberHours = "11", RememberMinutes="15", IsReset=false},
                new Event{ Id ="5", EventTitle = "Pain George", Description="Blanc. Le meilleur pain en ville!!", IsTimerRunning = false, BgColor = "#6231AE", StateBgColor = "#F8F8F8", Date = new DateTime(2021,1,1,1,44,0), RememberHours = "11", RememberMinutes="15", IsReset=false},
                new Event{ Id ="6", EventTitle = "Pain George", Description="Blanc. Le meilleur pain en ville!!", IsTimerRunning = false, BgColor = "#5221AE", StateBgColor = "#F8F8F8", Date = new DateTime(2021,1,1,1,44,0), RememberHours = "11", RememberMinutes="15", IsReset=false},
                new Event{ Id ="7", EventTitle = "Pain George", Description="Blanc. Le meilleur pain en ville!!", IsTimerRunning = false, BgColor = "#4211AE", StateBgColor = "#F8F8F8", Date = new DateTime(2021,1,1,1,44,0), RememberHours = "11", RememberMinutes="15", IsReset=false},
                new Event{ Id ="8", EventTitle = "Pain George", Description="Blanc. Le meilleur pain en ville!!", IsTimerRunning = false, BgColor = "#3251AE", StateBgColor = "#F8F8F8", Date = new DateTime(2021,1,1,1,44,0), RememberHours = "11", RememberMinutes="15", IsReset=false},
                new Event{ Id ="9", EventTitle = "Pain George", Description="Blanc. Le meilleur pain en ville!!", IsTimerRunning = false, BgColor = "#2251AE", StateBgColor = "#F8F8F8", Date = new DateTime(2021,1,1,1,44,0), RememberHours = "11", RememberMinutes="15", IsReset=false},
                new Event{ Id ="10", EventTitle = "Pain George", Description="Blanc. Le meilleur pain en ville!!", IsTimerRunning = false, BgColor = "#1251AE", StateBgColor = "#F8F8F8", Date = new DateTime(2021,1,1,1,44,0), RememberHours = "11", RememberMinutes="15", IsReset=false},
                new Event{ Id ="11", EventTitle = "Baguettes", Description="Longue. À essayer avec du fromage", IsTimerRunning = true, BgColor = "#6787FF", StateBgColor = "#F8F8F8", Date = new DateTime(2021,1,1,1,44,0), RememberHours = "17", RememberMinutes="29", IsReset=false},
            };
        }

        private void StartTimer()
        {
            var _timer = new Timer
            {
                //Trigger event every second
                Interval = 1000
            };
            _timer.Elapsed += TimerCallBack;
            _timer.Start();
        }

        private void TimerCallBack(object sender, ElapsedEventArgs e)
        {
            foreach (var evt in _Events)
            {
                if (evt.IsReset)
                {
                    int.TryParse(evt.RememberHours, out int iRememberHours);
                    int.TryParse(evt.RememberMinutes, out int iRememberMinutes);
                    evt.Date = new DateTime(2021, 1, 1, iRememberHours, iRememberMinutes, 0);
                    evt.Seconds = evt.Date.Second.ToString("00");
                    evt.Minutes = evt.Date.Minute.ToString("00");
                    evt.Hours = evt.Date.Hour.ToString("00");
                    evt.IsReset = false;
                }

                if (evt.IsTimerRunning)
                {
                    evt.Date = evt.Date.AddSeconds(-1);
                    evt.Seconds = evt.Date.Second.ToString("00");
                    evt.Minutes = evt.Date.Minute.ToString("00");
                    evt.Hours = evt.Date.Hour.ToString("00");
                    evt.StateBgColor = "#33cc33";
                }
                else
                {
                    evt.StateBgColor = "#F8F8F8";
                }
            }
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        private async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                _SelectedEvent = (e.CurrentSelection.FirstOrDefault() as Event);
                await Navigation.PushAsync(new ItemDetailPage(_SelectedEvent));
            }
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_EventToDelete != null)
            {
                _Events.Remove(_EventToDelete);
                _EventToDelete = null;
            }

            if (_EventToAdd != null)
            {
                _EventToAdd.RememberHours = _EventToAdd.Hours;
                _EventToAdd.RememberMinutes = _EventToAdd.Minutes;
                _Events.Add(_EventToAdd);
                _EventToAdd = null;
            }

            if (listView.SelectedItem != null)
            {
                listView.SelectedItem = null;
                _SelectedEvent = null;
            }
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var MyEvent = button.CommandParameter as Event;

            int.TryParse(MyEvent.RememberHours, out int iRememberHours);
            int.TryParse(MyEvent.RememberMinutes, out int iRememberMinutes);
            MyEvent.Date = new DateTime(2021, 1, 1, iRememberHours, iRememberMinutes, 0);
            MyEvent.Seconds = MyEvent.Date.Second.ToString("00");
            MyEvent.Minutes = MyEvent.Date.Minute.ToString("00");
            MyEvent.Hours = MyEvent.Date.Hour.ToString("00");
        }
    }
}
