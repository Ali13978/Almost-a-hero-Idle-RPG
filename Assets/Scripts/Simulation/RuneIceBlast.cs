using System;

namespace Simulation
{
	public class RuneIceBlast : Rune
	{
		public RuneIceBlast(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataIceBlast(RuneIceBlast.SPEED_FACTOR), RuneIds.ICE_ICE_BLAST, "RUNE_NAME_ICE_BLAST", "RUNE_DESC_ICE_BLAST", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(LM.Get("RING_SPECIAL_ICE")), AM.csr(GameMath.GetPercentString(RuneIceBlast.SPEED_FACTOR, false)));
		}

		public static float SPEED_FACTOR = 0.5f;
	}
}
