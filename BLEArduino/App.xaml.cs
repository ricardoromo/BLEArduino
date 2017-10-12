using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

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
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
