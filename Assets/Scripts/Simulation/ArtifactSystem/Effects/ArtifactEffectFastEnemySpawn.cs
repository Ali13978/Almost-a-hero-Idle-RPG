using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectFastEnemySpawn : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.fastEnemySpawnBelow += this.GetStageIncreaseForFastSpawn(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_FAST_SPAWN");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetStageIncreaseForFastSpawn(copiesAmount).ToString();
		}

		private int GetStageIncreaseForFastSpawn(int copiesAmount)
		{
			return 10000 * copiesAmount;
		}

		private const int STAGES_BASE = 10000;
	}
}
