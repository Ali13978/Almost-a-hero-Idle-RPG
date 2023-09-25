using System;

namespace Simulation
{
	public class BuffDataReduceCooldownsOnKill : BuffData
	{
		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			Hero hero = buff.GetBy() as Hero;
			if (hero != null)
			{
				hero.DecreaseAllSkillCooldowns(this.cooldownReduction);
			}
		}

		public float cooldownReduction;
	}
}
