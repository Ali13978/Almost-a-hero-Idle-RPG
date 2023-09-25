using System;

namespace Simulation
{
	public class BuffDataFlare : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			foreach (Enemy enemy in buff.GetWorld().activeChallenge.enemies)
			{
				if (enemy.IsAlive())
				{
					enemy.AddBuff(this.defenseDebuff, 0, false);
				}
			}
		}

		public BuffDataDefense defenseDebuff;
	}
}
