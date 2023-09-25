using System;
using Render;
using UnityEngine;

namespace Simulation
{
	public class BuffDataGoldyStand : BuffData
	{
		public BuffDataGoldyStand(int attackCount, float extraGoldPercentage)
		{
			this.attackCount = attackCount;
			this.extraGoldPercentage = extraGoldPercentage;
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			Enemy enemy = target as Enemy;
			if (enemy == null)
			{
				return;
			}
			if (this.hitCounter >= this.attackCount)
			{
				this.hitCounter = 0;
				double num = enemy.GetTotalLootToDrop() * (double)this.extraGoldPercentage;
				Unit by = buff.GetBy();
				World world = by.world;
				Vector3 pos = by.pos;
				int num2 = 10;
				for (int i = 0; i < num2; i++)
				{
					Vector3 velStart = new Vector3(GameMath.GetRandomFloat(0.8f, -0.8f, GameMath.RandType.NoSeed) - pos.x, GameMath.GetRandomFloat(1.2f, 1.8f, GameMath.RandType.NoSeed), 0f);
					DropCurrency dropCurrency = new DropCurrency(CurrencyType.GOLD, 0.0, world, false);
					if (world.gameMode == GameMode.CRUSADE)
					{
						dropCurrency.invPos = RenderManager.POS_GOLD_INV_TIMECHALLENGE;
					}
					dropCurrency.InitVel(0f, pos, by.pos.y, velStart);
					double num3 = num * GameMath.GetRandomDouble(0.5, 1.5, GameMath.RandType.NoSeed) / (double)num2;
					if (num3 > 0.0)
					{
						dropCurrency.amount = num3;
						world.drops.Add(dropCurrency);
					}
				}
			}
		}

		public override void OnDeathSelf(Buff buff)
		{
			this.hitCounter = 0;
		}

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			this.hitCounter++;
		}

		public override void OnDodged(Buff buff, Unit attacker, Damage damage)
		{
			this.hitCounter++;
		}

		private int attackCount;

		private float extraGoldPercentage;

		private int hitCounter;
	}
}
