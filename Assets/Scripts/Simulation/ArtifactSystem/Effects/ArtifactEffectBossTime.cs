using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectBossTime : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.bossTimeAdd += (float)this.GetTimeIncrement(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_BOSS_TIME");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetTimeInSecondsString((float)this.GetTimeIncrement(copiesAmount));
		}

		private int GetTimeIncrement(int copiesAmount)
		{
			return 20 * copiesAmount;
		}

		private const int TIME_BASE = 20;
	}
}
