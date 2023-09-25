using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectArtifactUpgradeCost : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Multiplier;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.artifactUpgradeCostFactor *= this.GetUpgradeCostFactor(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_COST_ARTIFACT_UPGRADE");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetDetailedNumberString(this.GetUpgradeCostFactor(copiesAmount));
		}

		public override double GetValue(int level, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetUpgradeCostFactor(level);
		}

		private double GetUpgradeCostFactor(int copiesAmount)
		{
			return GameMath.GetTotalDiscountFactor(0.25, copiesAmount);
		}

		private const double BASE_DISCOUNT = 0.25;
	}
}
