using System;
using System.Collections.Generic;

namespace Simulation
{
	public class CharmBuffBodyBlock : CharmBuff
	{
		protected override bool TryActivating()
		{
			this.numShieldsThrown = 0;
			this.lastShieldThrownTimer = 0f;
			return true;
		}

		protected override void OnUpdate(float dt)
		{
			if (this.state == EnchantmentBuffState.ACTIVE && this.numShieldsThrown < this.numShields)
			{
				this.lastShieldThrownTimer += dt;
				if (this.lastShieldThrownTimer >= CharmBuffBodyBlock.SHIELD_THROW_TIME_DELAY)
				{
					this.lastShieldThrownTimer = 0f;
					this.numShieldsThrown++;
					this.GiveShield();
				}
			}
		}

		private void GiveShield()
		{
			List<Hero> list = new List<Hero>();
			foreach (Hero hero in this.world.heroes)
			{
				if (hero.IsAlive())
				{
					list.Add(hero);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			Hero hero2 = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
			hero2.GainShield((double)this.shieldAmount, float.MaxValue);
			BuffData buffData = new BuffData();
			buffData.dur = 3f;
			buffData.id = 272;
			buffData.visuals |= 256;
			hero2.AddBuff(buffData, 0, false);
		}

		public override void OnPostDamage(Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (!(damager is Enemy))
			{
				return;
			}
			this.AddProgress(this.pic);
		}

		public int numShields;

		public float shieldAmount;

		private int numShieldsThrown;

		private float lastShieldThrownTimer;

		public static float SHIELD_THROW_TIME_DELAY = 0.25f;
	}
}
