using System;
using System.Collections.Generic;
using Simulation.ArtifactSystem.Effects;

namespace Simulation.ArtifactSystem
{
	public class EffectsDatabase
	{
		static EffectsDatabase()
		{
			foreach (KeyValuePair<int, int[]> keyValuePair in EffectsDatabase.TAL_Dictionary)
			{
				foreach (int key in keyValuePair.Value)
				{
					EffectsDatabase.Unique[key].ArtifactTotalLevelToUnlockEachCopy.Add(keyValuePair.Key);
				}
			}
		}

		public static readonly Dictionary<int, ArtifactEffect> Common = new Dictionary<int, ArtifactEffect>
		{
			{
				1,
				new ArtifactEffectGoldEarnings()
			},
			{
				2,
				new ArtifactEffectRingDamage()
			},
			{
				3,
				new ArtifactEffectHeroDamage()
			},
			{
				4,
				new ArtifactEffectHeroHealth()
			}
		};

		public static readonly Dictionary<int, int[]> TAL_Dictionary = new Dictionary<int, int[]>
		{
			{
				0,
				new int[]
				{
					100,
					101,
					109,
					108
				}
			},
			{
				100,
				new int[]
				{
					127,
					120,
					123,
					125
				}
			},
			{
				200,
				new int[]
				{
					103,
					104,
					102,
					110
				}
			},
			{
				300,
				new int[]
				{
					142,
					114,
					112,
					113
				}
			},
			{
				400,
				new int[]
				{
					117,
					115,
					116,
					137
				}
			},
			{
				500,
				new int[]
				{
					100,
					142,
					108
				}
			},
			{
				600,
				new int[]
				{
					120,
					127,
					105,
					106
				}
			},
			{
				800,
				new int[]
				{
					121,
					122,
					135,
					136
				}
			},
			{
				1000,
				new int[]
				{
					146,
					108,
					103,
					104
				}
			},
			{
				1200,
				new int[]
				{
					125,
					131,
					132,
					142
				}
			},
			{
				1400,
				new int[]
				{
					114,
					111,
					112,
					113,
					134
				}
			},
			{
				1600,
				new int[]
				{
					117,
					115,
					116,
					126
				}
			},
			{
				1800,
				new int[]
				{
					127,
					120,
					125,
					132
				}
			},
			{
				2000,
				new int[]
				{
					100,
					108,
					101,
					109,
					142
				}
			},
			{
				2250,
				new int[]
				{
					127,
					120,
					127
				}
			},
			{
				2500,
				new int[]
				{
					103,
					104,
					107
				}
			},
			{
				2750,
				new int[]
				{
					142,
					102,
					110,
					137
				}
			},
			{
				3000,
				new int[]
				{
					108,
					126,
					134,
					100
				}
			},
			{
				3250,
				new int[]
				{
					144,
					112,
					113,
					114,
					120
				}
			},
			{
				3500,
				new int[]
				{
					143,
					108,
					131,
					133
				}
			},
			{
				3750,
				new int[]
				{
					144,
					115,
					116,
					117
				}
			},
			{
				4000,
				new int[]
				{
					138,
					139,
					122,
					142
				}
			},
			{
				4250,
				new int[]
				{
					144,
					126,
					127,
					120
				}
			},
			{
				4500,
				new int[]
				{
					119,
					124,
					121,
					132
				}
			},
			{
				4750,
				new int[]
				{
					100,
					101,
					102,
					137,
					136
				}
			},
			{
				5000,
				new int[]
				{
					144,
					108,
					109,
					110
				}
			},
			{
				5500,
				new int[]
				{
					128,
					129,
					100,
					133
				}
			},
			{
				6000,
				new int[]
				{
					145,
					123,
					106,
					125
				}
			},
			{
				6750,
				new int[]
				{
					144,
					127,
					120,
					105,
					135
				}
			},
			{
				7500,
				new int[]
				{
					142,
					139,
					122,
					100
				}
			},
			{
				8250,
				new int[]
				{
					145,
					137,
					108,
					103,
					104
				}
			},
			{
				9000,
				new int[]
				{
					102,
					144,
					127,
					120
				}
			},
			{
				10000,
				new int[]
				{
					143,
					130,
					119,
					129
				}
			},
			{
				11000,
				new int[]
				{
					123,
					124,
					110,
					132
				}
			},
			{
				12000,
				new int[]
				{
					144,
					142,
					125
				}
			},
			{
				13000,
				new int[]
				{
					102,
					103,
					120,
					104
				}
			},
			{
				14000,
				new int[]
				{
					144,
					107,
					108
				}
			},
			{
				15500,
				new int[]
				{
					143,
					100,
					110
				}
			},
			{
				17000,
				new int[]
				{
					128,
					127,
					111,
					138
				}
			},
			{
				18500,
				new int[]
				{
					144,
					143,
					130
				}
			},
			{
				20000,
				new int[]
				{
					143,
					100,
					110
				}
			}
		};

		public static Dictionary<int, EffectsDatabase.UniqueEffectInfo> Unique = new Dictionary<int, EffectsDatabase.UniqueEffectInfo>
		{
			{
				100,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectHeroUpgradeCost())
			},
			{
				101,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectHeroCritChance())
			},
			{
				102,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectHeroCritDamage())
			},
			{
				103,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectHeroSkillDamage())
			},
			{
				104,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectHeroNonSkillDamage())
			},
			{
				105,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectLevelRequiredForSkills())
			},
			{
				106,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectHeroReviveTime())
			},
			{
				107,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectHeroUltimateCooldown())
			},
			{
				108,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectRingUpgradeCost())
			},
			{
				109,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectRingCritChance())
			},
			{
				110,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectRingCritDamage())
			},
			{
				111,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectBossTime())
			},
			{
				112,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectBossHealth())
			},
			{
				113,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectBossDamage())
			},
			{
				114,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectBossGold())
			},
			{
				115,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectNonBossHealth())
			},
			{
				116,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectNonBossDamage())
			},
			{
				117,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectNonBossGold())
			},
			{
				118,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectFastEnemySpawn())
			},
			{
				119,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectDragonSpawnRate())
			},
			{
				120,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectEpicBossMythstoneDrop())
			},
			{
				121,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectFreeChestNumItems())
			},
			{
				146,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectFreeChestCooldown())
			},
			{
				122,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectFreeChestCurrency())
			},
			{
				123,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectChestGold())
			},
			{
				124,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectChestChanceRate())
			},
			{
				125,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectOfflineEarningsGold())
			},
			{
				126,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectNonBossWaveSkipChance())
			},
			{
				127,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectPrestigeMythstones())
			},
			{
				128,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectHorseshoeCount())
			},
			{
				129,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectHorseshoeDuration())
			},
			{
				145,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectHorseshoeValue())
			},
			{
				130,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectDestructionCount())
			},
			{
				131,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectTimeWarpCount())
			},
			{
				132,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectTimeWarpSpeed())
			},
			{
				133,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectTimeWarpDuration())
			},
			{
				135,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectAutoTapDuration())
			},
			{
				134,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectAutoTapCount())
			},
			{
				136,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectGoldBagCount())
			},
			{
				137,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectGoldBagValue())
			},
			{
				138,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectShieldCount())
			},
			{
				139,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectShieldDuration())
			},
			{
				140,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectAdventureStageJump())
			},
			{
				142,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectArtifactUpgradeCost())
			},
			{
				143,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectMilestoneBonus())
			},
			{
				144,
				new EffectsDatabase.UniqueEffectInfo(new ArtifactEffectMilestoneCost())
			}
		};

		public static readonly int[] TotalArtifactLevelMilestones = new int[]
		{
			0,
			100,
			200,
			300,
			400,
			500,
			600,
			800,
			1000,
			1200,
			1400,
			1600,
			1800,
			2000,
			2250,
			2500,
			2750,
			3000,
			3250,
			3500,
			3750,
			4000,
			4250,
			4500,
			4750,
			5000,
			5500,
			6000,
			6750,
			7500,
			8250,
			9000,
			10000,
			11000,
			12000,
			13000,
			14000,
			15500,
			17000,
			18500,
			20000
		};

		private const int TAL_0000 = 0;

		private const int TAL_0100 = 100;

		private const int TAL_0200 = 200;

		private const int TAL_0300 = 300;

		private const int TAL_0400 = 400;

		private const int TAL_0500 = 500;

		private const int TAL_0600 = 600;

		private const int TAL_0800 = 800;

		private const int TAL_1000 = 1000;

		private const int TAL_1200 = 1200;

		private const int TAL_1400 = 1400;

		private const int TAL_1600 = 1600;

		private const int TAL_1800 = 1800;

		private const int TAL_2000 = 2000;

		private const int TAL_2250 = 2250;

		private const int TAL_2500 = 2500;

		private const int TAL_2750 = 2750;

		private const int TAL_3000 = 3000;

		private const int TAL_3250 = 3250;

		private const int TAL_3500 = 3500;

		private const int TAL_3750 = 3750;

		private const int TAL_4000 = 4000;

		private const int TAL_4250 = 4250;

		private const int TAL_4500 = 4500;

		private const int TAL_4750 = 4750;

		private const int TAL_5000 = 5000;

		private const int TAL_5500 = 5500;

		private const int TAL_6000 = 6000;

		private const int TAL_6750 = 6750;

		private const int TAL_7500 = 7500;

		private const int TAL_8250 = 8250;

		private const int TAL_9000 = 9000;

		private const int TAL_10000 = 10000;

		private const int TAL_11000 = 11000;

		private const int TAL_12000 = 12000;

		private const int TAL_13000 = 13000;

		private const int TAL_14000 = 14000;

		private const int TAL_15500 = 15500;

		private const int TAL_17000 = 17000;

		private const int TAL_18500 = 18500;

		private const int TAL_20000 = 20000;

		public class UniqueEffectInfo
		{
			public UniqueEffectInfo(ArtifactEffect effect)
			{
				this.Effect = effect;
				this.ArtifactTotalLevelToUnlockEachCopy = new List<int>();
			}

			public ArtifactEffect Effect;

			public List<int> ArtifactTotalLevelToUnlockEachCopy;
		}
	}
}
