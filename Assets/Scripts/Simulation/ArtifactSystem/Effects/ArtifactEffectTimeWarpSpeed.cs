using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectTimeWarpSpeed : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.timeWarpSpeedFactor += this.GetSpeedFactor(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_TIME_WARP_SPEED");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetSpeedFactor(copiesAmount), false);
		}

		private float GetSpeedFactor(int copiesAmount)
		{
			return 0.5f * (float)copiesAmount;
		}

		private const float BASE_SPEED_FACTOR = 0.5f;
	}
}
