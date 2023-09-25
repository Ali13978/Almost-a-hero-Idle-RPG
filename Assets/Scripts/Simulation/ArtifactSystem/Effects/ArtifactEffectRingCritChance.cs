using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectRingCritChance : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.critChanceTotemAdd += this.GetCritChance(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_CRIT_CHANCE_TOTEM");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetCritChance(copiesAmount), false);
		}

		private float GetCritChance(int copiesAmount)
		{
			return 0.1f * (float)copiesAmount;
		}

		private const float BASE_CRIT_CHANCE = 0.1f;
	}
}
