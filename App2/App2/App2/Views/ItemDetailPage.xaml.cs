using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
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
            SelectedEvent.Date = new DateTime(2021, 1, 1, iRememberHours, iRememberMinutes, 0);
            SelectedEvent.Seconds = SelectedEvent.Date.Second.ToString("00");
            SelectedEvent.Minutes = SelectedEvent.Date.Minute.ToString("00");
            SelectedEvent.Hours = SelectedEvent.Date.Hour.ToString("00");
            SelectedEvent.IsReset = true;
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            MainPage._EventToDelete = SelectedEvent;
            await Navigation.PopAsync();
        }
    }
}