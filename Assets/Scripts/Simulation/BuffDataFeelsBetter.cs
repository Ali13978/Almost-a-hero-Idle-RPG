using System;

namespace Simulation
{
	public class BuffDataFeelsBetter : BuffData
	{
		public override void OnReviveAlly(Buff buff, UnitHealthy revived)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			unitHealthy.GainShield(this.shieldPercentage, 1000f);
			unitHealthy.AddVisualBuff(3f, 256);
		}

		public double shieldPercentage;
	}
}
