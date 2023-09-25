using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectAutoTapDuration : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.autoTapDurationAdd += (float)this.GetDurationIncrement(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_AUTO_TAP_TIME");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetTimeInSecondsString((float)this.GetDurationIncrement(copiesAmount));
		}

		private int GetDurationIncrement(int copiesAmount)
		{
			return 300 * copiesAmount;
		}

		private const int DURATION_BASE = 300;
	}
}
