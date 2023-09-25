using System;

namespace Simulation
{
	public class RuneDaze : Rune
	{
		public RuneDaze(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataDaze(RuneDaze.DAMAGE_BOOST), RuneIds.LIGHTNING_DAZE, "RUNE_NAME_DAZE", "RUNE_DESC_DAZE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(LM.Get("RING_SPECIAL_LIGHTNING")), AM.csr(GameMath.GetPercentString(RuneDaze.DAMAGE_BOOST, false)));
		}

		public static double DAMAGE_BOOST = 0.2;
	}
}
