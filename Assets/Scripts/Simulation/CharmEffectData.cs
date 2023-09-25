using System;

namespace Simulation
{
	public abstract class CharmEffectData : EnchantmentEffectData
	{
		public CharmDataBase BaseData
		{
			get
			{
				return this.baseData as CharmDataBase;
			}
			set
			{
				this.baseData = value;
			}
		}

		public abstract int GetNumPacksRequired();

		public abstract string GetDesc(bool showUpgrade);

		public abstract string GetActivationDesc(bool showUpgrade);

		public void SpendDuplicated()
		{
			this.level = CharmEffectData.ConvertDuplicateToLevel(this.level, this.BaseData.maxLevel, ref this.unspendDuplicates);
		}

		public bool CanLevelUp()
		{
			if (this.level == -1)
			{
				return false;
			}
			int neededDuplicateToLevelUpFromLevel = CharmEffectData.GetNeededDuplicateToLevelUpFromLevel(this.level);
			return neededDuplicateToLevelUpFromLevel <= this.unspendDuplicates;
		}

		public void LevelUp()
		{
			int neededDuplicateToLevelUpFromLevel = CharmEffectData.GetNeededDuplicateToLevelUpFromLevel(this.level);
			if (neededDuplicateToLevelUpFromLevel <= this.unspendDuplicates)
			{
				this.unspendDuplicates -= neededDuplicateToLevelUpFromLevel;
				this.level++;
			}
		}

		public string GetProgresString()
		{
			int neededDuplicateToLevelUpFromLevel = CharmEffectData.GetNeededDuplicateToLevelUpFromLevel(this.level);
			return this.unspendDuplicates + "/" + neededDuplicateToLevelUpFromLevel;
		}

		public float GetProgress()
		{
			int neededDuplicateToLevelUpFromLevel = CharmEffectData.GetNeededDuplicateToLevelUpFromLevel(this.level);
			return GameMath.Clamp((float)this.unspendDuplicates / (float)neededDuplicateToLevelUpFromLevel, 0f, 1f);
		}

		public bool IsMaxed()
		{
			return this.level >= this.BaseData.maxLevel - 1;
		}

		public static double GetNeededScrapToLevelUpFromLevel(int currentLevel)
		{
			return CharmEffectData.SCRAPS_NEEDED_FOR_LEVEL_UP[currentLevel];
		}

		public static int GetNeededDuplicateToLevelUpFromLevel(int currentLevel)
		{
			if (currentLevel == -1)
			{
				return 0;
			}
			return CharmEffectData.DUPLICATES_NEEDED_FOR_LEVEL_UP[currentLevel];
		}

		public static int GetNeededDuplicateToLevelUpToTargetLevel(int currentLevel, int targetLevel)
		{
			int neededDuplicateToLevelUpFromLevel = CharmEffectData.GetNeededDuplicateToLevelUpFromLevel(currentLevel);
			int neededDuplicateToLevelUpFromLevel2 = CharmEffectData.GetNeededDuplicateToLevelUpFromLevel(targetLevel);
			int num = targetLevel - currentLevel;
			return num * (neededDuplicateToLevelUpFromLevel + neededDuplicateToLevelUpFromLevel2) / 2;
		}

		public static int ConvertDuplicateToLevel(int currentLevel, int maxLevel, ref int duplicateCount)
		{
			int num = currentLevel;
			int neededDuplicateToLevelUpFromLevel = CharmEffectData.GetNeededDuplicateToLevelUpFromLevel(num);
			while (duplicateCount >= neededDuplicateToLevelUpFromLevel)
			{
				duplicateCount -= neededDuplicateToLevelUpFromLevel;
				if (num >= maxLevel - 1)
				{
					break;
				}
				num++;
			}
			return num;
		}

		public double GetUniversalBonus()
		{
			return GameMath.PowDouble(CharmEffectData.GLOBAL_BONUS_PER_LEVEL, (double)(this.level + 1));
		}

		public static double GetUniversalBonus(int level)
		{
			return GameMath.PowDouble(CharmEffectData.GLOBAL_BONUS_PER_LEVEL, (double)level);
		}

		public bool IsLocked()
		{
			return this.level < 0;
		}

		public override string GetConditionDescFormat()
		{
			return LM.Get("CHARM_CONDITION_DESC");
		}

		public static double GLOBAL_BONUS_PER_LEVEL = 1.15;

		public const float MAXED_CHARMS_DROP_WEIGHT_FACTOR = 0.05f;

		public const float LAST_OPENED_CHARMS_DROP_WEIGHT_FACTOR = 0.05f;

		public static int DuplicateIncrementer = 0;

		public static int BaseDuplicateNeeded = 1;

		public static int[] DUPLICATES_NEEDED_FOR_LEVEL_UP = new int[]
		{
			10,
			15,
			20,
			25,
			30,
			35,
			40,
			45,
			50,
			55,
			60,
			65,
			70,
			75,
			90,
			120,
			150,
			200,
			250,
			300,
			400,
			500,
			600,
			700
		};

		public static double[] SCRAPS_NEEDED_FOR_LEVEL_UP = new double[]
		{
			150.0,
			200.0,
			250.0,
			300.0,
			350.0,
			400.0,
			450.0,
			500.0,
			550.0,
			600.0,
			650.0,
			700.0,
			750.0,
			800.0,
			850.0,
			900.0,
			950.0,
			1000.0,
			1050.0,
			1200.0,
			1400.0,
			1600.0,
			1800.0,
			2000.0
		};

		public int unspendDuplicates;
	}
}
