using System;

namespace Simulation
{
	public class BuffDataForge : BuffData
	{
		public override void OnNewStage(Buff buff)
		{
			buff.GetBy().AddBuff(this.effect, 0, false);
		}

		public BuffDataDamageAdd effect;
	}
}
