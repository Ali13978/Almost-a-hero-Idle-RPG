using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SpecialOfferKeeper
	{
		public bool CanAppear(DateTime currentTime)
		{
			return this.offerPack == null && this.GetRemainingDurToNextOffer(currentTime).TotalSeconds <= 0.0;
		}

		public bool HasEnd(DateTime currentTime)
		{
			return this.offerPack != null && this.GetRemainingDur(currentTime).TotalSeconds <= 0.0;
		}

		public TimeSpan GetRemainingDur(DateTime currentTime)
		{
			return this.offerEndTime - currentTime;
		}

		public TimeSpan GetRemainingDurToNextOffer(DateTime currentTime)
		{
			return this.nextOfferApperTime - currentTime;
		}

		public void KillOffer(DateTime nextAvailabilityTime)
		{
			this.offerPack = null;
			this.nextOfferApperTime = nextAvailabilityTime;
		}

		public void KillOffer()
		{
			this.offerPack = null;
		}

		public void PickRandomPack(Simulator simulator, DateTime time, List<ShopPack> packs)
		{
			List<float> list = new List<float>();
			foreach (ShopPack shopPack in packs)
			{
				list.Add(shopPack.GetChanceWeight(simulator));
			}
			int rouletteOutcome = GameMath.GetRouletteOutcome(list, GameMath.RandType.NoSeed);
			ShopPack shopPack2 = packs[rouletteOutcome];
			this.offerPack = shopPack2;
			this.offerPack.OnAppeared();
			this.offerEndTime = time + TimeSpan.FromSeconds(shopPack2.totalTime);
		}

		public bool IsAlive()
		{
			return this.offerPack != null;
		}

		public static SpecialOfferKeeper Create(ShopPack pack, DateTime time)
		{
			return new SpecialOfferKeeper
			{
				offerPack = pack,
				offerEndTime = time + TimeSpan.FromSeconds(pack.totalTime),
				nextOfferApperTime = DateTime.MinValue
			};
		}

		public static SpecialOfferKeeper CreateLoad(ShopPack pack, DateTime endTime, DateTime nextOfferTime)
		{
			return new SpecialOfferKeeper
			{
				offerPack = pack,
				offerEndTime = endTime,
				nextOfferApperTime = nextOfferTime
			};
		}

		public static SpecialOfferKeeper Empty()
		{
			return new SpecialOfferKeeper
			{
				offerPack = null,
				nextOfferApperTime = DateTime.MinValue
			};
		}

		public DateTime nextOfferApperTime;

		public DateTime offerEndTime;

		public ShopPack offerPack;
	}
}
