using System;

namespace Simulation
{
	public class BuffDataWeepingSong : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			foreach (Enemy enemy in buff.GetWorld().activeChallenge.enemies)
			{
				if (enemy.IsAlive())
				{
					if (this.defenseDebuff != null)
					{
						enemy.AddBuff(this.defenseDebuff, 0, false);
					}
					if (this.attackSpeedDebuff != null)
					{
						enemy.AddBuff(this.attackSpeedDebuff, 0, false);
					}
					if (this.dropGoldDebuff != null)
					{
						enemy.AddBuff(this.dropGoldDebuff, 0, false);
					}
				}
			}
		}

		public BuffDataDefense defenseDebuff;

		public BuffDataAttackSpeed attackSpeedDebuff;

		public BuffDataDropGold dropGoldDebuff;
	}
}
