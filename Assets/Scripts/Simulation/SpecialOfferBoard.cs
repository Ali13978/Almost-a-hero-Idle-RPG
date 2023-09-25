using System;
using System.Collections.Generic;
using Ui;

namespace Simulation
{
	public class SpecialOfferBoard
	{
		public SpecialOfferBoard()
		{
			this.standardOffer = new SpecialOfferKeeper();
			this.reAppearingUniqueOffer = new SpecialOfferKeeper();
			this.uniqueOffers = new List<SpecialOfferKeeper>();
			this.regionalOffers = new List<SpecialOfferKeeper>();
			this.seasonalOffers = new List<SpecialOfferKeeper>();
			this.outOfShopOffers = new List<SpecialOfferKeeper>();
			this.allStandardPacks = ShopPack.GetShopPacksByTag(ShopPack.Tags.STANDARD);
			this.allRegionalPacks = ShopPack.GetShopPacksByTag(ShopPack.Tags.REGIONAL);
			this.allUniquePacks = ShopPack.GetShopPacksByTag(ShopPack.Tags.UNIQUE);
			this.allSeasonalPacks = ShopPack.GetShopPacksByTag(ShopPack.Tags.SEASONAL);
			this.allOutOfShopPacks = ShopPack.GetShopPacksByTag(ShopPack.Tags.OUT_OF_SHOP);
			this.availablePacks = new List<ShopPack>();
		}

		public SpecialOfferKeeper this[int index]
		{
			get
			{
				if (this.standardOffer.IsAlive())
				{
					if (index == 0)
					{
						return this.standardOffer;
					}
					if (index == 1)
					{
						if (this.reAppearingUniqueOffer.IsAlive())
						{
							return this.reAppearingUniqueOffer;
						}
						if (index - 1 < this.uniqueOffers.Count)
						{
							return this.uniqueOffers[index - 1];
						}
						if (index - 1 - this.uniqueOffers.Count < this.regionalOffers.Count)
						{
							return this.regionalOffers[index - 1 - this.uniqueOffers.Count];
						}
						return this.seasonalOffers[index - 1 - this.uniqueOffers.Count - this.regionalOffers.Count];
					}
					else if (this.reAppearingUniqueOffer.IsAlive())
					{
						if (index - 2 < this.uniqueOffers.Count)
						{
							return this.uniqueOffers[index - 2];
						}
						if (index - 2 - this.uniqueOffers.Count < this.regionalOffers.Count)
						{
							return this.regionalOffers[index - 2 - this.uniqueOffers.Count];
						}
						return this.seasonalOffers[index - 2 - this.uniqueOffers.Count - this.regionalOffers.Count];
					}
					else
					{
						if (index - 1 < this.uniqueOffers.Count)
						{
							return this.uniqueOffers[index - 1];
						}
						if (index - 1 - this.uniqueOffers.Count < this.regionalOffers.Count)
						{
							return this.regionalOffers[index - 1 - this.uniqueOffers.Count];
						}
						return this.seasonalOffers[index - 1 - this.uniqueOffers.Count - this.regionalOffers.Count];
					}
				}
				else if (this.reAppearingUniqueOffer.IsAlive())
				{
					if (index == 0)
					{
						return this.reAppearingUniqueOffer;
					}
					if (index - 1 < this.uniqueOffers.Count)
					{
						return this.uniqueOffers[index - 1];
					}
					if (index - 1 - this.uniqueOffers.Count < this.regionalOffers.Count)
					{
						return this.regionalOffers[index - 1 - this.uniqueOffers.Count];
					}
					return this.seasonalOffers[index - 1 - this.uniqueOffers.Count - this.regionalOffers.Count];
				}
				else
				{
					if (index < this.uniqueOffers.Count)
					{
						return this.uniqueOffers[index];
					}
					if (index - this.uniqueOffers.Count < this.regionalOffers.Count)
					{
						return this.regionalOffers[index - this.uniqueOffers.Count];
					}
					return this.seasonalOffers[index - this.uniqueOffers.Count - this.regionalOffers.Count];
				}
			}
		}

		public void Init()
		{
		}

		public double GetNearestNextTimeOffer(DateTime time)
		{
			double num = double.MaxValue;
			if (!this.standardOffer.IsAlive())
			{
				num = this.standardOffer.GetRemainingDurToNextOffer(time).TotalSeconds;
			}
			if (this.reAppearingUniqueOffer.IsAlive())
			{
				double totalSeconds = this.standardOffer.GetRemainingDurToNextOffer(time).TotalSeconds;
				if (totalSeconds < num)
				{
					num = totalSeconds;
				}
			}
			return (num != double.MaxValue) ? num : 0.0;
		}

		public int GetAliveSpecialOfferCount()
		{
			int num = 0;
			if (this.standardOffer.offerPack != null)
			{
				num++;
			}
			if (this.reAppearingUniqueOffer.offerPack != null)
			{
				num++;
			}
			num += this.uniqueOffers.Count;
			num += this.regionalOffers.Count;
			return num + this.seasonalOffers.Count;
		}

		public void KillOffer(SpecialOfferKeeper offer, DateTime time)
		{
			if (this.standardOffer == offer)
			{
				this.previousStandardOffer = SaveLoadManager.ConvertShopPack(offer.offerPack);
				offer.KillOffer(time + SpecialOfferBoard.StandardOfferReAppearInterval);
			}
			else if (this.reAppearingUniqueOffer == offer)
			{
				this.previousReAppearOffer = SaveLoadManager.ConvertShopPack(offer.offerPack);
				offer.KillOffer(time + SpecialOfferBoard.UniqueOfferReAppearInterval);
			}
			else if (offer.offerPack.HasTag(ShopPack.Tags.REGIONAL))
			{
				this.regionalOffers.Remove(offer);
				offer.KillOffer();
			}
			else if (offer.offerPack.HasTag(ShopPack.Tags.SEASONAL))
			{
				this.seasonalOffers.Remove(offer);
				offer.KillOffer();
			}
			else if (offer.offerPack.HasTag(ShopPack.Tags.OUT_OF_SHOP))
			{
				this.outOfShopOffers.Remove(offer);
				offer.KillOffer();
			}
			else
			{
				this.uniqueOffers.Remove(offer);
				offer.KillOffer();
			}
		}

		public void Update(Simulator simulator, DateTime time)
		{
			if (this.standardOffer.HasEnd(time))
			{
				this.KillOffer(this.standardOffer, time);
			}
			if (this.reAppearingUniqueOffer.HasEnd(time))
			{
				this.KillOffer(this.reAppearingUniqueOffer, time);
			}
			for (int i = this.uniqueOffers.Count - 1; i >= 0; i--)
			{
				SpecialOfferKeeper specialOfferKeeper = this.uniqueOffers[i];
				if (specialOfferKeeper.HasEnd(time))
				{
					this.KillOffer(specialOfferKeeper, time);
				}
			}
			for (int j = this.regionalOffers.Count - 1; j >= 0; j--)
			{
				SpecialOfferKeeper specialOfferKeeper2 = this.regionalOffers[j];
				if (specialOfferKeeper2.HasEnd(time))
				{
					this.KillOffer(specialOfferKeeper2, time);
				}
			}
			for (int k = this.seasonalOffers.Count - 1; k >= 0; k--)
			{
				SpecialOfferKeeper specialOfferKeeper3 = this.seasonalOffers[k];
				if (specialOfferKeeper3.HasEnd(time))
				{
					this.KillOffer(specialOfferKeeper3, time);
				}
			}
			for (int l = this.outOfShopOffers.Count - 1; l >= 0; l--)
			{
				SpecialOfferKeeper specialOfferKeeper4 = this.outOfShopOffers[l];
				if (specialOfferKeeper4.HasEnd(time))
				{
					this.KillOffer(specialOfferKeeper4, time);
				}
			}
			if (this.standardOffer.CanAppear(time))
			{
				ShopPack shopPack = SaveLoadManager.ConvertShopPack(this.previousStandardOffer);
				this.availablePacks.Clear();
				Type type = (shopPack != null) ? shopPack.GetType() : null;
				bool flag = false;
				foreach (ShopPack shopPack2 in this.allStandardPacks)
				{
					if (shopPack2.CanAppear(simulator) && shopPack2.GetType() != type)
					{
						this.availablePacks.Add(shopPack2);
						flag = true;
					}
				}
				if (flag)
				{
					this.standardOffer.PickRandomPack(simulator, time, this.availablePacks);
					this.hasAllSeen = false;
					UiManager.stateJustChanged = true;
				}
			}
			this.availablePacks.Clear();
			foreach (ShopPack shopPack3 in this.allUniquePacks)
			{
				if (shopPack3.CanAppear(simulator))
				{
					this.availablePacks.Add(shopPack3);
				}
			}
			foreach (ShopPack shopPack4 in this.availablePacks)
			{
				IHasPackState hasPackState = shopPack4 as IHasPackState;
				if (hasPackState != null && !hasPackState.isAppeared && !hasPackState.isPurchased)
				{
					if (!this.ThereIsOfferOfType(this.uniqueOffers, shopPack4.GetType()))
					{
						this.uniqueOffers.Add(SpecialOfferKeeper.Create(shopPack4, time));
						this.hasAllSeen = false;
						shopPack4.OnAppeared();
						UiManager.stateJustChanged = true;
					}
				}
			}
			this.availablePacks.Clear();
			foreach (ShopPack shopPack5 in this.allRegionalPacks)
			{
				if (shopPack5.CanAppear(simulator))
				{
					this.availablePacks.Add(shopPack5);
				}
			}
			foreach (ShopPack shopPack6 in this.availablePacks)
			{
				IHasPackState hasPackState2 = shopPack6 as IHasPackState;
				if (hasPackState2 != null && !hasPackState2.isPurchased)
				{
					if (!this.ThereIsOfferOfType(this.regionalOffers, shopPack6.GetType()))
					{
						this.regionalOffers.Add(SpecialOfferKeeper.Create(shopPack6, shopPack6.timeStart));
						if (!hasPackState2.isAppeared)
						{
							this.hasAllSeen = false;
							shopPack6.OnAppeared();
						}
						UiManager.stateJustChanged = true;
					}
				}
			}
			this.availablePacks.Clear();
			foreach (ShopPack shopPack7 in this.allSeasonalPacks)
			{
				if (shopPack7.CanAppear(simulator))
				{
					this.availablePacks.Add(shopPack7);
				}
			}
			foreach (ShopPack shopPack8 in this.availablePacks)
			{
				IHasPackState hasPackState3 = shopPack8 as IHasPackState;
				if (hasPackState3 != null && !hasPackState3.isPurchased)
				{
					if (!this.ThereIsOfferOfType(this.seasonalOffers, shopPack8.GetType()))
					{
						this.seasonalOffers.Add(SpecialOfferKeeper.Create(shopPack8, shopPack8.timeStart));
						if (!hasPackState3.isAppeared)
						{
							this.hasAllSeen = false;
							shopPack8.OnAppeared();
						}
						UiManager.stateJustChanged = true;
					}
				}
			}
			this.availablePacks.Clear();
			foreach (ShopPack shopPack9 in this.allOutOfShopPacks)
			{
				if (shopPack9.CanAppear(simulator))
				{
					this.availablePacks.Add(shopPack9);
				}
			}
			foreach (ShopPack shopPack10 in this.availablePacks)
			{
				IHasPackState hasPackState4 = shopPack10 as IHasPackState;
				if (hasPackState4 != null && !hasPackState4.isPurchased)
				{
					if (!this.ThereIsOfferOfType(this.outOfShopOffers, shopPack10.GetType()))
					{
						this.outOfShopOffers.Add(SpecialOfferKeeper.Create(shopPack10, shopPack10.timeStart));
						if (!hasPackState4.isAppeared)
						{
							this.hasAllSeenOutOfShop = false;
							shopPack10.OnAppeared();
						}
						UiManager.stateJustChanged = true;
					}
				}
			}
			this.availablePacks.Clear();
			if (this.reAppearingUniqueOffer.CanAppear(time))
			{
				ShopPack shopPack11 = SaveLoadManager.ConvertShopPack(this.previousReAppearOffer);
				foreach (ShopPack shopPack12 in this.allUniquePacks)
				{
					IHasPackState hasPackState5 = shopPack12 as IHasPackState;
					if (shopPack12.CanAppear(simulator))
					{
						if (hasPackState5 != null && hasPackState5.isAppeared && !hasPackState5.isPurchased)
						{
							if (shopPack11 == null || shopPack12.GetType() != shopPack11.GetType())
							{
								if (!this.ThereIsOfferOfType(this.uniqueOffers, shopPack12.GetType()))
								{
									this.availablePacks.Add(shopPack12);
								}
							}
						}
					}
				}
				if (this.availablePacks.Count > 0)
				{
					this.reAppearingUniqueOffer.PickRandomPack(simulator, time, this.availablePacks);
					this.hasAllSeen = false;
					UiManager.stateJustChanged = true;
				}
			}
		}

		private bool ThereIsOfferOfType(List<SpecialOfferKeeper> container, Type targetType)
		{
			int num = container.Count - 1;
			while (num >= 0 && container[num].offerPack.GetType() != targetType)
			{
				num--;
			}
			return num >= 0;
		}

		private void RemoveAllOffersThatCanNotAppear(List<ShopPack> container, Simulator simulator)
		{
			int num = container.Count;
			for (int i = num - 1; i >= 0; i--)
			{
				if (!container[i].CanAppear(simulator))
				{
					num--;
					if (i != num)
					{
						container[i] = container[num];
					}
					container.RemoveAt(num);
				}
			}
		}

		private void RemoveAllOffersOfType(List<ShopPack> container, Type type)
		{
			int num = container.Count;
			for (int i = num - 1; i >= 0; i--)
			{
				if (container[i].GetType() == type)
				{
					num--;
					if (i == num)
					{
						container[i] = container[num];
					}
					container.RemoveAt(num);
				}
			}
		}

		public bool AnyAnnouncedOfferAvailable(Simulator sim)
		{
			return this.CurrentAnnouncingOffer(sim) != null;
		}

		public SpecialOfferKeeper CurrentAnnouncingOffer(Simulator sim)
		{
			if (this.OfferShouldBeAnnounced(this.standardOffer, sim))
			{
				return this.standardOffer;
			}
			foreach (SpecialOfferKeeper specialOfferKeeper in this.uniqueOffers)
			{
				if (this.OfferShouldBeAnnounced(specialOfferKeeper, sim))
				{
					return specialOfferKeeper;
				}
			}
			foreach (SpecialOfferKeeper specialOfferKeeper2 in this.regionalOffers)
			{
				if (this.OfferShouldBeAnnounced(specialOfferKeeper2, sim))
				{
					return specialOfferKeeper2;
				}
			}
			foreach (SpecialOfferKeeper specialOfferKeeper3 in this.seasonalOffers)
			{
				if (this.OfferShouldBeAnnounced(specialOfferKeeper3, sim))
				{
					return specialOfferKeeper3;
				}
			}
			return null;
		}

		public SpecialOfferKeeper GetNextUnannouncedOffer(Simulator sim)
		{
			if (this.OfferCanBeAnnounced(this.standardOffer, sim))
			{
				return this.standardOffer;
			}
			foreach (SpecialOfferKeeper specialOfferKeeper in this.uniqueOffers)
			{
				if (this.OfferCanBeAnnounced(specialOfferKeeper, sim))
				{
					return specialOfferKeeper;
				}
			}
			foreach (SpecialOfferKeeper specialOfferKeeper2 in this.regionalOffers)
			{
				if (this.OfferCanBeAnnounced(specialOfferKeeper2, sim))
				{
					return specialOfferKeeper2;
				}
			}
			foreach (SpecialOfferKeeper specialOfferKeeper3 in this.seasonalOffers)
			{
				if (this.OfferCanBeAnnounced(specialOfferKeeper3, sim))
				{
					return specialOfferKeeper3;
				}
			}
			return null;
		}

		private bool OfferShouldBeAnnounced(SpecialOfferKeeper offer, Simulator sim)
		{
			return offer.IsAlive() && offer.offerPack.shouldBeAnnounceWithPopup;
		}

		private bool OfferCanBeAnnounced(SpecialOfferKeeper offer, Simulator sim)
		{
			if (offer.IsAlive() && offer.offerPack.shouldBeAnnounceWithPopup)
			{
				Simulator.AnnouncedOfferInfo announcedOfferInfo = sim.announcedOffersTimes.Find((Simulator.AnnouncedOfferInfo info) => info.offerId == offer.offerPack.id.Value);
				return announcedOfferInfo == null || (TrustedTime.IsReady() && TrustedTime.Get() > announcedOfferInfo.announcingTime.AddDays(1.0));
			}
			return false;
		}

		public static TimeSpan StandardOfferReAppearInterval = TimeSpan.FromHours(24.0);

		public static TimeSpan UniqueOfferReAppearInterval = TimeSpan.FromDays(7.0);

		public int previousStandardOffer;

		public SpecialOfferKeeper standardOffer;

		public int previousReAppearOffer;

		public SpecialOfferKeeper reAppearingUniqueOffer;

		public List<SpecialOfferKeeper> uniqueOffers;

		public List<SpecialOfferKeeper> regionalOffers;

		public List<SpecialOfferKeeper> seasonalOffers;

		public List<SpecialOfferKeeper> outOfShopOffers;

		public bool hasAllSeen;

		public bool hasAllSeenOutOfShop;

		private List<ShopPack> allStandardPacks;

		private List<ShopPack> allRegionalPacks;

		private List<ShopPack> allUniquePacks;

		private List<ShopPack> allSeasonalPacks;

		private List<ShopPack> allOutOfShopPacks;

		private List<ShopPack> availablePacks;
	}
}
