using System;

namespace Simulation
{
	public class BuffDataInstincts : BuffData
	{
		public override void OnCastSpellSelf(Buff buff, Skill skill)
		{
			Hero hero = (Hero)buff.GetBy();
			if (skill.data.dataBase is SkillDataBaseCrowAttack)
			{
				hero.DecreaseSkillCooldown(typeof(SkillDataBaseRoar), this.cooldownReductionAmount);
			}
		}

		public float cooldownReductionAmount;
	}
}
