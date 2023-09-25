using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge21 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 20;
			this.numDuplicatesTotal = 2;
			this.numHeroesMin = 1;
			this.numHeroesMax = 1;
			this.hasRing = true;
			this.ratPower = 0.35;
			this.minPower = -25.0;
			this.maxPower = 180.0;
			this.numWaves = 50;
			this.dur = (float)(5 * TimeChallenge.MINUTE);
			for (int i = 0; i < 9; i++)
			{
				ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(2);
				challengeUpgradeSkillPoints.waveReq = i * 5;
				challengeUpgradeSkillPoints.cost = 1000000.0 * GameMath.PowInt(2.5, i * 6);
				this.allUpgrades.Add(challengeUpgradeSkillPoints);
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularWolf();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 0.20000000298023224;
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
			return Mathf.Max(2f, (float)totWave / (float)this.numWaves * 6f);
		}
	}
}
