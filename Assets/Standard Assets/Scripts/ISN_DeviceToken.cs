using System;

public class ISN_DeviceToken
{
	public ISN_DeviceToken(string base64String, string token)
	{
		this._tokenBytes = Convert.FromBase64String(base64String);
		this._tokenString = token;
	}

	public string DeviceId
	{
		get
		{
			return this._tokenString;
		}
	}

	public byte[] Bytes
	{
		get
		{
			return this._tokenBytes;
		}
	}

	public string TokenString
	{
		get
		{
			return this._tokenString;
		}
	}

	private string _tokenString;

	private byte[] _tokenBytes;
}
