using System;

public class ISN_Build
{
	public ISN_Build()
	{
	}

	public ISN_Build(string data)
	{
		string[] array = data.Split(new char[]
		{
			'|'
		});
		this._Version = array[0];
		string value = array[1].Trim();
		if (string.IsNullOrEmpty(value))
		{
			this._Number = 1;
		}
		else
		{
			this._Number = Convert.ToInt32(value);
		}
	}

	public string Version
	{
		get
		{
			return this._Version;
		}
	}

	public int Number
	{
		get
		{
			return this._Number;
		}
	}

	public static ISN_Build Current
	{
		get
		{
			if (ISN_Build._Current == null)
			{
				ISN_Build._Current = new ISN_Build();
			}
			return ISN_Build._Current;
		}
	}

	private string _Version = "1.0";

	private int _Number = 1;

	private static ISN_Build _Current;
}
