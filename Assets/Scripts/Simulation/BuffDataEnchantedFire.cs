using System;

namespace Simulation
{
	public class BuffDataEnchantedFire : BuffData
	{
		public BuffDataEnchantedFire(float reduction)
		{
			this.reduction = reduction;
			this.id = 74;
		}

		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			buff.GetBy().MultiplyTotemHeat(1f - this.reduction);
		}

		private float reduction;
	}
}
