using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectHeroUpgradeCost : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Multiplier;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.costHeroUpgradeFactor *= this.GetValue(copiesAmount, universalTotalBonus);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_COST_HERO_UPGRADE");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetDetailedNumberString(this.GetValue(copiesAmount, universalTotalBonus));
		}

		public override double GetValue(int level, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetUpgradeCostFactor(level);
		}

		private double GetUpgradeCostFactor(int copiesAmount)
		{
			return GameMath.GetTotalDiscountFactor(0.30000001192092896, copiesAmount);
		}

		private const double BASE_DISCOUNT = 0.30000001192092896;
	}
}
