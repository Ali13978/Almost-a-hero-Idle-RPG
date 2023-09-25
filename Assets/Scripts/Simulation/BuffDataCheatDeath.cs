using System;

namespace Simulation
{
	public class BuffDataCheatDeath : BuffData
	{
		public BuffDataCheatDeath(double health, float cooldown)
		{
			this.health = health;
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

		public override void OnDeathSelf(Buff buff)
		{
			if (this.genericFlag)
			{
				this.genericFlag = false;
				this.timerVisual = 1f;
				Hero hero = buff.GetBy() as Hero;
				if (hero != null)
				{
					hero.HealWithoutCallback(this.health);
				}
			}
		}

		private double health;

		private float cooldown;

		private float timerVisual;
	}
}
