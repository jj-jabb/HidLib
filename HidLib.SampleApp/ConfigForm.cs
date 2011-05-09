using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HidLib.SampleApp
{
	public partial class ConfigForm : Form, IDeviceListener
	{
		MainForm parentForm;
		public ConfigForm(MainForm parentForm)
		{
			InitializeComponent();

			this.parentForm = parentForm;
			UsbIRRT.Singleton.DeviceListener = this;
		}

		private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			UsbIRRT.Singleton.DeviceListener = parentForm;
		}

		public void DataRead(DecodedData data)
		{
			// TODO: Do something with the data (or simply return)
		}
	}
}
