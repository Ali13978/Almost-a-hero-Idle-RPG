using System;

public class ISN_LocalReceiptResult
{
	public ISN_LocalReceiptResult(string data)
	{
		if (data.Length > 0)
		{
			this._Receipt = Convert.FromBase64String(data);
			this._ReceiptString = data;
		}
	}

	public byte[] Receipt
	{
		get
		{
			return this._Receipt;
		}
	}

	public string ReceiptString
	{
		get
		{
			return this._ReceiptString;
		}
	}

	private byte[] _Receipt;

	private string _ReceiptString = string.Empty;
}
