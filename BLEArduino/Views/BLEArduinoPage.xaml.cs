using System;
using System.Collections.Generic;

using BLEArduino.BLE;
using Xamarin.Forms;

namespace BLEArduino.Views
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

        private async void Led_Action_Clicked(object sender, System.EventArgs e)
        {
            await BluetoothManager.Instance.WriteToBLE();
        }

        private void Disconnect_Device_Clicked(object sender, System.EventArgs e)
        {
            BluetoothManager.Instance.DisconnectDevice();
            Navigation.PushAsync(new DeviceListPage());
        }

        private void Instance_DeviceDisconnectedEvent(object sender, System.EventArgs e)
        {
            if (BluetoothManager.Instance.BLEDevice == null)
            {
                Navigation.PushAsync(new DeviceListPage());
            }
        }

    }
}
