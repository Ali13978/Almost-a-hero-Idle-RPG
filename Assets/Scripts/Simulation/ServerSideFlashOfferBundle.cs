using System;
using System.Collections.Generic;

namespace Simulation
{
	public class ServerSideFlashOfferBundle
	{
		public static ServerSideFlashOfferBundle CreateHalloweenBundle()
		{
			return ServerSideFlashOfferBundle.CreateBundleWith(FlashOfferFactory.HallowenOffers);
		}

		public static ServerSideFlashOfferBundle CreateSecondAnniversaryBundle()
		{
			return ServerSideFlashOfferBundle.CreateBundleWith(FlashOfferFactory.SecondAnniversaryOffers);
		}

		public FlashOffer GetSkinOfferWithId(int skinId)
		{
			return this.offers.Find((FlashOffer o) => (o.type == FlashOffer.Type.COSTUME || o.type == FlashOffer.Type.COSTUME_PLUS_SCRAP) && o.genericIntId == skinId);
		}

		private static ServerSideFlashOfferBundle CreateBundleWith(List<FlashOfferFactory.AdventureOfferInfo> offersInfo)
		{
			ServerSideFlashOfferBundle serverSideFlashOfferBundle = new ServerSideFlashOfferBundle();
			serverSideFlashOfferBundle.offers = new List<FlashOffer>();
			for (int i = 0; i < offersInfo.Count; i++)
			{
				serverSideFlashOfferBundle.offers.Add(offersInfo[i].flashOffer.Clone());
			}
			return serverSideFlashOfferBundle;
		}

		public List<FlashOffer> offers;

		public bool hasSeen;
	}
}
