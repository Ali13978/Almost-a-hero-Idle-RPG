using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectHorseshoeDuration : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.horseshoeDurationAdd += (float)this.GetDurationIncrement(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_HORSESHOE_DURATION");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetTimeInSecondsString((float)this.GetDurationIncrement(copiesAmount));
		}

		private int GetDurationIncrement(int copiesAmount)
		{
			return 200 * copiesAmount;
		}

		private const int DURATION_BASE = 200;
	}
}
