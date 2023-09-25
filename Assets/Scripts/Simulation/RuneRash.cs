using System;

namespace Simulation
{
	public class RuneRash : Rune
	{
		public RuneRash(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataRash(RuneRash.DELAY, RuneRash.CHARGE_REQUIRED), RuneIds.LIGHTNING_RASH, "RUNE_NAME_RASH", "RUNE_DESC_RASH", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetTimeInMilliSecondsString(RuneRash.DELAY)), AM.csr(LM.Get("RING_SPECIAL_LIGHTNING")), AM.csr(RuneRash.CHARGE_REQUIRED.ToString()));
		}

		public static float DELAY = 0.2f;

		public static int CHARGE_REQUIRED = 5;
	}
}
