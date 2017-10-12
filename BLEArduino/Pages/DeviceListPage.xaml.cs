using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using Xamarin.Forms;

namespace BLEArduino
{
	public partial class DeviceListPage : ContentPage
	{
		public DeviceListPage()
		{
			this.BindingContext = BluetoothManager.Instance.DeviceList;
			InitializeComponent();
		}


		protected async override void OnAppearing()
		{
			base.OnAppearing();
			BluetoothManager.Instance.StartScanning();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}

		async void Device_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			BluetoothManager.Instance.StopScanning();
			try
			{
				BluetoothManager.Instance.BLEDevice = e.Item as IDevice;
				var device = e.Item as IDevice;
				if (BluetoothManager.Instance.AdapterBLE.ConnectedDevices.Count == 0)
				{
					await BluetoothManager.Instance.AdapterBLE.ConnectToDeviceAsync(device);
					await Navigation.PopAsync();
				}
				else
				{
					await BluetoothManager.Instance.AdapterBLE.DisconnectDeviceAsync(device);
				}
			}
			catch (DeviceConnectionException ex)
			{
				await DisplayAlert("Error", "Could not connect to :" + ex.DeviceId, "OK");
			}

		}
	}
}
