using System;

public class TutorialMissionPrestige : TutorialMission
{
	public TutorialMissionPrestige(CurrencyType rewardCurrency, double rewardAmount) : base(rewardCurrency, rewardAmount)
	{
	}

	public override string GetDescription()
	{
		return LM.Get("TUTORIAL_MISSION_PRESTIGE");
	}

	public override void OnPrestige()
	{
		base.Progress = 1f;
	}
}
