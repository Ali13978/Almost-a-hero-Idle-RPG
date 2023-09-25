using System;

namespace Simulation
{
	public class RuneExplosive : Rune
	{
		public RuneExplosive(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataExplosive(RuneExplosive.DURATION, RuneExplosive.CHANCE), RuneIds.FIRE_EXPLOSIVE, "RUNE_NAME_EXPLOSIVE", "RUNE_DESC_EXPLOSIVE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneExplosive.CHANCE, false)), AM.csr(GameMath.GetTimeLessDetailedString((double)RuneExplosive.DURATION, false)), AM.csr(LM.Get("RING_SPECIAL_FIRE")));
		}

		public static float DURATION = 10f;

		public static float CHANCE = 0.3f;
	}
}
