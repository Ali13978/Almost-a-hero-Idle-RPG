using System;
using Render;
using UnityEngine;

namespace Simulation
{
	public class RiftEffectStunDropsGold : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 0.5f;
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectStunDropsGold
			{
				goldPerStun = this.goldPerStun
			};
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_STUN_DROPS_GOLD"), GameMath.GetPercentString(this.goldPerStun, false));
		}

		public override void OnEnemyStunned(Enemy enemy, World world)
		{
			double num = world.activeChallenge.GetTotalGoldOfWave() * this.goldPerStun;
			int randomInt = GameMath.GetRandomInt(3, 6, GameMath.RandType.NoSeed);
			Vector3 pos = enemy.pos;
			pos.y += enemy.GetHeight() * 0.5f;
			for (int i = 0; i < randomInt; i++)
			{
				Vector3 velStart = new Vector3(GameMath.GetRandomFloat(0.8f, -0.8f, GameMath.RandType.NoSeed) - pos.x, GameMath.GetRandomFloat(1.2f, 1.8f, GameMath.RandType.NoSeed), 0f);
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.GOLD, 0.0, world, false);
				if (world.gameMode == GameMode.CRUSADE)
				{
					dropCurrency.invPos = RenderManager.POS_GOLD_INV_TIMECHALLENGE;
				}
				dropCurrency.InitVel(0f, pos, enemy.pos.y, velStart);
				double num2 = num * GameMath.GetRandomDouble(0.5, 1.5, GameMath.RandType.NoSeed) / (double)randomInt;
				if (num2 > 0.0)
				{
					dropCurrency.amount = num2;
					world.drops.Add(dropCurrency);
				}
			}
		}

		public double goldPerStun = 0.1;
	}
}
