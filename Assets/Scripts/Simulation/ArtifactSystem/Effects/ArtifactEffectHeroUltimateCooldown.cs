using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectHeroUltimateCooldown : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Reducer;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.ultiCoolDownMaxFactor *= this.GetUltiCooldownReductionFactor(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_ULTI_COOLDOWN");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(1f - this.GetUltiCooldownReductionFactor(copiesAmount), false);
		}

		private float GetUltiCooldownReductionFactor(int copiesAmount)
		{
			return GameMath.GetTotalDiscountFactor(0.2f, copiesAmount);
		}

		private const float BASE_REDUCTION_PERCENT = 0.2f;
	}
}
