using System;
using Simulation;

public class TutorialMissionLevelUpHero : TutorialMission
{
	public TutorialMissionLevelUpHero(CurrencyType rewardCurrency, double rewardAmount) : base(rewardCurrency, rewardAmount)
	{
	}

	public override string GetDescription()
	{
		return LM.Get("TUTORIAL_MISSION_HERO_LEVEL_UP");
	}

	protected override void OnUpdate(Simulator simulator, Taps taps)
	{
		base.Progress = 0f;
		int i = 0;
		int count = simulator.GetActiveWorld().heroes.Count;
		while (i < count)
		{
			Hero hero = simulator.GetActiveWorld().heroes[i];
			if (hero.GetLevel() - i > 0)
			{
				base.Progress = 1f;
				return;
			}
			float b = (float)hero.GetXp() / (float)hero.GetXpNeedForNextLevel();
			base.Progress = GameMath.GetMaxFloat(base.Progress, b);
			i++;
		}
	}
}
