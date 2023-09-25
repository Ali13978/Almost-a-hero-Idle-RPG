using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectRingCritDamage : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.critFactorTotemAdd += (double)this.GetCritFactor(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_CRIT_FACTOR_TOTEM");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetCritFactor(copiesAmount), false);
		}

		private float GetCritFactor(int copiesAmount)
		{
			return 0.5f * (float)copiesAmount;
		}

		private const float BASE_CRIT_FACTOR = 0.5f;
	}
}
