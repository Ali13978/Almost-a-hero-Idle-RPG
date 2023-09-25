using System;

public class ReplayKitVideoShareResult
{
	public ReplayKitVideoShareResult(string[] sourcesArray)
	{
		this._Sources = sourcesArray;
	}

	public string[] Sources
	{
		get
		{
			return this._Sources;
		}
	}

	private string[] _Sources = new string[0];
}
