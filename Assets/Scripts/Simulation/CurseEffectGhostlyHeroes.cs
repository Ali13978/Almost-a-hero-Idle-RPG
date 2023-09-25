using System;

namespace Simulation
{
	public class CurseEffectGhostlyHeroes : CurseEffectDataPermanent
	{
		public CurseEffectGhostlyHeroes()
		{
			this.baseData = new CurseDataBase
			{
				id = 1019,
				nameKey = "CURSE_GHOSTLY_HEROES_NAME",
				conditionKey = "CHARM_CONDITION_PERMANENT",
				descKey = "CURSE_GHOSTLY_HEROES_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffGhostlyHeroes curseBuff = new CurseBuffGhostlyHeroes
			{
				enchantmentData = this
			};
			challenge.AddCurseBuff(curseBuff);
		}

		public override string GetDesc()
		{
			return LM.Get(base.descKey);
		}

		public override string GetConditionDescription()
		{
			return LM.Get(base.conditionKey);
		}

		public override string GetConditionDescriptionNoColor()
		{
			return LM.Get(base.conditionKey);
		}

		public override float GetWeight()
		{
			return 0.3f;
		}
	}
}
