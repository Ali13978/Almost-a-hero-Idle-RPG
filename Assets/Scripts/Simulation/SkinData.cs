using System;

namespace Simulation
{
	public class SkinData : IComparable<SkinData>
	{
		public bool isHalloweenSkin
		{
			get
			{
				return this.family == SkinFamily.Halloween;
			}
		}

		public bool isChristmasSkin
		{
			get
			{
				return this.family == SkinFamily.Christmas;
			}
		}

		public bool isSecondAnniversarySkin
		{
			get
			{
				return this.family == SkinFamily.SecondAnniversary;
			}
		}

		public string GetName()
		{
			if (this.index <= 7)
			{
				return string.Format(LM.Get("HERO_EVOLVE_SKIN_COMBINER"), LM.Get(this.nameKey), LM.Get(this.belongsTo.nameKey));
			}
			return LM.Get(this.nameKey);
		}

		public string GetKey()
		{
			if (this.index <= 7)
			{
				return this.belongsTo.nameKey + "_" + this.nameKey;
			}
			return this.nameKey;
		}

		public string GetDesc()
		{
			return LM.Get(this.descKey);
		}

		public string GetUnlockHint()
		{
			if (this.unlockType == SkinData.UnlockType.HERO_EVOLVE_LEVEL)
			{
				return this.unlockRequirement.GetReqString(false);
			}
			return this.unlockRequirement.GetReqString(true);
		}

		public bool CanAfford(double bid)
		{
			return this.cost <= bid;
		}

		public bool IsUnlockRequirementSatisfied(Simulator sim)
		{
			return (this.isHalloweenSkin && sim.halloweenEnabled) || (this.isChristmasSkin && sim.IsChristmasTreeEnabled()) || (this.isSecondAnniversarySkin && sim.IsSecondAnniversaryEventEnabled()) || this.unlockRequirement == null || this.unlockRequirement.IsSatisfied(sim, this);
		}

		public bool WillSkinUnlockOnHeroLevel(int level)
		{
			if (this.unlockRequirement != null && this.unlockRequirement is SkinUnlockReqHeroLevel)
			{
				SkinUnlockReqHeroLevel skinUnlockReqHeroLevel = this.unlockRequirement as SkinUnlockReqHeroLevel;
				if (level >= skinUnlockReqHeroLevel.targetEvolveLevel)
				{
					return true;
				}
			}
			return false;
		}

		public int CompareTo(SkinData obj)
		{
			SkinUnlockReqHeroLevel skinUnlockReqHeroLevel = this.unlockRequirement as SkinUnlockReqHeroLevel;
			SkinUnlockReqHeroLevel skinUnlockReqHeroLevel2 = obj.unlockRequirement as SkinUnlockReqHeroLevel;
			if (obj == null)
			{
				return 1;
			}
			if (skinUnlockReqHeroLevel != null && skinUnlockReqHeroLevel2 != null)
			{
				return skinUnlockReqHeroLevel2.targetEvolveLevel - skinUnlockReqHeroLevel.targetEvolveLevel;
			}
			return obj.index - this.index;
		}

		public HeroDataBase belongsTo;

		public int id;

		public string descKey = "blabla";

		public string nameKey = "NAME";

		public bool isNew = true;

		public SkinFamily family;

		public int index;

		public SkinUnlockReq unlockRequirement;

		public SkinData.UnlockType unlockType;

		public CurrencyType currency;

		public double cost;

		public enum UnlockType
		{
			HERO_EVOLVE_LEVEL,
			CURRENCY
		}
	}
}
