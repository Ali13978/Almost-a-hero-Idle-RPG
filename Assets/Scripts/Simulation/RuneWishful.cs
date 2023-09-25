using System;

namespace Simulation
{
	public class RuneWishful : Rune
	{
		public RuneWishful(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataWishful(RuneWishful.DURATION_RECHARGE), RuneIds.EARTH_WISHFUL, "RUNE_NAME_WISHFUL", "RUNE_DESC_WISHFUL", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetTimeInMilliSecondsString(RuneWishful.DURATION_RECHARGE)), AM.csr(GameMath.GetPercentString(RuneWishful.MAX_DURATION_RECHARGE_RATIO, false)));
		}

		public static float DURATION_RECHARGE = 0.2f;

		public static float MAX_DURATION_RECHARGE_RATIO = 0.5f;
	}
}
