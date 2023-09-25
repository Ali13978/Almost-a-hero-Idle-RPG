using System;
using UnityEngine;

namespace Simulation
{
	public class CharmBuffSpecialDelivery : CharmBuff
	{
		protected override bool TryActivating()
		{
			double goldToReward = this.goldPercentageAmount * this.world.activeChallenge.GetTotalGoldOfWave();
			TreasureDrop item = new TreasureDrop
			{
				scale = 0.82f,
				world = this.world,
				goldToReward = goldToReward,
				pos = new Vector3(GameMath.GetRandomFloat(-0.22f, 0.22f, GameMath.RandType.NoSeed), 2.1f, 0f),
				speed = -2f,
				acc = -0.8f,
				state = TreasureDrop.State.DROP,
				stateTime = 0f,
				groundPosY = -0.15f,
				soundExplode = new SoundEventSound(SoundType.GAMEPLAY, "charm", false, 0f, new SoundSimple(SoundArchieve.inst.chestDeaths[0], 1f, float.MaxValue))
			};
			this.world.treasureDrops.Add(item);
			return true;
		}

		public override void OnHeroHealed(Hero hero, double ratioHealed)
		{
			this.AddProgress(this.pic * (float)ratioHealed);
		}

		public double goldPercentageAmount;
	}
}
