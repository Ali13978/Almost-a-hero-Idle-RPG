using System;

namespace Simulation
{
	public class BuffDataParanoia : BuffData
	{
		public override void OnDodged(Buff buff, Unit attacker, Damage damage)
		{
			Hero hero = (Hero)buff.GetBy();
			hero.DecreaseSkillCooldown(typeof(SkillDataBaseHeHasThePower), this.coolDownDecrease);
		}

		public float coolDownDecrease;
	}
}
