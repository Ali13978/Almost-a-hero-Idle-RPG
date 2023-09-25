using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectEpicBossMythstoneDrop : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Multiplier;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.epicBossDropMythstonesFactor += this.GetValue(copiesAmount, universalTotalBonus);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_EPIC_BOSS_DROP_MYTHSTONE");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetDetailedNumberString(this.GetValue(copiesAmount, universalTotalBonus));
		}

		public override double GetValue(int level, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetDropIncreaseFactor(level);
		}

		private double GetDropIncreaseFactor(int copiesAmount)
		{
			return GameMath.GetTotalIncreaseFactor(0.3298, copiesAmount);
		}

		private const double BASE_DROP_INCREASE_RATE = 0.3298;
	}
}
