using System;

namespace Simulation
{
	public class BuffDataUltiCooldownTE : BuffData
	{
		public BuffDataUltiCooldownTE(float decreaseCooldown)
		{
			this.decreaseCooldown = decreaseCooldown;
		}

		public override void OnCastSpellSelf(Buff buff, Skill skill)
		{
			Hero hero = buff.GetBy() as Hero;
			SkillTree skillTree = hero.GetSkillTree();
			if (skillTree.ulti == skill.GetDataBase())
			{
				hero.DecreaseSkillCooldown(skillTree.branches[0][0].GetType(), this.decreaseCooldown);
				hero.DecreaseSkillCooldown(skillTree.branches[1][0].GetType(), this.decreaseCooldown);
			}
		}

		private float decreaseCooldown;
	}
}
