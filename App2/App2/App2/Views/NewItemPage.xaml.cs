using App2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        private Event MyEvent { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            DateTime Date = new DateTime(2021, 1, 1, 2, 3, 0);

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
                IsReset = false
            };

            BindingContext = MyEvent;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MainPage._EventToAdd = MyEvent;
            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}