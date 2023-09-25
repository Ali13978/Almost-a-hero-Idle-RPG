using System;
using Simulation;

public abstract class TutorialMission
{
	public TutorialMission(CurrencyType rewardCurrency, double rewardAmount)
	{
		this.RewardCurrency = rewardCurrency;
		this.RewardAmount = rewardAmount;
		this.Claimed = false;
	}

	public float Progress { get; protected set; }

	public CurrencyType RewardCurrency { get; private set; }

	public double RewardAmount { get; private set; }

	public bool IsComplete
	{
		get
		{
			return this.Progress >= 1f;
		}
	}

	public bool Claimed { get; private set; }

	public virtual void OnReset()
	{
	}

	public void SetLoadState(float progress, bool claimed)
	{
		this.Progress = progress;
		this.Claimed = claimed;
	}

	public void Claim(Simulator simulator)
	{
		switch (this.RewardCurrency)
		{
		case CurrencyType.GOLD:
			simulator.GetActiveWorld().RainGold(this.RewardAmount);
			goto IL_6A;
		case CurrencyType.SCRAP:
			simulator.GetActiveWorld().RainScraps(this.RewardAmount);
			goto IL_6A;
		case CurrencyType.GEM:
			simulator.GetActiveWorld().RainCredits(this.RewardAmount);
			goto IL_6A;
		}
		throw new NotImplementedException();
		IL_6A:
		this.Claimed = true;
	}

	public void Reset()
	{
		this.Progress = 0f;
		this.Claimed = false;
		this.OnReset();
	}

	public abstract string GetDescription();

	public void Update(Simulator simulator, Taps taps)
	{
		if (!this.IsComplete && !this.Claimed)
		{
			this.OnUpdate(simulator, taps);
		}
	}

	public virtual void OnEnemyTookDamage(Damage damage, Unit damager, UnitHealthy damaged)
	{
	}

	public virtual void OnUltiUsed(Hero hero)
	{
	}

	public virtual void OnEnemyKilled(UnitHealthy enemy, Unit killer)
	{
	}

	public virtual void OnGoldCollected(double amount)
	{
	}

	public virtual void OnPrestige()
	{
	}

	protected virtual void OnUpdate(Simulator simulator, Taps taps)
	{
	}

	public static readonly TutorialMission[] List = new TutorialMission[]
	{
		new TutorialMissionCollectGold(200.0, CurrencyType.GOLD, 500.0),
		new TutorialMissionTap(250, CurrencyType.GOLD, 2000.0),
		new TutorialMissionKillEnemies(150, CurrencyType.GOLD, 5000.0),
		new TutorialMissionDealDamage(250000.0, CurrencyType.GOLD, 20000.0),
		new TutorialMissionCollectGold(50000.0, CurrencyType.GOLD, 100000.0),
		new TutorialMissionKillEnemies(200, CurrencyType.GOLD, 1000000.0),
		new TutorialMissionDealDamage(10000000.0, CurrencyType.GOLD, 5000000.0),
		new TutorialMissionKillEnemies(250, CurrencyType.GOLD, 10000000.0),
		new TutorialMissionTap(250, CurrencyType.GOLD, 15000000.0),
		new TutorialMissionDealDamage(500000000.0, CurrencyType.GOLD, 20000000.0),
		new TutorialMissionCollectGold(50000000.0, CurrencyType.GOLD, 50000000.0),
		new TutorialMissionKillEnemies(150, CurrencyType.GOLD, 100000000.0),
		new TutorialMissionTap(250, CurrencyType.GOLD, 500000000.0),
		new TutorialMissionDealDamage(50000000000.0, CurrencyType.GOLD, 1000000000.0),
		new TutorialMissionKillEnemies(75, CurrencyType.GOLD, 3000000000.0),
		new TutorialMissionHireHeroes(5, CurrencyType.GOLD, 2000000000.0),
		new TutorialMissionReachStageAndPrestige(100, CurrencyType.GOLD, 1000.0),
		new TutorialMissionHireHeroes(1, CurrencyType.GOLD, 10000.0),
		new TutorialMissionTap(500, CurrencyType.GOLD, 20000.0),
		new TutorialMissionKillEnemies(250, CurrencyType.GOLD, 50000.0),
		new TutorialMissionCollectGold(250000.0, CurrencyType.GOLD, 250000.0),
		new TutorialMissionDealDamage(2000000.0, CurrencyType.GOLD, 1000000.0),
		new TutorialMissionKillEnemies(300, CurrencyType.GOLD, 5000000.0),
		new TutorialMissionTap(500, CurrencyType.GOLD, 10000000.0),
		new TutorialMissionCollectGold(25000000.0, CurrencyType.GOLD, 25000000.0),
		new TutorialMissionDealDamage(500000000.0, CurrencyType.GOLD, 100000000.0),
		new TutorialMissionKillEnemies(300, CurrencyType.GOLD, 500000000.0),
		new TutorialMissionReachStage(100, CurrencyType.GEM, 100.0)
	};
}
