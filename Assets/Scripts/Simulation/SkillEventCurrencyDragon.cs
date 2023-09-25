using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simulation
{
	public class SkillEventCurrencyDragon : SkillEvent
	{
		public override void Apply(Unit by)
		{
			World world = by.world;
			bool flag = false;
			if (world.activeChallenge is TimeChallenge)
			{
				TimeChallenge timeChallenge = world.activeChallenge as TimeChallenge;
				if (timeChallenge.enemies.Count == 0 || timeChallenge.enemies[0].data.goldToDrop == 0.0)
				{
					flag = true;
				}
			}
			Hero hero = by as Hero;
			int randomInt = GameMath.GetRandomInt(0, this.allowedCurrencyTypes.Count, GameMath.RandType.NoSeed);
			KeyValuePair<CurrencyType, double>[] array = this.allowedCurrencyTypes.ToArray<KeyValuePair<CurrencyType, double>>();
			KeyValuePair<CurrencyType, double> keyValuePair = array[randomInt];
			double num = 0.0;
			if (!flag && keyValuePair.Key == CurrencyType.GOLD)
			{
				num = world.activeChallenge.GetTotalGoldOfWave();
				num *= keyValuePair.Value;
			}
			CurrencyDragon dragon = new CurrencyDragon
			{
				dropCurrency = keyValuePair.Key,
				dropAmount = num,
				maxTime = GameMath.GetRandomFloat(8f, 10f, GameMath.RandType.NoSeed),
				direction = 1f,
				speed = GameMath.GetRandomFloat(0.3f, 0.35f, GameMath.RandType.NoSeed),
				pos = new Vector3(1f - this.initialX, GameMath.GetRandomFloat(0.5f, 0.6f, GameMath.RandType.NoSeed), 0f),
				stateOffset = this.initialX,
				yOffset = this.yOffset,
				visualVariation = hero.GetEquippedSkinIndex()
			};
			if (by.GetId() == "GOBLIN")
			{
				world.OnGoblinSummonDragon();
			}
			world.AddCurrencyDragon(dragon);
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
		}

		public Dictionary<CurrencyType, double> allowedCurrencyTypes;

		public float initialX;

		public float yOffset;
	}
}
