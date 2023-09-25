using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectNonBossHealth : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Multiplier;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.healthEnemyFactor *= this.GetValue(copiesAmount, universalTotalBonus);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_HEALTH_ENEMY");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetDetailedNumberString(this.GetValue(copiesAmount, universalTotalBonus));
		}

		private double GetHealthFactor(int copiesAmount)
		{
			return GameMath.GetTotalDiscountFactor(0.37, copiesAmount);
		}

		public override double GetValue(int level, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetHealthFactor(level);
		}

		private const double BASE_DISCOUNT = 0.37;
	}
}
