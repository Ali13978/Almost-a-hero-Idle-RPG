using System;

namespace Simulation
{
	public class BuffDataColdWind : BuffData
	{
		public BuffDataColdWind(float duration, float attackSpeed)
		{
			this.duration = duration;
			this.attackSpeed = attackSpeed;
			this.id = 26;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (damage.isCrit)
			{
				BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
				buffDataAttackSpeed.id = 10;
				buffDataAttackSpeed.dur = this.duration;
				buffDataAttackSpeed.isStackable = false;
				buffDataAttackSpeed.attackSpeedAdd = -this.attackSpeed;
				buffDataAttackSpeed.visuals |= 2;
				target.AddBuff(buffDataAttackSpeed, 0, false);
			}
		}

		private float duration;

		private float attackSpeed;
	}
}
