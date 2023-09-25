using System;
using System.Collections;
using System.Collections.Generic;
using Simulation;
using UnityEngine;

namespace SocialRewards
{
	public static class Manager
	{
		public static void Init(Simulator simulator, MonoBehaviour coroutiner)
		{
			Manager.simulator = simulator;
			Manager.coroutiner = coroutiner;
		}

		public static Network? GetNetworkPendingOfReward()
		{
			if (!Manager.socialNetworkOpened)
			{
				return null;
			}
			foreach (KeyValuePair<Network, Status> keyValuePair in Manager.simulator.socialRewardsStatus)
			{
				if (keyValuePair.Value.pendingReward && !keyValuePair.Value.rewardGiven)
				{
					return new Network?(keyValuePair.Key);
				}
			}
			return null;
		}

		public static void PlayerWantsToGoTo(Network network)
		{
			if (Manager.IsOfferAvailable(network))
			{
				Manager.simulator.socialRewardsStatus[network].pendingReward = true;
			}
			Manager.socialNetworkOpened = false;
			if (network != Network.Facebook)
			{
				if (network != Network.Twitter)
				{
					if (network == Network.Instagram)
					{
						Manager.OpenSocialNetwork("instagram://user?username=almostaherogame", "https://www.instagram.com/almostaherogame/");
					}
				}
				else
				{
					Manager.OpenSocialNetwork("twitter://user?user_id=839990607296778240", "https://twitter.com/almostaherogame");
				}
			}
			else
			{
				Manager.OpenSocialNetwork("fb://page/1461084277278488", "https://www.facebook.com/almostaherogame/");
			}
		}

		public static void OnRewardGiven(Network network)
		{
			Manager.simulator.socialRewardsStatus[network].rewardGiven = true;
			PlayfabManager.SendPlayerEvent(PlayfabEventId.SOCIAL_REWARD_COLLECT, new Dictionary<string, object>
			{
				{
					"socialNetwork",
					network
				}
			}, null, null, true);
		}

		public static bool IsOfferAvailable(Network network)
		{
			return !Manager.simulator.socialRewardsStatus[network].rewardGiven;
		}

		public static void OnApplicationPause()
		{
			Manager.socialNetworkOpened = true;
		}

		private static void OpenSocialNetwork(string appUrl, string browserUrl)
		{
			Application.OpenURL(appUrl);
			Manager.coroutiner.StartCoroutine(Manager.WaitToOpenBrowser(browserUrl));
		}

		private static IEnumerator WaitToOpenBrowser(string browserURL)
		{
			yield return Manager.WaitTime;
			if (!Manager.socialNetworkOpened)
			{
				Application.OpenURL(browserURL);
			}
			yield break;
		}

		public const double GemsRewardAmount = 50.0;

		public static readonly List<Network> NetworkList = Utility.GetEnumList<Network>();

		private static Simulator simulator;

		private static MonoBehaviour coroutiner;

		private static bool socialNetworkOpened = true;

		private static readonly WaitForSeconds WaitTime = new WaitForSeconds(0.5f);

		private const string FacebookUrl = "https://www.facebook.com/almostaherogame/";

		private const string TwitterUrl = "https://twitter.com/almostaherogame";

		private const string InstagramUrl = "https://www.instagram.com/almostaherogame/";

		private const string FacebookAppUrl = "fb://page/1461084277278488";

		private const string TwitterAppUrl = "twitter://user?user_id=839990607296778240";

		private const string InstagramAppUrl = "instagram://user?username=almostaherogame";
	}
}
