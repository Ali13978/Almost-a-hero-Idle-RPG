using System;
using Simulation;

public class TutorialMissionDealDamage : TutorialMission
{
	public TutorialMissionDealDamage(double targetDamage, CurrencyType rewardCurrency, double rewardAmount) : base(rewardCurrency, rewardAmount)
	{
		this.targetDamage = targetDamage;
	}

	public override string GetDescription()
	{
		return string.Format(LM.Get("TUTORIAL_MISSION_DEAL_DAMAGE"), GameMath.GetDoubleString(this.targetDamage));
	}

	public override void OnEnemyTookDamage(Damage damage, Unit damager, UnitHealthy damaged)
	{
		base.Progress += (float)(damage.amount / this.targetDamage);
	}

	private double targetDamage;
}
