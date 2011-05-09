using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HidLib.SampleApp
{
	public partial class MainForm : Form, IDeviceListener
	{
		public MainForm()
		{
			InitializeComponent();

			Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

			UsbIRRT.Singleton.DeviceListener = this;
		}

		void Application_ApplicationExit(object sender, EventArgs e)
		{
			// free up UsbIRRT resources
			UsbIRRT.DisposeSingleton();
		}

		// IDeviceListener.DataRead
		public void DataRead(DecodedData data)
		{
			// TODO: do something with the decoded data
		}

		private void btnConfig_Click(object sender, EventArgs e)
		{
			ConfigForm configForm = new ConfigForm(this);
			configForm.ShowDialog();
		}
	}
}
