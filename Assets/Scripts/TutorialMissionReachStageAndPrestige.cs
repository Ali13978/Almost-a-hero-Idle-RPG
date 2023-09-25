using System;
using Simulation;
using Static;

public class TutorialMissionReachStageAndPrestige : TutorialMission
{
	public TutorialMissionReachStageAndPrestige(int targetStage, CurrencyType rewardCurrency, double rewardAmount) : base(rewardCurrency, rewardAmount)
	{
		this.targetStage = targetStage;
	}

	public override string GetDescription()
	{
		return StringExtension.Concat(string.Format(LM.Get("TUTORIAL_MISSION_STAGE"), this.targetStage), " + ", LM.Get("TUTORIAL_MISSION_PRESTIGE"));
	}

	protected override void OnUpdate(Simulator simulator, Taps taps)
	{
		if (!this.waitingPrestige)
		{
			int num = this.targetStage * 11 + 1;
			ChallengeStandard challengeStandard = simulator.GetWorld(GameMode.STANDARD).activeChallenge as ChallengeStandard;
			if (challengeStandard.totWave < num)
			{
				base.Progress = (float)challengeStandard.totWave / (float)(num + 1);
			}
			else
			{
				this.waitingPrestige = true;
				this.previousNumPrestiges = simulator.numPrestiges;
			}
		}
		else if (base.Progress < 1f)
		{
			if (simulator.numPrestiges > this.previousNumPrestiges)
			{
				base.Progress = 1f;
			}
			else
			{
				int num2 = this.targetStage * 11 + 1;
				base.Progress = (float)num2 / (float)(num2 + 1);
			}
		}
	}

	public override void OnReset()
	{
		this.waitingPrestige = false;
		this.previousNumPrestiges = 0;
	}

	private int targetStage;

	private bool waitingPrestige;

	private int previousNumPrestiges;
}
