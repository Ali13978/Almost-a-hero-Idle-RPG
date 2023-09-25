using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectDragonSpawnRate : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.dragonSpawnRateFactor += this.GetSpawnRateIncreaseRate(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_DRONE_SPAWN_RATE");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetSpawnRateIncreaseRate(copiesAmount), false);
		}

		private float GetSpawnRateIncreaseRate(int copiesAmount)
		{
			return 0.3f * (float)copiesAmount;
		}

		private const float BASE_SPAWN_INCREASE_RATE = 0.3f;
	}
}
