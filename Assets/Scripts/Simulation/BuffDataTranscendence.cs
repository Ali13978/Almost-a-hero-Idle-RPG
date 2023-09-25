using System;

namespace Simulation
{
	public class BuffDataTranscendence : BuffData
	{
		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			buff.IncreaseGenericCounter();
			if (buff.GetGenericCounter() >= this.numHitReq)
			{
				buff.ZeroGenericCounter();
				this.HealMostDamagedAlly(buff);
			}
		}

		private void HealMostDamagedAlly(Buff buff)
		{
			Unit by = buff.GetBy();
			UnitHealthy unitHealthy = null;
			foreach (UnitHealthy unitHealthy2 in by.GetAllies())
			{
				if (unitHealthy2.IsAlive())
				{
					if (unitHealthy == null || unitHealthy2.GetHealthRatio() < unitHealthy.GetHealthRatio())
					{
						unitHealthy = unitHealthy2;
					}
				}
			}
			if (unitHealthy != null)
			{
				BuffDataHealthRegen buffDataHealthRegen = new BuffDataHealthRegen();
				buffDataHealthRegen.id = 95;
				buffDataHealthRegen.isStackable = true;
				buffDataHealthRegen.dur = BuffDataTranscendence.DURATION;
				buffDataHealthRegen.healthRegenAdd = this.healRatio / (double)BuffDataTranscendence.DURATION;
				buffDataHealthRegen.visuals |= 64;
				unitHealthy.AddBuff(buffDataHealthRegen, 0, false);
			}
		}

		public double healRatio;

		public int numHitReq;

		public static float DURATION = 1f;
	}
}
