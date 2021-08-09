using Minuteurs.Models;
using Minuteurs.Views;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Timers;
using Xamarin.Forms;
using System.Text.Json;
using System.Diagnostics;

namespace Minuteurs
{
    public partial class MainPage : ContentPage
    {
        private Event _SelectedEvent;
        internal static Event EventToDelete { get; set; }
        internal static Event EventToAdd { get; set; }

        private ObservableCollection<Event> EventsList { get; set; }

        public MainPage()
        {
            InitializeComponent();

            //EventsList = GetEvents();
            EventsList = GetEvents_FileLoadData();

            listView.ItemsSource = EventsList;

           StartTimer();

        }

        private ObservableCollection<Event> GetEvents_FileLoadData()
        {
            string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TimerManager_Data.txt");

            if (File.Exists(_fileName))
            {
                string AllText = File.ReadAllText(_fileName);
                try
                {
                    return JsonSerializer.Deserialize<ObservableCollection<Event>>(AllText);
                }
                catch
                {
                    Debug.WriteLine("Erreur GetEvents_FileLoadData création d'un fichier vierge");
                    return new ObservableCollection<Event>();
                }
            }
            else
            {
                return new ObservableCollection<Event>();
            }
        }

        private async void FileSaveData(ObservableCollection<Event> events)
        {
            string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TimerManager_Data.txt");
            
            using (FileStream createStream = File.Create(_fileName))
            {
                await JsonSerializer.SerializeAsync(createStream, events);
                createStream.Dispose();
            }
            Debug.WriteLine(File.ReadAllText(_fileName));
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
            foreach (var evt in EventsList)
            {
                if (evt.IsReset)
                {
                    int.TryParse(evt.RememberHours, out int iRememberHours);
                    int.TryParse(evt.RememberMinutes, out int iRememberMinutes);
                    int.TryParse(evt.RememberSeconds, out int iRememberSeconds);

                    evt.Date = new DateTime(2021, 1, 1, iRememberHours, iRememberMinutes, iRememberSeconds);
                    evt.Hours = evt.Date.Hour.ToString("00");
                    evt.Minutes = evt.Date.Minute.ToString("00");
                    evt.Seconds = evt.Date.Second.ToString("00");
                    evt.IsReset = false;
                }

                if (evt.IsTimerRunning)
                {
                    evt.Date = evt.Date.AddSeconds(-1);
                    evt.Hours = evt.Date.Hour.ToString("00");
                    evt.Minutes = evt.Date.Minute.ToString("00");
                    evt.Seconds = evt.Date.Second.ToString("00");
                    evt.StateBgColor = "#33cc33";

                    if (evt.Hours == "00" && evt.Minutes == "00" && evt.Seconds == "00" && evt.IsAudioPlaying == false)
                    {
                        evt.IsAudioPlaying = true;
                        evt.Timespan = 5;

                        DependencyService.Get<IAudioService>().PlayAudioFile("Kitchen_Timer_Sound_Effect.mp3");
                    }
                    else if (evt.IsAudioPlaying)
                    {
                        if (evt.Timespan == 0)
                        {
                            evt.Timespan = 5;
                            DependencyService.Get<IAudioService>().PlayAudioFile("Kitchen_Timer_Sound_Effect.mp3");
                            GC.Collect();
                        }
                        else
                        {
                            evt.Timespan--;
                        }
                    }
                }
                else
                {
                    evt.StateBgColor = "#F8F8F8";
                    evt.IsTimerRunning = false;

                    if (evt.IsAudioPlaying)
                    {
                        evt.IsAudioPlaying = false;
                        GC.Collect();
                    }
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

            if (EventToDelete != null)
            {
                EventsList.Remove(EventToDelete);
                EventToDelete = null;
            }

            if (EventToAdd != null)
            {
                int.TryParse(EventToAdd.Hours, out int iRememberHours);
                int.TryParse(EventToAdd.Minutes, out int iRememberMinutes);
                int.TryParse(EventToAdd.Seconds, out int iRememberSeconds);
                EventToAdd.Date = new DateTime(2021, 1, 1, iRememberHours, iRememberMinutes, iRememberSeconds);

                EventToAdd.RememberHours = EventToAdd.Hours;
                EventToAdd.RememberMinutes = EventToAdd.Minutes;
                EventToAdd.RememberSeconds = EventToAdd.Seconds;
                EventsList.Add(EventToAdd);
                EventToAdd = null;
            }

            if (listView.SelectedItem != null)
            {
                listView.SelectedItem = null;
                _SelectedEvent = null;
            }

            FileSaveData(EventsList);
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var MyEvent = button.CommandParameter as Event;

            int.TryParse(MyEvent.RememberHours, out int iRememberHours);
            int.TryParse(MyEvent.RememberMinutes, out int iRememberMinutes);
            int.TryParse(MyEvent.RememberSeconds, out int iRememberSeconds);

            MyEvent.Date = new DateTime(2021, 1, 1, iRememberHours, iRememberMinutes, iRememberSeconds);
            MyEvent.Seconds = MyEvent.Date.Second.ToString("00");
            MyEvent.Minutes = MyEvent.Date.Minute.ToString("00");
            MyEvent.Hours = MyEvent.Date.Hour.ToString("00");
        }

        private void MoveItemBottom_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var MyEvent = button.CommandParameter as Event;

            int index = EventsList.IndexOf(MyEvent);

            if (EventsList.Count > index + 1)
            {
                EventsList.RemoveAt(index);
                EventsList.Insert(index + 1, MyEvent);
            }
        }

        private void MoveItemTop_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var MyEvent = button.CommandParameter as Event;

            int index = EventsList.IndexOf(MyEvent);

            if (index > 0)
            {
                EventsList.RemoveAt(index);
                EventsList.Insert(index - 1, MyEvent);
            }
        }
    }

    public interface IAudioService
    {
        void PlayAudioFile(string filename);
    }
}
