using System;

namespace Simulation
{
	public class BuffDataTEMissChance : BuffData
	{
		public BuffDataTEMissChance(float missChanceFactorAdd)
		{
			this.buffDataMiss = new BuffDataMissChanceAdd();
			this.buffDataMiss.isPermenant = true;
			this.buffDataMiss.missChanceAdd = missChanceFactorAdd;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.missChanceFactor += this.missChanceFactorAdd;
		}

		public override void OnNewEnemies(Buff buff)
		{
			foreach (Enemy enemy in buff.GetBy().world.activeChallenge.enemies)
			{
				enemy.AddBuff(this.buffDataMiss, 0, false);
			}
		}

		public float missChanceFactorAdd;

		public BuffDataMissChanceAdd buffDataMiss;
	}
}
