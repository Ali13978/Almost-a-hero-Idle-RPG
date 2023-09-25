using System;
using Render;
using UnityEngine;

namespace Simulation
{
	public class BuffDataPickPocket : BuffData
	{
		public BuffDataPickPocket(double goldFactor, float cooldown)
		{
			this.goldFactor = goldFactor;
			this.cooldown = cooldown;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (this.timerVisual > 0f)
			{
				this.visuals = 4096;
				this.timerVisual -= dt;
			}
			else
			{
				this.visuals = 0;
			}
			if (this.genericFlag)
			{
				return;
			}
			this.genericTimer += dt;
			if (this.genericTimer >= this.cooldown)
			{
				this.genericTimer = 0f;
				this.genericFlag = true;
			}
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (target.IsAlive() && this.genericFlag && target is Enemy)
			{
				this.genericFlag = false;
				Unit by = buff.GetBy();
				World world = by.world;
				double num = (target as Enemy).GetTotalLootToDrop() * this.goldFactor;
				int randomInt = GameMath.GetRandomInt(3, 5, GameMath.RandType.NoSeed);
				Vector3 pos = target.pos;
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
					double num2 = num * GameMath.GetRandomDouble(0.5, 1.5, GameMath.RandType.NoSeed) / (double)randomInt;
					if (num2 > 0.0)
					{
						dropCurrency.amount = num2;
						world.drops.Add(dropCurrency);
					}
				}
				this.timerVisual = 2f;
				Enemy enemy = target as Enemy;
				if ((enemy.data.dataBase.type == EnemyDataBase.Type.BOSS || enemy.data.dataBase.type == EnemyDataBase.Type.EPIC) && buff.GetBy().GetId() == "SHEELA")
				{
					buff.GetWorld().OnVStealFromBoss();
				}
			}
		}

		private double goldFactor;

		private float cooldown;

		private float timerVisual;
	}
}
