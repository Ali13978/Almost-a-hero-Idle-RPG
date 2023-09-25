using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectHeroNonSkillDamage : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.damageHeroNonSkillFactor += (double)this.GetDamageFactor(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_DAMAGE_HERO_NONSKILL");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return GameMath.GetPercentString(this.GetDamageFactor(copiesAmount), false);
		}

		private float GetDamageFactor(int copiesAmount)
		{
			return 0.3f * (float)copiesAmount;
		}

		private const float BASE_CRIT_FACTOR = 0.3f;
	}
}
