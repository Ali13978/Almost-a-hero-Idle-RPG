using System;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class MerchantItemGoldBag : MerchantItem
	{
		public override string GetId()
		{
			return "GOLD_PACK";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_GOLDBAG");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			World activeWorld = sim.GetActiveWorld();
			double num = (double)count * MerchantItemGoldBag.GetAmountOfGold(sim) / (double)activeWorld.universalBonus.goldBagValueFactor;
			double num2 = (double)(activeWorld.universalBonus.goldBagValueFactor - 1f) * num;
			string str = string.Empty;
			if (num2 > 0.0)
			{
				str = AM.csl(" (+" + GameMath.GetDoubleString(num2) + ")");
			}
			return string.Format(LM.Get("MERCHANT_ITEM_DESC_GOLDBAG"), AM.csm(GameMath.GetDoubleString(num)) + str);
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			World activeWorld = sim.GetActiveWorld();
			double amountOfGold = MerchantItemGoldBag.GetAmountOfGold(sim);
			UnityEngine.Debug.Log(GameMath.GetDoubleString(amountOfGold));
			activeWorld.RainGold(amountOfGold);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.merchantItemGoldBag, 1f));
		}

		public static double GetAmountOfGold(Simulator sim)
		{
			World activeWorld = sim.GetActiveWorld();
			int num = activeWorld.GetStageNumber();
			if (activeWorld.activeChallenge is ChallengeStandard)
			{
				ChallengeStandard challengeStandard = (ChallengeStandard)activeWorld.activeChallenge;
				if (ChallengeStandard.IsBossWave(challengeStandard.totWave))
				{
					num--;
				}
			}
			double power = (double)num * 0.5;
			return 15000.0 * UnitMath.GetGoldToDropForPower(power) * activeWorld.universalBonus.goldFactor * activeWorld.universalBonus.gearGoldFactor * activeWorld.universalBonus.mineGoldFactor * (double)activeWorld.universalBonus.goldBagValueFactor * activeWorld.universalBonus.goldBagAdDragonFactor * activeWorld.activeChallenge.totalGainedUpgrades.goldFactor * (activeWorld.universalBonus.bountyIncreasePerDamageTakenFromHero * 50.0 + 1.0) * activeWorld.universalBonus.GetGoldFactorForCurrentSupportersInTeam(activeWorld.GetNumHeroesOfClassInTeam(HeroClass.SUPPORTER));
		}

		private const double AMOUNT = 15000.0;

		public static int BASE_COUNT = 5;

		public static int BASE_COST = 10;
	}
}
