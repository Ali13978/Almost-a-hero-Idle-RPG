using System;

namespace Simulation
{
	public class RuneRecycle : Rune
	{
		public RuneRecycle(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataRecycleEarth(RuneRecycle.DURATION_RECHARGE), RuneIds.EARTH_RECYCLE, "RUNE_NAME_RECYCLE", "RUNE_DESC_RECYCLE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetTimeInSecondsString(RuneRecycle.DURATION_RECHARGE)), AM.csr(LM.Get("RING_SPECIAL_EARTH")));
		}

		public static float DURATION_RECHARGE = 1.5f;
	}
}
