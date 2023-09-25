using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectNonBossWaveSkipChance : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.waveSkipChanceAdd += this.GetChanceIncreaseFactor(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_WAVE_SKIP_CHANCE");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetChanceIncreaseFactor(copiesAmount), false);
		}

		private float GetChanceIncreaseFactor(int copiesAmount)
		{
			return 0.3f * (float)copiesAmount;
		}

		private const float BASE_FACTOR = 0.3f;
	}
}
