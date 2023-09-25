using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulation
{
	public class BuffDataOffenceDeffence : BuffData
	{
		public BuffDataOffenceDeffence(int triggerCount, float shieldAmount)
		{
			this.triggerCount = triggerCount;
			this.shieldAmount = shieldAmount;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (damage.type == DamageType.SKILL)
			{
				return;
			}
			this.hitCount++;
			if (this.hitCount >= this.triggerCount)
			{
				List<UnitHealthy> list = buff.GetBy().GetAllies().ToList<UnitHealthy>();
				list.Remove(buff.GetBy() as UnitHealthy);
				if (list.Count > 0)
				{
					this.hitCount = 0;
					UnitHealthy unitHealthy = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
					unitHealthy.GainShield((double)this.shieldAmount, float.PositiveInfinity);
					unitHealthy.AddVisualBuff(3f, 256);
				}
			}
		}

		private int triggerCount;

		private float shieldAmount;

		private int hitCount;
	}
}
