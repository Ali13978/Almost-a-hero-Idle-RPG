using System;

namespace Simulation
{
	public class BuffDataStormer : BuffData
	{
		public BuffDataStormer(float critChance)
		{
			this.critChance = critChance;
			this.id = 166;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (buff.GetGenericCounter() > 0)
			{
				totEffect.critChanceAdd += this.critChance;
			}
		}

		public override void OnIceManaFull(Buff buff)
		{
			buff.IncreaseGenericCounter();
		}

		public override void OnAfterIceShardRain(Buff buff)
		{
			buff.ZeroGenericCounter();
		}

		private float critChance;
	}
}
