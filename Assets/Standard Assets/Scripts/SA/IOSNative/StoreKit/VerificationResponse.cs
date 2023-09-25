using System;

namespace SA.IOSNative.StoreKit
{
	public class VerificationResponse
	{
		public VerificationResponse(string productIdentifier, string dataArray)
		{
			string[] array = dataArray.Split(new char[]
			{
				'|'
			});
			this._Status = Convert.ToInt32(array[0]);
			this._OriginalJSON = array[1];
			this._Receipt = array[2];
			this._ProductIdentifier = productIdentifier;
		}

		public int Status
		{
			get
			{
				return this._Status;
			}
		}

		public string Receipt
		{
			get
			{
				return this._Receipt;
			}
		}

		public string ProductIdentifier
		{
			get
			{
				return this._ProductIdentifier;
			}
		}

		public string OriginalJSON
		{
			get
			{
				return this._OriginalJSON;
			}
		}

		private int _Status;

		private string _Receipt;

		private string _ProductIdentifier;

		private string _OriginalJSON;
	}
}
