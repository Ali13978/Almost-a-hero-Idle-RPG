using System;

namespace SA.IOSDeploy
{
	[Serializable]
	public class Lib
	{
		public Lib(iOSLibrary lib)
		{
			this.Type = lib;
		}

		public string Name
		{
			get
			{
				return ISD_LibHandler.stringValueOf(this.Type);
			}
		}

		public bool IsOpen;

		public iOSLibrary Type;

		public bool IsOptional;
	}
}
