using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectGoldBagValue : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.goldBagValueFactor += this.GetGoldIncrease(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_GOLD_BAG_VALUE");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetGoldIncrease(copiesAmount), false);
		}

		private float GetGoldIncrease(int copiesAmount)
		{
			return 0.75f * (float)copiesAmount;
		}

		private const float BASE_FACTOR = 0.75f;
	}
}
