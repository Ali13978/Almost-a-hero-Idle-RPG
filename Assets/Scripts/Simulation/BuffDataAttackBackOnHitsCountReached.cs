using System;
using Static;

namespace Simulation
{
	public class BuffDataAttackBackOnHitsCountReached : BuffData
	{
		public BuffDataAttackBackOnHitsCountReached(int id, int hitsCountToAttackBack)
		{
			this.id = id;
			this.hitsCountToAttackBack = hitsCountToAttackBack;
			this.isPermenant = true;
		}

		public override bool IsToSave()
		{
			return true;
		}

		public override string GetSaveDataGenericKey()
		{
			return StringExtension.Concat("AttackBackOnHitsCountReached", this.id.ToString());
		}

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			UnitHealthy unitHealthy = attacker as UnitHealthy;
			if (unitHealthy != null && !(buff.GetBy() as UnitHealthy).IsAlly(attacker))
			{
				buff.IncreaseGenericCounter();
				if (buff.GetGenericCounter() >= this.hitsCountToAttackBack)
				{
					buff.ZeroGenericCounter();
					Unit by = buff.GetBy();
					by.AttackImmediately(unitHealthy);
				}
			}
		}

		private int hitsCountToAttackBack;
	}
}
