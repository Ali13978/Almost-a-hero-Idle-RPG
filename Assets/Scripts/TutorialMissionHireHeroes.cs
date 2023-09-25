using System;
using Simulation;

public class TutorialMissionHireHeroes : TutorialMission
{
	public TutorialMissionHireHeroes(int targetHeroes, CurrencyType rewardCurrency, double rewardAmount) : base(rewardCurrency, rewardAmount)
	{
		this.targetHeroes = targetHeroes;
	}

	public override string GetDescription()
	{
		if (this.targetHeroes > 1)
		{
			return string.Format(LM.Get("TUTORIAL_MISSION_HEROES"), this.targetHeroes);
		}
		return LM.Get("TUTORIAL_MISSION_HERO");
	}

	protected override void OnUpdate(Simulator simulator, Taps taps)
	{
		base.Progress = (float)simulator.GetActiveWorldNumHeroes() / (float)this.targetHeroes;
	}

	private int targetHeroes;
}
