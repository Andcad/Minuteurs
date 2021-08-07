using System;
using Minuteurs.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Minuteurs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        private Event SelectedEvent { get; set; }

        public ItemDetailPage()
        {
            InitializeComponent();
        }

        internal ItemDetailPage(Event selectedEvent)
        {
            InitializeComponent();

            SelectedEvent = selectedEvent;

            BindingContext = SelectedEvent;
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            int.TryParse(SelectedEvent.RememberHours, out int iRememberHours);
            int.TryParse(SelectedEvent.RememberMinutes, out int iRememberMinutes);
            int.TryParse(SelectedEvent.RememberSeconds, out int iRememberSeconds);

            SelectedEvent.Date = new DateTime(2021, 1, 1, iRememberHours, iRememberMinutes, iRememberSeconds);
            SelectedEvent.Seconds = SelectedEvent.Date.Second.ToString("00");
            SelectedEvent.Minutes = SelectedEvent.Date.Minute.ToString("00");
            SelectedEvent.Hours = SelectedEvent.Date.Hour.ToString("00");
            SelectedEvent.IsReset = true;
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            MainPage.EventToDelete = SelectedEvent;
            await Navigation.PopAsync();
        }
    }
}