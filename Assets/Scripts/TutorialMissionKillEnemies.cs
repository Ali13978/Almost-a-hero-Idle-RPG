using System;
using Simulation;

public class TutorialMissionKillEnemies : TutorialMission
{
	public TutorialMissionKillEnemies(int targetEnemiesKilledCount, CurrencyType rewardCurrency, double rewardAmount) : base(rewardCurrency, rewardAmount)
	{
		this.targetEnemiesKilledCount = targetEnemiesKilledCount;
		this.progressUnit = 1f / (float)targetEnemiesKilledCount;
	}

	public override string GetDescription()
	{
		return string.Format(LM.Get("TUTORIAL_MISSION_KILL_ENEMIES"), this.targetEnemiesKilledCount);
	}

	public override void OnEnemyKilled(UnitHealthy enemy, Unit killer)
	{
		base.Progress += this.progressUnit;
	}

	private int targetEnemiesKilledCount;

	private float progressUnit;
}
