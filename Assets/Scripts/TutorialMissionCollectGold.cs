using System;

public class TutorialMissionCollectGold : TutorialMission
{
	public TutorialMissionCollectGold(double targetGoldCollected, CurrencyType rewardCurrency, double rewardAmount) : base(rewardCurrency, rewardAmount)
	{
		this.targetGoldCollected = targetGoldCollected;
	}

	public override string GetDescription()
	{
		return string.Format(LM.Get("TUTORIAL_MISSION_COLLECT_GOLD"), GameMath.GetDoubleString(this.targetGoldCollected));
	}

	public override void OnGoldCollected(double amount)
	{
		base.Progress += (float)(amount / this.targetGoldCollected);
	}

	private double targetGoldCollected;
}
