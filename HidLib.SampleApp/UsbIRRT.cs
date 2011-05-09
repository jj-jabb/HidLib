using System;
using System.Collections.Generic;
using System.Text;

using HidLib;

namespace HidLib.SampleApp
{
	public class UsbIRRT : Device
	{
		public enum ReportID : byte
		{
			LastReceivedIRCode = 1,
			ReadPowerOnEnabled = 2,
			ReadTrainendIRCode = 3,
			SetPowerOnEnabled = 4,
			SetTrainedIRCode = 5,
			SetIRPollingTime = 6,
			ReadIrmpVersion = 7,
			SetMinRepeats = 8,
			RequestBootloader = 9
		}

		object listenerChangeLock = new object();
		IDeviceListener _deviceListener;
		IDeviceListener deviceListener
		{
			get
			{
				// don't allow the listener to be changed while getting
				// a reference to it
				IDeviceListener listener;
				lock (listenerChangeLock)
				{
					listener = _deviceListener;
				}
				return listener;
			}
			set
			{
				// don't allow the listener to be accessed while being
				// changed
				if (_deviceListener != value)
					lock (listenerChangeLock)
					{
						_deviceListener = value;
					}
			}
		}

		public IDeviceListener DeviceListener
		{
			get { return deviceListener; }
			set { deviceListener = value; }
		}

		private static UsbIRRT singleton;
		public static UsbIRRT Singleton
		{
			get
			{
				if (singleton == null)
					singleton = new UsbIRRT();

				return singleton;
			}
		}

		public static void DisposeSingleton()
		{
			if (singleton != null)
			{
				singleton.Dispose();
				singleton = null;
			}
		}

		private UsbIRRT()
			: base(0x16c0, 0x05df)
		{
		}

		public string ReadIRMPVersion()
		{
			Byte[] buffer = new Byte[12];
			ReadFeatureReport((byte)ReportID.ReadIrmpVersion, buffer);
			return System.Text.ASCIIEncoding.ASCII.GetString(buffer, 1, 11);
		}

		///  <summary>
		///  Only for testing purposes.
		///  </summary>
		private void testCom()
		{
			Boolean success;
			Byte[] buffer = new Byte[Capabilities.FeatureReportByteLength];
			success = ReadFeatureReport(2, buffer);
			success = ReadFeatureReport(3, buffer);
			success = ReadFeatureReport(7, buffer);

			buffer = new Byte[Capabilities.FeatureReportByteLength];
			buffer[1] = 1;
			success = WriteFeatureReport(4, buffer);
			//buffer[1] = 255;
			//success = device.WriteFeatureReport(5, buffer);
			buffer[2] = 80;
			success = WriteFeatureReport(6, buffer);
			buffer[1] = 15;
			success = WriteFeatureReport(8, buffer);
		}

		protected override void ReadThread_DataRead(DeviceData data)
		{
			// decode device data here
			// cache the listener so we only go through the lock once
			IDeviceListener listener = deviceListener;
			deviceListener.DataRead(new DecodedData(data));
		}

		protected override void ReadThread_ReadError(Exception error)
		{
			// default implementation write the error to the debug window (VS Output window)
			base.ReadThread_ReadError(error);
		}
	}
}
