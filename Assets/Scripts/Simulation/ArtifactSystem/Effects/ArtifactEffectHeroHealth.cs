using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectHeroHealth : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override string GetNextLevelIncreaseString(int level, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetValue(level + 1, universalTotalBonus) - this.GetValue(level, universalTotalBonus), false);
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int level)
		{
			universalTotalBonus.healthHeroFactor += this.GetValue(level, universalTotalBonus);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_HEALTH_HERO");
		}

		public override string GetValueString(int level, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetValue(level, universalTotalBonus), false);
		}

		public override double GetValue(int level, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetHealthFactor(level) * universalTotalBonus.commonArtifactFactor * universalTotalBonus.commonArtifactHealthFactor;
		}

		private double GetHealthFactor(int level)
		{
			return 0.5 + GameMath.PowDouble(1.1, (double)level) - 1.0;
		}

		private const double HEALTH_FACTOR_BASE = 0.5;

		private const double HEALTH_FACTOR_EXP = 1.1;
	}
}
