using Xamarin.Forms;

namespace BLEArduino
{
	public partial class BLEArduinoPage : ContentPage
	{
		public BLEArduinoPage()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (BluetoothManager.Instance.BLEDevice == null)
			{
				Navigation.PushAsync(new DeviceListPage());
			}
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}

		void Led_Action_Clicked(object sender, System.EventArgs e)
		{
			BluetoothManager.Instance.WriteToBLE();
		}

		void Disconnect_Device_Clicked(object sender, System.EventArgs e)
		{
			BluetoothManager.Instance.DisconnectDevice();
			Navigation.PushAsync(new DeviceListPage());
		}

		void Instance_DeviceDisconnectedEvent(object sender, System.EventArgs e)
		{
			if (BluetoothManager.Instance.BLEDevice == null)
			{
				Navigation.PushAsync(new DeviceListPage());
			}
		}
	}
}
