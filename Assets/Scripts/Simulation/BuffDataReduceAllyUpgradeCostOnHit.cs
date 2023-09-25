using System;
using System.Collections.Generic;

namespace Simulation
{
	public class BuffDataReduceAllyUpgradeCostOnHit : BuffData
	{
		public BuffDataReduceAllyUpgradeCostOnHit()
		{
			this.isStackable = false;
		}

		public override void OnInit(Buff buff)
		{
			Unit by = buff.GetBy();
			foreach (UnitHealthy unitHealthy in by.GetAllies())
			{
				if (unitHealthy != by)
				{
					Buff buffWithId = unitHealthy.GetBuffWithId(333);
					int genericCounter = 0;
					if (buffWithId != null)
					{
						genericCounter = buffWithId.GetGenericCounter();
					}
					unitHealthy.RemoveBuff(333);
					unitHealthy.AddBuff(new BuffDataReduceUpgradeCost
					{
						id = 333,
						isPermenant = true,
						doNotRemoveOnRefresh = true,
						costDecMax = (double)this.maxReduceCost,
						costDecPerHit = (double)this.reduceCost
					}, 0, false);
					if (buffWithId != null)
					{
						buffWithId = unitHealthy.GetBuffWithId(333);
						buffWithId.SetGenericCounter(genericCounter);
					}
				}
			}
		}

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (attacker != null && unitHealthy.IsAlly(attacker))
			{
				return;
			}
			List<UnitHealthy> list = new List<UnitHealthy>();
			foreach (UnitHealthy unitHealthy2 in unitHealthy.GetAllies())
			{
				if (unitHealthy2 != unitHealthy)
				{
					list.Add(unitHealthy2);
				}
			}
			if (list.Count > 0)
			{
				UnitHealthy randomListElement = list.GetRandomListElement<UnitHealthy>();
				Buff buffWithId = randomListElement.GetBuffWithId(333);
				if (buffWithId == null)
				{
					randomListElement.AddBuff(new BuffDataReduceUpgradeCost
					{
						id = 333,
						isPermenant = true,
						doNotRemoveOnRefresh = true,
						costDecMax = (double)this.maxReduceCost,
						costDecPerHit = (double)this.reduceCost
					}, 0, false);
					buffWithId = randomListElement.GetBuffWithId(333);
				}
				buffWithId.IncreaseGenericCounter();
			}
		}

		public float reduceCost;

		public float maxReduceCost;
	}
}
