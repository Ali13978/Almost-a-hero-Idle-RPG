using System;

namespace Simulation
{
	public class BuffDataParley : BuffData
	{
		public BuffDataParley(double damageReduce, float cooldown)
		{
			this.damageReduce = damageReduce;
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

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (this.genericFlag && target.IsAlive())
			{
				UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
				if (!unitHealthy.IsAlly(target))
				{
					this.timerVisual = 1f;
					this.genericFlag = false;
					target.AddBuff(new BuffDataDamageTE
					{
						id = 271,
						visuals = 16,
						damageAdd = -this.damageReduce,
						dur = float.PositiveInfinity,
						isStackable = false
					}, 0, false);
				}
			}
		}

		private double damageReduce;

		private float cooldown;

		private float timerVisual;
	}
}
