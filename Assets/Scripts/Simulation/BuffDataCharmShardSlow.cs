using System;

namespace Simulation
{
	public class BuffDataCharmShardSlow : BuffData
	{
		public BuffDataCharmShardSlow(float duration, float attackSpeed)
		{
			this.duration = duration;
			this.attackSpeed = attackSpeed;
			this.id = 313;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
			buffDataAttackSpeed.id = 314;
			buffDataAttackSpeed.dur = this.duration;
			buffDataAttackSpeed.isStackable = false;
			buffDataAttackSpeed.attackSpeedAdd = -this.attackSpeed;
			buffDataAttackSpeed.visuals |= 2;
			target.AddBuff(buffDataAttackSpeed, 0, false);
		}

		private float duration;

		private float attackSpeed;
	}
}
