using System;
using System.Collections.Generic;

namespace Simulation
{
	public abstract class ArtifactEffect
	{
		public abstract bool IsLimited();

		public abstract double GetReqMinQuality();

		public abstract float GetChanceWeight();

		public abstract double GetAmountMin();

		public abstract double GetAmountMax();

		public abstract double GetAmountAllowed(List<Artifact> otherArtifacts);

		public abstract void SetQuality(double quality);

		public virtual void PreApply(UniversalTotalBonus totBonus)
		{
		}

		public virtual void CalculateAdditionalBonuses(UniversalTotalBonus totBonus, int artifactRarity)
		{
		}

		public abstract void Apply(UniversalTotalBonus totBonus);

		public virtual void Apply(UniversalTotalBonus universalTotalBonus, int artifactsTotalLevel)
		{
			this.Apply(universalTotalBonus);
		}

		public abstract double GetAmount();

		public abstract string GetAmountString();

		public abstract double GetQuality();

		public abstract double GetQuality(double amount);

		public abstract string GetStringSelf(int levelDiff);

		public abstract ArtifactEffectCategory GetCategorySelf();

		public abstract ArtifactEffectType GetEffectTypeSelf();

		public abstract ArtifactEffect GetCopy();

		public abstract int GetLevel();

		public static int LEVEL_BASIC;

		public static int LEVEL_COMMON = 1;

		public static int LEVEL_UNCOMMON = 2;

		public static int LEVEL_RARE = 3;

		public static int LEVEL_EPIC = 4;

		public static int LEVEL_LEGENDARY = 5;

		public static int LEVEL_MYTHICAL = 6;
	}
}
