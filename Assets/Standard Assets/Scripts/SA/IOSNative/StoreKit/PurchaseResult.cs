using System;
using SA.Common.Models;

namespace SA.IOSNative.StoreKit
{
	public class PurchaseResult : Result
	{
		public PurchaseResult(string productIdentifier, Error e) : base(e)
		{
			this._ProductIdentifier = productIdentifier;
			this._State = PurchaseState.Failed;
		}

		public PurchaseResult(string productIdentifier, PurchaseState state, string applicationUsername = "", string receipt = "", string transactionIdentifier = "")
		{
			this._ProductIdentifier = productIdentifier;
			this._State = state;
			this._Receipt = receipt;
			this._TransactionIdentifier = transactionIdentifier;
			this._ApplicationUsername = applicationUsername;
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

		public PurchaseState State
		{
			get
			{
				return this._State;
			}
		}

		public string ProductIdentifier
		{
			get
			{
				return this._ProductIdentifier;
			}
		}

		public string ApplicationUsername
		{
			get
			{
				return this._ApplicationUsername;
			}
		}

		public string Receipt
		{
			get
			{
				return this._Receipt;
			}
		}

		public string TransactionIdentifier
		{
			get
			{
				return this._TransactionIdentifier;
			}
		}

		private string _ProductIdentifier = string.Empty;

		private PurchaseState _State = PurchaseState.Failed;

		private string _Receipt = string.Empty;

		private string _TransactionIdentifier = string.Empty;

		private string _ApplicationUsername = string.Empty;
	}
}
