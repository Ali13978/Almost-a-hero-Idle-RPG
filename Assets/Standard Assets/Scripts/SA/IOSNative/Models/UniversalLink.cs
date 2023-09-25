using System;

namespace SA.IOSNative.Models
{
	public class UniversalLink
	{
		public UniversalLink(string absoluteUrl)
		{
			this._AbsoluteUrl = absoluteUrl;
			if (this._AbsoluteUrl.Length > 0)
			{
				this._URI = new Uri(this._AbsoluteUrl);
			}
		}

		public bool IsEmpty
		{
			get
			{
				return this._AbsoluteUrl.Equals(string.Empty);
			}
		}

		public Uri URI
		{
			get
			{
				return this._URI;
			}
		}

		public string Host
		{
			get
			{
				return this._URI.Host;
			}
		}

		public string AbsoluteUrl
		{
			get
			{
				return this._AbsoluteUrl;
			}
		}

		private Uri _URI;

		private string _AbsoluteUrl = string.Empty;
	}
}
