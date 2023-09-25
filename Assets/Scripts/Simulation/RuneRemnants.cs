using System;

namespace Simulation
{
	public class RuneRemnants : Rune
	{
		public RuneRemnants(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataRemnants(RuneRemnants.DURATION_ADD), RuneIds.EARTH_REMNANTS, "RUNE_NAME_REMNANTS", "RUNE_DESC_REMNANTS", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), new object[]
			{
				AM.csr(RuneRemnants.NUM_METEORITES.ToString()),
				AM.csr(GameMath.GetPercentString(RuneRemnants.DAMAGE_RATIO, false)),
				AM.csr(GameMath.GetTimeInSecondsString(RuneRemnants.DURATION_ADD)),
				AM.csr(LM.Get("RING_SPECIAL_EARTH"))
			});
		}

		public static int NUM_METEORITES = 5;

		public static double DAMAGE_RATIO = 0.9;

		public static float DURATION_ADD = 5f;
	}
}
