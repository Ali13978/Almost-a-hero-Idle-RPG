using System;
using System.Collections.Generic;

public class AdventureAutoPlayCheat
{
	public AdventureAutoPlayCheat()
	{
		this.timePassed = 0f;
		this.prestigedAt = new List<int>();
	}

	public float stateTimer;

	public int numPrestige;

	public int maxStageInCurrentRun;

	public float timeStuckInStage;

	public float timePassed;

	public List<int> prestigedAt;
}
