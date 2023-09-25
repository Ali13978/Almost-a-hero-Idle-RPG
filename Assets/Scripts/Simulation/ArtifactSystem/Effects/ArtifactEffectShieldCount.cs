using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectShieldCount : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.shieldCountAdd += this.GetAmountToAdd(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_SHIELD_COUNT");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetAmountToAdd(copiesAmount).ToString();
		}

		private int GetAmountToAdd(int copiesAmount)
		{
			return 3 * copiesAmount;
		}

		private const int AMOUNT_BASE = 3;
	}
}
