using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectMilestoneCost : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Multiplier;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.milestoneCostFactor += this.GetValue(copiesAmount, universalTotalBonus);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_MILESTONE_COST");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetDetailedNumberString(this.GetValue(copiesAmount, universalTotalBonus));
		}

		private double GetUpgradeCostFactor(int copiesAmount)
		{
			return GameMath.GetTotalDiscountFactor(0.30000001192092896, copiesAmount);
		}

		public override double GetValue(int level, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetUpgradeCostFactor(level);
		}

		private const double BASE_DISCOUNT = 0.30000001192092896;
	}
}
