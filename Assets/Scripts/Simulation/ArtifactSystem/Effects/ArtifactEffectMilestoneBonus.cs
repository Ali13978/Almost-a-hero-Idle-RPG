using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectMilestoneBonus : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.milestoneBonusFactor += this.GetBonus(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_MILESTONE_BONUS");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetBonus(copiesAmount), false);
		}

		private double GetBonus(int copiesAmount)
		{
			return 0.05 * (double)copiesAmount;
		}

		private const double BASE_FACTOR = 0.05;
	}
}
