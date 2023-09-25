using System;
using UnityEngine;

namespace Simulation
{
	public class CharmBuffWealthFromAbove : CharmBuff
	{
		protected override bool TryActivating()
		{
			for (int i = 0; i < this.totalNumCoins; i++)
			{
				Vector3 a = new Vector3(GameMath.GetRandomFloat(-0.6f, 0.6f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.25f, 0.15f, GameMath.RandType.NoSeed), 0f);
				Vector3 startPos = a + Vector3.up * 1.5f;
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.GOLD, this.goldAmount * this.world.activeChallenge.GetTotalGoldOfWave(), this.world, false);
				dropCurrency.InitVel((float)i * 0.06f, startPos, a.y, Vector3.zero);
				this.world.drops.Add(dropCurrency);
			}
			return true;
		}

		public override void OnHeroShielded(Hero hero)
		{
			this.AddProgress(this.pic);
		}

		public int totalNumCoins;

		public double goldAmount;
	}
}
