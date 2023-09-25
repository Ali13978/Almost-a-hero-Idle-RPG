using System;

public struct IndexChangePair
{
	public bool IsFullChange()
	{
		return this.from >= 0 && this.to >= 0;
	}

	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"From: ",
			this.from,
			", To: ",
			this.to
		});
	}

	public int from;

	public int to;
}
