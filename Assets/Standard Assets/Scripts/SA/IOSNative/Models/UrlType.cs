using System;
using System.Collections.Generic;

namespace SA.IOSNative.Models
{
	[Serializable]
	public class UrlType
	{
		public UrlType(string identifier)
		{
			this.Identifier = identifier;
		}

		public void AddSchemes(string schemes)
		{
			this.Schemes.Add(schemes);
		}

		public string Identifier = string.Empty;

		public List<string> Schemes = new List<string>();

		public bool IsOpen = true;
	}
}
