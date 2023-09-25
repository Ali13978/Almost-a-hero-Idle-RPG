using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectBossDamage : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Multiplier;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.damageBossFactor *= this.GetValue(copiesAmount, universalTotalBonus);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_DAMAGE_BOSS");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetDetailedNumberString(this.GetValue(copiesAmount, universalTotalBonus));
		}

		public override double GetValue(int level, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetDamageFactor(level);
		}

		private double GetDamageFactor(int copiesAmount)
		{
			return GameMath.GetTotalDiscountFactor(0.37000000476837158, copiesAmount);
		}

		private const double BASE_DISCOUNT = 0.37000000476837158;
	}
}
