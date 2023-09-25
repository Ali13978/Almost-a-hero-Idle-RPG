using System;

namespace Simulation
{
	public class BuffDataNegotiate : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			foreach (Enemy enemy in buff.GetWorld().activeChallenge.enemies)
			{
				if (enemy.IsAlive() && this.missChanceDebuff != null)
				{
					enemy.AddBuff(this.missChanceDebuff, 0, false);
				}
			}
		}

		public BuffDataMissChanceAdd missChanceDebuff;
	}
}
