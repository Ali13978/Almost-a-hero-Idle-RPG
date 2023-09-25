using System;
using System.Collections.Generic;

namespace Simulation
{
	public static class FlashOfferFactory
	{
		public static FlashOfferBundle CreateRandomBundleWithotCharms(DateTime? lastTimeCreated, int maxStage)
		{
			if (TrustedTime.IsReady())
			{
				List<FlashOffer> list = new List<FlashOffer>();
				for (int i = 0; i < 3; i++)
				{
					list.Add(FlashOfferFactory.CreateRandomAdventureOffer(list, lastTimeCreated == null, maxStage));
				}
				DateTime dateTime = TrustedTime.Get();
				if (lastTimeCreated != null && (!Cheats.shortOfferTimes || !Cheats.shortOfferTimeSettings.flashOffers))
				{
					dateTime = dateTime.AddSeconds(-((dateTime - lastTimeCreated.Value).TotalSeconds % 39600.0));
				}
				return new FlashOfferBundle
				{
					timeCreated = dateTime,
					offers = null,
					adventureOffers = list,
					hasSeen = false
				};
			}
			throw new Exception("Trusted time is not ready you cannot create bundle without it");
		}

		public static FlashOfferBundle CreateRandomBundle(DateTime? lastTimeCreated, int maxStage)
		{
			if (TrustedTime.IsReady())
			{
				List<FlashOffer> list = new List<FlashOffer>();
				for (int i = 0; i < 3; i++)
				{
					list.Add(FlashOfferFactory.CreateRandomCharmOffer(list));
				}
				List<FlashOffer> list2 = new List<FlashOffer>();
				for (int j = 0; j < 3; j++)
				{
					list2.Add(FlashOfferFactory.CreateRandomAdventureOffer(list2, lastTimeCreated == null, maxStage));
				}
				double num = 39600.0;
				DateTime dateTime = TrustedTime.Get();
				if (lastTimeCreated != null && (!Cheats.shortOfferTimes || !Cheats.shortOfferTimeSettings.flashOffers))
				{
					dateTime = dateTime.AddSeconds(-((dateTime - lastTimeCreated.Value).TotalSeconds % num));
				}
				return new FlashOfferBundle
				{
					timeCreated = dateTime,
					offers = list,
					adventureOffers = list2,
					hasSeen = false
				};
			}
			throw new Exception("Trusted time is not ready you cannot create bundle without it");
		}

		public static FlashOffer CreateRandomCharmOffer(List<FlashOffer> flashOffers)
		{
			List<int> list = new List<int>();
			List<float> list2 = new List<float>();
			using (Dictionary<int, CharmEffectData>.Enumerator enumerator = Main.instance.GetSim().allCharmEffects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, CharmEffectData> charm = enumerator.Current;
					if (charm.Value.level >= 0 && flashOffers.Find((FlashOffer offer) => offer.charmId == charm.Key) == null)
					{
						list.Add(charm.Key);
						list2.Add((!charm.Value.IsMaxed()) ? 1f : 0.2f);
					}
				}
			}
			int id = (list.Count <= 0) ? CharmIds.allIds[0] : list[GameMath.GetRouletteOutcome(list2, GameMath.RandType.NoSeed)];
			return FlashOfferFactory.CreateCharmOffer(id);
		}

		public static FlashOffer CreateRandomAdventureOffer(List<FlashOffer> adventureOffers, bool forceFreeGems, int maxStage)
		{
			FlashOfferFactory.AdventureOfferInfo adventureOfferInfo;
			if (forceFreeGems && adventureOffers.Count == 0)
			{
				adventureOfferInfo = FlashOfferFactory.AdventureOffers[1];
			}
			else
			{
				FlashOfferFactory.availableAdventureOfferWeights.Clear();
				FlashOfferFactory.availableAdventureOfferIndexes.Clear();
				for (int i = FlashOfferFactory.AdventureOffers.Count - 1; i >= 0; i--)
				{
					if (FlashOfferFactory.IsAdventureOfferAvailable(adventureOffers, FlashOfferFactory.AdventureOffers[i].flashOffer, maxStage))
					{
						FlashOfferFactory.availableAdventureOfferWeights.Add(FlashOfferFactory.AdventureOffers[i].weight);
						FlashOfferFactory.availableAdventureOfferIndexes.Add(i);
					}
				}
				int rouletteOutcome = GameMath.GetRouletteOutcome(FlashOfferFactory.availableAdventureOfferWeights, GameMath.RandType.NoSeed);
				int index = FlashOfferFactory.availableAdventureOfferIndexes[rouletteOutcome];
				adventureOfferInfo = FlashOfferFactory.AdventureOffers[index];
				FlashOffer.Type type = adventureOfferInfo.flashOffer.type;
				if (type != FlashOffer.Type.RUNE)
				{
					if (type == FlashOffer.Type.COSTUME)
					{
						adventureOfferInfo.flashOffer.genericIntId = Main.instance.GetSim().GetAllBuyableSkins().GetRandomListElement<SkinData>().id;
					}
				}
				else
				{
					adventureOfferInfo.flashOffer.genericStringId = Main.instance.GetSim().GetAllBuyableRunes().GetRandomListElement<Rune>().id;
				}
			}
			adventureOfferInfo.flashOffer.purchasesLeft = adventureOfferInfo.stock;
			return adventureOfferInfo.flashOffer.Clone();
		}

		private static bool IsAdventureOfferAvailable(List<FlashOffer> adventureOffers, FlashOffer offer, int maxStage)
		{
			if (adventureOffers.Find((FlashOffer o) => o.type == offer.type) != null)
			{
				return false;
			}
			if (offer.tier == 0 && maxStage > 450)
			{
				return false;
			}
			if (offer.tier == 1 && (maxStage < 451 || maxStage > 1100))
			{
				return false;
			}
			if (offer.tier == 2 && maxStage < 1101)
			{
				return false;
			}
			Simulator sim = Main.instance.GetSim();
			switch (offer.type)
			{
			case FlashOffer.Type.RUNE:
				return sim != null && sim.GetAllBuyableRunes().Count > 0;
			case FlashOffer.Type.COSTUME:
				return sim != null && sim.GetAllBuyableSkins().Count > 0;
			case FlashOffer.Type.TRINKET_PACK:
				return sim != null && sim.numTrinketSlots > 0;
			default:
				return true;
			}
		}

		private static FlashOffer CreateCharmOffer(int id)
		{
			FlashOffer flashOffer = new FlashOffer
			{
				type = FlashOffer.Type.CHARM,
				charmId = id
			};
			flashOffer.purchasesLeft = Main.instance.GetSim().GetFlashOfferPurchasesAllowedCount(flashOffer);
			return flashOffer;
		}

		public static Dictionary<double, double> DiscountedCostumeCosts = new Dictionary<double, double>
		{
			{
				1500.0,
				1000.0
			},
			{
				1250.0,
				850.0
			},
			{
				1000.0,
				650.0
			},
			{
				500.0,
				350.0
			},
			{
				3000.0,
				2500.0
			},
			{
				2000.0,
				1500.0
			},
			{
				2500.0,
				2000.0
			},
			{
				2100.0,
				1600.0
			},
			{
				2600.0,
				2100.0
			},
			{
				2800.0,
				2300.0
			}
		};

		public static readonly List<FlashOfferFactory.AdventureOfferInfo> HallowenOffers = new List<FlashOfferFactory.AdventureOfferInfo>
		{
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.COSTUME_PLUS_SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = HeroSkins.BELLY_FRANKENSTEIN,
					isHalloween = true
				},
				cost = 875.0,
				stock = 1,
				amount = 1000
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.COSTUME_PLUS_SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = HeroSkins.LENNY_ZOMBIE,
					isHalloween = true
				},
				cost = 875.0,
				stock = 1,
				amount = 1000
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.COSTUME_PLUS_SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = HeroSkins.LIA_MUMMY,
					isHalloween = true
				},
				cost = 875.0,
				stock = 1,
				amount = 1000
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.COSTUME_PLUS_SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = HeroSkins.TAM_CLOWN,
					isHalloween = true
				},
				cost = 875.0,
				stock = 1,
				amount = 1000
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.COSTUME_PLUS_SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = HeroSkins.BABU_WITCH,
					isHalloween = true
				},
				cost = 1120.0,
				stock = 1,
				amount = 1000
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.GEM,
					isHalloween = true
				},
				cost = 100.0,
				stock = 1,
				amount = 1100
			}
		};

		public static readonly List<FlashOfferFactory.AdventureOfferInfo> SecondAnniversaryOffers = new List<FlashOfferFactory.AdventureOfferInfo>
		{
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.COSTUME,
					costType = FlashOffer.CostType.GEM,
					genericIntId = HeroSkins.SAM_GNOME,
					isAnniverary = true
				},
				cost = 1500.0,
				stock = 1
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.COSTUME,
					costType = FlashOffer.CostType.GEM,
					genericIntId = HeroSkins.BOOMER_PRITE,
					isAnniverary = true
				},
				cost = 1500.0,
				stock = 1
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.COSTUME,
					costType = FlashOffer.CostType.GEM,
					genericIntId = HeroSkins.WENDLE_FISHERMAN,
					isAnniverary = true
				},
				cost = 1500.0,
				stock = 1
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.COSTUME,
					costType = FlashOffer.CostType.GEM,
					genericIntId = HeroSkins.JIM_JAZZ,
					isAnniverary = true
				},
				cost = 1000.0,
				stock = 1
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.COSTUME,
					costType = FlashOffer.CostType.GEM,
					genericIntId = HeroSkins.GOBLIN_BIRTHDAY,
					isAnniverary = true
				},
				cost = 2000.0,
				stock = 1
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.COSTUME,
					costType = FlashOffer.CostType.GEM,
					genericIntId = HeroSkins.DRUID_SHEEP,
					isAnniverary = true
				},
				cost = 1500.0,
				stock = 1
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.TOKEN,
					costType = FlashOffer.CostType.FREE,
					isAnniverary = true
				},
				stock = 1,
				amount = 222,
				eventUnlockInfo = new FlashOfferFactory.AdventureOfferInfo.EventUnlockInfo
				{
					eventId = "secondAnniversary",
					internalEventId = "freeTokens"
				}
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.FREE,
					isAnniverary = true
				},
				stock = 1,
				amount = 222,
				eventUnlockInfo = new FlashOfferFactory.AdventureOfferInfo.EventUnlockInfo
				{
					eventId = "secondAnniversary",
					internalEventId = "freeScraps"
				}
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.GEM,
					costType = FlashOffer.CostType.FREE,
					isAnniverary = true
				},
				stock = 1,
				amount = 222,
				eventUnlockInfo = new FlashOfferFactory.AdventureOfferInfo.EventUnlockInfo
				{
					eventId = "secondAnniversary",
					internalEventId = "freeGems"
				}
			}
		};

		public static readonly List<FlashOfferFactory.AdventureOfferInfo> AdventureOffers = new List<FlashOfferFactory.AdventureOfferInfo>
		{
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.GEM,
					costType = FlashOffer.CostType.FREE,
					tier = 0
				},
				weight = 4f,
				stock = 1,
				amount = 10
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.GEM,
					costType = FlashOffer.CostType.AD,
					tier = 0
				},
				weight = 8f,
				stock = 3,
				amount = 20
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.FREE,
					tier = 0
				},
				weight = 4f,
				stock = 1,
				amount = 50
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 0,
					tier = 0
				},
				weight = 10f,
				stock = 3,
				amount = 120,
				cost = 20.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 1,
					tier = 0
				},
				weight = 8f,
				stock = 1,
				amount = 650,
				cost = 100.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 2,
					tier = 0
				},
				weight = 4f,
				stock = 1,
				amount = 1750,
				cost = 250.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.TOKEN,
					costType = FlashOffer.CostType.FREE,
					tier = 0
				},
				weight = 3f,
				stock = 1,
				amount = 30
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.TOKEN,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 0,
					tier = 0
				},
				weight = 7f,
				stock = 1,
				amount = 100,
				cost = 25.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.TOKEN,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 1,
					tier = 0
				},
				weight = 4f,
				stock = 1,
				amount = 225,
				cost = 50.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.GEM,
					costType = FlashOffer.CostType.FREE,
					tier = 1
				},
				weight = 4f,
				stock = 1,
				amount = 15
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.GEM,
					costType = FlashOffer.CostType.AD,
					tier = 1
				},
				weight = 8f,
				stock = 3,
				amount = 30
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.FREE,
					tier = 1
				},
				weight = 4f,
				stock = 1,
				amount = 80
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 0,
					tier = 1
				},
				weight = 9f,
				stock = 3,
				amount = 160,
				cost = 20.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 1,
					tier = 1
				},
				weight = 12f,
				stock = 1,
				amount = 850,
				cost = 100.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 2,
					tier = 1
				},
				weight = 6f,
				stock = 1,
				amount = 2250,
				cost = 250.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.TOKEN,
					costType = FlashOffer.CostType.FREE,
					tier = 1
				},
				weight = 3f,
				stock = 1,
				amount = 50
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.TOKEN,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 0,
					tier = 1
				},
				weight = 7f,
				stock = 1,
				amount = 150,
				cost = 25.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.TOKEN,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 1,
					tier = 1
				},
				weight = 4f,
				stock = 1,
				amount = 350,
				cost = 50.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.RUNE,
					costType = FlashOffer.CostType.GEM,
					tier = 1
				},
				weight = 3f,
				stock = 1,
				amount = 1,
				cost = 400.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.COSTUME,
					costType = FlashOffer.CostType.GEM,
					tier = 1
				},
				weight = 3f,
				stock = 1,
				amount = 1
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.TRINKET_PACK,
					costType = FlashOffer.CostType.GEM,
					tier = 1
				},
				weight = 0f,
				stock = 1,
				amount = 1,
				cost = 200.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.RUNE,
					costType = FlashOffer.CostType.GEM,
					tier = 2
				},
				weight = 3f,
				stock = 1,
				amount = 1,
				cost = 400.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.COSTUME,
					costType = FlashOffer.CostType.GEM,
					tier = 2
				},
				weight = 3f,
				stock = 1,
				amount = 1
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.TRINKET_PACK,
					costType = FlashOffer.CostType.GEM,
					tier = 2
				},
				weight = 0f,
				stock = 1,
				amount = 1,
				cost = 200.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.GEM,
					costType = FlashOffer.CostType.FREE,
					tier = 2
				},
				weight = 4f,
				stock = 1,
				amount = 20
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.GEM,
					costType = FlashOffer.CostType.AD,
					tier = 2
				},
				weight = 8f,
				stock = 3,
				amount = 40
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.FREE,
					tier = 2
				},
				weight = 4f,
				stock = 1,
				amount = 100
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 0,
					tier = 2
				},
				weight = 9f,
				stock = 3,
				amount = 200,
				cost = 20.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 1,
					tier = 2
				},
				weight = 12f,
				stock = 1,
				amount = 1050,
				cost = 100.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.SCRAP,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 2,
					tier = 2
				},
				weight = 6f,
				stock = 1,
				amount = 2750,
				cost = 250.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.TOKEN,
					costType = FlashOffer.CostType.FREE,
					tier = 2
				},
				weight = 3f,
				stock = 1,
				amount = 100
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.TOKEN,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 0,
					tier = 2
				},
				weight = 8f,
				stock = 1,
				amount = 175,
				cost = 25.0
			},
			new FlashOfferFactory.AdventureOfferInfo
			{
				flashOffer = new FlashOffer
				{
					type = FlashOffer.Type.TOKEN,
					costType = FlashOffer.CostType.GEM,
					genericIntId = 1,
					tier = 2
				},
				weight = 6f,
				stock = 1,
				amount = 400,
				cost = 50.0
			}
		};

		private static List<float> availableAdventureOfferWeights = new List<float>(FlashOfferFactory.AdventureOffers.Count);

		private static List<int> availableAdventureOfferIndexes = new List<int>(FlashOfferFactory.AdventureOffers.Count);

		public class AdventureOfferInfo
		{
			public FlashOffer flashOffer;

			public float weight;

			public int stock;

			public int amount;

			public double cost;

			public FlashOfferFactory.AdventureOfferInfo.EventUnlockInfo eventUnlockInfo;

			public class EventUnlockInfo
			{
				public string eventId;

				public string internalEventId;
			}
		}
	}
}
