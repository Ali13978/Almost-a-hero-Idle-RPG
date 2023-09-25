using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Tapdaq
{
	[Serializable]
	public class TestDevicesList
	{
		public TestDevicesList(List<TestDevice> devices, TestDeviceType deviceType)
		{
			foreach (TestDevice testDevice in devices)
			{
				if (testDevice.type == deviceType)
				{
					if (!string.IsNullOrEmpty(testDevice.adMobId))
					{
						this.adMobDevices.Add(testDevice.adMobId);
					}
					if (!string.IsNullOrEmpty(testDevice.facebookId))
					{
						this.facebookDevices.Add(testDevice.facebookId);
					}
				}
			}
		}

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}

		public string GetAdMobListJson()
		{
			return JsonConvert.SerializeObject(this.adMobDevices);
		}

		public string GetFacebookListJson()
		{
			return JsonConvert.SerializeObject(this.facebookDevices);
		}

		[SerializeField]
		public List<string> adMobDevices = new List<string>();

		[SerializeField]
		public List<string> facebookDevices = new List<string>();
	}
}
