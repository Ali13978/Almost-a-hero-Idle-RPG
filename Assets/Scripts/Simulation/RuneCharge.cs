using System;

namespace Simulation
{
	public class RuneCharge : Rune
	{
		public RuneCharge(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataCharge(RuneCharge.CHARGE_REQ_DEC), RuneIds.LIGHTNING_CHARGE, "RUNE_NAME_CHARGE", "RUNE_DESC_CHARGE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(LM.Get("RING_SPECIAL_LIGHTNING")), AM.csr(RuneCharge.CHARGE_REQ_DEC.ToString()));
		}

		public static int CHARGE_REQ_DEC = 10;
	}
}
