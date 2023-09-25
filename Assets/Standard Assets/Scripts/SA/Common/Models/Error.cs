using System;

namespace SA.Common.Models
{
	public class Error
	{
		public Error()
		{
			this._Code = 0;
			this._Messgae = "Unknown Error";
		}

		public Error(int code, string message = "")
		{
			this._Code = code;
			this._Messgae = message;
		}

		public Error(string errorData)
		{
			string[] array = errorData.Split(new char[]
			{
				'|'
			});
			this._Code = Convert.ToInt32(array[0]);
			this._Messgae = array[1];
		}

		public int Code
		{
			get
			{
				return this._Code;
			}
		}

		public string Message
		{
			get
			{
				return this._Messgae;
			}
		}

		private int _Code;

		private string _Messgae = string.Empty;
	}
}
