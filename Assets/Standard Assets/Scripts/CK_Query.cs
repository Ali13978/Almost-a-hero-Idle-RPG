using System;

public class CK_Query
{
	public CK_Query(string predicate, string recordType)
	{
		this._Predicate = predicate;
		this._RecordType = recordType;
	}

	public string Predicate
	{
		get
		{
			return this._Predicate;
		}
	}

	public string RecordType
	{
		get
		{
			return this._RecordType;
		}
	}

	private string _Predicate;

	private string _RecordType;
}
