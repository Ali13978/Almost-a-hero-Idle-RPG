using System;

namespace Simulation
{
	public class RuneNova : Rune
	{
		public RuneNova(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataNova(RuneNova.SLOW_DOWN_FACTOR, RuneNova.AREA), RuneIds.ICE_NOVA, "RUNE_NAME_NOVA", "RUNE_DESC_NOVA", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(LM.Get("RING_SPECIAL_ICE")), AM.csr(GameMath.GetPercentString(RuneNova.SLOW_DOWN_FACTOR, false)));
		}

		public static float SLOW_DOWN_FACTOR = 0.25f;

		public static float AREA = 0.5f;
	}
}
