using System;

namespace Simulation
{
	public class BuffDataFurySkill : BuffData
	{
		public BuffDataFurySkill(float decrease)
		{
			this.decrease = decrease;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			Hero hero = buff.GetBy() as Hero;
			if (!hero.IsAlly(target))
			{
				hero.DecreaseAllCooldowns(this.decrease);
			}
		}

		private float decrease;
	}
}
