using System;

namespace Simulation.ArtifactSystem.Effects
{
	public class ArtifactEffectFreeChestNumItems : ArtifactEffect
	{
		public override EffectOperation GetEffectOperation()
		{
			return EffectOperation.Increaser;
		}

		public override void Apply(UniversalTotalBonus universalTotalBonus, int copiesAmount)
		{
			universalTotalBonus.heroItemsInFreeChestAdd += this.GetItemsToAdd(copiesAmount);
		}

		public override string GetDescription()
		{
			return LM.Get("ARTIFACT_EFFECT_HERO_ITEMS_IN_FREE_CHEST");
		}

		public override string GetValueString(int copiesAmount, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetItemsToAdd(copiesAmount).ToString();
		}

		private int GetItemsToAdd(int copiesAmount)
		{
			return copiesAmount;
		}

		private const int BASE_ITEMS_AMOUNT = 1;
	}
}
