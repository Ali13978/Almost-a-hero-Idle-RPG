using System;

namespace Simulation
{
	public class Unlock
	{
		public Unlock(uint id, World world, UnlockReq req, UnlockReward reward)
		{
			this.id = id;
			this.world = world;
			this.req = req;
			this.reward = reward;
			this.isHidden = false;
			this.isCollected = false;
		}

		public Unlock(uint id, World world, UnlockReq req, UnlockReward reward, bool isHiddenUnlock)
		{
			this.id = id;
			this.world = world;
			this.req = req;
			this.reward = reward;
			this.isHidden = isHiddenUnlock;
			this.isCollected = false;
		}

		public uint GetId()
		{
			return this.id;
		}

		public bool IsReqSatisfied(Simulator sim)
		{
			return this.req.IsSatisfied(sim, this.world);
		}

		public void GiveReward(Simulator sim)
		{
			if (this.isCollected)
			{
				return;
			}
			this.isCollected = true;
			this.reward.Give(sim, this.world);
		}

		public string GetReqString()
		{
			return this.req.GetReqString();
		}

		public string GetReqStringLessDetail()
		{
			return this.req.GetReqStringLessDetail();
		}

		public string GetReqStringEvenLessDetail()
		{
			return this.req.GetReqStringEvenLessDetail();
		}

		public int GetReqInt()
		{
			return this.req.GetReqInt();
		}

		public string GetReqSatisfiedString()
		{
			return this.req.GetReqSatisfiedString();
		}

		public string GetRewardString()
		{
			return this.reward.GetRewardString();
		}

		public string GetRewardedString()
		{
			return this.reward.GetRewardedString();
		}

		public int GetNextUnlockStage(int curStageNo)
		{
			if (this.isCollected)
			{
				return -1;
			}
			if (this.req is UnlockReqReachStage)
			{
				UnlockReqReachStage unlockReqReachStage = (UnlockReqReachStage)this.req;
				int stageNo = unlockReqReachStage.GetStageNo();
				if (curStageNo < stageNo)
				{
					return stageNo;
				}
			}
			return -1;
		}

		public bool HasReqOfType(Type reqType)
		{
			return this.req.GetType() == reqType;
		}

		public bool HasRewardOfType(Type rewardType)
		{
			return this.reward.GetType() == rewardType || this.reward.GetType().IsSubclassOf(rewardType);
		}

		public double GetRewardCurrencyAmount()
		{
			if (this.reward is UnlockRewardCurrency)
			{
				return ((UnlockRewardCurrency)this.reward).GetAmount();
			}
			return 0.0;
		}

		public bool DoesRewardCurrency()
		{
			return this.reward is UnlockRewardCurrency;
		}

		public void RainCurrency()
		{
			if (!(this.reward is UnlockRewardCurrency))
			{
				return;
			}
			this.isCollected = true;
			UnlockRewardCurrency unlockRewardCurrency = (UnlockRewardCurrency)this.reward;
			unlockRewardCurrency.RainCurrency(this.world);
		}

		public int GetReqAmount()
		{
			if (this.req is UnlockReqReachStage)
			{
				UnlockReqReachStage unlockReqReachStage = (UnlockReqReachStage)this.req;
				return unlockReqReachStage.GetStageNo();
			}
			if (this.req is UnlockReqReachHeroLevel)
			{
				UnlockReqReachHeroLevel unlockReqReachHeroLevel = (UnlockReqReachHeroLevel)this.req;
				return unlockReqReachHeroLevel.GetLevel();
			}
			if (this.req is UnlockReqTimeChallenge)
			{
				return (this.req as UnlockReqTimeChallenge).challenge.GetTargetTotWave();
			}
			if (this.req is UnlockReqRiftChallenge)
			{
				return (this.req as UnlockReqRiftChallenge).challenge.GetTargetTotWave();
			}
			throw new NotImplementedException();
		}

		public string GetHeroId()
		{
			if (!(this.reward is UnlockRewardHero))
			{
				return "$NONE$";
			}
			UnlockRewardHero unlockRewardHero = (UnlockRewardHero)this.reward;
			return unlockRewardHero.GetHeroId();
		}

		public string GetTotemId()
		{
			if (!(this.reward is UnlockRewardTotem))
			{
				return "$NONE$";
			}
			UnlockRewardTotem unlockRewardTotem = (UnlockRewardTotem)this.reward;
			return unlockRewardTotem.GetTotemId();
		}

		public string GetRuneId()
		{
			if (!(this.reward is UnlockRewardRune))
			{
				return "$NONE$";
			}
			UnlockRewardRune unlockRewardRune = (UnlockRewardRune)this.reward;
			return unlockRewardRune.GetRuneId();
		}

		public string GetMerchantItemId()
		{
			if (!(this.reward is UnlockRewardMerchantItem))
			{
				return "$NONE$";
			}
			UnlockRewardMerchantItem unlockRewardMerchantItem = (UnlockRewardMerchantItem)this.reward;
			return unlockRewardMerchantItem.GetMerchantItemId();
		}

		public ChallengeWithTime GetTimeChallengeIfExists()
		{
			if (!(this.req is UnlockReqTimeChallenge))
			{
				return null;
			}
			UnlockReqTimeChallenge unlockReqTimeChallenge = (UnlockReqTimeChallenge)this.req;
			return unlockReqTimeChallenge.challenge;
		}

		public ChallengeRift GetRiftChallengeIfExist()
		{
			if (!(this.req is UnlockReqRiftChallenge))
			{
				return null;
			}
			UnlockReqRiftChallenge unlockReqRiftChallenge = (UnlockReqRiftChallenge)this.req;
			return unlockReqRiftChallenge.challenge;
		}

		public void DEBUGreset()
		{
			this.isCollected = false;
		}

		public UnlockReward GetReward()
		{
			return this.reward;
		}

		private uint id;

		private World world;

		private UnlockReq req;

		private UnlockReward reward;

		public bool isCollected;

		public bool isHidden;
	}
}
