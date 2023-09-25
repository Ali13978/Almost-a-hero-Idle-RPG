using System;
using Simulation;

public class TutorialMissionTap : TutorialMission
{
	public TutorialMissionTap(int targetTaps, CurrencyType rewardCurrency, double rewardAmount) : base(rewardCurrency, rewardAmount)
	{
		this.targetTaps = targetTaps;
	}

	public override string GetDescription()
	{
		return string.Format(LM.Get("TUTORIAL_MISSION_TAP"), this.targetTaps);
	}

	protected override void OnUpdate(Simulator simulator, Taps taps)
	{
		if (taps == null)
		{
			return;
		}
		base.Progress += (float)taps.GetNumNew() / (float)this.targetTaps;
	}

	private int targetTaps;
}
