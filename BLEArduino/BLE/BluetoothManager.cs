using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace BLEArduino
{
	public class BluetoothManager
	{
		#region Singleton
		private static readonly Lazy<BluetoothManager> lazyBluetoothManager = new Lazy<BluetoothManager>(() => new BluetoothManager());
		public static BluetoothManager Instance
		{
			get { return lazyBluetoothManager.Value; }

		}
		#endregion

		public IBluetoothLE IBLE;
		public IAdapter AdapterBLE { get; set; }
		public IDevice BLEDevice { get; set; }



		public ObservableCollection<IDevice> DeviceList { get; set;}
		private bool ledStatus;
		private Guid ServiceGuid = Guid.Parse("713D0000-503E-4C75-BA94-3148F18D941E");
		public BluetoothManager()
		{
			IBLE = CrossBluetoothLE.Current;
			AdapterBLE = CrossBluetoothLE.Current.Adapter;
			DeviceList = new ObservableCollection<IDevice>();

			AdapterBLE.DeviceDiscovered += Adapter_DeviceDiscovered;
			AdapterBLE.DeviceConnected += Adapter_DeviceConnected;
			AdapterBLE.DeviceDisconnected += Adapter_DeviceDisconnected;
			AdapterBLE.ScanTimeoutElapsed += Adapter_ScanTimeoutElapsed;
		}

		public void StartScanning()
		{
			StartScanning(Guid.Empty);
		}

		void StartScanning(Guid forService)
		{
			if (AdapterBLE.IsScanning)
			{
				AdapterBLE.StopScanningForDevicesAsync();
				Debug.WriteLine("adapter.StopScanningForDevices()");
			}
			else
			{
				DeviceList.Clear();
				AdapterBLE.StartScanningForDevicesAsync();
				Debug.WriteLine("adapter.StartScanningForDevices(" + forService + ")");
			}
		}

		public async void StopScanning()
		{
			if (AdapterBLE.IsScanning)
			{
				Debug.WriteLine("Still scanning, stopping the scan");
				await AdapterBLE.StopScanningForDevicesAsync();
			}
		}

		void Adapter_DeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
		{
			DeviceList.Add(e.Device);
		}

		void Adapter_DeviceConnected(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
		{
			Debug.WriteLine("Device already connected");
		}

		void Adapter_DeviceDisconnected(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
		{
		//	DeviceDisconnectedEvent?.Invoke(sender,e);
			Debug.WriteLine("Device already disconnected");
		}

		void Adapter_ScanTimeoutElapsed(object sender, EventArgs e)
		{
			AdapterBLE.StopScanningForDevicesAsync();
			Debug.WriteLine("Timeout", "Bluetooth scan timeout elapsed");
		}

		public void DisconnectDevice()
		{
			if (BLEDevice != null)
			{
				AdapterBLE.DisconnectDeviceAsync(BLEDevice);
				BLEDevice.Dispose();
				BLEDevice = null;
			}
		}

		public async void WriteToBLE()
		{ 
			var services = await BLEDevice.GetServicesAsync();
			if (services == null)
				return;
			var characteristics = await services[0].GetCharacteristicsAsync();
			var characteristic = characteristics[0];

			if (characteristic != null)
			{
				if (characteristic.CanWrite)
				{
					byte[] buf = new byte[] { 0x01, 0x00 };
					if (ledStatus)
					{
						buf[1] = 0x01;
						await characteristic.WriteAsync(buf);
					}
					else
					{
						buf[1] = 0x00;
						await characteristic.WriteAsync(buf);
					}
					ledStatus = !ledStatus;
				}
			}
		
		}

	}
}
