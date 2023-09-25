using System;

public class ISN_DeviceGUID
{
	public ISN_DeviceGUID(string data)
	{
		this._Base64 = data;
		this._Bytes = Convert.FromBase64String(data);
	}

	public string Base64String
	{
		get
		{
			return this._Base64;
		}
	}

	public byte[] Bytes
	{
		get
		{
			return this._Bytes;
		}
	}

	private byte[] _Bytes;

	private string _Base64;
}
