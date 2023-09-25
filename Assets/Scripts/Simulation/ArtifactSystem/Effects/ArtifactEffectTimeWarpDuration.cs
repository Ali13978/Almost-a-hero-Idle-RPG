using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectTimeWarpDuration : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.timeWarpDurationAdd += (float)this.GetDurationIncrement(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_TIME_WARP_DURATION");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetTimeInSecondsString((float)this.GetDurationIncrement(copiesAmount));
		}

		private int GetDurationIncrement(int copiesAmount)
		{
			return 150 * copiesAmount;
		}

		private const int DURATION_BASE = 150;
	}
}
