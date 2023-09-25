using System;

namespace Simulation
{
	public class BuffDataEnchantFire : BuffData
	{
		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			buff.GetBy().AddBuff(this.effect, 0, false);
		}

		public BuffDataCritChance effect;
	}
}
