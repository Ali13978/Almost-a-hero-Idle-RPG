using System;
using System.Collections.Generic;

public class RiftAutoPlayCheat
{
	public RiftAutoPlayCheat()
	{
		this.currentRiftId = 0;
		this.finishDurations = new List<float>();
		for (int i = 0; i < 120; i++)
		{
			this.finishDurations.Add(0f);
		}
		this.heroIds = new List<string>
		{
			"THOUR",
			"HORATIO",
			"BLIND_ARCHER",
			"SAM",
			"SHEELA"
		};
		this.state = RiftAutoPlayState.START;
	}

	public int currentRiftId;

	public List<float> finishDurations;

	public List<string> heroIds;

	public RiftAutoPlayState state;
}
