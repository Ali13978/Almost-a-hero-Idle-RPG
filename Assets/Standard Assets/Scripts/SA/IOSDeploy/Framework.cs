using System;
using System.Collections.Generic;
using SA.Common.Util;

namespace SA.IOSDeploy
{
	[Serializable]
	public class Framework
	{
		public Framework(iOSFramework type, bool Embaded = false)
		{
			this.Type = type;
			this.IsEmbeded = Embaded;
		}

		public Framework(string frameworkName)
		{
			frameworkName = frameworkName.Replace(".framework", string.Empty);
			this.Type = General.ParseEnum<iOSFramework>(frameworkName);
		}

		public string Name
		{
			get
			{
				return this.Type.ToString() + ".framework";
			}
		}

		public int[] BaseIndexes()
		{
			string[] array = ISD_FrameworkHandler.BaseFrameworksArray();
			List<int> list = new List<int>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(i);
			}
			return list.ToArray();
		}

		public string TypeString
		{
			get
			{
				return this.Type.ToString();
			}
		}

		public bool IsOpen;

		public iOSFramework Type;

		public bool IsOptional;

		public bool IsEmbeded;
	}
}
