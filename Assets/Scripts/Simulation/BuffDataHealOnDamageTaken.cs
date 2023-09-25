using System;

namespace Simulation
{
	public class BuffDataHealOnDamageTaken : BuffData
	{
		public override bool IsToSave()
		{
			return true;
		}

		public override string GetSaveDataGenericKey()
		{
			return "BuffGeneric.HealEveryAmountOfAttacks";
		}

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			buff.IncreaseGenericCounter();
			if (buff.GetGenericCounter() >= this.attacksAmountTrigger)
			{
				UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
				double healthRegenAdd = this.healRatio / 0.20000000298023224;
				BuffDataHealthRegen buffData = new BuffDataHealthRegen
				{
					dur = 0.2f,
					healthRegenAdd = healthRegenAdd,
					isStackable = true
				};
				unitHealthy.AddBuff(buffData, 0, false);
				unitHealthy.AddVisualBuff(0.5f, 64);
				buff.ZeroGenericCounter();
			}
		}

		public double healRatio;

		public int attacksAmountTrigger;
	}
}
