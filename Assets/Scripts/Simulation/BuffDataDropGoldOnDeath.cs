using System;
using Render;
using UnityEngine;

namespace Simulation
{
	public class BuffDataDropGoldOnDeath : BuffData
	{
		public override void OnDeathSelf(Buff buff)
		{
			if (!GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed))
			{
				return;
			}
			Hero hero = buff.GetBy() as Hero;
			this.genericFlag = false;
			Unit by = buff.GetBy();
			World world = by.world;
			double num = world.activeChallenge.GetTotalGoldOfWave() * this.dropGoldFactorAdd;
			int randomInt = GameMath.GetRandomInt(3, 5, GameMath.RandType.NoSeed);
			Vector3 pos = hero.pos;
			pos.y += by.GetHeight() * 0.5f;
			for (int i = 0; i < randomInt; i++)
			{
				Vector3 velStart = new Vector3(GameMath.GetRandomFloat(0.8f, -0.8f, GameMath.RandType.NoSeed) - pos.x, GameMath.GetRandomFloat(1.2f, 1.8f, GameMath.RandType.NoSeed), 0f);
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.GOLD, 0.0, world, false);
				if (world.gameMode == GameMode.CRUSADE)
				{
					dropCurrency.invPos = RenderManager.POS_GOLD_INV_TIMECHALLENGE;
				}
				dropCurrency.InitVel(0f, pos, by.pos.y, velStart);
				double num2 = num * GameMath.GetRandomDouble(0.75, 1.25, GameMath.RandType.NoSeed) / (double)randomInt;
				if (num2 > 0.0)
				{
					dropCurrency.amount = num2;
					world.drops.Add(dropCurrency);
				}
			}
		}

		public double dropGoldFactorAdd;

		public float chance;
	}
}
