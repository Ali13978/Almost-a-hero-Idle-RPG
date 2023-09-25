using System;

namespace Simulation
{
	public class BuffDataHardTraining : BuffData
	{
		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			Unit by = buff.GetBy();
			if (!(by is Hero))
			{
				return;
			}
			Hero hero = (Hero)by;
			if (hero.IsUsingSkill())
			{
				return;
			}
			hero.DecreaseSkillCooldown(typeof(SkillDataBaseShockWave), this.cooldownDecrease);
		}

		public float cooldownDecrease;
	}
}
