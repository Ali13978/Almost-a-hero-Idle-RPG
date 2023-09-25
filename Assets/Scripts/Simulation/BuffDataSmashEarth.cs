using System;

namespace Simulation
{
	public class BuffDataSmashEarth : BuffData
	{
		public BuffDataSmashEarth(double dmgRatio, float durationAdd)
		{
			this.dmgRatio = dmgRatio;
			this.durationAdd = durationAdd;
			this.id = 162;
		}

		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			double num = 0.0;
			foreach (Hero hero in buff.GetWorld().heroes)
			{
				if (hero.IsAlive())
				{
					num += hero.GetDamage();
				}
			}
			num *= this.dmgRatio;
			totEffect.ringDamageAdd += num;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemEarthDurationAdd += this.durationAdd;
		}

		private double dmgRatio;

		private float durationAdd;
	}
}
