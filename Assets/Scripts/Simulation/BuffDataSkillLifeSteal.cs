using System;

namespace Simulation
{
	public class BuffDataSkillLifeSteal : BuffData
	{
		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (damage.type == DamageType.SKILL)
			{
				UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
				double num = damage.amount * this.healRatio / unitHealthy.GetHealthMax();
				unitHealthy.Heal(num);
				if (unitHealthy.GetId() == "BLIND_ARCHER")
				{
					unitHealthy.world.OnLiaStealHealth();
				}
			}
		}

		public double healRatio;
	}
}
