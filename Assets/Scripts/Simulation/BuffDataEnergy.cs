using System;

namespace Simulation
{
	public class BuffDataEnergy : BuffData
	{
		public BuffDataEnergy(int chargeToEarn)
		{
			this.chargeToEarn = chargeToEarn;
			this.id = 76;
		}

		public override void OnOpponentDeath(Buff buff, UnitHealthy dead)
		{
			buff.GetBy().AddCharge(this.chargeToEarn);
		}

		public int chargeToEarn;
	}
}
