using System;

public class ISN_TimeZone
{
	public ISN_TimeZone()
	{
	}

	public ISN_TimeZone(string data)
	{
		string[] array = data.Split(new char[]
		{
			'|'
		});
		this._Name = array[0];
		this._SecondsFromGMT = Convert.ToInt64(array[1]);
	}

	public string Name
	{
		get
		{
			return this._Name;
		}
	}

	public long SecondsFromGMT
	{
		get
		{
			return this._SecondsFromGMT;
		}
	}

	public static ISN_TimeZone LocalTimeZone
	{
		get
		{
			if (ISN_TimeZone._LocalTimeZone == null)
			{
				ISN_TimeZone._LocalTimeZone = new ISN_TimeZone();
			}
			return ISN_TimeZone._LocalTimeZone;
		}
	}

	private long _SecondsFromGMT = 7200L;

	private string _Name = "Europe/Kiev";

	private static ISN_TimeZone _LocalTimeZone;
}
