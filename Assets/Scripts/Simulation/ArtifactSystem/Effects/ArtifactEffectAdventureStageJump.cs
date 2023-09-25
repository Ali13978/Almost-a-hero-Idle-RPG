using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectAdventureStageJump : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.stagesToJumpInAdventure += this.GetStagesToJump(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_ADVENTURE_STAGE_JUMP");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetStagesToJump(copiesAmount).ToString();
		}

		private int GetStagesToJump(int copiesAmount)
		{
			return 200 * copiesAmount;
		}

		private const int BASE_STAGES = 200;
	}
}
