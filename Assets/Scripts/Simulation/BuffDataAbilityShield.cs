using System;

namespace Simulation
{
	public class BuffDataAbilityShield : BuffData
	{
		public BuffDataAbilityShield(float reduction, float duration)
		{
			this.reduction = reduction;
			this.duration = duration;
		}

		public override void OnCastSpellSelf(Buff buff, Skill skill)
		{
			this.genericTimer = this.duration;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			this.genericTimer -= dt;
			if (this.genericTimer > 0f)
			{
				totEffect.damageTakenFactor *= (double)this.reduction;
				this.visuals = 256;
			}
			else
			{
				this.visuals = 0;
			}
		}

		private float reduction;

		private float duration;
	}
}
