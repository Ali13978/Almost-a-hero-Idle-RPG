using System;

namespace Simulation
{
	public class BuffDataBackForRevenge : BuffData
	{
		public BuffDataBackForRevenge(float cooldown)
		{
			this.cooldown = cooldown;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			Hero hero = buff.GetBy() as Hero;
			if (this.timerVisual > 0f)
			{
				this.timerVisual -= dt;
				this.visuals = 4096;
			}
			else
			{
				this.visuals = 0;
			}
			if (this.genericFlag)
			{
				bool flag = true;
				foreach (Hero hero2 in hero.world.heroes)
				{
					if (hero2.IsAlive())
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					hero.SetTillReviveTime(0.25f);
					this.genericFlag = false;
					this.timerVisual = 2f;
				}
			}
			else
			{
				this.genericTimer += dt;
				if (this.genericTimer >= this.cooldown)
				{
					this.genericTimer = 0f;
					this.genericFlag = true;
				}
			}
		}

		private float cooldown;

		private float timerVisual;
	}
}
