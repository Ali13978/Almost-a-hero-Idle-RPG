using System;
using SA.Common.Models;

namespace SA.IOSNative.StoreKit
{
	public class RestoreResult : Result
	{
		public RestoreResult(Error e) : base(e)
		{
		}

		public RestoreResult()
		{
		}

		public TransactionErrorCode TransactionErrorCode
		{
			get
			{
				if (this._Error != null)
				{
					return (TransactionErrorCode)this._Error.Code;
				}
				return TransactionErrorCode.SKErrorNone;
			}
		}
	}
}
