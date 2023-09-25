using System;
using Simulation;

public class TutorialMissionSaveCoins : TutorialMission
{
	public TutorialMissionSaveCoins(double targetCoins, CurrencyType rewardCurrency, double rewardAmount) : base(rewardCurrency, rewardAmount)
	{
		this.targetCoins = targetCoins;
	}

	public override string GetDescription()
	{
		return string.Format(LM.Get("TUTORIAL_MISSION_COINS"), this.targetCoins);
	}

	protected override void OnUpdate(Simulator simulator, Taps taps)
	{
		base.Progress = (float)(simulator.GetGold().GetAmount() / this.targetCoins);
	}

	private double targetCoins;
}
