using System;
using System.Collections.Generic;

namespace Simulation
{
	public abstract class ShopPack
	{
		public virtual float GetChanceWeight(Simulator sim)
		{
			throw new NotImplementedException();
		}

		public virtual bool CanAppear(Simulator sim)
		{
			throw new NotImplementedException();
		}

		private static void InitAll()
		{
			ShopPack.all = new List<ShopPack>();
			ShopPack.all.Add(new ShopPackStarter());
			ShopPack.all.Add(new ShopPackRune());
			ShopPack.all.Add(new ShopPackToken());
			ShopPack.all.Add(new ShopPackCurrency());
			ShopPack.all.Add(new ShopPackFiveTrinkets());
			ShopPack.all.Add(new ShopPackBigGem());
			ShopPack.all.Add(new ShopPackBigGemTwo());
			ShopPack.all.Add(new ShopPackRiftOffer01());
			ShopPack.all.Add(new ShopPackRiftOffer02());
			ShopPack.all.Add(new ShopPackRiftOffer03());
			ShopPack.all.Add(new ShopPackRiftOffer04());
			ShopPack.all.Add(new ShopPackRegional01Mexico());
			ShopPack.all.Add(new ShopPackRegional01Brazil());
			ShopPack.all.Add(new ShopPackStage100());
			ShopPack.all.Add(new ShopPackHalloweenGems());
			ShopPack.all.Add(new ShopPackChristmasGemsSmall());
			ShopPack.all.Add(new ShopPackChristmasGemsBig());
			ShopPack.all.Add(new ShopPackStage300());
			ShopPack.all.Add(new ShopPackStage800());
			ShopPack.all.Add(new ShopPackSecondAnniversaryGems());
			ShopPack.all.Add(new ShopPackSecondAnniversaryCurrencyBundle());
			ShopPack.all.Add(new ShopPackSecondAnniversaryGemsTwo());
			ShopPack.all.Add(new ShopPackSecondAnniversaryCurrencyBundleTwo());
		}

		public static T GetShopPack<T>() where T : ShopPack
		{
			foreach (ShopPack shopPack in ShopPack.all)
			{
				T t = shopPack as T;
				if (t != null)
				{
					return t;
				}
			}
			return (T)((object)null);
		}

		public static bool IsAnyOfferAvailable(Simulator sim)
		{
			if (ShopPack.all == null)
			{
				ShopPack.InitAll();
			}
			foreach (ShopPack shopPack in ShopPack.all)
			{
				if (shopPack.CanAppear(sim))
				{
					return true;
				}
			}
			return false;
		}

		public static List<ShopPack> GetShopPacksByTag(ShopPack.Tags tags)
		{
			if (ShopPack.all == null)
			{
				ShopPack.InitAll();
			}
			List<ShopPack> list = new List<ShopPack>(10);
			foreach (ShopPack shopPack in ShopPack.all)
			{
				if (shopPack.HasTag(tags))
				{
					list.Add(shopPack);
				}
			}
			return list;
		}

		public string GetPriceString()
		{
			if (this.isIAP)
			{
				return IapManager.productPriceStringsLocal[this.iapIndex];
			}
			return GameMath.GetDoubleString(this.cost);
		}

		public virtual void OnAppeared()
		{
			this.SendOnAppearPlayfabEvent();
		}

		public virtual void OnCheckout()
		{
			this.SendOnCheckoutPlayfabEvent();
		}

		public virtual void OnPurchaseCompleted()
		{
			this.SendOnPurchasePlayfabEvent();
		}

		public virtual void OnPurchaseClicked()
		{
			this.SendOnPurchaseClickPlayfabEvent();
		}

		public virtual List<int> GetSkins()
		{
			return null;
		}

		public virtual double GetTotalTime()
		{
			return this.totalTime;
		}

		public int GetNumGears(UniversalTotalBonus universalTotalBonus)
		{
			if (this.numGears == null)
			{
				return 0;
			}
			int num = 0;
			foreach (int num2 in this.numGears)
			{
				num += num2;
			}
			if (this is ShopPackLootpackFree)
			{
				num += universalTotalBonus.heroItemsInFreeChestAdd;
			}
			return num;
		}

		public virtual bool ShouldDiscard(Simulator sim)
		{
			return false;
		}

		public virtual bool ShouldOverrideExisting(Simulator sim)
		{
			return false;
		}

		public virtual bool CanBeDiscarded(Simulator sim)
		{
			return true;
		}

		public int GetLootpackMaxGearLevel()
		{
			if (this.numGears == null)
			{
				return 0;
			}
			for (int i = this.numGears.Length - 1; i >= 0; i--)
			{
				if (this.numGears[i] > 0)
				{
					return i;
				}
			}
			return 0;
		}

		public virtual string GetName()
		{
			return LM.Get(this.name);
		}

		public bool HasTag(ShopPack.Tags tag)
		{
			return (this.tags & tag) == tag;
		}

		public bool IsOfferFromOtherIap
		{
			get
			{
				return this.originalIapIndex > -1;
			}
		}

		public string OriginalLocalizedPrice
		{
			get
			{
				return IapManager.productPriceStringsLocal[this.originalIapIndex];
			}
		}

		public void SendOnAppearPlayfabEvent()
		{
			if (!this.isOffer)
			{
				return;
			}
			KeyValuePair<List<string>, List<double>> keyValuePair = this.PackRewards();
			if (!this.isIAP)
			{
				PlayfabManager.SendPlayerEvent(PlayfabEventId.SHOP_CURRENCY_OFFER_REVEALED, new Dictionary<string, object>
				{
					{
						"name",
						LM.GetFromEN(this.name)
					},
					{
						"reward_types",
						keyValuePair.Key
					},
					{
						"reward_amounts",
						keyValuePair.Value
					},
					{
						"cost_currency",
						this.currency
					},
					{
						"cost_amount",
						this.cost
					}
				}, null, null, true);
			}
			else
			{
				Dictionary<string, object> body = new Dictionary<string, object>
				{
					{
						"name",
						LM.GetFromEN(this.name)
					},
					{
						"product",
						IapManager.productIds[this.iapIndex]
					},
					{
						"reward_types",
						keyValuePair.Key
					},
					{
						"reward_amounts",
						keyValuePair.Value
					}
				};
				PlayfabManager.SendPlayerEvent(PlayfabEventId.SHOP_REAL_MONEY_OFFER_REVEALED, body, null, null, true);
			}
		}

		public void SendOnCheckoutPlayfabEvent()
		{
			if (!this.isOffer)
			{
				return;
			}
			KeyValuePair<List<string>, List<double>> keyValuePair = this.PackRewards();
			if (!this.isIAP)
			{
				PlayfabManager.SendPlayerEvent(PlayfabEventId.SHOP_CURRENCY_OFFER_CHECKOUT, new Dictionary<string, object>
				{
					{
						"name",
						LM.GetFromEN(this.name)
					},
					{
						"reward_types",
						keyValuePair.Key
					},
					{
						"reward_amounts",
						keyValuePair.Value
					},
					{
						"cost_currency",
						this.currency
					},
					{
						"cost_amount",
						this.cost
					}
				}, null, null, true);
			}
			else
			{
				Dictionary<string, object> body = new Dictionary<string, object>
				{
					{
						"name",
						LM.GetFromEN(this.name)
					},
					{
						"product",
						IapManager.productIds[this.iapIndex]
					},
					{
						"reward_types",
						keyValuePair.Key
					},
					{
						"reward_amounts",
						keyValuePair.Value
					}
				};
				PlayfabManager.SendPlayerEvent(PlayfabEventId.SHOP_REAL_MONEY_OFFER_CHECKOUT, body, null, null, true);
			}
		}

		public void SendOnPurchaseClickPlayfabEvent()
		{
			if (!this.isOffer)
			{
				return;
			}
			KeyValuePair<List<string>, List<double>> keyValuePair = this.PackRewards();
			if (!this.isIAP)
			{
				PlayfabManager.SendPlayerEvent(PlayfabEventId.SHOP_CURRENCY_OFFER_PURCHASE_CLICKED, new Dictionary<string, object>
				{
					{
						"name",
						LM.GetFromEN(this.name)
					},
					{
						"reward_types",
						keyValuePair.Key
					},
					{
						"reward_amounts",
						keyValuePair.Value
					},
					{
						"spent_currency",
						this.currency
					},
					{
						"spent_amount",
						this.cost
					}
				}, null, null, true);
			}
			else if (IapManager.IsInitialized() && IapManager.productPrices != null && IapManager.productPrices.Length > this.iapIndex)
			{
				Dictionary<string, object> body = new Dictionary<string, object>
				{
					{
						"name",
						LM.GetFromEN(this.name)
					},
					{
						"product",
						IapManager.productIds[this.iapIndex]
					},
					{
						"reward_types",
						keyValuePair.Key
					},
					{
						"reward_amounts",
						keyValuePair.Value
					},
					{
						"cost_currency",
						IapManager.storeCurrency
					},
					{
						"cost_amount",
						IapManager.productPrices[this.iapIndex]
					}
				};
				PlayfabManager.SendPlayerEvent(PlayfabEventId.SHOP_REAL_MONEY_OFFER_PURCHASE_CLICKED, body, null, null, true);
			}
		}

		public void SendOnPurchasePlayfabEvent()
		{
			if (!this.isOffer)
			{
				return;
			}
			KeyValuePair<List<string>, List<double>> keyValuePair = this.PackRewards();
			if (!this.isIAP)
			{
				PlayfabManager.SendPlayerEvent(PlayfabEventId.SHOP_CURRENCY_OFFER_PURCHASED, new Dictionary<string, object>
				{
					{
						"name",
						LM.GetFromEN(this.name)
					},
					{
						"reward_types",
						keyValuePair.Key
					},
					{
						"reward_amounts",
						keyValuePair.Value
					},
					{
						"cost_currency",
						this.currency
					},
					{
						"cost_amount",
						this.cost
					}
				}, null, null, true);
			}
			else if (IapManager.IsInitialized() && IapManager.productPrices != null && IapManager.productPrices.Length > this.iapIndex)
			{
				Dictionary<string, object> body = new Dictionary<string, object>
				{
					{
						"name",
						LM.GetFromEN(this.name)
					},
					{
						"product",
						IapManager.productIds[this.iapIndex]
					},
					{
						"reward_types",
						keyValuePair.Key
					},
					{
						"reward_amounts",
						keyValuePair.Value
					},
					{
						"cost_currency",
						IapManager.storeCurrency
					},
					{
						"cost_amount",
						IapManager.productPrices[this.iapIndex]
					}
				};
				PlayfabManager.SendPlayerEvent(PlayfabEventId.SHOP_REAL_MONEY_OFFER_PURCHASED, body, null, null, true);
			}
		}

		private KeyValuePair<List<string>, List<double>> PackRewards()
		{
			List<string> list = new List<string>();
			List<double> list2 = new List<double>();
			KeyValuePair<List<string>, List<double>> result = new KeyValuePair<List<string>, List<double>>(list, list2);
			if (this.credits > 0.0)
			{
				list.Add(CurrencyType.GEM.ToString());
				list2.Add(this.credits);
			}
			if (this.scrapsMax > 0.0)
			{
				list.Add(CurrencyType.SCRAP.ToString());
				list2.Add(this.scrapsMax);
			}
			if (this.tokensMax > 0.0)
			{
				list.Add(CurrencyType.TOKEN.ToString());
				list2.Add(this.tokensMax);
			}
			if (this.numRunes > 0)
			{
				list.Add("RUNE");
				list2.Add((double)this.numRunes);
			}
			if (this.numGears != null)
			{
				list.Add("HERO_ITEM");
				int num = 0;
				for (int i = 0; i < this.numGears.Length; i++)
				{
					num += this.numGears[i];
				}
				list2.Add((double)num);
			}
			if (this.numSkins > 0)
			{
				list.Add("SKIN");
				list2.Add((double)this.numSkins);
			}
			return result;
		}

		public const int FORCE_OFFER_FREQUENCY = 2;

		public static readonly Type FORCED_OFFER = typeof(ShopPackRune);

		public const double RIFT_OFFER_CAN_APPEAR_PERIOD = 604800.0;

		public static double TIME_BETWEEN_SHOP_OFFERS = 43200.0;

		public const float XMAS_OFFER_DURATION = 129600f;

		public const float CURRENCY_OFFER_DURATION = 43200f;

		public const float FIVE_TRINKETS_DURATION = 28800f;

		public const float TOKEN_OFFER_DURATION = 43200f;

		public const float STARTER_PACK_OFFER_DURATION = 172800f;

		public const float STARTER_PACK_REPEATED_OFFER_DURATION = 43200f;

		public const float RUNE_OFFER_DURATION = 43200f;

		public const float PIJAMA_OFFER_DURATION = 43200f;

		public const float MIDGAME_ONETIME_OFFER_DURATION = 86400f;

		public const float STAGE_100_ONETIME_OFFER_DURATION = 86400f;

		public const float STAGE_300_ONETIME_OFFER_DURATION = 86400f;

		public const float STAGE_800_ONETIME_OFFER_DURATION = 86400f;

		public const float RIFT_OFFER_DURATION = 86400f;

		public const float REGIONAL_OFFER_01_DURATION = 604800f;

		public DateTime timeStart;

		public double totalTime;

		public string name;

		public bool isIAP;

		public CurrencyType currency;

		public double cost;

		public int iapIndex;

		protected int originalIapIndex = -1;

		public float discountPercentage = -1f;

		public int[] numGears;

		public int undiscoveredGearAssuredFrequency;

		public float runeChance;

		public int numRunes;

		public int runeAssuredFrequency;

		public int numTrinkets;

		public int numTrinketPacks;

		public int numCharms;

		public double credits;

		public double candies;

		public double scrapsMin;

		public double scrapsMax;

		public double tokensMin;

		public double tokensMax;

		public int numSkins;

		private static List<ShopPack> all;

		public bool isOffer;

		public ShopPack.Tags tags;

		public bool spamProtection;

		public bool shouldBeAnnounceWithPopup;

		public OfferId? id;

		[Flags]
		public enum Tags
		{
			STANDARD = 1,
			UNIQUE = 2,
			RIFT = 4,
			REGIONAL = 8,
			SEASONAL = 16,
			OUT_OF_SHOP = 32
		}
	}
}
