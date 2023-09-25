using System;

namespace Simulation
{
	public class BuffDataRestoration : BuffData
	{
		public BuffDataRestoration(double healthRegen, float cooldown)
		{
			this.healthRegen = healthRegen;
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

		public override void OnCastSpellSelf(Buff buff, Skill skill)
		{
			if (this.genericFlag)
			{
				this.genericFlag = false;
				Unit by = buff.GetBy();
				this.timerVisual = 1f;
				foreach (UnitHealthy unitHealthy in by.GetAllies())
				{
					if (unitHealthy != by)
					{
						if (unitHealthy.IsAlive())
						{
							BuffDataHealthRegen buffDataHealthRegen = new BuffDataHealthRegen();
							buffDataHealthRegen.id = 102;
							buffDataHealthRegen.isStackable = true;
							buffDataHealthRegen.dur = 3f;
							buffDataHealthRegen.healthRegenAdd = this.healthRegen / 3.0;
							buffDataHealthRegen.visuals |= 64;
							unitHealthy.AddBuff(buffDataHealthRegen, 0, true);
						}
					}
				}
			}
		}

		private double healthRegen;

		private float timerVisual;

		private float cooldown;
	}
}
