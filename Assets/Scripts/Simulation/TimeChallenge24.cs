using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class TimeChallenge24 : TimeChallenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.id = 23;
			this.numDuplicatesTotal = 3;
			this.numHeroesMin = 1;
			this.numHeroesMax = 1;
			this.hasRing = false;
			this.ratPower = 0.4;
			this.minPower = -50.0;
			this.maxPower = 215.0;
			this.numWaves = 100;
			this.dur = (float)(15 * TimeChallenge.MINUTE);
			for (int i = 0; i < 10; i++)
			{
				ChallengeUpgradeSkillPoints challengeUpgradeSkillPoints = new ChallengeUpgradeSkillPoints(3);
				challengeUpgradeSkillPoints.waveReq = i * 4;
				challengeUpgradeSkillPoints.cost = 1000000.0 * GameMath.PowInt(3.0, i * 5);
				this.allUpgrades.Add(challengeUpgradeSkillPoints);
			}
		}

		protected override void SetEnvironments()
		{
			EnemyDataBase enemyDataBase = EnemyFactory.CreateRegularWolf();
			enemyDataBase.spawnWeight = 1f;
			enemyDataBase.damage *= 0.10000000149011612;
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
