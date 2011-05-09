using System;

namespace HidLib.SampleApp
{
	public interface IDeviceListener
	{
		void DataRead(DecodedData data);
	}
}
