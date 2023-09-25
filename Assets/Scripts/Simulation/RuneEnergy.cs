using System;

namespace Simulation
{
	public class RuneEnergy : Rune
	{
		public RuneEnergy(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataEnergy(RuneEnergy.CHARGE_TO_EARN), RuneIds.LIGHTNING_ENERGY, "RUNE_NAME_ENERGY", "RUNE_DESC_ENERGY", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(LM.Get("RING_SPECIAL_LIGHTNING")), AM.csr(RuneEnergy.CHARGE_TO_EARN.ToString()));
		}

		public static int CHARGE_TO_EARN = 10;
	}
}
