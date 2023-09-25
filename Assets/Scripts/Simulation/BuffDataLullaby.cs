using System;

namespace Simulation
{
	public class BuffDataLullaby : BuffData
	{
		public override void OnCastSpellSelf(Buff buff, Skill skill)
		{
			Unit by = buff.GetBy();
			foreach (Unit unit in by.GetAllies())
			{
				if (unit is Hero)
				{
					Hero hero = (Hero)unit;
					hero.DecreaseSkillCooldowns(this.cooldownReductionAmount);
				}
			}
		}

		public float cooldownReductionAmount;
	}
}
