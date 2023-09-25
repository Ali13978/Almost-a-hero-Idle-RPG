using System;

namespace Simulation
{
	public class BuffDataCallOfWild : BuffData
	{
		public BuffDataCallOfWild(float durDecrease)
		{
			this.durDecrease = durDecrease;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			Hero hero = (Hero)buff.GetBy();
			hero.DecreaseSkillCooldown(typeof(SkillDataBaseCrowAttack), this.durDecrease);
		}

		private float durDecrease;
	}
}
