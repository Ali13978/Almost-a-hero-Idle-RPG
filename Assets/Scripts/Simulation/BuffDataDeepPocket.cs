using System;
using Render;
using UnityEngine;

namespace Simulation
{
	public class BuffDataDeepPocket : BuffData
	{
		public BuffDataDeepPocket(double goldFactor, int numDropTimes, float cooldown)
		{
			this.goldFactor = goldFactor;
			this.numDropTimes = numDropTimes;
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
			if (this.dropCounter > 0)
			{
				if (this.enemyChosen == null || !this.enemyChosen.IsAlive())
				{
					this.dropCounter = 0;
					this.enemyChosen = null;
				}
				else
				{
					this.timerDrop += dt;
					if (this.timerDrop >= 1f)
					{
						this.dropCounter--;
						this.timerDrop = 0f;
						Unit by = buff.GetBy();
						World world = by.world;
						double num = this.enemyChosen.GetTotalLootToDrop() * this.goldFactor;
						int randomInt = GameMath.GetRandomInt(1, 4, GameMath.RandType.NoSeed);
						Vector3 pos = this.enemyChosen.pos;
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
				}
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
			if (target.IsAlive() && target is Enemy && this.genericFlag)
			{
				this.genericFlag = false;
				this.dropCounter = this.numDropTimes;
				this.timerVisual = (float)this.numDropTimes + 1f;
				this.timerDrop = 1f;
				this.enemyChosen = (target as Enemy);
				if ((this.enemyChosen.data.dataBase.type == EnemyDataBase.Type.BOSS || this.enemyChosen.data.dataBase.type == EnemyDataBase.Type.EPIC) && buff.GetBy().GetId() == "SHEELA")
				{
					buff.GetWorld().OnVStealFromBoss();
				}
			}
		}

		private double goldFactor;

		private int numDropTimes;

		private float cooldown;

		private float timerVisual;

		private float timerDrop;

		private int dropCounter;

		private Enemy enemyChosen;
	}
}
