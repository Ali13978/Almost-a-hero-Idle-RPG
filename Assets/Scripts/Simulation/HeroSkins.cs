using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class HeroSkins
	{
		static HeroSkins()
		{
			HeroSkins.skinIdChart = new Dictionary<string, List<int>>
			{
				{
					"HORATIO",
					new List<int>
					{
						HeroSkins.HILT_EVOLVE_0,
						HeroSkins.HILT_EVOLVE_1,
						HeroSkins.HILT_EVOLVE_2,
						HeroSkins.HILT_EVOLVE_3,
						HeroSkins.HILT_EVOLVE_4,
						HeroSkins.HILT_EVOLVE_5,
						HeroSkins.HILT_EVOLVE_6,
						HeroSkins.HILT_SKELETON,
						HeroSkins.HILT_PIJAMA,
						HeroSkins.HILT_VIKING,
						HeroSkins.HILT_CHRISTMAS
					}
				},
				{
					"THOUR",
					new List<int>
					{
						HeroSkins.BELLY_EVOLVE_0,
						HeroSkins.BELLY_EVOLVE_1,
						HeroSkins.BELLY_EVOLVE_2,
						HeroSkins.BELLY_EVOLVE_3,
						HeroSkins.BELLY_EVOLVE_4,
						HeroSkins.BELLY_EVOLVE_5,
						HeroSkins.BELLY_EVOLVE_6,
						HeroSkins.BELLY_DIVER,
						HeroSkins.BELLY_PIJAMA,
						HeroSkins.BELLY_ROBOT,
						HeroSkins.BELLY_FRANKENSTEIN
					}
				},
				{
					"IDA",
					new List<int>
					{
						HeroSkins.VEXX_EVOLVE_0,
						HeroSkins.VEXX_EVOLVE_1,
						HeroSkins.VEXX_EVOLVE_2,
						HeroSkins.VEXX_EVOLVE_3,
						HeroSkins.VEXX_EVOLVE_4,
						HeroSkins.VEXX_EVOLVE_5,
						HeroSkins.VEXX_EVOLVE_6,
						HeroSkins.VEXX_ROSA,
						HeroSkins.VEXX_PIJAMA,
						HeroSkins.VEXX_THOR
					}
				},
				{
					"KIND_LENNY",
					new List<int>
					{
						HeroSkins.LENNY_EVOLVE_0,
						HeroSkins.LENNY_EVOLVE_1,
						HeroSkins.LENNY_EVOLVE_2,
						HeroSkins.LENNY_EVOLVE_3,
						HeroSkins.LENNY_EVOLVE_4,
						HeroSkins.LENNY_EVOLVE_5,
						HeroSkins.LENNY_EVOLVE_6,
						HeroSkins.LENNY_ARCTIC,
						HeroSkins.LENNY_STEAMP,
						HeroSkins.LENNY_PIJAMA,
						HeroSkins.LENNY_REFERRAL,
						HeroSkins.LENNY_ZOMBIE,
						HeroSkins.LENNY_CHRISTMAS
					}
				},
				{
					"DEREK",
					new List<int>
					{
						HeroSkins.WENDLE_EVOLVE_0,
						HeroSkins.WENDLE_EVOLVE_1,
						HeroSkins.WENDLE_EVOLVE_2,
						HeroSkins.WENDLE_EVOLVE_3,
						HeroSkins.WENDLE_EVOLVE_4,
						HeroSkins.WENDLE_EVOLVE_5,
						HeroSkins.WENDLE_EVOLVE_6,
						HeroSkins.WENDLE_PUNK,
						HeroSkins.WENDLE_GENTMAN,
						HeroSkins.WENDLE_PIJAMA,
						HeroSkins.WENDLE_FISHERMAN
					}
				},
				{
					"SHEELA",
					new List<int>
					{
						HeroSkins.V_EVOLVE_0,
						HeroSkins.V_EVOLVE_1,
						HeroSkins.V_EVOLVE_2,
						HeroSkins.V_EVOLVE_3,
						HeroSkins.V_EVOLVE_4,
						HeroSkins.V_EVOLVE_5,
						HeroSkins.V_EVOLVE_6,
						HeroSkins.V_PRINCIBE,
						HeroSkins.V_NINJA,
						HeroSkins.V_PIJAMA,
						HeroSkins.V_CHRISTMAS
					}
				},
				{
					"BOMBERMAN",
					new List<int>
					{
						HeroSkins.BOOMER_EVOLVE_0,
						HeroSkins.BOOMER_EVOLVE_1,
						HeroSkins.BOOMER_EVOLVE_2,
						HeroSkins.BOOMER_EVOLVE_3,
						HeroSkins.BOOMER_EVOLVE_4,
						HeroSkins.BOOMER_EVOLVE_5,
						HeroSkins.BOOMER_EVOLVE_6,
						HeroSkins.BOOMER_GASMASK,
						HeroSkins.BOOMER_SCIENTIST,
						HeroSkins.BOOMER_PIJAMA,
						HeroSkins.BOOMER_PRITE
					}
				},
				{
					"SAM",
					new List<int>
					{
						HeroSkins.SAM_EVOLVE_0,
						HeroSkins.SAM_EVOLVE_1,
						HeroSkins.SAM_EVOLVE_2,
						HeroSkins.SAM_EVOLVE_3,
						HeroSkins.SAM_EVOLVE_4,
						HeroSkins.SAM_EVOLVE_5,
						HeroSkins.SAM_EVOLVE_6,
						HeroSkins.SAM_SECURITY,
						HeroSkins.SAM_JAIL,
						HeroSkins.SAM_PIJAMA
					}
				},
				{
					"BLIND_ARCHER",
					new List<int>
					{
						HeroSkins.LIA_EVOLVE_0,
						HeroSkins.LIA_EVOLVE_1,
						HeroSkins.LIA_EVOLVE_2,
						HeroSkins.LIA_EVOLVE_3,
						HeroSkins.LIA_EVOLVE_4,
						HeroSkins.LIA_EVOLVE_5,
						HeroSkins.LIA_EVOLVE_6,
						HeroSkins.LIA_MUMMY
					}
				},
				{
					"JIM",
					new List<int>
					{
						HeroSkins.JIM_EVOLVE_0,
						HeroSkins.JIM_EVOLVE_1,
						HeroSkins.JIM_EVOLVE_2,
						HeroSkins.JIM_EVOLVE_3,
						HeroSkins.JIM_EVOLVE_4,
						HeroSkins.JIM_EVOLVE_5,
						HeroSkins.JIM_EVOLVE_6,
						HeroSkins.JIM_GNOME,
						HeroSkins.JIM_GUITAR,
						HeroSkins.JIM_PIJAMA
					}
				},
				{
					"TAM",
					new List<int>
					{
						HeroSkins.TAM_EVOLVE_0,
						HeroSkins.TAM_EVOLVE_1,
						HeroSkins.TAM_EVOLVE_2,
						HeroSkins.TAM_EVOLVE_3,
						HeroSkins.TAM_EVOLVE_4,
						HeroSkins.TAM_EVOLVE_5,
						HeroSkins.TAM_EVOLVE_6,
						HeroSkins.TAM_COWBOY,
						HeroSkins.TAM_PRIDE,
						HeroSkins.TAM_PIJAMA,
						HeroSkins.TAM_CLOWN
					}
				},
				{
					"WARLOCK",
					new List<int>
					{
						HeroSkins.WARLOCK_EVOLVE_0,
						HeroSkins.WARLOCK_EVOLVE_1,
						HeroSkins.WARLOCK_EVOLVE_2,
						HeroSkins.WARLOCK_EVOLVE_3,
						HeroSkins.WARLOCK_EVOLVE_4,
						HeroSkins.WARLOCK_EVOLVE_5,
						HeroSkins.WARLOCK_EVOLVE_6,
						HeroSkins.WARLOCK_LUCHATOR,
						HeroSkins.WARLOCK_VAMPIRE,
						HeroSkins.WARLOCK_PIJAMA,
						HeroSkins.WARLOCK_CHRISTMAS
					}
				},
				{
					"GOBLIN",
					new List<int>
					{
						HeroSkins.GOBLIN_EVOLVE_0,
						HeroSkins.GOBLIN_EVOLVE_1,
						HeroSkins.GOBLIN_EVOLVE_2,
						HeroSkins.GOBLIN_EVOLVE_3,
						HeroSkins.GOBLIN_EVOLVE_4,
						HeroSkins.GOBLIN_EVOLVE_5,
						HeroSkins.GOBLIN_EVOLVE_6,
						HeroSkins.GOBLIN_LEPRECH,
						HeroSkins.GOBLIN_TURIST,
						HeroSkins.GOBLIN_PIJAMA
					}
				},
				{
					"BABU",
					new List<int>
					{
						HeroSkins.BABU_EVOLVE_0,
						HeroSkins.BABU_EVOLVE_1,
						HeroSkins.BABU_EVOLVE_2,
						HeroSkins.BABU_EVOLVE_3,
						HeroSkins.BABU_EVOLVE_4,
						HeroSkins.BABU_EVOLVE_5,
						HeroSkins.BABU_EVOLVE_6,
						HeroSkins.BABU_WITCH,
						HeroSkins.BABU_QUEEN,
						HeroSkins.BABU_HIPO,
						HeroSkins.BABU_CHRISTMAS
					}
				},
				{
					"DRUID",
					new List<int>
					{
						HeroSkins.DRUID_EVOLVE_0,
						HeroSkins.DRUID_EVOLVE_1,
						HeroSkins.DRUID_EVOLVE_2,
						HeroSkins.DRUID_EVOLVE_3,
						HeroSkins.DRUID_EVOLVE_4,
						HeroSkins.DRUID_EVOLVE_5,
						HeroSkins.DRUID_EVOLVE_6,
						HeroSkins.DRUID_FOREST,
						HeroSkins.DRUID_PIJAMA
					}
				}
			};
		}

		public static int GetEvolveSkinKey(string heroId, int level)
		{
			List<int> list = HeroSkins.skinIdChart[heroId];
			return list[level];
		}

		private static void FillEvolveSkins(HeroDataBase heroData, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			List<int> list = HeroSkins.skinIdChart[heroData.id];
			heroData.equippedSkin = HeroSkins.CreateEvolveSkinData(heroData, list[0], 1);
			heroData.equippedSkin.isNew = false;
			sim.AddBoughtSkin(heroData.equippedSkin);
			allSkins.Add(heroData.equippedSkin);
			heroData.equippedSkin.nameKey = "UI_LEVEL_ADVENTURER";
			allSkins.Add(HeroSkins.CreateEvolveSkinData(heroData, list[1], 2));
			allSkins[count + 1].nameKey = "UI_LEVEL_COMMON";
			allSkins.Add(HeroSkins.CreateEvolveSkinData(heroData, list[2], 3));
			allSkins[count + 2].nameKey = "UI_LEVEL_UNCOMMON";
			allSkins.Add(HeroSkins.CreateEvolveSkinData(heroData, list[3], 4));
			allSkins[count + 3].nameKey = "UI_LEVEL_RARE";
			allSkins.Add(HeroSkins.CreateEvolveSkinData(heroData, list[4], 5));
			allSkins[count + 4].nameKey = "UI_LEVEL_EPIC";
			allSkins.Add(HeroSkins.CreateEvolveSkinData(heroData, list[5], 6));
			allSkins[count + 5].nameKey = "UI_LEVEL_LEGENDARY";
			allSkins.Add(HeroSkins.CreateEvolveSkinData(heroData, list[6], 7));
			allSkins[count + 6].nameKey = "UI_LEVEL_MYTHICAL";
		}

		private static SkinData CreateSecondAnniversarySkinData(HeroDataBase data, int skinId, int index, string nameKey, double cost)
		{
			return new SkinData
			{
				belongsTo = data,
				id = skinId,
				index = index,
				unlockRequirement = new SkinUnlockReqHeroLevel
				{
					targetEvolveLevel = 2
				},
				unlockType = SkinData.UnlockType.CURRENCY,
				currency = CurrencyType.GEM,
				family = SkinFamily.SecondAnniversary,
				cost = cost,
				nameKey = nameKey
			};
		}

		private static SkinData CreateChristmasSkinData(HeroDataBase data, int skinId, int index, string nameKey, double cost)
		{
			return new SkinData
			{
				belongsTo = data,
				id = skinId,
				index = index,
				unlockRequirement = new SkinUnlockReqHeroLevel
				{
					targetEvolveLevel = 1
				},
				unlockType = SkinData.UnlockType.CURRENCY,
				currency = CurrencyType.GEM,
				family = SkinFamily.Christmas,
				cost = cost,
				nameKey = nameKey
			};
		}

		private static SkinData CreateHalloweenSkinData(HeroDataBase data, int skinId, int index, string nameKey)
		{
			return new SkinData
			{
				belongsTo = data,
				id = skinId,
				index = index,
				unlockRequirement = new SkinUnlockReqHeroLevel
				{
					targetEvolveLevel = 5
				},
				unlockType = SkinData.UnlockType.CURRENCY,
				currency = CurrencyType.GEM,
				family = SkinFamily.Halloween,
				cost = 1250.0,
				nameKey = nameKey
			};
		}

		private static SkinData CreatePijamaSkinData(HeroDataBase data, int skinId, int index)
		{
			SkinData skinData = new SkinData
			{
				belongsTo = data,
				id = skinId,
				index = index,
				family = SkinFamily.Pajama,
				unlockType = SkinData.UnlockType.CURRENCY
			};
			SkinUnlockReqHeroLevel unlockRequirement = new SkinUnlockReqHeroLevel
			{
				targetEvolveLevel = 1
			};
			skinData.unlockRequirement = unlockRequirement;
			return skinData;
		}

		private static SkinData CreateRareSkinData(HeroDataBase data, int skinId, int index)
		{
			SkinData skinData = new SkinData
			{
				belongsTo = data,
				id = skinId,
				index = index,
				family = SkinFamily.Rare,
				unlockType = SkinData.UnlockType.CURRENCY
			};
			SkinUnlockReqHeroLevel unlockRequirement = new SkinUnlockReqHeroLevel
			{
				targetEvolveLevel = 3
			};
			skinData.unlockRequirement = unlockRequirement;
			return skinData;
		}

		private static SkinData CreateEpicSkinData(HeroDataBase data, int skinId, int index)
		{
			SkinData skinData = new SkinData
			{
				belongsTo = data,
				id = skinId,
				index = index,
				family = SkinFamily.Epic,
				unlockType = SkinData.UnlockType.CURRENCY
			};
			SkinUnlockReqHeroLevel unlockRequirement = new SkinUnlockReqHeroLevel
			{
				targetEvolveLevel = 4
			};
			skinData.unlockRequirement = unlockRequirement;
			return skinData;
		}

		private static SkinData CreateEvolveSkinData(HeroDataBase data, int skinId, int index)
		{
			SkinData skinData = new SkinData
			{
				belongsTo = data,
				id = skinId,
				index = index,
				family = SkinFamily.Evolve,
				unlockType = SkinData.UnlockType.HERO_EVOLVE_LEVEL
			};
			SkinUnlockReqHeroLevel unlockRequirement = new SkinUnlockReqHeroLevel
			{
				targetEvolveLevel = index - 1
			};
			skinData.unlockRequirement = unlockRequirement;
			skinData.cost = (double)(index - 1);
			return skinData;
		}

		private static void FillHoratio(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreatePijamaSkinData(data, HeroSkins.HILT_PIJAMA, 8);
			skinData.cost = 500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "PAJAMA_HILT_NAME";
			SkinData skinData2 = HeroSkins.CreateRareSkinData(data, HeroSkins.HILT_SKELETON, 9);
			skinData2.cost = 1500.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "SKELETON_HILT_NAME";
			SkinData skinData3 = HeroSkins.CreateEpicSkinData(data, HeroSkins.HILT_VIKING, 10);
			skinData3.cost = 1000.0;
			skinData3.currency = CurrencyType.GEM;
			skinData3.nameKey = "VIKING_HILT_NAME";
			SkinData item = HeroSkins.CreateChristmasSkinData(data, HeroSkins.HILT_CHRISTMAS, 11, "SKIN_CHRISTMAS_HORATIO", 3000.0);
			allSkins.Add(skinData);
			allSkins.Add(skinData2);
			allSkins.Add(skinData3);
			allSkins.Add(item);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillThour(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreatePijamaSkinData(data, HeroSkins.BELLY_PIJAMA, 8);
			skinData.cost = 500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "PAJAMA_BELLYLARF_NAME";
			SkinData skinData2 = HeroSkins.CreateRareSkinData(data, HeroSkins.BELLY_DIVER, 9);
			skinData2.cost = 1500.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "DIVER_BELLYLARF_NAME";
			SkinData skinData3 = HeroSkins.CreateEpicSkinData(data, HeroSkins.BELLY_ROBOT, 10);
			skinData3.cost = 1000.0;
			skinData3.currency = CurrencyType.GEM;
			skinData3.nameKey = "ROBOT_BELLYLARF_NAME";
			SkinData item = HeroSkins.CreateHalloweenSkinData(data, HeroSkins.BELLY_FRANKENSTEIN, 11, "SKIN_HALLOWEEN_BELLY");
			allSkins.Add(skinData);
			allSkins.Add(skinData2);
			allSkins.Add(skinData3);
			allSkins.Add(item);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillVEXX(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreatePijamaSkinData(data, HeroSkins.VEXX_PIJAMA, 8);
			skinData.cost = 500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "PAJAMA_VEXX_NAME";
			SkinData skinData2 = HeroSkins.CreateRareSkinData(data, HeroSkins.VEXX_ROSA, 9);
			skinData2.cost = 1000.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "ROSIE_VEXX_NAME";
			SkinData skinData3 = HeroSkins.CreateEpicSkinData(data, HeroSkins.VEXX_THOR, 10);
			skinData3.cost = 1500.0;
			skinData3.currency = CurrencyType.GEM;
			skinData3.nameKey = "THOR_VEXX_NAME";
			SkinData item = HeroSkins.CreateChristmasSkinData(data, HeroSkins.VEXX_CHRISTMAS, 11, "SKIN_CHRISTMAS_VEXX", 2000.0);
			allSkins.Add(skinData);
			allSkins.Add(skinData2);
			allSkins.Add(skinData3);
			allSkins.Add(item);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillKindLenny(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreatePijamaSkinData(data, HeroSkins.LENNY_PIJAMA, 8);
			skinData.cost = 500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "PAJAMA_LENNY_NAME";
			SkinData skinData2 = HeroSkins.CreateRareSkinData(data, HeroSkins.LENNY_ARCTIC, 9);
			skinData2.cost = 1500.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "ARCTIC_LENNY_NAME";
			SkinData skinData3 = HeroSkins.CreateEpicSkinData(data, HeroSkins.LENNY_STEAMP, 10);
			skinData3.cost = 1500.0;
			skinData3.currency = CurrencyType.GEM;
			skinData3.nameKey = "STEAM_LENNY_NAME";
			SkinData item = HeroSkins.CreateHalloweenSkinData(data, HeroSkins.LENNY_ZOMBIE, 12, "SKIN_HALLOWEEN_LENNY");
			SkinData item2 = HeroSkins.CreateChristmasSkinData(data, HeroSkins.LENNY_CHRISTMAS, 13, "SKIN_CHRISTMAS_LENNY", 2500.0);
			allSkins.Add(skinData);
			allSkins.Add(skinData2);
			allSkins.Add(skinData3);
			allSkins.Add(item);
			allSkins.Add(item2);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillWendle(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreateRareSkinData(data, HeroSkins.WENDLE_PUNK, 9);
			skinData.cost = 1500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "PUNK_WENDLE_NAME";
			SkinData skinData2 = HeroSkins.CreateEpicSkinData(data, HeroSkins.WENDLE_GENTMAN, 10);
			skinData2.cost = 1000.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "GENTLEMAN_WENDLE_NAME";
			SkinData skinData3 = HeroSkins.CreatePijamaSkinData(data, HeroSkins.WENDLE_PIJAMA, 8);
			skinData3.cost = 500.0;
			skinData3.currency = CurrencyType.GEM;
			skinData3.nameKey = "PAJAMA_WENDLE_NAME";
			SkinData item = HeroSkins.CreateSecondAnniversarySkinData(data, HeroSkins.WENDLE_FISHERMAN, 11, "FISHER_WENDLE_NAME", 2500.0);
			allSkins.Add(skinData3);
			allSkins.Add(skinData);
			allSkins.Add(skinData2);
			allSkins.Add(item);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillV(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreatePijamaSkinData(data, HeroSkins.V_PIJAMA, 8);
			skinData.cost = 500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "PAJAMA_V_NAME";
			SkinData skinData2 = HeroSkins.CreateRareSkinData(data, HeroSkins.V_NINJA, 9);
			skinData2.cost = 1500.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "NINJA_V_NAME";
			SkinData skinData3 = HeroSkins.CreateEpicSkinData(data, HeroSkins.V_PRINCIBE, 10);
			skinData3.cost = 1000.0;
			skinData3.currency = CurrencyType.GEM;
			skinData3.nameKey = "PRINCIPE_V_NAME";
			SkinData item = HeroSkins.CreateChristmasSkinData(data, HeroSkins.V_CHRISTMAS, 11, "SKIN_CHRISTMAS_V", 2100.0);
			allSkins.Add(skinData);
			allSkins.Add(skinData3);
			allSkins.Add(skinData2);
			allSkins.Add(item);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillBoomer(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreateRareSkinData(data, HeroSkins.BOOMER_GASMASK, 9);
			skinData.cost = 1500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "GASMASK_BOOMER_NAME";
			SkinData skinData2 = HeroSkins.CreatePijamaSkinData(data, HeroSkins.BOOMER_PIJAMA, 8);
			skinData2.cost = 500.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "PAJAMA_BOOMER_NAME";
			SkinData skinData3 = HeroSkins.CreateEpicSkinData(data, HeroSkins.BOOMER_SCIENTIST, 10);
			skinData3.cost = 1500.0;
			skinData3.currency = CurrencyType.GEM;
			skinData3.nameKey = "SCIENTIST_BOOMER_NAME";
			SkinData item = HeroSkins.CreateSecondAnniversarySkinData(data, HeroSkins.BOOMER_PRITE, 11, "PRITE_BOOMER_NAME", 2000.0);
			allSkins.Add(skinData2);
			allSkins.Add(skinData);
			allSkins.Add(skinData3);
			allSkins.Add(item);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillSam(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreatePijamaSkinData(data, HeroSkins.SAM_PIJAMA, 8);
			skinData.cost = 500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "PAJAMA_SAM_NAME";
			SkinData skinData2 = HeroSkins.CreateRareSkinData(data, HeroSkins.SAM_JAIL, 9);
			skinData2.cost = 1000.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "JAIL_SAM_NAME";
			SkinData skinData3 = HeroSkins.CreateEpicSkinData(data, HeroSkins.SAM_SECURITY, 10);
			skinData3.cost = 1500.0;
			skinData3.currency = CurrencyType.GEM;
			skinData3.nameKey = "SECURITY_SAM_NAME";
			SkinData item = HeroSkins.CreateSecondAnniversarySkinData(data, HeroSkins.SAM_GNOME, 11, "GNOME_SAM_NAME", 2500.0);
			allSkins.Add(skinData);
			allSkins.Add(skinData3);
			allSkins.Add(skinData2);
			allSkins.Add(item);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillLia(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreatePijamaSkinData(data, HeroSkins.LIA_PIJAMA, 8);
			skinData.cost = 500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "PAJAMA_LIA_NAME";
			SkinData skinData2 = HeroSkins.CreateEpicSkinData(data, HeroSkins.LIA_SAMURAI, 10);
			skinData2.cost = 1000.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "SAMURAI_LIA_NAME";
			SkinData skinData3 = HeroSkins.CreateRareSkinData(data, HeroSkins.LIA_POSTAPO, 9);
			skinData3.cost = 1000.0;
			skinData3.currency = CurrencyType.GEM;
			skinData3.nameKey = "POSTAPOC_LIA_NAME";
			SkinData item = HeroSkins.CreateHalloweenSkinData(data, HeroSkins.LIA_MUMMY, 11, "SKIN_HALLOWEEN_LIA");
			allSkins.Add(skinData);
			allSkins.Add(skinData2);
			allSkins.Add(skinData3);
			allSkins.Add(item);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillJim(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreatePijamaSkinData(data, HeroSkins.JIM_PIJAMA, 8);
			skinData.cost = 500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "PAJAMA_JIM_NAME";
			SkinData skinData2 = HeroSkins.CreateRareSkinData(data, HeroSkins.JIM_GNOME, 9);
			skinData2.cost = 1000.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "GNOME_JIM_NAME";
			SkinData skinData3 = HeroSkins.CreateEpicSkinData(data, HeroSkins.JIM_GUITAR, 10);
			skinData3.cost = 1500.0;
			skinData3.currency = CurrencyType.GEM;
			skinData3.nameKey = "GUITAR_JIM_NAME";
			SkinData item = HeroSkins.CreateSecondAnniversarySkinData(data, HeroSkins.JIM_JAZZ, 11, "JAZZ_JIM_NAME", 2000.0);
			allSkins.Add(skinData);
			allSkins.Add(skinData2);
			allSkins.Add(skinData3);
			allSkins.Add(item);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillTam(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreatePijamaSkinData(data, HeroSkins.TAM_PIJAMA, 8);
			skinData.cost = 500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "PAJAMA_TAM_NAME";
			SkinData skinData2 = HeroSkins.CreateRareSkinData(data, HeroSkins.TAM_PRIDE, 9);
			skinData2.cost = 1500.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "PIRATE_TAM_NAME";
			SkinData skinData3 = HeroSkins.CreateEpicSkinData(data, HeroSkins.TAM_COWBOY, 10);
			skinData3.cost = 1000.0;
			skinData3.currency = CurrencyType.GEM;
			skinData3.nameKey = "COWBOY_TAM_NAME";
			SkinData item = HeroSkins.CreateHalloweenSkinData(data, HeroSkins.TAM_CLOWN, 11, "SKIN_HALLOWEEN_TAM");
			allSkins.Add(skinData);
			allSkins.Add(skinData3);
			allSkins.Add(skinData2);
			allSkins.Add(item);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillWarlock(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreatePijamaSkinData(data, HeroSkins.WARLOCK_PIJAMA, 8);
			skinData.cost = 500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "PAJAMA_UNO_NAME";
			SkinData skinData2 = HeroSkins.CreateRareSkinData(data, HeroSkins.WARLOCK_VAMPIRE, 9);
			skinData2.cost = 1500.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "VAMPIRE_UNO_NAME";
			SkinData skinData3 = HeroSkins.CreateEpicSkinData(data, HeroSkins.WARLOCK_LUCHATOR, 10);
			skinData3.cost = 1000.0;
			skinData3.currency = CurrencyType.GEM;
			skinData3.nameKey = "LUCHADOR_UNO_NAME";
			SkinData item = HeroSkins.CreateChristmasSkinData(data, HeroSkins.WARLOCK_CHRISTMAS, 11, "SKIN_CHRISTMAS_UNO", 2600.0);
			allSkins.Add(skinData);
			allSkins.Add(skinData3);
			allSkins.Add(skinData2);
			allSkins.Add(item);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillGoblin(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreatePijamaSkinData(data, HeroSkins.GOBLIN_PIJAMA, 8);
			skinData.cost = 500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "PAJAMA_REDROH_NAME";
			SkinData skinData2 = HeroSkins.CreateRareSkinData(data, HeroSkins.GOBLIN_LEPRECH, 9);
			skinData2.cost = 1000.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "LEPRECHAUN_REDROH_NAME";
			SkinData skinData3 = HeroSkins.CreateEpicSkinData(data, HeroSkins.GOBLIN_TURIST, 10);
			skinData3.cost = 1500.0;
			skinData3.currency = CurrencyType.GEM;
			skinData3.nameKey = "TURIST_REDROH_NAME";
			SkinData item = HeroSkins.CreateSecondAnniversarySkinData(data, HeroSkins.GOBLIN_BIRTHDAY, 11, "BIRTHDAY_REDROH_NAME", 3000.0);
			allSkins.Add(skinData);
			allSkins.Add(skinData2);
			allSkins.Add(skinData3);
			allSkins.Add(item);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillBabu(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreatePijamaSkinData(data, HeroSkins.BABU_HIPO, 8);
			skinData.cost = 500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "PAJAMA_BABU_NAME";
			SkinData skinData2 = HeroSkins.CreateRareSkinData(data, HeroSkins.BABU_QUEEN, 9);
			skinData2.cost = 1000.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "QUEEN_BABU_NAME";
			SkinData skinData3 = HeroSkins.CreateHalloweenSkinData(data, HeroSkins.BABU_WITCH, 10, "SKIN_HALLOWEEN_BABU");
			skinData3.cost = 1600.0;
			SkinData item = HeroSkins.CreateChristmasSkinData(data, HeroSkins.BABU_CHRISTMAS, 11, "SKIN_CHRISTMAS_BABU", 2600.0);
			allSkins.Add(skinData);
			allSkins.Add(skinData2);
			allSkins.Add(skinData3);
			allSkins.Add(item);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		private static void FillDruid(HeroDataBase data, Simulator sim)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			int count = allSkins.Count;
			HeroSkins.FillEvolveSkins(data, sim);
			SkinData skinData = HeroSkins.CreatePijamaSkinData(data, HeroSkins.DRUID_PIJAMA, 8);
			skinData.cost = 500.0;
			skinData.currency = CurrencyType.GEM;
			skinData.nameKey = "SKIN_GIRAFFE_RON";
			SkinData skinData2 = HeroSkins.CreateRareSkinData(data, HeroSkins.DRUID_FOREST, 9);
			skinData2.cost = 1000.0;
			skinData2.currency = CurrencyType.GEM;
			skinData2.nameKey = "SKIN_PLANT_RON";
			SkinData item = HeroSkins.CreateChristmasSkinData(data, HeroSkins.DRUID_CHRISTMAS, 10, "SKIN_CHRISTMAS_RON", 2800.0);
			SkinData item2 = HeroSkins.CreateSecondAnniversarySkinData(data, HeroSkins.DRUID_SHEEP, 11, "SHEEP_DRUID_NAME", 2500.0);
			allSkins.Add(skinData);
			allSkins.Add(skinData2);
			allSkins.Add(item);
			allSkins.Add(item2);
			sim.skinIndexRangePerHero.Add(data.GetId(), new KeyValuePair<int, int>(count, allSkins.Count));
		}

		public static List<SkinData> GetSkinsByFamily(Simulator sim, SkinFamily family)
		{
			List<SkinData> allSkins = sim.GetAllSkins();
			return allSkins.FindAll((SkinData skin) => skin.family == family);
		}

		public static void FillAllSkins(HeroDataBase item, Simulator sim)
		{
			if (item.id == "HORATIO")
			{
				HeroSkins.FillHoratio(item, sim);
			}
			else if (item.id == "THOUR")
			{
				HeroSkins.FillThour(item, sim);
			}
			else if (item.id == "IDA")
			{
				HeroSkins.FillVEXX(item, sim);
			}
			else if (item.id == "KIND_LENNY")
			{
				HeroSkins.FillKindLenny(item, sim);
			}
			else if (item.id == "DEREK")
			{
				HeroSkins.FillWendle(item, sim);
			}
			else if (item.id == "SHEELA")
			{
				HeroSkins.FillV(item, sim);
			}
			else if (item.id == "BOMBERMAN")
			{
				HeroSkins.FillBoomer(item, sim);
			}
			else if (item.id == "SAM")
			{
				HeroSkins.FillSam(item, sim);
			}
			else if (item.id == "BLIND_ARCHER")
			{
				HeroSkins.FillLia(item, sim);
			}
			else if (item.id == "JIM")
			{
				HeroSkins.FillJim(item, sim);
			}
			else if (item.id == "TAM")
			{
				HeroSkins.FillTam(item, sim);
			}
			else if (item.id == "WARLOCK")
			{
				HeroSkins.FillWarlock(item, sim);
			}
			else if (item.id == "GOBLIN")
			{
				HeroSkins.FillGoblin(item, sim);
			}
			else if (item.id == "BABU")
			{
				HeroSkins.FillBabu(item, sim);
			}
			else if (item.id == "DRUID")
			{
				HeroSkins.FillDruid(item, sim);
			}
			else
			{
				HeroSkins.FillEvolveSkins(item, sim);
				UnityEngine.Debug.LogErrorFormat("Hero Skins not set for hero {0} correctly, assinging common skins only", new object[]
				{
					item.id
				});
			}
		}

		public static Dictionary<string, List<int>> skinIdChart;

		public static int HILT_EVOLVE_0;

		public static int HILT_EVOLVE_1 = 1;

		public static int HILT_EVOLVE_2 = 3;

		public static int HILT_EVOLVE_3 = 4;

		public static int HILT_EVOLVE_4 = 5;

		public static int HILT_EVOLVE_5 = 6;

		public static int HILT_EVOLVE_6 = 7;

		public static int HILT_SKELETON = 8;

		public static int HILT_PIJAMA = 9;

		public static int HILT_VIKING = 10;

		public static int HILT_CHRISTMAS = 11;

		public static int BELLY_EVOLVE_0 = 30;

		public static int BELLY_EVOLVE_1 = 31;

		public static int BELLY_EVOLVE_2 = 32;

		public static int BELLY_EVOLVE_3 = 33;

		public static int BELLY_EVOLVE_4 = 34;

		public static int BELLY_EVOLVE_5 = 35;

		public static int BELLY_EVOLVE_6 = 36;

		public static int BELLY_DIVER = 37;

		public static int BELLY_PIJAMA = 38;

		public static int BELLY_ROBOT = 39;

		public static int BELLY_FRANKENSTEIN = 40;

		public static int VEXX_EVOLVE_0 = 60;

		public static int VEXX_EVOLVE_1 = 61;

		public static int VEXX_EVOLVE_2 = 62;

		public static int VEXX_EVOLVE_3 = 63;

		public static int VEXX_EVOLVE_4 = 64;

		public static int VEXX_EVOLVE_5 = 65;

		public static int VEXX_EVOLVE_6 = 66;

		public static int VEXX_ROSA = 67;

		public static int VEXX_PIJAMA = 68;

		public static int VEXX_THOR = 69;

		public static int VEXX_CHRISTMAS = 70;

		public static int LENNY_EVOLVE_0 = 90;

		public static int LENNY_EVOLVE_1 = 91;

		public static int LENNY_EVOLVE_2 = 92;

		public static int LENNY_EVOLVE_3 = 93;

		public static int LENNY_EVOLVE_4 = 94;

		public static int LENNY_EVOLVE_5 = 95;

		public static int LENNY_EVOLVE_6 = 96;

		public static int LENNY_ARCTIC = 97;

		public static int LENNY_STEAMP = 98;

		public static int LENNY_PIJAMA = 99;

		public static int LENNY_REFERRAL = 100;

		public static int LENNY_ZOMBIE = 101;

		public static int LENNY_CHRISTMAS = 102;

		public static int WENDLE_EVOLVE_0 = 120;

		public static int WENDLE_EVOLVE_1 = 121;

		public static int WENDLE_EVOLVE_2 = 122;

		public static int WENDLE_EVOLVE_3 = 123;

		public static int WENDLE_EVOLVE_4 = 124;

		public static int WENDLE_EVOLVE_5 = 125;

		public static int WENDLE_EVOLVE_6 = 126;

		public static int WENDLE_PUNK = 127;

		public static int WENDLE_GENTMAN = 128;

		public static int WENDLE_PIJAMA = 129;

		public static int WENDLE_FISHERMAN = 130;

		public static int V_EVOLVE_0 = 150;

		public static int V_EVOLVE_1 = 151;

		public static int V_EVOLVE_2 = 152;

		public static int V_EVOLVE_3 = 153;

		public static int V_EVOLVE_4 = 154;

		public static int V_EVOLVE_5 = 155;

		public static int V_EVOLVE_6 = 156;

		public static int V_PRINCIBE = 157;

		public static int V_NINJA = 158;

		public static int V_PIJAMA = 159;

		public static int V_CHRISTMAS = 160;

		public static int BOOMER_EVOLVE_0 = 180;

		public static int BOOMER_EVOLVE_1 = 181;

		public static int BOOMER_EVOLVE_2 = 182;

		public static int BOOMER_EVOLVE_3 = 183;

		public static int BOOMER_EVOLVE_4 = 184;

		public static int BOOMER_EVOLVE_5 = 185;

		public static int BOOMER_EVOLVE_6 = 186;

		public static int BOOMER_GASMASK = 187;

		public static int BOOMER_SCIENTIST = 188;

		public static int BOOMER_PIJAMA = 189;

		public static int BOOMER_PRITE = 190;

		public static int SAM_EVOLVE_0 = 210;

		public static int SAM_EVOLVE_1 = 211;

		public static int SAM_EVOLVE_2 = 212;

		public static int SAM_EVOLVE_3 = 213;

		public static int SAM_EVOLVE_4 = 214;

		public static int SAM_EVOLVE_5 = 215;

		public static int SAM_EVOLVE_6 = 216;

		public static int SAM_SECURITY = 217;

		public static int SAM_JAIL = 218;

		public static int SAM_PIJAMA = 219;

		public static int SAM_GNOME = 220;

		public static int LIA_EVOLVE_0 = 240;

		public static int LIA_EVOLVE_1 = 241;

		public static int LIA_EVOLVE_2 = 242;

		public static int LIA_EVOLVE_3 = 243;

		public static int LIA_EVOLVE_4 = 244;

		public static int LIA_EVOLVE_5 = 245;

		public static int LIA_EVOLVE_6 = 246;

		public static int LIA_PIJAMA = 247;

		public static int LIA_POSTAPO = 248;

		public static int LIA_SAMURAI = 249;

		public static int LIA_MUMMY = 250;

		public static int JIM_EVOLVE_0 = 270;

		public static int JIM_EVOLVE_1 = 271;

		public static int JIM_EVOLVE_2 = 272;

		public static int JIM_EVOLVE_3 = 273;

		public static int JIM_EVOLVE_4 = 274;

		public static int JIM_EVOLVE_5 = 275;

		public static int JIM_EVOLVE_6 = 276;

		public static int JIM_GNOME = 277;

		public static int JIM_GUITAR = 278;

		public static int JIM_PIJAMA = 279;

		public static int JIM_JAZZ = 280;

		public static int TAM_EVOLVE_0 = 300;

		public static int TAM_EVOLVE_1 = 301;

		public static int TAM_EVOLVE_2 = 302;

		public static int TAM_EVOLVE_3 = 303;

		public static int TAM_EVOLVE_4 = 304;

		public static int TAM_EVOLVE_5 = 305;

		public static int TAM_EVOLVE_6 = 306;

		public static int TAM_COWBOY = 307;

		public static int TAM_PRIDE = 308;

		public static int TAM_PIJAMA = 309;

		public static int TAM_CLOWN = 310;

		public static int WARLOCK_EVOLVE_0 = 330;

		public static int WARLOCK_EVOLVE_1 = 331;

		public static int WARLOCK_EVOLVE_2 = 332;

		public static int WARLOCK_EVOLVE_3 = 333;

		public static int WARLOCK_EVOLVE_4 = 334;

		public static int WARLOCK_EVOLVE_5 = 335;

		public static int WARLOCK_EVOLVE_6 = 336;

		public static int WARLOCK_LUCHATOR = 337;

		public static int WARLOCK_VAMPIRE = 338;

		public static int WARLOCK_PIJAMA = 339;

		public static int WARLOCK_CHRISTMAS = 340;

		public static int GOBLIN_EVOLVE_0 = 360;

		public static int GOBLIN_EVOLVE_1 = 361;

		public static int GOBLIN_EVOLVE_2 = 362;

		public static int GOBLIN_EVOLVE_3 = 363;

		public static int GOBLIN_EVOLVE_4 = 364;

		public static int GOBLIN_EVOLVE_5 = 365;

		public static int GOBLIN_EVOLVE_6 = 366;

		public static int GOBLIN_LEPRECH = 367;

		public static int GOBLIN_TURIST = 368;

		public static int GOBLIN_PIJAMA = 369;

		public static int GOBLIN_BIRTHDAY = 370;

		public static int BABU_EVOLVE_0 = 390;

		public static int BABU_EVOLVE_1 = 391;

		public static int BABU_EVOLVE_2 = 392;

		public static int BABU_EVOLVE_3 = 393;

		public static int BABU_EVOLVE_4 = 394;

		public static int BABU_EVOLVE_5 = 395;

		public static int BABU_EVOLVE_6 = 396;

		public static int BABU_WITCH = 397;

		public static int BABU_QUEEN = 398;

		public static int BABU_HIPO = 399;

		public static int BABU_CHRISTMAS = 400;

		public static int DRUID_EVOLVE_0 = 420;

		public static int DRUID_EVOLVE_1 = 421;

		public static int DRUID_EVOLVE_2 = 422;

		public static int DRUID_EVOLVE_3 = 423;

		public static int DRUID_EVOLVE_4 = 424;

		public static int DRUID_EVOLVE_5 = 425;

		public static int DRUID_EVOLVE_6 = 426;

		public static int DRUID_FOREST = 428;

		public static int DRUID_PIJAMA = 429;

		public static int DRUID_CHRISTMAS = 430;

		public static int DRUID_SHEEP = 431;
	}
}
