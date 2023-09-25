using System;

namespace Simulation
{
	public class BuffDataBlindNotDeaf : BuffData
	{
		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (damage.isMissed || damage.isDodged)
			{
				return;
			}
			buff.GetBy().AddBuff(this.damageBuff, 0, false);
		}

		public BuffDataDamageAdd damageBuff;
	}
}
