using System;
using System.Collections.Generic;

namespace Simulation
{
	public class BuffDataAngerStun : BuffData
	{
		public BuffDataAngerStun(float stunDur, int triggerNum)
		{
			this.stunDur = stunDur;
			this.triggerNum = triggerNum;
		}

		public override void OnDodged(Buff buff, Unit attacker, Damage damage)
		{
			this.Evaluate(buff, attacker);
		}

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			this.Evaluate(buff, attacker);
		}

		private void Evaluate(Buff buff, Unit attacker)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (unitHealthy.IsAlly(attacker))
			{
				return;
			}
			this.hitCounter++;
			if (this.hitCounter > this.triggerNum)
			{
				this.hitCounter = 0;
				IEnumerable<UnitHealthy> allies = attacker.GetAllies();
				foreach (UnitHealthy unitHealthy2 in allies)
				{
					if (!unitHealthy2.IsInvulnerable())
					{
						BuffDataStun buffDataStun = new BuffDataStun();
						buffDataStun.id = 175;
						buffDataStun.visuals |= 512;
						buffDataStun.dur = this.stunDur;
						unitHealthy2.AddBuff(buffDataStun, 0, false);
					}
				}
			}
		}

		public float stunDur;

		public int triggerNum;

		private int hitCounter;
	}
}
