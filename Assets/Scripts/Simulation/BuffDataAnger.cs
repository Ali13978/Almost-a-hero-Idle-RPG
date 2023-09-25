using System;

namespace Simulation
{
	public class BuffDataAnger : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageAddFactor += this.damageFactor;
			totEffect.attackSpeedAdd += this.attackSpeedFactor;
			if (buff.GetBy().GetId() == "THOUR")
			{
				buff.GetBy().world.OnBellylarfAnger(dt);
			}
		}

		public double damageFactor;

		public float attackSpeedFactor;
	}
}
