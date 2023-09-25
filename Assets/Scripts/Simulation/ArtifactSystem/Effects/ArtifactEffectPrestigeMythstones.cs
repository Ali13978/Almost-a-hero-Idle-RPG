using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectPrestigeMythstones : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Multiplier;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.prestigeMythFactor *= this.GetValue(copiesAmount, universalTotalBonus);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_PRESTIGE_MYTH");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetDetailedNumberString(this.GetValue(copiesAmount, universalTotalBonus));
		}

		private double GetCurrencyFactor(int copiesAmount)
		{
			return GameMath.GetTotalIncreaseFactor(0.3298, copiesAmount);
		}

		public override double GetValue(int level, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetCurrencyFactor(level);
		}

		private const double BASE_MYTHSTONES_INCREASE_FACTOR = 0.3298;
	}
}
