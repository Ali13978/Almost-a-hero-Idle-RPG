using System;
using System.Collections.Generic;
using SA.Common.Models;

public class GK_PlayerSignatureResult : Result
{
	public GK_PlayerSignatureResult(string publicKeyUrl, string signature, string salt, string timestamp)
	{
		this._PublicKeyUrl = publicKeyUrl;
		string[] array = signature.Split(new char[]
		{
			','
		});
		List<byte> list = new List<byte>();
		foreach (string value in array)
		{
			list.Add(Convert.ToByte(value));
		}
		this._Signature = list.ToArray();
		array = salt.Split(new char[]
		{
			','
		});
		list = new List<byte>();
		foreach (string value2 in array)
		{
			list.Add(Convert.ToByte(value2));
		}
		this._Salt = list.ToArray();
		this._Timestamp = Convert.ToInt64(timestamp);
	}

	public GK_PlayerSignatureResult(Error errro) : base(errro)
	{
	}

	public string PublicKeyUrl
	{
		get
		{
			return this._PublicKeyUrl;
		}
	}

	public byte[] Signature
	{
		get
		{
			return this._Signature;
		}
	}

	public byte[] Salt
	{
		get
		{
			return this._Salt;
		}
	}

	public long Timestamp
	{
		get
		{
			return this._Timestamp;
		}
	}

	public string _PublicKeyUrl;

	public byte[] _Signature;

	public byte[] _Salt;

	public long _Timestamp;
}
