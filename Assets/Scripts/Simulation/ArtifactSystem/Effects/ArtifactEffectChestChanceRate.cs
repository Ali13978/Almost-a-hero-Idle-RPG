using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectChestChanceRate : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.chestChanceFactor += this.GetChanceIncrease(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_CHEST_CHANCE");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetChanceIncrease(copiesAmount), false);
		}

		private float GetChanceIncrease(int copiesAmount)
		{
			return 0.5f * (float)copiesAmount;
		}

		private const float BASE_FACTOR = 0.5f;
	}
}
