using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectShieldDuration : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.shieldDurationAdd += (float)this.GetDurationIncrement(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_SHIELD_DURATION");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetTimeInSecondsString((float)this.GetDurationIncrement(copiesAmount));
		}

		private int GetDurationIncrement(int copiesAmount)
		{
			return 60 * copiesAmount;
		}

		private const int DURATION_BASE = 60;
	}
}
