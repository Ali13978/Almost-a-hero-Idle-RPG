using System;

namespace Simulation
{
	public class BuffDataDamageTETotemByHero : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			if (buff.GetBy() is Hero && (buff.GetBy() as Hero).IsAlive())
			{
				totEffect.ringDamageAdd += buff.GetBy().GetDamage() * this.heroDamageRatio;
			}
		}

		public double heroDamageRatio;
	}
}
