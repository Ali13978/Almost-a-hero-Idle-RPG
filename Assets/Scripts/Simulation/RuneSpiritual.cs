using System;

namespace Simulation
{
	public class RuneSpiritual : Rune
	{
		public RuneSpiritual(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataSpiritual(), RuneIds.EARTH_SPIRITUAL, "RUNE_NAME_SPIRITUAL", "RUNE_DESC_SPIRITUAL", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(LM.Get("RING_SPECIAL_EARTH")));
		}
	}
}
