using System;
using Simulation;
using UnityEngine;

namespace Ui
{
	public class UiCommandAdCollect : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			World activeWorld = sim.GetActiveWorld();
			activeWorld.adDragonState = World.AdDragonState.ACTIVATE;
			activeWorld.adDragonTimeCounter = 0f;
			double adRewardAmount = activeWorld.adRewardAmount;
			int num = (activeWorld.adRewardCurrencyType != CurrencyType.GOLD) ? 10 : 20;
			if (adRewardAmount < (double)num)
			{
				num = (int)adRewardAmount;
			}
			double amount = adRewardAmount / (double)num;
			for (int i = 0; i < num; i++)
			{
				Vector3 adDragonPos = activeWorld.adDragonPos;
				Vector3 vector = new Vector3(activeWorld.adDragonPos.x + GameMath.GetRandomFloat(-0.3f, 0.3f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.4f, 0.2f, GameMath.RandType.NoSeed), 0f);
				Vector3 velStart = new Vector3(GameMath.GetRandomFloat(0.8f, -0.8f, GameMath.RandType.NoSeed) - adDragonPos.x, GameMath.GetRandomFloat(1.2f, 1.8f, GameMath.RandType.NoSeed), 0f);
				DropCurrency dropCurrency = new DropCurrency(activeWorld.adRewardCurrencyType, amount, activeWorld, false);
				dropCurrency.InitVel(0f, adDragonPos, vector.y, velStart);
				activeWorld.drops.Add(dropCurrency);
			}
			if (this.canDropCandies && activeWorld.canDropCandies)
			{
				int randomInt = GameMath.GetRandomInt(25, 35, GameMath.RandType.NoSeed);
				for (int j = 0; j < randomInt; j++)
				{
					Vector3 adDragonPos2 = activeWorld.adDragonPos;
					Vector3 vector2 = new Vector3(activeWorld.adDragonPos.x + GameMath.GetRandomFloat(-0.3f, 0.3f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.4f, 0.2f, GameMath.RandType.NoSeed), 0f);
					Vector3 velStart2 = new Vector3(GameMath.GetRandomFloat(-0.8f, 0.8f, GameMath.RandType.NoSeed) - adDragonPos2.x * 0.5f, GameMath.GetRandomFloat(0f, -0.5f, GameMath.RandType.NoSeed), 0f);
					DropCurrency dropCurrency2 = new DropCurrency(CurrencyType.CANDY, 2.0, activeWorld, true);
					dropCurrency2.InitVel(0f, adDragonPos2, vector2.y, velStart2);
					dropCurrency2.durNonExistence = (float)j * 0.02f;
					dropCurrency2.amount = 2.0;
					activeWorld.drops.Add(dropCurrency2);
				}
			}
		}

		public bool canDropCandies;
	}
}
