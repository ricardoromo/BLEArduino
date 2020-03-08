using System;
using BLEArduino.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BLEArduino
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new BLEArduinoPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
