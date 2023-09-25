using System;
using Simulation;

public class TutorialMissionReachStage : TutorialMission
{
	public TutorialMissionReachStage(int targetStage, CurrencyType rewardCurrency, double rewardAmount) : base(rewardCurrency, rewardAmount)
	{
		this.targetStage = targetStage;
	}

	public override string GetDescription()
	{
		return string.Format(LM.Get("TUTORIAL_MISSION_STAGE"), this.targetStage);
	}

	protected override void OnUpdate(Simulator simulator, Taps taps)
	{
		int num = this.targetStage * 11 + 1;
		ChallengeStandard challengeStandard = simulator.GetWorld(GameMode.STANDARD).activeChallenge as ChallengeStandard;
		base.Progress = (float)challengeStandard.totWave / (float)num;
	}

	private int targetStage;
}
