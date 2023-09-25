using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectHeroReviveTime : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Reducer;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.reviveTimeFactor *= this.GetReviveTimeReductionFactor(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_REVIVE_TIME");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(1f - this.GetReviveTimeReductionFactor(copiesAmount), false);
		}

		private float GetReviveTimeReductionFactor(int copiesAmount)
		{
			return GameMath.GetTotalDiscountFactor(0.2f, copiesAmount);
		}

		private const float BASE_REDUCTION_PERCENT = 0.2f;
	}
}
