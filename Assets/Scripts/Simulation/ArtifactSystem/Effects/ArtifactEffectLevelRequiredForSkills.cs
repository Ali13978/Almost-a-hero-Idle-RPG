using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectLevelRequiredForSkills : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Reducer;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.heroLevelRequiredForSkillDecrease += this.GetLevelRequirementDecrease(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_HERO_LEVEL_REQ_FOR_SKILL");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			int levelRequirementDecrease = this.GetLevelRequirementDecrease(copiesAmount);
			return (levelRequirementDecrease <= 0) ? "0" : levelRequirementDecrease.ToString();
		}

		private int GetLevelRequirementDecrease(int copiesAmount)
		{
			return 3 * copiesAmount;
		}

		private const int LEVEL_REQ_DEC_BASE = 3;
	}
}
