using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectHorseshoeValue : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.horseshoeValueFactor += this.GetValueIncrement(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_HORSESHOE_VALUE");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetValueIncrement(copiesAmount), false);
		}

		private float GetValueIncrement(int copiesAmount)
		{
			return 1f * (float)copiesAmount;
		}

		private const float VALUE_INC = 1f;
	}
}
