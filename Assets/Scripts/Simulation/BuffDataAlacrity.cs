using System;

namespace Simulation
{
	public class BuffDataAlacrity : BuffData
	{
		public BuffDataAlacrity(float cdReduce, float cooldown)
		{
			this.cdReduce = cdReduce;
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

		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			if (this.genericFlag)
			{
				Hero hero = buff.GetBy() as Hero;
				if (!hero.IsAlly(killed))
				{
					this.genericFlag = false;
					this.timerVisual = 1f;
					SkillTree skillTree = hero.GetSkillTree();
					hero.DecreaseSkillCooldown(skillTree.ulti.GetType(), this.cdReduce);
					hero.DecreaseSkillCooldown(skillTree.branches[0][0].GetType(), this.cdReduce);
					hero.DecreaseSkillCooldown(skillTree.branches[1][0].GetType(), this.cdReduce);
				}
			}
		}

		private float cdReduce;

		private float cooldown;

		private float timerVisual;
	}
}
