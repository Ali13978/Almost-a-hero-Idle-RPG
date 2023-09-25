using System;
using System.Collections.Generic;

namespace Simulation
{
	public static class RiftFactory
	{
		static RiftFactory()
		{
			RiftFactory.setHuman = new RiftEnemySet
			{
				numEnemiesMin = 3,
				numEnemiesMax = 5,
				types = new List<string>
				{
					"CORRUPTED HUMAN",
					"SEMI CORRUPTED HUMAN"
				}
			};
			RiftFactory.setElf = new RiftEnemySet
			{
				numEnemiesMin = 3,
				numEnemiesMax = 5,
				types = new List<string>
				{
					"CORRUPTED ELF",
					"SEMI CORRUPTED ELF"
				}
			};
			RiftFactory.setDwarf = new RiftEnemySet
			{
				numEnemiesMin = 3,
				numEnemiesMax = 5,
				types = new List<string>
				{
					"CORRUPTED DWARF",
					"SEMI CORRUPTED DWARF"
				}
			};
			RiftFactory.setMagolies = new RiftEnemySet
			{
				numEnemiesMin = 2,
				numEnemiesMax = 4,
				types = new List<string>
				{
					"MANGOLIES"
				}
			};
			RiftFactory.setAll = new RiftEnemySet
			{
				numEnemiesMin = 2,
				numEnemiesMax = 5,
				types = new List<string>
				{
					"CORRUPTED HUMAN",
					"CORRUPTED ELF",
					"CORRUPTED DWARF",
					"MANGOLIES"
				}
			};
			RiftFactory.setAnimals = new RiftEnemySet
			{
				numEnemiesMin = 3,
				numEnemiesMax = 6,
				types = new List<string>
				{
					"BAT",
					"SPIDER"
				}
			};
			RiftFactory.setChest = new RiftEnemySet
			{
				numEnemiesMin = 1,
				numEnemiesMax = 5,
				types = new List<string>
				{
					"CHEST"
				}
			};
			RiftFactory.setBosses = new RiftEnemySet
			{
				numEnemiesMin = 3,
				numEnemiesMax = 5,
				types = new List<string>
				{
					"CORRUPTED HUMAN",
					"CORRUPTED ELF",
					"CORRUPTED DWARF",
					"MANGOLIES"
				},
				bosses = new List<string>
				{
					"BOSS ELF",
					"BOSS DWARF",
					"BOSS HUMAN",
					"BOSS MANGOLIES"
				}
			};
			RiftFactory.setFinalBoss = new RiftEnemySet
			{
				numEnemiesMin = 3,
				numEnemiesMax = 5,
				dontSpawnRegularEnemiesWithBoss = true,
				types = new List<string>
				{
					"MANGOLIES"
				},
				bosses = new List<string>
				{
					"BOSS WISE SNAKE"
				}
			};
			RiftFactory.setTestCurses = new RiftCursesSetup
			{
				progressPerWave = 0.28f,
				progressPerMinute = 2f
			};
		}

		public static ChallengeRift CreateBaseRift()
		{
			return new ChallengeRift
			{
				riftEffects = new List<RiftEffect>(),
				allEnvironments = new List<Environment>(),
				riftPointReward = 50.0,
				allUpgrades = new List<ChallengeUpgrade>(),
				dur = (float)(5 * ChallengeRift.MINUTE)
			};
		}

		public static void FillRiftWorld(World worldRift)
		{
			worldRift.allChallenges = new List<Challenge>();
			List<RiftData> list = new List<RiftData>
			{
				new RiftData
				{
					id = 10,
					discovery = 0,
					difLevel = 0,
					startLevel = 9,
					enemySet = RiftFactory.setHuman,
					numHeroes = 3,
					setup = RiftFactory.setupA,
					rewardFactor = 0.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectShorterUltimateCD()
					}
				},
				new RiftData
				{
					id = 20,
					discovery = 0,
					difLevel = 1,
					startLevel = 9,
					enemySet = RiftFactory.setElf,
					numHeroes = 3,
					setup = RiftFactory.setupB,
					rewardFactor = 0.8,
					effects = new List<RiftEffect>
					{
						new RiftEffectShorterUltimateCD()
					}
				},
				new RiftData
				{
					id = 30,
					discovery = 0,
					difLevel = 2,
					startLevel = 9,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 3,
					setup = RiftFactory.setupE,
					rewardFactor = 0.8,
					effects = new List<RiftEffect>
					{
						new RiftEffectShorterUltimateCD(),
						new RiftEffectCritChance()
					}
				},
				new RiftData
				{
					id = 40,
					discovery = 0,
					difLevel = 3,
					startLevel = 10,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 4,
					setup = RiftFactory.setupI,
					rewardFactor = 0.85,
					effects = new List<RiftEffect>
					{
						new RiftEffectFastEnemies(),
						new RiftEffectLongerUltimateCD()
					}
				},
				new RiftData
				{
					id = 50,
					discovery = 0,
					difLevel = 4,
					startLevel = 10,
					enemySet = RiftFactory.setBosses,
					numHeroes = 5,
					setup = RiftFactory.setupF,
					rewardFactor = 0.75,
					effects = new List<RiftEffect>
					{
						new RiftEffectDyingDealsDamage(),
						new RiftEffectShorterRespawns(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 60,
					discovery = 1,
					difLevel = 5,
					startLevel = 10,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 5,
					setup = RiftFactory.setupI,
					rewardFactor = 1.0,
					effects = new List<RiftEffect>
					{
						new RiftEffectCritChance(),
						new RiftEffectShorterRespawns()
					}
				},
				new RiftData
				{
					id = 70,
					discovery = 1,
					difLevel = 6,
					startLevel = 11,
					enemySet = RiftFactory.setHuman,
					numHeroes = 5,
					setup = RiftFactory.setupG,
					rewardFactor = 1.6,
					effects = new List<RiftEffect>
					{
						new RiftEffectNoAbilityDamage(),
						new RiftEffectCritChance()
					}
				},
				new RiftData
				{
					id = 80,
					discovery = 1,
					difLevel = 7,
					startLevel = 11,
					enemySet = RiftFactory.setElf,
					numHeroes = 3,
					setup = RiftFactory.setupJ,
					rewardFactor = 1.38,
					effects = new List<RiftEffect>
					{
						new RiftEffectUpgradeCostReduction(),
						new RiftEffectLongerUltimateCD(),
						new RiftEffectRegeneration()
					}
				},
				new RiftData
				{
					id = 90,
					discovery = 1,
					difLevel = 8,
					startLevel = 11,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 3,
					setup = RiftFactory.setupG,
					rewardFactor = 1.38,
					effects = new List<RiftEffect>
					{
						new RiftEffectAllDamageBuff()
					}
				},
				new RiftData
				{
					id = 100,
					discovery = 1,
					difLevel = 9,
					startLevel = 12,
					enemySet = RiftFactory.setBosses,
					numHeroes = 3,
					setup = RiftFactory.setupD,
					rewardFactor = 1.65,
					effects = new List<RiftEffect>
					{
						new RiftEffectNoGoldDrop(),
						new RiftEffectGoldToDamage(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 110,
					discovery = 2,
					difLevel = 10,
					startLevel = 12,
					enemySet = RiftFactory.setAll,
					numHeroes = 3,
					setup = RiftFactory.setupH,
					rewardFactor = 1.1,
					effects = new List<RiftEffect>
					{
						new RiftEffectReflectDamage(),
						new RiftEffectFastEnemies()
					}
				},
				new RiftData
				{
					id = 120,
					discovery = 2,
					difLevel = 11,
					startLevel = 12,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 4,
					setup = RiftFactory.setupF,
					rewardFactor = 1.0,
					effects = new List<RiftEffect>
					{
						new RiftEffectMeteorShower(),
						new RiftEffectDyingDealsDamage(),
						new RiftEffectAllDamageBuff()
					}
				},
				new RiftData
				{
					id = 130,
					discovery = 2,
					difLevel = 12,
					startLevel = 13,
					enemySet = RiftFactory.setHuman,
					numHeroes = 5,
					setup = RiftFactory.setupE,
					rewardFactor = 0.95,
					effects = new List<RiftEffect>
					{
						new RiftEffectUpgradeCostReduction(),
						new RiftEffectShorterRespawns()
					}
				},
				new RiftData
				{
					id = 140,
					discovery = 2,
					difLevel = 13,
					startLevel = 13,
					enemySet = RiftFactory.setElf,
					numHeroes = 5,
					setup = RiftFactory.setupC,
					rewardFactor = 1.25,
					effects = new List<RiftEffect>
					{
						new RiftEffectFastEnemies(),
						new RiftEffectEveryoneDodges(),
						new RiftEffectStunDropsGold()
					}
				},
				new RiftData
				{
					id = 150,
					discovery = 2,
					difLevel = 14,
					startLevel = 13,
					enemySet = RiftFactory.setBosses,
					numHeroes = 5,
					setup = RiftFactory.setupD,
					rewardFactor = 1.45,
					effects = new List<RiftEffect>
					{
						new RiftEffectHeroHealthToDamage(),
						new RiftEffectOnlyDefenseCharms(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 160,
					discovery = 3,
					difLevel = 15,
					startLevel = 14,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 4,
					setup = RiftFactory.setupF,
					rewardFactor = 0.33,
					effects = new List<RiftEffect>
					{
						new RiftEffectGoldToDamage(),
						new RiftEffectCharmsProgress()
					}
				},
				new RiftData
				{
					id = 170,
					discovery = 3,
					difLevel = 16,
					startLevel = 14,
					enemySet = RiftFactory.setAll,
					numHeroes = 3,
					setup = RiftFactory.setupB,
					rewardFactor = 1.6,
					effects = new List<RiftEffect>
					{
						new RiftEffectNoAbilityDamage(),
						new RiftEffectOnlyAttackCharms()
					}
				},
				new RiftData
				{
					id = 180,
					discovery = 3,
					difLevel = 17,
					startLevel = 14,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 3,
					setup = RiftFactory.setupJ,
					rewardFactor = 1.5,
					effects = new List<RiftEffect>
					{
						new RiftEffectRegeneration(),
						new RiftEffectLongerRespawns()
					}
				},
				new RiftData
				{
					id = 190,
					discovery = 3,
					difLevel = 18,
					startLevel = 15,
					enemySet = RiftFactory.setChest,
					numHeroes = 4,
					setup = RiftFactory.setupE,
					rewardFactor = 0.25,
					effects = new List<RiftEffect>
					{
						new RiftEffectGoldToDamage(),
						new RiftEffectTreasureChests()
					}
				},
				new RiftData
				{
					id = 200,
					discovery = 3,
					difLevel = 19,
					startLevel = 15,
					enemySet = RiftFactory.setBosses,
					numHeroes = 5,
					setup = RiftFactory.setupI,
					rewardFactor = 0.98,
					effects = new List<RiftEffect>
					{
						new RiftEffectFastEnemies(),
						new RiftEffectShorterRespawns(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 210,
					discovery = 4,
					difLevel = 20,
					startLevel = 15,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 4,
					setup = RiftFactory.setupC,
					rewardFactor = 0.95,
					effects = new List<RiftEffect>
					{
						new RiftEffectShorterUltimateCD(),
						new RiftEffectStunDropsGold()
					}
				},
				new RiftData
				{
					id = 220,
					discovery = 4,
					difLevel = 21,
					startLevel = 16,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 3,
					setup = RiftFactory.setupE,
					rewardFactor = 1.1,
					effects = new List<RiftEffect>
					{
						new RiftEffectEveryoneDodges(),
						new RiftEffectAllDamageBuff()
					}
				},
				new RiftData
				{
					id = 230,
					discovery = 4,
					difLevel = 22,
					startLevel = 16,
					enemySet = RiftFactory.setChest,
					numHeroes = 3,
					setup = RiftFactory.setupB,
					rewardFactor = 0.75,
					effects = new List<RiftEffect>
					{
						new RiftEffectReflectDamage(),
						new RiftEffectTreasureChests()
					}
				},
				new RiftData
				{
					id = 240,
					discovery = 4,
					difLevel = 23,
					startLevel = 16,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 2,
					setup = RiftFactory.setupF,
					rewardFactor = 2.25,
					effects = new List<RiftEffect>
					{
						new RiftEffectLongerUltimateCD(),
						new RiftEffectOnlyDefenseCharms()
					}
				},
				new RiftData
				{
					id = 250,
					discovery = 4,
					difLevel = 24,
					startLevel = 17,
					enemySet = RiftFactory.setBosses,
					numHeroes = 3,
					setup = RiftFactory.setupC,
					rewardFactor = 1.6,
					effects = new List<RiftEffect>
					{
						new RiftEffectNoGoldDrop(),
						new RiftEffectDoubledCrits(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 260,
					discovery = 5,
					difLevel = 25,
					startLevel = 17,
					enemySet = RiftFactory.setElf,
					numHeroes = 4,
					setup = RiftFactory.setupI,
					rewardFactor = 0.95,
					effects = new List<RiftEffect>
					{
						new RiftEffectRegeneration(),
						new RiftEffectCritChance()
					}
				},
				new RiftData
				{
					id = 270,
					discovery = 5,
					difLevel = 26,
					startLevel = 17,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 5,
					setup = RiftFactory.setupK,
					rewardFactor = 1.2,
					effects = new List<RiftEffect>
					{
						new RiftEffectCharmsProgress(),
						new RiftEffectOnlyAttackCharms()
					}
				},
				new RiftData
				{
					id = 280,
					discovery = 5,
					difLevel = 27,
					startLevel = 18,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 3,
					setup = RiftFactory.setupF,
					rewardFactor = 1.5,
					effects = new List<RiftEffect>
					{
						new RiftEffectReflectDamage(),
						new RiftEffectDyingHealsAllies()
					}
				},
				new RiftData
				{
					id = 290,
					discovery = 5,
					difLevel = 28,
					startLevel = 18,
					enemySet = RiftFactory.setAll,
					numHeroes = 2,
					setup = RiftFactory.setupE,
					rewardFactor = 1.75,
					effects = new List<RiftEffect>
					{
						new RiftEffectFastEnemies(),
						new RiftEffectDyingHealsAllies(),
						new RiftEffectEveryoneDodges()
					}
				},
				new RiftData
				{
					id = 300,
					discovery = 5,
					difLevel = 29,
					startLevel = 18,
					enemySet = RiftFactory.setBosses,
					numHeroes = 5,
					setup = RiftFactory.setupJ,
					rewardFactor = 0.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectShorterRespawns(),
						new RiftEffectAllDamageBuff(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 310,
					discovery = 6,
					difLevel = 30,
					startLevel = 19,
					enemySet = RiftFactory.setChest,
					numHeroes = 3,
					setup = RiftFactory.setupC,
					rewardFactor = 1.0,
					effects = new List<RiftEffect>
					{
						new RiftEffectTreasureChests()
					}
				},
				new RiftData
				{
					id = 320,
					discovery = 6,
					difLevel = 31,
					startLevel = 19,
					enemySet = RiftFactory.setElf,
					numHeroes = 3,
					setup = RiftFactory.setupK,
					rewardFactor = 1.25,
					effects = new List<RiftEffect>
					{
						new RiftEffectDoubledCrits(),
						new RiftEffectDyingDealsDamage()
					}
				},
				new RiftData
				{
					id = 330,
					discovery = 6,
					difLevel = 32,
					startLevel = 19,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 1,
					setup = RiftFactory.setupB,
					rewardFactor = 2.95,
					effects = new List<RiftEffect>
					{
						new RiftEffectLongerRespawns(),
						new RiftEffectEveryoneDodges()
					}
				},
				new RiftData
				{
					id = 340,
					discovery = 6,
					difLevel = 33,
					startLevel = 20,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 3,
					setup = RiftFactory.setupJ,
					rewardFactor = 1.65,
					effects = new List<RiftEffect>
					{
						new RiftEffectAllDamageBuff(),
						new RiftEffectReflectDamage()
					}
				},
				new RiftData
				{
					id = 350,
					discovery = 6,
					difLevel = 34,
					startLevel = 20,
					enemySet = RiftFactory.setBosses,
					numHeroes = 3,
					setup = RiftFactory.setupD,
					rewardFactor = 1.5,
					effects = new List<RiftEffect>
					{
						new RiftEffectMeteorShower(),
						new RiftEffectDoubledCrits(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 360,
					discovery = 7,
					difLevel = 35,
					startLevel = 20,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 3,
					setup = RiftFactory.setupF,
					rewardFactor = 1.05,
					effects = new List<RiftEffect>
					{
						new RiftEffectRegeneration(),
						new RiftEffectDyingDealsDamage()
					}
				},
				new RiftData
				{
					id = 370,
					discovery = 7,
					difLevel = 36,
					startLevel = 21,
					enemySet = RiftFactory.setHuman,
					numHeroes = 1,
					setup = RiftFactory.setupJ,
					rewardFactor = 3.2,
					effects = new List<RiftEffect>
					{
						new RiftEffectCharmsProgress(),
						new RiftEffectShorterRespawns(),
						new RiftEffectCritChance()
					}
				},
				new RiftData
				{
					id = 380,
					discovery = 7,
					difLevel = 37,
					startLevel = 21,
					enemySet = RiftFactory.setChest,
					numHeroes = 3,
					setup = RiftFactory.setupJ,
					rewardFactor = 1.3,
					effects = new List<RiftEffect>
					{
						new RiftEffectTreasureChests(),
						new RiftEffectNoGoldDrop()
					}
				},
				new RiftData
				{
					id = 390,
					discovery = 7,
					difLevel = 38,
					startLevel = 21,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 4,
					setup = RiftFactory.setupB,
					rewardFactor = 0.6,
					effects = new List<RiftEffect>
					{
						new RiftEffectCritChance(),
						new RiftEffectStunDropsGold(),
						new RiftEffectMeteorShower()
					}
				},
				new RiftData
				{
					id = 400,
					discovery = 7,
					difLevel = 39,
					startLevel = 22,
					enemySet = RiftFactory.setBosses,
					numHeroes = 3,
					setup = RiftFactory.setupL,
					rewardFactor = 0.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectCharmsProgress(),
						new RiftEffectMeteorShower(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 410,
					discovery = 8,
					difLevel = 40,
					startLevel = 22,
					enemySet = RiftFactory.setAll,
					numHeroes = 3,
					setup = RiftFactory.setupC,
					rewardFactor = 1.35,
					effects = new List<RiftEffect>
					{
						new RiftEffectFastEnemies(),
						new RiftEffectReflectDamage()
					}
				},
				new RiftData
				{
					id = 420,
					discovery = 8,
					difLevel = 41,
					startLevel = 22,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 5,
					setup = RiftFactory.setupG,
					rewardFactor = 0.71,
					effects = new List<RiftEffect>
					{
						new RiftEffectDyingDealsDamage(),
						new RiftEffectDoubledCrits()
					}
				},
				new RiftData
				{
					id = 430,
					discovery = 8,
					difLevel = 42,
					startLevel = 23,
					enemySet = RiftFactory.setHuman,
					numHeroes = 3,
					setup = RiftFactory.setupI,
					rewardFactor = 1.4,
					effects = new List<RiftEffect>
					{
						new RiftEffectHeroHealthToDamage(),
						new RiftEffectLongerUltimateCD(),
						new RiftEffectDoubledCrits()
					}
				},
				new RiftData
				{
					id = 440,
					discovery = 8,
					difLevel = 43,
					startLevel = 23,
					enemySet = RiftFactory.setElf,
					numHeroes = 4,
					setup = RiftFactory.setupD,
					rewardFactor = 1.25,
					effects = new List<RiftEffect>
					{
						new RiftEffectDyingDealsDamage(),
						new RiftEffectShorterRespawns()
					}
				},
				new RiftData
				{
					id = 450,
					discovery = 8,
					difLevel = 44,
					startLevel = 23,
					enemySet = RiftFactory.setBosses,
					numHeroes = 3,
					setup = RiftFactory.setupE,
					rewardFactor = 1.65,
					effects = new List<RiftEffect>
					{
						new RiftEffectLongerUltimateCD(),
						new RiftEffectDyingHealsAllies(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 460,
					discovery = 9,
					difLevel = 45,
					startLevel = 24,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 2,
					setup = RiftFactory.setupB,
					rewardFactor = 0.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectMeteorShower(),
						new RiftEffectShorterRespawns()
					}
				},
				new RiftData
				{
					id = 470,
					discovery = 9,
					difLevel = 46,
					startLevel = 24,
					enemySet = RiftFactory.setAll,
					numHeroes = 3,
					setup = RiftFactory.setupH,
					rewardFactor = 0.75,
					effects = new List<RiftEffect>
					{
						new RiftEffectDoubledCrits(),
						new RiftEffectHeroHealthToDamage(),
						new RiftEffectAllDamageBuff()
					}
				},
				new RiftData
				{
					id = 480,
					discovery = 9,
					difLevel = 47,
					startLevel = 24,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 5,
					setup = RiftFactory.setupC,
					rewardFactor = 1.55,
					effects = new List<RiftEffect>
					{
						new RiftEffectEveryoneDodges(),
						new RiftEffectCritChance()
					}
				},
				new RiftData
				{
					id = 490,
					discovery = 9,
					difLevel = 48,
					startLevel = 25,
					enemySet = RiftFactory.setHuman,
					numHeroes = 3,
					setup = RiftFactory.setupG,
					rewardFactor = 1.55,
					effects = new List<RiftEffect>
					{
						new RiftEffectNoGoldDrop(),
						new RiftEffectLongerRespawns()
					}
				},
				new RiftData
				{
					id = 500,
					discovery = 9,
					difLevel = 49,
					startLevel = 25,
					enemySet = RiftFactory.setBosses,
					numHeroes = 4,
					setup = RiftFactory.setupI,
					rewardFactor = 0.35,
					effects = new List<RiftEffect>
					{
						new RiftEffectOnlyUtilityCharms(),
						new RiftEffectGoldToDamage(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 510,
					discovery = 10,
					difLevel = 50,
					startLevel = 25,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 3,
					setup = RiftFactory.setupD,
					rewardFactor = 1.85,
					effects = new List<RiftEffect>
					{
						new RiftEffectCritChance(),
						new RiftEffectShorterRespawns(),
						new RiftEffectRegeneration()
					}
				},
				new RiftData
				{
					id = 520,
					discovery = 10,
					difLevel = 51,
					startLevel = 26,
					enemySet = RiftFactory.setChest,
					numHeroes = 3,
					setup = RiftFactory.setupE,
					rewardFactor = 1.2,
					effects = new List<RiftEffect>
					{
						new RiftEffectStunDropsGold(),
						new RiftEffectTreasureChests()
					}
				},
				new RiftData
				{
					id = 530,
					discovery = 10,
					difLevel = 52,
					startLevel = 26,
					enemySet = RiftFactory.setAll,
					numHeroes = 4,
					setup = RiftFactory.setupK,
					rewardFactor = 0.38,
					effects = new List<RiftEffect>
					{
						new RiftEffectGoldToDamage()
					}
				},
				new RiftData
				{
					id = 540,
					discovery = 10,
					difLevel = 53,
					startLevel = 26,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 3,
					setup = RiftFactory.setupG,
					rewardFactor = 1.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectNoAbilityDamage(),
						new RiftEffectOnlyAttackCharms()
					}
				},
				new RiftData
				{
					id = 550,
					discovery = 10,
					difLevel = 54,
					startLevel = 27,
					enemySet = RiftFactory.setBosses,
					numHeroes = 3,
					setup = RiftFactory.setupC,
					rewardFactor = 1.55,
					effects = new List<RiftEffect>
					{
						new RiftEffectOnlyUtilityCharms(),
						new RiftEffectUpgradeCostReduction(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 560,
					discovery = 11,
					difLevel = 55,
					startLevel = 27,
					enemySet = RiftFactory.setElf,
					numHeroes = 1,
					setup = RiftFactory.setupF,
					rewardFactor = 2.3,
					effects = new List<RiftEffect>
					{
						new RiftEffectDyingHealsAllies(),
						new RiftEffectReflectDamage()
					}
				},
				new RiftData
				{
					id = 570,
					discovery = 11,
					difLevel = 56,
					startLevel = 27,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 5,
					setup = RiftFactory.setupJ,
					rewardFactor = 0.84,
					effects = new List<RiftEffect>
					{
						new RiftEffectShorterRespawns(),
						new RiftEffectShorterUltimateCD()
					}
				},
				new RiftData
				{
					id = 580,
					discovery = 11,
					difLevel = 57,
					startLevel = 28,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 3,
					setup = RiftFactory.setupD,
					rewardFactor = 1.85,
					effects = new List<RiftEffect>
					{
						new RiftEffectEveryoneDodges(),
						new RiftEffectUpgradeCostReduction()
					}
				},
				new RiftData
				{
					id = 590,
					discovery = 11,
					difLevel = 58,
					startLevel = 28,
					enemySet = RiftFactory.setAll,
					numHeroes = 4,
					setup = RiftFactory.setupG,
					rewardFactor = 0.83,
					effects = new List<RiftEffect>
					{
						new RiftEffectShorterUltimateCD()
					}
				},
				new RiftData
				{
					id = 600,
					discovery = 11,
					difLevel = 59,
					startLevel = 28,
					enemySet = RiftFactory.setBosses,
					numHeroes = 3,
					setup = RiftFactory.setupK,
					rewardFactor = 1.48,
					effects = new List<RiftEffect>
					{
						new RiftEffectAllDamageBuff(),
						new RiftEffectLongerRespawns(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 610,
					discovery = 12,
					difLevel = 60,
					startLevel = 29,
					enemySet = RiftFactory.setHuman,
					numHeroes = 2,
					setup = RiftFactory.setupH,
					rewardFactor = 0.85,
					effects = new List<RiftEffect>
					{
						new RiftEffectCharmsProgress(),
						new RiftEffectDyingDealsDamage()
					}
				},
				new RiftData
				{
					id = 620,
					discovery = 12,
					difLevel = 61,
					startLevel = 29,
					enemySet = RiftFactory.setElf,
					numHeroes = 5,
					setup = RiftFactory.setupB,
					rewardFactor = 0.95,
					effects = new List<RiftEffect>
					{
						new RiftEffectDoubledCrits(),
						new RiftEffectRegeneration()
					}
				},
				new RiftData
				{
					id = 630,
					discovery = 12,
					difLevel = 62,
					startLevel = 29,
					enemySet = RiftFactory.setChest,
					numHeroes = 1,
					setup = RiftFactory.setupE,
					rewardFactor = 2.05,
					effects = new List<RiftEffect>
					{
						new RiftEffectTreasureChests(),
						new RiftEffectEveryoneDodges(),
						new RiftEffectNoGoldDrop()
					}
				},
				new RiftData
				{
					id = 640,
					discovery = 12,
					difLevel = 63,
					startLevel = 30,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 3,
					setup = RiftFactory.setupD,
					rewardFactor = 1.45,
					effects = new List<RiftEffect>
					{
						new RiftEffectOnlyAttackCharms(),
						new RiftEffectAllDamageBuff()
					}
				},
				new RiftData
				{
					id = 650,
					discovery = 12,
					difLevel = 64,
					startLevel = 30,
					enemySet = RiftFactory.setBosses,
					numHeroes = 4,
					setup = RiftFactory.setupC,
					rewardFactor = 1.05,
					effects = new List<RiftEffect>
					{
						new RiftEffectLongerUltimateCD(),
						new RiftEffectReflectDamage(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 660,
					discovery = 13,
					difLevel = 65,
					startLevel = 30,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 3,
					setup = RiftFactory.setupK,
					rewardFactor = 1.15,
					effects = new List<RiftEffect>
					{
						new RiftEffectCharmsProgress()
					}
				},
				new RiftData
				{
					id = 670,
					discovery = 13,
					difLevel = 66,
					startLevel = 31,
					enemySet = RiftFactory.setHuman,
					numHeroes = 2,
					setup = RiftFactory.setupG,
					rewardFactor = 1.35,
					effects = new List<RiftEffect>
					{
						new RiftEffectMeteorShower(),
						new RiftEffectRegeneration()
					}
				},
				new RiftData
				{
					id = 680,
					discovery = 13,
					difLevel = 67,
					startLevel = 31,
					enemySet = RiftFactory.setElf,
					numHeroes = 5,
					setup = RiftFactory.setupC,
					rewardFactor = 0.86,
					effects = new List<RiftEffect>
					{
						new RiftEffectDoubledCrits(),
						new RiftEffectHeroHealthToDamage()
					}
				},
				new RiftData
				{
					id = 690,
					discovery = 13,
					difLevel = 68,
					startLevel = 31,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 3,
					setup = RiftFactory.setupF,
					rewardFactor = 1.74,
					effects = new List<RiftEffect>
					{
						new RiftEffectDyingHealsAllies(),
						new RiftEffectReflectDamage()
					}
				},
				new RiftData
				{
					id = 700,
					discovery = 13,
					difLevel = 69,
					startLevel = 32,
					enemySet = RiftFactory.setBosses,
					numHeroes = 4,
					setup = RiftFactory.setupE,
					rewardFactor = 0.95,
					effects = new List<RiftEffect>
					{
						new RiftEffectCritChance(),
						new RiftEffectDyingHealsAllies(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 710,
					discovery = 14,
					difLevel = 70,
					startLevel = 32,
					enemySet = RiftFactory.setAll,
					numHeroes = 1,
					setup = RiftFactory.setupH,
					rewardFactor = 2.16,
					effects = new List<RiftEffect>
					{
						new RiftEffectUpgradeCostReduction()
					}
				},
				new RiftData
				{
					id = 720,
					discovery = 14,
					difLevel = 71,
					startLevel = 32,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 5,
					setup = RiftFactory.setupJ,
					rewardFactor = 0.7,
					effects = new List<RiftEffect>
					{
						new RiftEffectShorterUltimateCD(),
						new RiftEffectRegeneration()
					}
				},
				new RiftData
				{
					id = 730,
					discovery = 14,
					difLevel = 72,
					startLevel = 33,
					enemySet = RiftFactory.setHuman,
					numHeroes = 3,
					setup = RiftFactory.setupD,
					rewardFactor = 2.1,
					effects = new List<RiftEffect>
					{
						new RiftEffectLongerRespawns(),
						new RiftEffectFastEnemies()
					}
				},
				new RiftData
				{
					id = 740,
					discovery = 14,
					difLevel = 73,
					startLevel = 33,
					enemySet = RiftFactory.setElf,
					numHeroes = 1,
					setup = RiftFactory.setupG,
					rewardFactor = 0.85,
					effects = new List<RiftEffect>
					{
						new RiftEffectDyingDealsDamage(),
						new RiftEffectShorterRespawns()
					}
				},
				new RiftData
				{
					id = 750,
					discovery = 14,
					difLevel = 74,
					startLevel = 33,
					enemySet = RiftFactory.setBosses,
					numHeroes = 2,
					setup = RiftFactory.setupF,
					rewardFactor = 1.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectShorterRespawns(),
						new RiftEffectNoAbilityDamage(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 760,
					discovery = 15,
					difLevel = 75,
					startLevel = 34,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 3,
					setup = RiftFactory.setupH,
					rewardFactor = 0.56,
					effects = new List<RiftEffect>
					{
						new RiftEffectMeteorShower(),
						new RiftEffectCritChance()
					}
				},
				new RiftData
				{
					id = 770,
					discovery = 15,
					difLevel = 76,
					startLevel = 34,
					enemySet = RiftFactory.setAll,
					numHeroes = 3,
					setup = RiftFactory.setupI,
					rewardFactor = 0.4,
					effects = new List<RiftEffect>
					{
						new RiftEffectGoldToDamage(),
						new RiftEffectLongerUltimateCD(),
						new RiftEffectAllDamageBuff()
					}
				},
				new RiftData
				{
					id = 780,
					discovery = 15,
					difLevel = 77,
					startLevel = 34,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 3,
					setup = RiftFactory.setupC,
					rewardFactor = 1.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectNoGoldDrop(),
						new RiftEffectNoAbilityDamage()
					}
				},
				new RiftData
				{
					id = 790,
					discovery = 15,
					difLevel = 78,
					startLevel = 35,
					enemySet = RiftFactory.setHuman,
					numHeroes = 5,
					setup = RiftFactory.setupM,
					rewardFactor = 0.75,
					effects = new List<RiftEffect>
					{
						new RiftEffectCharmsProgress(),
						new RiftEffectShorterUltimateCD(),
						new RiftEffectShorterRespawns()
					}
				},
				new RiftData
				{
					id = 800,
					discovery = 15,
					difLevel = 79,
					startLevel = 35,
					enemySet = RiftFactory.setBosses,
					numHeroes = 5,
					setup = RiftFactory.setupM,
					rewardFactor = 1.4,
					effects = new List<RiftEffect>
					{
						new RiftEffectFastEnemies(),
						new RiftEffectLongerRespawns(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 810,
					discovery = 16,
					difLevel = 80,
					startLevel = 35,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 2,
					setup = RiftFactory.setupD,
					rewardFactor = 1.42,
					effects = new List<RiftEffect>
					{
						new RiftEffectShorterUltimateCD(),
						new RiftEffectAllDamageBuff(),
						new RiftEffectMeteorShower()
					}
				},
				new RiftData
				{
					id = 820,
					discovery = 16,
					difLevel = 81,
					startLevel = 35,
					enemySet = RiftFactory.setChest,
					numHeroes = 3,
					setup = RiftFactory.setupE,
					rewardFactor = 0.28,
					effects = new List<RiftEffect>
					{
						new RiftEffectGoldDamageToHeroes(),
						new RiftEffectGoldToDamage(),
						new RiftEffectTreasureChests()
					}
				},
				new RiftData
				{
					id = 830,
					discovery = 16,
					difLevel = 82,
					startLevel = 36,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 4,
					setup = RiftFactory.setupH,
					rewardFactor = 0.55,
					effects = new List<RiftEffect>
					{
						new RiftEffectTimeDealsDamage(),
						new RiftEffectStunningEnemies()
					}
				},
				new RiftData
				{
					id = 840,
					discovery = 16,
					difLevel = 83,
					startLevel = 36,
					enemySet = RiftFactory.setElf,
					numHeroes = 5,
					setup = RiftFactory.setupC,
					rewardFactor = 0.58,
					effects = new List<RiftEffect>
					{
						new RiftEffectDyingHeroesDropGold(),
						new RiftEffectShorterRespawns(),
						new RiftEffectFastHeroAttackSpeed()
					}
				},
				new RiftData
				{
					id = 850,
					discovery = 16,
					difLevel = 84,
					startLevel = 36,
					enemySet = RiftFactory.setBosses,
					numHeroes = 3,
					setup = RiftFactory.setupG,
					rewardFactor = 1.35,
					effects = new List<RiftEffect>
					{
						new RiftEffectHalfHeal(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 860,
					discovery = 17,
					difLevel = 85,
					startLevel = 36,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 3,
					setup = RiftFactory.setupI,
					rewardFactor = 0.98,
					effects = new List<RiftEffect>
					{
						new RiftEffectCharmsProgress(),
						new RiftEffectUpgradeCostReduction()
					}
				},
				new RiftData
				{
					id = 870,
					discovery = 17,
					difLevel = 85,
					startLevel = 37,
					enemySet = RiftFactory.setHuman,
					numHeroes = 2,
					setup = RiftFactory.setupB,
					rewardFactor = 1.02,
					effects = new List<RiftEffect>
					{
						new RiftEffectFastHeroAttackSpeed(),
						new RiftEffectCritChance()
					}
				},
				new RiftData
				{
					id = 880,
					discovery = 17,
					difLevel = 86,
					startLevel = 37,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 1,
					setup = RiftFactory.setupC,
					rewardFactor = 2.35,
					effects = new List<RiftEffect>
					{
						new RiftEffectNoGoldDrop(),
						new RiftEffectHalfHeal()
					}
				},
				new RiftData
				{
					id = 890,
					discovery = 17,
					difLevel = 86,
					startLevel = 37,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 5,
					setup = RiftFactory.setupG,
					rewardFactor = 0.95,
					effects = new List<RiftEffect>
					{
						new RiftEffectStunDropsGold(),
						new RiftEffectDoubleHeal()
					}
				},
				new RiftData
				{
					id = 900,
					discovery = 17,
					difLevel = 87,
					startLevel = 37,
					enemySet = RiftFactory.setBosses,
					numHeroes = 4,
					setup = RiftFactory.setupD,
					rewardFactor = 1.55,
					effects = new List<RiftEffect>
					{
						new RiftEffectDyingHeroesDropGold(),
						new RiftEffectTimeDealsDamage(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 910,
					discovery = 18,
					difLevel = 87,
					startLevel = 37,
					enemySet = RiftFactory.setHuman,
					numHeroes = 3,
					setup = RiftFactory.setupE,
					rewardFactor = 0.65,
					effects = new List<RiftEffect>
					{
						new RiftEffectDoubleHeal(),
						new RiftEffectMeteorShower(),
						new RiftEffectDyingDealsDamage()
					}
				},
				new RiftData
				{
					id = 920,
					discovery = 18,
					difLevel = 88,
					startLevel = 38,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 4,
					setup = RiftFactory.setupC,
					rewardFactor = 0.88,
					effects = new List<RiftEffect>
					{
						new RiftEffectHeroHealthToDamage(),
						new RiftEffectStunningEnemies()
					}
				},
				new RiftData
				{
					id = 930,
					discovery = 18,
					difLevel = 88,
					startLevel = 38,
					enemySet = RiftFactory.setElf,
					numHeroes = 2,
					setup = RiftFactory.setupI,
					rewardFactor = 0.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectReflectDamage(),
						new RiftEffectFastHeroAttackSpeed(),
						new RiftEffectTimeDealsDamage()
					}
				},
				new RiftData
				{
					id = 940,
					discovery = 18,
					difLevel = 89,
					startLevel = 38,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 5,
					setup = RiftFactory.setupF,
					rewardFactor = 1.2,
					effects = new List<RiftEffect>
					{
						new RiftEffectDoubleHeal(),
						new RiftEffectGoldDamageToHeroes()
					}
				},
				new RiftData
				{
					id = 950,
					discovery = 18,
					difLevel = 89,
					startLevel = 38,
					enemySet = RiftFactory.setBosses,
					numHeroes = 1,
					setup = RiftFactory.setupD,
					rewardFactor = 1.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectFastEnemies(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 960,
					discovery = 19,
					difLevel = 90,
					startLevel = 38,
					enemySet = RiftFactory.setHuman,
					numHeroes = 4,
					setup = RiftFactory.setupH,
					rewardFactor = 0.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectDyingDealsDamage(),
						new RiftEffectShorterUltimateCD()
					}
				},
				new RiftData
				{
					id = 970,
					discovery = 19,
					difLevel = 90,
					startLevel = 39,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 3,
					setup = RiftFactory.setupF,
					rewardFactor = 1.5,
					effects = new List<RiftEffect>
					{
						new RiftEffectOnlyDefenseCharms(),
						new RiftEffectHalfHeal(),
						new RiftEffectReflectDamage()
					}
				},
				new RiftData
				{
					id = 980,
					discovery = 19,
					difLevel = 91,
					startLevel = 39,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 5,
					setup = RiftFactory.setupC,
					rewardFactor = 1.0,
					effects = new List<RiftEffect>
					{
						new RiftEffectStunningEnemies(),
						new RiftEffectRegeneration()
					}
				},
				new RiftData
				{
					id = 990,
					discovery = 19,
					difLevel = 91,
					startLevel = 39,
					enemySet = RiftFactory.setChest,
					numHeroes = 3,
					setup = RiftFactory.setupJ,
					rewardFactor = 1.75,
					effects = new List<RiftEffect>
					{
						new RiftEffectTreasureChests(),
						new RiftEffectNoGoldDrop(),
						new RiftEffectDyingHeroesDropGold()
					}
				},
				new RiftData
				{
					id = 1000,
					discovery = 19,
					difLevel = 92,
					startLevel = 39,
					enemySet = RiftFactory.setFinalBoss,
					numHeroes = 2,
					setup = RiftFactory.setupM,
					rewardFactor = 1.65,
					effects = new List<RiftEffect>
					{
						new RiftEffectDyingHealsAllies(),
						new RiftEffectWiseSnakeBoss()
					}
				},
				new RiftData
				{
					id = 1010,
					discovery = 20,
					difLevel = 92,
					startLevel = 39,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 4,
					setup = RiftFactory.setupC,
					rewardFactor = 0.55,
					effects = new List<RiftEffect>
					{
						new RiftEffectMeteorShower(),
						new RiftEffectOnlyUtilityCharms()
					}
				},
				new RiftData
				{
					id = 1020,
					discovery = 20,
					difLevel = 93,
					startLevel = 40,
					enemySet = RiftFactory.setElf,
					numHeroes = 3,
					setup = RiftFactory.setupK,
					rewardFactor = 1.0,
					effects = new List<RiftEffect>
					{
						new RiftEffectNoAbilityDamage(),
						new RiftEffectHalfHeal()
					}
				},
				new RiftData
				{
					id = 1030,
					discovery = 20,
					difLevel = 93,
					startLevel = 40,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 5,
					setup = RiftFactory.setupG,
					rewardFactor = 0.85,
					effects = new List<RiftEffect>
					{
						new RiftEffectCritChance(),
						new RiftEffectUpgradeCostReduction()
					}
				},
				new RiftData
				{
					id = 1040,
					discovery = 20,
					difLevel = 94,
					startLevel = 40,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 4,
					setup = RiftFactory.setupC,
					rewardFactor = 0.75,
					effects = new List<RiftEffect>
					{
						new RiftEffectFastHeroAttackSpeed(),
						new RiftEffectReflectDamage(),
						new RiftEffectDyingHealsAllies()
					}
				},
				new RiftData
				{
					id = 1050,
					discovery = 20,
					difLevel = 94,
					startLevel = 40,
					enemySet = RiftFactory.setBosses,
					numHeroes = 4,
					setup = RiftFactory.setupF,
					rewardFactor = 0.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectRegeneration(),
						new RiftEffectDoubleHeal(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 1060,
					discovery = 21,
					difLevel = 95,
					startLevel = 40,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 5,
					setup = RiftFactory.setupD,
					rewardFactor = 0.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectFastHeroAttackSpeed(),
						new RiftEffectAllDamageBuff()
					}
				},
				new RiftData
				{
					id = 1070,
					discovery = 21,
					difLevel = 95,
					startLevel = 41,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 3,
					setup = RiftFactory.setupH,
					rewardFactor = 1.3,
					effects = new List<RiftEffect>
					{
						new RiftEffectStunningEnemies(),
						new RiftEffectEveryoneDodges(),
						new RiftEffectOnlyAttackCharms()
					}
				},
				new RiftData
				{
					id = 1080,
					discovery = 21,
					difLevel = 96,
					startLevel = 41,
					enemySet = RiftFactory.setChest,
					numHeroes = 3,
					setup = RiftFactory.setupF,
					rewardFactor = 1.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectTreasureChests(),
						new RiftEffectDyingDealsDamage(),
						new RiftEffectNoGoldDrop()
					}
				},
				new RiftData
				{
					id = 1090,
					discovery = 21,
					difLevel = 96,
					startLevel = 41,
					enemySet = RiftFactory.setHuman,
					numHeroes = 2,
					setup = RiftFactory.setupE,
					rewardFactor = 1.2,
					effects = new List<RiftEffect>
					{
						new RiftEffectCharmsProgress(),
						new RiftEffectTimeDealsDamage(),
						new RiftEffectDyingHealsAllies()
					}
				},
				new RiftData
				{
					id = 1100,
					discovery = 21,
					difLevel = 96,
					startLevel = 41,
					enemySet = RiftFactory.setBosses,
					numHeroes = 4,
					setup = RiftFactory.setupC,
					rewardFactor = 1.3,
					effects = new List<RiftEffect>
					{
						new RiftEffectOnlyDefenseCharms(),
						new RiftEffectLongerUltimateCD(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 1110,
					discovery = 22,
					difLevel = 97,
					startLevel = 41,
					enemySet = RiftFactory.setElf,
					numHeroes = 1,
					setup = RiftFactory.setupD,
					rewardFactor = 1.8,
					effects = new List<RiftEffect>
					{
						new RiftEffectGoldToDamage(),
						new RiftEffectStunDropsGold(),
						new RiftEffectDoubleHeal()
					}
				},
				new RiftData
				{
					id = 1120,
					discovery = 22,
					difLevel = 97,
					startLevel = 42,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 5,
					setup = RiftFactory.setupF,
					rewardFactor = 1.1,
					effects = new List<RiftEffect>
					{
						new RiftEffectFastEnemies(),
						new RiftEffectFastHeroAttackSpeed()
					}
				},
				new RiftData
				{
					id = 1130,
					discovery = 22,
					difLevel = 97,
					startLevel = 42,
					enemySet = RiftFactory.setAnimals,
					numHeroes = 4,
					setup = RiftFactory.setupD,
					rewardFactor = 1.9,
					effects = new List<RiftEffect>
					{
						new RiftEffectMeteorShower(),
						new RiftEffectDyingHealsAllies()
					}
				},
				new RiftData
				{
					id = 1140,
					discovery = 22,
					difLevel = 98,
					startLevel = 42,
					enemySet = RiftFactory.setHuman,
					numHeroes = 5,
					setup = RiftFactory.setupE,
					rewardFactor = 2.3,
					effects = new List<RiftEffect>
					{
						new RiftEffectDyingHeroesDropGold(),
						new RiftEffectReflectDamage(),
						new RiftEffectNoGoldDrop()
					}
				},
				new RiftData
				{
					id = 1150,
					discovery = 22,
					difLevel = 98,
					startLevel = 42,
					enemySet = RiftFactory.setBosses,
					numHeroes = 3,
					setup = RiftFactory.setupC,
					rewardFactor = 2.1,
					effects = new List<RiftEffect>
					{
						new RiftEffectRegeneration(),
						new RiftEffectLongerRespawns(),
						new RiftEffectBoss()
					}
				},
				new RiftData
				{
					id = 1160,
					discovery = 23,
					difLevel = 98,
					startLevel = 42,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 3,
					setup = RiftFactory.setupI,
					rewardFactor = 2.5,
					effects = new List<RiftEffect>
					{
						new RiftEffectCharmsProgress(),
						new RiftEffectGoldToDamage()
					}
				},
				new RiftData
				{
					id = 1170,
					discovery = 23,
					difLevel = 98,
					startLevel = 43,
					enemySet = RiftFactory.setMagolies,
					numHeroes = 5,
					setup = RiftFactory.setupD,
					rewardFactor = 2.1,
					effects = new List<RiftEffect>
					{
						new RiftEffectHeroHealthToDamage(),
						new RiftEffectNoAbilityDamage(),
						new RiftEffectFastHeroAttackSpeed()
					}
				},
				new RiftData
				{
					id = 1180,
					discovery = 23,
					difLevel = 99,
					startLevel = 43,
					enemySet = RiftFactory.setChest,
					numHeroes = 5,
					setup = RiftFactory.setupH,
					rewardFactor = 2.4,
					effects = new List<RiftEffect>
					{
						new RiftEffectTreasureChests(),
						new RiftEffectAllDamageBuff(),
						new RiftEffectRegeneration()
					}
				},
				new RiftData
				{
					id = 1190,
					discovery = 23,
					difLevel = 99,
					startLevel = 43,
					enemySet = RiftFactory.setDwarf,
					numHeroes = 5,
					setup = RiftFactory.setupF,
					rewardFactor = 2.2,
					effects = new List<RiftEffect>
					{
						new RiftEffectGoldDamageToHeroes(),
						new RiftEffectDyingHeroesDropGold()
					}
				},
				new RiftData
				{
					id = 1200,
					discovery = 23,
					difLevel = 100,
					startLevel = 43,
					enemySet = RiftFactory.setBosses,
					numHeroes = 5,
					setup = RiftFactory.setupM,
					rewardFactor = 2.0,
					effects = new List<RiftEffect>
					{
						new RiftEffectTimeDealsDamage(),
						new RiftEffectStunningEnemies(),
						new RiftEffectBoss()
					}
				}
			};
			for (int i = 0; i < list.Count; i++)
			{
				worldRift.allChallenges.Add(RiftFactory.CreateRiftChallengeProcedurallyByLevel(list[i], i));
			}
			foreach (Challenge challenge in worldRift.allChallenges)
			{
				challenge.Init(worldRift);
			}
		}

		public static ChallengeRift CreateRiftChallengeProcedurallyByLevel(RiftData riftData, int index)
		{
			int difLevel = riftData.difLevel;
			bool isBossRift = false;
			foreach (RiftEffect riftEffect in riftData.effects)
			{
				if (riftEffect is RiftEffectBoss)
				{
					if (riftData.enemySet.bosses == null || riftData.enemySet.bosses.Count == 0)
					{
						riftData.enemySet = RiftFactory.setBosses;
					}
					isBossRift = true;
				}
				else if (riftEffect is RiftEffectTreasureChests)
				{
					riftData.enemySet = RiftFactory.setChest;
				}
			}
			List<EnemyDataBase> list = new List<EnemyDataBase>();
			List<EnemyDataBase> list2 = new List<EnemyDataBase>();
			foreach (string name in riftData.enemySet.types)
			{
				EnemyDataBase enemyDataBase = EnemyFactory.CreateEnemy(name, true);
				enemyDataBase.goldToDrop *= GameMath.PowInt(3.4 + (double)riftData.startLevel * 0.033, riftData.startLevel);
				list.Add(enemyDataBase);
			}
			if (riftData.enemySet.bosses != null)
			{
				foreach (string name2 in riftData.enemySet.bosses)
				{
					EnemyDataBase enemyDataBase2 = EnemyFactory.CreateEnemy(name2, true);
					enemyDataBase2.goldToDrop *= GameMath.PowInt(3.4 + (double)riftData.startLevel * 0.033, riftData.startLevel);
					list2.Add(enemyDataBase2);
				}
			}
			ChallengeRift challengeRift = new ChallengeRift();
			challengeRift.id = riftData.id;
			challengeRift.riftData = riftData;
			challengeRift.firstTimeCharmSelectionDuration = 10f;
			challengeRift.isBossRift = isBossRift;
			if (index == 0)
			{
				challengeRift.firstTimeCharmForceWave = 10;
			}
			challengeRift.riftEffects = riftData.effects;
			challengeRift.dur = riftData.setup.duration;
			Bg bg = Bg.RIFT_DUNGEON;
			int num = index % 3;
			if (num == 1)
			{
				bg = Bg.RIFT_DUNGEON_1;
			}
			else if (num == 2)
			{
				bg = Bg.RIFT_DUNGEON_2;
			}
			Challenge challenge = challengeRift;
			List<Environment> list3 = new List<Environment>();
			List<Environment> list4 = list3;
			Bg bg2 = bg;
			List<EnemyDataBase> enemiesRegular = list;
			List<EnemyDataBase> enemiesBoss = list2;
			bool dontSpawnRegularEnemiesWithBoss = riftData.enemySet.dontSpawnRegularEnemiesWithBoss;
			list4.Add(new Environment(bg2, enemiesRegular, enemiesBoss, null, null, dontSpawnRegularEnemiesWithBoss));
			challenge.allEnvironments = list3;
			challengeRift.allUpgrades = new List<ChallengeUpgrade>();
			challengeRift.riftPointReward = 0.0;
			challengeRift.heroStartLevel = riftData.startLevel;
			challengeRift.discoveryIndex = riftData.discovery;
			if (riftData.cursesSetup != null)
			{
			}
			double num2 = 68.0;
			double num3 = 1.127;
			double num4 = 0.00085;
			double num5 = 7.5;
			double num6 = 8.0;
			double num7 = 1.31;
			double num8 = GameMath.PowDouble(num3 - (double)riftData.startLevel * num4, (double)(riftData.startLevel - RiftFactory.START_LEVEL)) * num5 + num6 * (double)(riftData.startLevel - RiftFactory.START_LEVEL);
			num2 += (double)difLevel * num7 + num8;
			num2 += riftData.setup.difficulty;
			num2 += (double)riftData.numHeroes * 0.5;
			num2 += ((!riftData.hasRing) ? 0.0 : 0.5);
			foreach (RiftEffect riftEffect2 in challengeRift.riftEffects)
			{
				num2 += (double)riftEffect2.difficultyFactor;
			}
			challengeRift.baseEnemyPower = num2;
			challengeRift.incEnemyPowerPerWave = riftData.setup.diffPerWave + (double)difLevel * 0.012 * riftData.setup.diffPerWave;
			challengeRift.numEnemiesMin = riftData.enemySet.numEnemiesMin;
			challengeRift.numEnemiesMax = riftData.enemySet.numEnemiesMax;
			double num9 = RiftFactory.DUST_REWARDS[index];
			challengeRift.targetTotWaveNo = riftData.setup.numWaves;
			challengeRift.riftPointReward = num9 * riftData.rewardFactor * riftData.setup.rewardMul;
			challengeRift.numHeroesMin = riftData.numHeroes;
			challengeRift.numHeroesMax = riftData.numHeroes;
			challengeRift.hasRing = riftData.hasRing;
			return challengeRift;
		}

		public static RiftCursesSetup GetCurseSetupFor(ChallengeRift challengeRift, int riftNo)
		{
			RiftCursesSetup copy = RiftFactory.setTestCurses.GetCopy(riftNo);
			if (challengeRift.riftData.setup == RiftFactory.setupD || challengeRift.riftData.setup == RiftFactory.setupK || challengeRift.riftData.setup == RiftFactory.setupM)
			{
				copy.progressPerWave = 0.25f;
			}
			else if (challengeRift.riftData.setup == RiftFactory.setupG)
			{
				copy.progressPerWave = 0.26f;
			}
			return copy;
		}

		private static RiftEnemySet setHuman;

		private static RiftEnemySet setElf;

		private static RiftEnemySet setDwarf;

		private static RiftEnemySet setMagolies;

		private static RiftEnemySet setAll;

		private static RiftEnemySet setAnimals;

		private static RiftEnemySet setChest;

		private static RiftEnemySet setBosses;

		private static RiftEnemySet setFinalBoss;

		public static RiftCursesSetup setTestCurses;

		private static RiftSetup setupA = new RiftSetup
		{
			duration = 300f,
			numWaves = 25,
			charmCD = 40,
			rewardMul = 0.5,
			difficulty = 0.0,
			diffPerWave = 0.2
		};

		private static RiftSetup setupB = new RiftSetup
		{
			duration = 300f,
			numWaves = 25,
			charmCD = 40,
			rewardMul = 0.5,
			difficulty = 0.0,
			diffPerWave = 0.2
		};

		private static RiftSetup setupC = new RiftSetup
		{
			duration = 300f,
			numWaves = 50,
			charmCD = 40,
			rewardMul = 1.05,
			difficulty = 0.5,
			diffPerWave = 0.12
		};

		private static RiftSetup setupD = new RiftSetup
		{
			duration = 420f,
			numWaves = 120,
			charmCD = 40,
			rewardMul = 1.6,
			difficulty = 0.0,
			diffPerWave = 0.06
		};

		private static RiftSetup setupE = new RiftSetup
		{
			duration = 480f,
			numWaves = 60,
			charmCD = 40,
			rewardMul = 1.25,
			difficulty = 0.5,
			diffPerWave = 0.1
		};

		private static RiftSetup setupF = new RiftSetup
		{
			duration = 480f,
			numWaves = 80,
			charmCD = 40,
			rewardMul = 1.5,
			difficulty = 0.5,
			diffPerWave = 0.08
		};

		private static RiftSetup setupG = new RiftSetup
		{
			duration = 480f,
			numWaves = 100,
			charmCD = 40,
			rewardMul = 1.75,
			difficulty = 0.5,
			diffPerWave = 0.07
		};

		private static RiftSetup setupH = new RiftSetup
		{
			duration = 600f,
			numWaves = 25,
			charmCD = 40,
			rewardMul = 0.55,
			difficulty = 1.0,
			diffPerWave = 0.25
		};

		private static RiftSetup setupI = new RiftSetup
		{
			duration = 600f,
			numWaves = 50,
			charmCD = 40,
			rewardMul = 1.05,
			difficulty = 0.5,
			diffPerWave = 0.14
		};

		private static RiftSetup setupJ = new RiftSetup
		{
			duration = 600f,
			numWaves = 75,
			charmCD = 40,
			rewardMul = 1.55,
			difficulty = 0.5,
			diffPerWave = 0.1
		};

		private static RiftSetup setupK = new RiftSetup
		{
			duration = 750f,
			numWaves = 150,
			charmCD = 40,
			rewardMul = 2.75,
			difficulty = 1.0,
			diffPerWave = 0.055
		};

		private static RiftSetup setupL = new RiftSetup
		{
			duration = 750f,
			numWaves = 50,
			charmCD = 40,
			rewardMul = 1.1,
			difficulty = 1.5,
			diffPerWave = 0.15
		};

		private static RiftSetup setupM = new RiftSetup
		{
			duration = 750f,
			numWaves = 150,
			charmCD = 40,
			rewardMul = 2.85,
			difficulty = 1.0,
			diffPerWave = 0.056
		};

		private static int START_LEVEL = 9;

		public static double[] DUST_REWARDS = new double[]
		{
			20.0,
			21.0,
			22.0,
			23.0,
			24.0,
			28.0,
			29.4,
			30.8,
			32.2,
			33.6,
			39.2,
			41.2,
			43.1,
			45.1,
			47.0,
			54.9,
			57.6,
			60.4,
			63.1,
			65.9,
			76.8,
			80.7,
			84.5,
			88.4,
			92.2,
			108.0,
			113.0,
			118.0,
			124.0,
			129.0,
			151.0,
			158.0,
			166.0,
			173.0,
			181.0,
			211.0,
			221.0,
			232.0,
			242.0,
			253.0,
			295.0,
			310.0,
			325.0,
			339.0,
			354.0,
			413.0,
			434.0,
			455.0,
			475.0,
			496.0,
			579.0,
			607.0,
			636.0,
			665.0,
			694.0,
			810.0,
			850.0,
			891.0,
			931.0,
			972.0,
			1130.0,
			1190.0,
			1250.0,
			1300.0,
			1360.0,
			1590.0,
			1670.0,
			1750.0,
			1830.0,
			1900.0,
			2220.0,
			2330.0,
			2440.0,
			2560.0,
			2670.0,
			3110.0,
			3270.0,
			3420.0,
			3580.0,
			3730.0,
			4360.0,
			4570.0,
			4790.0,
			5010.0,
			5230.0,
			6100.0,
			6400.0,
			6710.0,
			7010.0,
			7320.0,
			8540.0,
			8960.0,
			9390.0,
			9820.0,
			10200.0,
			12000.0,
			12600.0,
			13100.0,
			13700.0,
			14300.0,
			16700.0,
			17600.0,
			18400.0,
			19200.0,
			20100.0,
			23400.0,
			24600.0,
			25800.0,
			26900.0,
			28100.0,
			32800.0,
			34400.0,
			36100.0,
			37700.0,
			39400.0,
			45900.0,
			48200.0,
			50500.0,
			52800.0,
			55100.0,
			64300.0,
			67500.0,
			70700.0,
			73900.0,
			77100.0,
			90000.0,
			94500.0,
			99000.0,
			103000.0,
			108000.0,
			126000.0,
			132000.0,
			139000.0,
			145000.0,
			151000.0,
			176000.0,
			185000.0,
			194000.0,
			203000.0,
			212000.0,
			247000.0,
			259000.0,
			272000.0,
			284000.0,
			296000.0,
			346000.0,
			363000.0,
			380000.0,
			398000.0,
			415000.0
		};
	}
}
