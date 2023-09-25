using System;

namespace Simulation
{
	public class BuffDataFuelThemUp : BuffData
	{
		public override void OnReviveAlly(Buff buff, UnitHealthy revived)
		{
			Hero hero = buff.GetBy() as Hero;
			if (hero == null)
			{
				return;
			}
			if (!hero.IsAlive())
			{
				return;
			}
			Hero hero2 = revived as Hero;
			if (hero2 == null)
			{
				return;
			}
			hero2.DecreaseUltiCooldown(this.allyCooldownDecrease);
			hero.DecreaseUltiCooldown(this.selfCooldownDecrease);
		}

		public float allyCooldownDecrease;

		public float selfCooldownDecrease;
	}
}
