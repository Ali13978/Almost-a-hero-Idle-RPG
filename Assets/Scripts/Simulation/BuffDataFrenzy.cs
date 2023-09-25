using System;

namespace Simulation
{
	public class BuffDataFrenzy : BuffData
	{
		public BuffDataFrenzy(float attackSpeed, float duration)
		{
			this.attackSpeed = attackSpeed;
			this.duration = duration;
		}

		public override void OnCastSpellSelf(Buff buff, Skill skill)
		{
			if (skill.GetDataBase() is SkillDataBaseCrowAttack)
			{
				BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
				buffDataAttackSpeed.id = 3;
				buffDataAttackSpeed.dur = this.duration;
				buffDataAttackSpeed.attackSpeedAdd = this.attackSpeed;
				buffDataAttackSpeed.reloadSpeedAdd = this.attackSpeed;
				buffDataAttackSpeed.visuals |= 1;
				buff.GetBy().AddBuff(buffDataAttackSpeed, 0, false);
			}
		}

		private float attackSpeed;

		private float duration;
	}
}
