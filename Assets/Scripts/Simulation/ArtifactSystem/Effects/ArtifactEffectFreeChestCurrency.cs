using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectFreeChestCurrency : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.currencyInFreeChestFactor += this.GetCurrencyFactor(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_CURRENCY_FACTOR_IN_FREE_CHEST");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetCurrencyFactor(copiesAmount), false);
		}

		private double GetCurrencyFactor(int copiesAmount)
		{
			return 0.5 * (double)copiesAmount;
		}

		private const double BASE_CURRENCY_INCREASE_FACTOR = 0.5;
	}
}
