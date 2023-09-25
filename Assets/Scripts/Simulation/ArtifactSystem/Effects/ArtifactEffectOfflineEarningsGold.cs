using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectOfflineEarningsGold : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.goldOfflineFactor += this.GetGoldIncrease(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_GOLD_OFFLINE");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetGoldIncrease(copiesAmount), false);
		}

		private double GetGoldIncrease(int copiesAmount)
		{
			return 0.2 * (double)copiesAmount;
		}

		private const double BASE_FACTOR = 0.2;
	}
}
