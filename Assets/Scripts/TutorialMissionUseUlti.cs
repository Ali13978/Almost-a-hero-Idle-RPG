using System;
using Simulation;

public class TutorialMissionUseUlti : TutorialMission
{
	public TutorialMissionUseUlti(int targetUltisCount, CurrencyType rewardCurrency, double rewardAmount) : base(rewardCurrency, rewardAmount)
	{
		this.targetUltisCount = targetUltisCount;
		this.progressUnit = 1f / (float)targetUltisCount;
	}

	public override string GetDescription()
	{
		return string.Format(LM.Get("TUTORIAL_MISSION_USE_ULTIS"), this.targetUltisCount);
	}

	public override void OnUltiUsed(Hero hero)
	{
		base.Progress += this.progressUnit;
	}

	private int targetUltisCount;

	private float progressUnit;
}
