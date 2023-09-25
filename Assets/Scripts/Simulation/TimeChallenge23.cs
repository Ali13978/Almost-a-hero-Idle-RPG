using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TimeChallenge23 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 22;
			this.numDuplicatesTotal = 0;
			this.numHeroesMin = 0;
			this.numHeroesMax = 0;
			this.hasRing = true;
			this.ratPower = 0.25;
			this.minPower = -65.0;
			this.maxPower = 305.0;
			this.numWaves = 100;
			this.dur = (float)(15 * TimeChallenge.MINUTE);
			for (int i = 0; i < 20; i++)
			{
				ChallengeUpgradeDamageTotem challengeUpgradeDamageTotem = new ChallengeUpgradeDamageTotem((double)(6 + i));
				if (i > 9)
				{
					challengeUpgradeDamageTotem.multiplier = (double)(15 + i * 2);
				}
				challengeUpgradeDamageTotem.waveReq = 3 * i;
				challengeUpgradeDamageTotem.cost = 0.0;
				this.allUpgrades.Add(challengeUpgradeDamageTotem);
				if (i < 19)
				{
					ChallengeDegradeGold challengeDegradeGold = new ChallengeDegradeGold(0.5);
					if (i > 14)
					{
						challengeDegradeGold.multiplier = 0.01;
					}
					else if (i > 9)
					{
						challengeDegradeGold.multiplier = 0.1;
					}
					else if (i > 4)
					{
						challengeDegradeGold.multiplier = 0.25;
					}
					challengeDegradeGold.waveReq = 3 * i + 3;
					challengeDegradeGold.cost = 0.0;
					this.allUpgrades.Add(challengeDegradeGold);
				}
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularBat();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 0.01;
			Environment item = new Environment(Bg.TRAINING_GROUND, new List<EnemyDataBase>
			{
				enemyDataBase
			}, null, null, null, false);
			this.allEnvironments = new List<Environment>
			{
				item
			};
			this.activeEnv = this.allEnvironments[0];
		}

		protected override float GetSpawnPoints(int totWave)
		{
			return 5f;
		}
	}
}
