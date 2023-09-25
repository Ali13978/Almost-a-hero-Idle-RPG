using System;

namespace SA.IOSNative.Models
{
	public class LaunchUrl
	{
		public LaunchUrl(string data)
		{
			string[] array = data.Split(new char[]
			{
				'|'
			});
			this._AbsoluteUrl = array[0];
			this._SourceApplication = array[1];
			if (this._AbsoluteUrl.Length > 0)
			{
				this._URI = new Uri(this._AbsoluteUrl);
			}
		}

		public LaunchUrl(string absoluteUrl, string sourceApplication)
		{
			this._AbsoluteUrl = absoluteUrl;
			this._SourceApplication = sourceApplication;
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

		public string SourceApplication
		{
			get
			{
				return this._SourceApplication;
			}
		}

		private Uri _URI;

		private string _AbsoluteUrl = string.Empty;

		private string _SourceApplication = string.Empty;
	}
}
