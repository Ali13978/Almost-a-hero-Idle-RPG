using System;

namespace Simulation
{
	public class BuffDataSharpEdge : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.critChanceAdd += this.critChance;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (!damage.isCrit)
			{
				return;
			}
			target.AddBuff(this.effect, 0, false);
		}

		public BuffDataDefense effect;

		public float critChance;
	}
}
