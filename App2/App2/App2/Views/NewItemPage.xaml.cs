using Minuteurs.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Minuteurs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        private Event MyEvent { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            DateTime Date = new DateTime(2021, 1, 1, 0, 5, 0);

            MyEvent = new Event()
            {
                Id = Guid.NewGuid().ToString(),
                EventTitle = "test",
                Description = "tests",
                BgColor = "Red",
                Date = Date,
                Seconds = Date.Second.ToString("00"),
                Minutes = Date.Minute.ToString("00"),
                Hours = Date.Hour.ToString("00"),
                StateBgColor = "#F8F8F8",
                IsTimerRunning = false,
                IsReset = false,
                IsAudioPlaying = false
        };

            BindingContext = MyEvent;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MainPage.EventToAdd = MyEvent;
            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}