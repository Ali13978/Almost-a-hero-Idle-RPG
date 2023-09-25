using System;
using System.Collections.Generic;

namespace Simulation
{
	public class CharmBuffVengefulSparks : CharmBuff
	{
		protected override bool TryActivating()
		{
			this.counterCharms = 0;
			this.lastTimer = 0f;
			return true;
		}

		protected override void OnUpdate(float dt)
		{
			if (this.state == EnchantmentBuffState.ACTIVE && this.counterCharms < this.totalNumCharms)
			{
				this.lastTimer += dt;
				if (this.lastTimer >= CharmBuffVengefulSparks.TIME_DELAY)
				{
					this.lastTimer = 0f;
					this.counterCharms++;
					this.AddProgressToRandomCharm();
				}
			}
		}

		private void AddProgressToRandomCharm()
		{
			ChallengeRift challengeRift = this.world.activeChallenge as ChallengeRift;
			if (challengeRift == null)
			{
				return;
			}
			List<CharmBuff> list = new List<CharmBuff>();
			List<CharmBuff> charmsThatAreNotAlwaysActive = challengeRift.GetCharmsThatAreNotAlwaysActive();
			foreach (CharmBuff charmBuff in charmsThatAreNotAlwaysActive)
			{
				if (!(charmBuff is CharmBuffVengefulSparks))
				{
					if (charmBuff.progress < 1.9f)
					{
						list.Add(charmBuff);
					}
				}
			}
			if (list.Count <= 0)
			{
				return;
			}
			CharmBuff charmBuff2 = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
			charmBuff2.AddProgress(this.progressAddAmount);
		}

		public override void OnPreDamage(Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (damager is Enemy)
			{
				this.AddProgress(this.pic);
			}
		}

		public int totalNumCharms;

		public float progressAddAmount;

		private int counterCharms;

		private float lastTimer;

		public static float TIME_DELAY = 0.25f;
	}
}
