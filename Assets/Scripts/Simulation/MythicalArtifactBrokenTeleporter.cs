using System;
using System.Linq;
using Static;

namespace Simulation
{
	public class MythicalArtifactBrokenTeleporter : MythicalArtifactEffect
	{
		static MythicalArtifactBrokenTeleporter()
		{
			MythicalArtifactBrokenTeleporter.skipLimitsBase = new int[]
			{
				100,
				200,
				300
			};
			MythicalArtifactBrokenTeleporter.skipCountsBase = new int[]
			{
				3,
				2,
				1
			};
			MythicalArtifactBrokenTeleporter.levelIncreases = new int[MythicalArtifactBrokenTeleporter.MAX_RANK];
			for (int i = 0; i < MythicalArtifactBrokenTeleporter.MAX_RANK; i++)
			{
				if (i < 30)
				{
					MythicalArtifactBrokenTeleporter.levelIncreases[i] = 10;
				}
				else if (i < 80)
				{
					if (i % 3 == 0)
					{
						MythicalArtifactBrokenTeleporter.levelIncreases[i] = 10;
					}
					else if (i % 3 == 1)
					{
						MythicalArtifactBrokenTeleporter.levelIncreases[i] = 10;
					}
					else if (i % 3 == 2)
					{
						MythicalArtifactBrokenTeleporter.levelIncreases[i] = 15;
					}
				}
				else if (i < 160)
				{
					if (i % 3 == 0)
					{
						MythicalArtifactBrokenTeleporter.levelIncreases[i] = 10;
					}
					else if (i % 3 == 1)
					{
						MythicalArtifactBrokenTeleporter.levelIncreases[i] = 15;
					}
					else if (i % 3 == 2)
					{
						MythicalArtifactBrokenTeleporter.levelIncreases[i] = 20;
					}
				}
				else if (i < 240)
				{
					if (i % 3 == 0)
					{
						MythicalArtifactBrokenTeleporter.levelIncreases[i] = 15;
					}
					else if (i % 3 == 1)
					{
						MythicalArtifactBrokenTeleporter.levelIncreases[i] = 20;
					}
					else if (i % 3 == 2)
					{
						MythicalArtifactBrokenTeleporter.levelIncreases[i] = 25;
					}
				}
				else if (i % 3 == 0)
				{
					MythicalArtifactBrokenTeleporter.levelIncreases[i] = 20;
				}
				else if (i % 3 == 1)
				{
					MythicalArtifactBrokenTeleporter.levelIncreases[i] = 25;
				}
				else if (i % 3 == 2)
				{
					MythicalArtifactBrokenTeleporter.levelIncreases[i] = 30;
				}
			}
			MythicalArtifactBrokenTeleporter.calculatedLevelLimits = new int[MythicalArtifactBrokenTeleporter.levelIncreases.Length + 1][];
			int[] array = MythicalArtifactBrokenTeleporter.skipLimitsBase.ToArray<int>();
			MythicalArtifactBrokenTeleporter.calculatedLevelLimits[0] = new int[]
			{
				array[0],
				array[1],
				array[2]
			};
			for (int j = 0; j < MythicalArtifactBrokenTeleporter.levelIncreases.Length; j++)
			{
				array[j % 3] += MythicalArtifactBrokenTeleporter.levelIncreases[j];
				MythicalArtifactBrokenTeleporter.calculatedLevelLimits[j + 1] = new int[]
				{
					array[0],
					array[1],
					array[2]
				};
			}
		}

		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.stageSkipPair = this.skipPairs;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.BROKEN_TELEPORTER;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactBrokenTeleporter mythicalArtifactBrokenTeleporter = new MythicalArtifactBrokenTeleporter();
			mythicalArtifactBrokenTeleporter.SetRank(base.GetRank());
			return mythicalArtifactBrokenTeleporter;
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank) / 1000.0;
		}

		public override string GetName()
		{
			return MythicalArtifactBrokenTeleporter.GetNameStatic();
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_STAGE_SKIP_CHANCE");
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_STAGE_SKIP_CHANCE");
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			if (this.skipPairs != null)
			{
				return (double)this.skipPairs.skipCounts[0];
			}
			return 0.0;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactBrokenTeleporter.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			int[] array = MythicalArtifactBrokenTeleporter.calculatedLevelLimits[rank];
			int[] array2 = new int[3];
			if (rank + levelDiff < MythicalArtifactBrokenTeleporter.calculatedLevelLimits.Length)
			{
				int[] array3 = MythicalArtifactBrokenTeleporter.calculatedLevelLimits[rank + levelDiff];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = array3[i] - array[i];
				}
			}
			int num = array.Length;
			string text = "ARTIFACT_EFFECT_BROKEN_TELEPORTER_DESC".LocFormat(GameMath.GetTimeInSecondsString(MythicalArtifactBrokenTeleporter.timeLimit));
			text += "\n";
			text += "\n";
			for (int j = 0; j < num; j++)
			{
				int num2 = array[j];
				int num3 = MythicalArtifactBrokenTeleporter.skipCountsBase[j];
				string key = (num3 >= 2) ? "ARTIFACT_EFFECT_BROKEN_TELEPORTER_DESC_LAYER_PLURAL" : "ARTIFACT_EFFECT_BROKEN_TELEPORTER_DESC_LAYER";
				int num4 = array2[j];
				if (num4 != 0)
				{
					text += key.LocFormat(num2 + AM.csart(" (+" + num4 + ")"), num3);
				}
				else
				{
					text += key.LocFormat(num2, num3);
				}
				text += "\n";
			}
			return text;
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactBrokenTeleporter.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactBrokenTeleporter.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.StageSkipChance;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactBrokenTeleporter.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactBrokenTeleporter.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactBrokenTeleporter.MAX_RANK, rank);
			this.skipPairs = new LevelSkipPairs
			{
				skipLimits = MythicalArtifactBrokenTeleporter.calculatedLevelLimits[rank],
				skipCounts = MythicalArtifactBrokenTeleporter.skipCountsBase,
				timeLimit = MythicalArtifactBrokenTeleporter.timeLimit
			};
		}

		public override bool CanBeDisabled()
		{
			return true;
		}

		private LevelSkipPairs skipPairs;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		private static float timeLimit = 15f;

		private static readonly int[] skipLimitsBase;

		private static readonly int[] skipCountsBase;

		private static readonly int[][] calculatedLevelLimits;

		private static readonly int[] levelIncreases;

		public static int MAX_RANK = 299;
	}
}
