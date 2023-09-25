using System;
using Render;
using UnityEngine;

namespace Simulation
{
	public class TreasureDrop
	{
		public void Update(float dt)
		{
			this.stateTime += dt;
			if (this.state == TreasureDrop.State.DROP)
			{
				this.speed += this.acc * dt;
				this.pos.y = this.pos.y + this.speed * dt;
				if (this.pos.y < this.groundPosY)
				{
					this.state = TreasureDrop.State.EXPLODE;
					this.stateTime = 0f;
					this.DropLootGold();
					if (this.soundExplode != null)
					{
						this.world.AddSoundEvent(this.soundExplode);
					}
				}
			}
			else if (this.state == TreasureDrop.State.EXPLODE && this.stateTime > TreasureDrop.EXPLOSION_TIME)
			{
				this.state = TreasureDrop.State.REMOVE;
				this.stateTime = 0f;
			}
		}

		private void DropLootGold()
		{
			float num = 0.42f;
			int min = 6;
			int exclusiveMax = 9;
			int randomInt = GameMath.GetRandomInt(min, exclusiveMax, GameMath.RandType.NoSeed);
			Vector3 startPos = this.pos;
			float num2 = 0.3f;
			startPos.x += GameMath.GetRandomFloat(-num2 * 0.4f + 0.1f, num2 * 0.4f, GameMath.RandType.NoSeed);
			startPos.y += num2 * 0.5f + GameMath.GetRandomFloat(-num2 * 0.25f, num2 * 0.25f, GameMath.RandType.NoSeed);
			for (int i = 0; i < randomInt; i++)
			{
				Vector3 velStart = new Vector3(GameMath.GetRandomFloat(0.4f, -0.4f, GameMath.RandType.NoSeed) - startPos.x * 0.25f, GameMath.GetRandomFloat(1.2f, 2.2f, GameMath.RandType.NoSeed), 0f);
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.GOLD, 0.0, this.world, false);
				if (this.world.gameMode == GameMode.CRUSADE)
				{
					dropCurrency.invPos = RenderManager.POS_GOLD_INV_TIMECHALLENGE;
				}
				dropCurrency.InitVel(num / (float)randomInt * (float)i, startPos, this.pos.y, velStart);
				double num3 = this.goldToReward * GameMath.GetRandomDouble(0.7, 1.3, GameMath.RandType.NoSeed) / (double)randomInt;
				if (num3 > 0.0)
				{
					dropCurrency.amount = num3;
					this.world.drops.Add(dropCurrency);
				}
			}
		}

		public World world;

		public Vector3 pos;

		public float scale;

		public float speed;

		public float acc;

		public float stateTime;

		public float groundPosY;

		public SoundEvent soundExplode;

		public double goldToReward;

		public TreasureDrop.State state;

		public static float EXPLOSION_TIME = 0.62f;

		public enum State
		{
			DROP,
			EXPLODE,
			REMOVE
		}
	}
}
