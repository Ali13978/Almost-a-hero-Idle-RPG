using System;

namespace Simulation
{
	public class BuffDataProtection : BuffData
	{
		public BuffDataProtection(double shieldRatio, float cooldown)
		{
			this.shieldRatio = shieldRatio;
			this.cooldown = cooldown;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (this.timerVisual > 0f)
			{
				this.visuals = 4096;
				this.timerVisual -= dt;
			}
			else
			{
				this.visuals = 0;
			}
			if (this.genericFlag)
			{
				return;
			}
			this.genericTimer += dt;
			if (this.genericTimer >= this.cooldown)
			{
				this.genericTimer = 0f;
				this.genericFlag = true;
			}
		}

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			if (this.genericFlag)
			{
				UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
				if (unitHealthy.IsAlly(attacker))
				{
					return;
				}
				if (unitHealthy.IsAlive())
				{
					this.genericFlag = false;
					unitHealthy.GainShield(this.shieldRatio, float.PositiveInfinity);
					unitHealthy.AddVisualBuff(3f, 256);
					this.timerVisual = 2f;
				}
			}
		}

		private double shieldRatio;

		private float cooldown;

		private float timerVisual;
	}
}
