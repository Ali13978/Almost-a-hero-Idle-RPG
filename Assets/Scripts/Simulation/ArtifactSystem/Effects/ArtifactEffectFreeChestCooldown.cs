using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectFreeChestCooldown : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Multiplier;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.freePackCooldownFactor *= (float)this.GetValue(copiesAmount, universalTotalBonus);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_FREE_PACK_COOLDOWN");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetDetailedNumberString(this.GetValue(copiesAmount, universalTotalBonus));
		}

		public override double GetValue(int level, UniversalTotalBonus universalTotalBonus)
		{
			return (double)this.GetCooldownReduction(level);
		}

		private float GetCooldownReduction(int copiesAmount)
		{
			return GameMath.GetTotalDiscountFactor(0.25f, copiesAmount);
		}

		private const float BASE_CD_REDUCTION = 0.25f;
	}
}
