using System;
using Static;

namespace Simulation
{
	public class BuffDataAttackSpeedOnHitsCountToSameTarget : BuffData
	{
		public override bool IsToSave()
		{
			return true;
		}

		public override string GetSaveDataGenericKey()
		{
			return StringExtension.Concat("BuffDataAttackSpeedOnHitsCountToSameTarget", this.id.ToString());
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (damage.type == DamageType.SKILL || target == null || buff.GetBy().HasBuffWithId(350))
			{
				return;
			}
			if (target != this.previousTarget)
			{
				this.buffGiven = false;
				buff.ZeroGenericCounter();
			}
			if (this.buffGiven)
			{
				return;
			}
			this.previousTarget = target;
			buff.IncreaseGenericCounter();
			if (buff.GetGenericCounter() >= this.hitsCount)
			{
				this.buffGiven = true;
				buff.ZeroGenericCounter();
				BuffDataAttackSpeedFactor buffData = new BuffDataAttackSpeedFactor
				{
					isStackable = false,
					attackSpeedFactor = this.attackSpeed,
					dur = this.buffDuration,
					id = 350,
					visuals = 1
				};
				buff.GetBy().AddBuff(buffData, 0, false);
			}
		}

		public int hitsCount;

		public float buffDuration;

		public float attackSpeed;

		private UnitHealthy previousTarget;

		private bool buffGiven;
	}
}
