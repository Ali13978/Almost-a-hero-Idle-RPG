using System;

namespace Simulation
{
	public class RuneStinger : Rune
	{
		public RuneStinger(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataStinger(RuneStinger.DELAY), RuneIds.ICE_STINGER, "RUNE_NAME_STINGER", "RUNE_DESC_STINGER", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetTimeInMilliSecondsString(RuneStinger.DELAY)));
		}

		public static float DELAY = 0.2f;
	}
}
