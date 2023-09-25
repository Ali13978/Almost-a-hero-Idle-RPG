using System;

namespace Simulation
{
	public class BuffDataIceRage : BuffData
	{
		public BuffDataIceRage(float manaInc)
		{
			this.manaInc = manaInc;
			this.id = 106;
		}

		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			buff.GetBy().AddIceMana(this.manaInc * 100f);
		}

		private float manaInc;
	}
}
