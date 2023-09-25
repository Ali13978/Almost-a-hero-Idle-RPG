using System;
using UnityEngine;

namespace Simulation
{
	public class RiftEffectDyingHeroesDropGold : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 0.6f;
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectDyingHeroesDropGold();
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_DYING_HEROES_DROP_GOLD"), GameMath.GetPercentString(1.0, false));
		}

		public override void OnDeathAny(Unit unit)
		{
			if (unit is Hero)
			{
				World world = unit.world;
				ChallengeRift challengeRift = unit.world.activeChallenge as ChallengeRift;
				double num = challengeRift.GetTotalGoldOfWave() * 1.0;
				int randomInt = GameMath.GetRandomInt(3, 8, GameMath.RandType.NoSeed);
				Vector3 pos = unit.pos;
				pos.y += unit.GetHeight() * 0.5f;
				for (int i = 0; i < randomInt; i++)
				{
					Vector3 velStart = new Vector3(GameMath.GetRandomFloat(0.8f, -0.8f, GameMath.RandType.NoSeed) - pos.x, GameMath.GetRandomFloat(1.2f, 1.8f, GameMath.RandType.NoSeed), 0f);
					DropCurrency dropCurrency = new DropCurrency(CurrencyType.GOLD, 0.0, world, false);
					dropCurrency.InitVel(0f, pos, unit.pos.y, velStart);
					double num2 = num * GameMath.GetRandomDouble(0.5, 1.5, GameMath.RandType.NoSeed) / (double)randomInt;
					if (num2 > 0.0)
					{
						dropCurrency.amount = num2;
						world.drops.Add(dropCurrency);
					}
				}
			}
		}

		private const double percentage = 1.0;
	}
}
