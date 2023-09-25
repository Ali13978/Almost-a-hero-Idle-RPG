using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge04 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 3;
			this.numHeroesMin = 0;
			this.numHeroesMax = 0;
			this.hasRing = true;
			this.ratPower = 0.3;
			this.minPower = -3.0;
			this.maxPower = 55.0;
			this.numWaves = 40;
			this.dur = (float)(4 * TimeChallenge.MINUTE);
			ChallengeUpgradeDamageTotem challengeUpgradeDamageTotem = new ChallengeUpgradeDamageTotem(1.5);
			challengeUpgradeDamageTotem.waveReq = 5;
			challengeUpgradeDamageTotem.cost = 10000.0;
			ChallengeUpgradeDamageTotem challengeUpgradeDamageTotem2 = new ChallengeUpgradeDamageTotem(1.75);
			challengeUpgradeDamageTotem2.waveReq = 5;
			challengeUpgradeDamageTotem2.cost = 100000.0;
			ChallengeUpgradeDamageTotem challengeUpgradeDamageTotem3 = new ChallengeUpgradeDamageTotem(2.0);
			challengeUpgradeDamageTotem3.waveReq = 5;
			challengeUpgradeDamageTotem3.cost = 1000000.0;
			ChallengeUpgradeDamageTotem challengeUpgradeDamageTotem4 = new ChallengeUpgradeDamageTotem(2.5);
			challengeUpgradeDamageTotem4.waveReq = 5;
			challengeUpgradeDamageTotem4.cost = 10000000.0;
			ChallengeUpgradeDamageTotem challengeUpgradeDamageTotem5 = new ChallengeUpgradeDamageTotem(3.0);
			challengeUpgradeDamageTotem5.waveReq = 5;
			challengeUpgradeDamageTotem5.cost = 100000000.0;
			ChallengeUpgradeDamageTotem challengeUpgradeDamageTotem6 = new ChallengeUpgradeDamageTotem(3.5);
			challengeUpgradeDamageTotem6.waveReq = 5;
			challengeUpgradeDamageTotem6.cost = 1000000000.0;
			ChallengeUpgradeDamageTotem challengeUpgradeDamageTotem7 = new ChallengeUpgradeDamageTotem(4.0);
			challengeUpgradeDamageTotem7.waveReq = 5;
			challengeUpgradeDamageTotem7.cost = 10000000000.0;
			this.allUpgrades = new List<ChallengeUpgrade>
			{
				challengeUpgradeDamageTotem,
				challengeUpgradeDamageTotem2,
				challengeUpgradeDamageTotem3,
				challengeUpgradeDamageTotem4,
				challengeUpgradeDamageTotem5,
				challengeUpgradeDamageTotem6,
				challengeUpgradeDamageTotem7
			};
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularBat();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.healthMax *= 5.0;
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
			return Mathf.Max(1f, ((float)totWave % 5f + 1f) * 1f + 1f);
		}
	}
}
