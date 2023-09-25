using System;
using UnityEngine;

namespace Simulation
{
	public class BuffEventGreedGrenade : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			double num = world.activeChallenge.GetTotalGoldOfWave() * this.goldFactor;
			int randomInt = GameMath.GetRandomInt(5, 9, GameMath.RandType.NoSeed);
			Vector3 pos = by.pos;
			pos.x += 1f;
			pos.y += by.GetHeight() * 0.5f;
			for (int i = 0; i < randomInt; i++)
			{
				Vector3 velStart = new Vector3(GameMath.GetRandomFloat(0.8f, -0.8f, GameMath.RandType.NoSeed) - pos.x, GameMath.GetRandomFloat(1.2f, 1.8f, GameMath.RandType.NoSeed), 0f);
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.GOLD, 0.0, world, false);
				dropCurrency.InitVel(0f, pos, by.pos.y, velStart);
				double num2 = num * GameMath.GetRandomDouble(0.75, 1.25, GameMath.RandType.NoSeed) / (double)randomInt;
				if (num2 > 0.0)
				{
					dropCurrency.amount = num2;
					world.drops.Add(dropCurrency);
				}
			}
		}

		public double goldFactor;
	}
}
