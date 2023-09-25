using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class CharmBuffFieryFire : CharmBuff
	{
		protected override bool TryActivating()
		{
			this.numFireballsThrown = 0;
			this.lastFireballThrownTimer = 0f;
			return true;
		}

		protected override void OnUpdate(float dt)
		{
			if (this.state == EnchantmentBuffState.ACTIVE && this.numFireballsThrown < this.numFireballs)
			{
				this.lastFireballThrownTimer += dt;
				if (this.lastFireballThrownTimer >= CharmBuffFieryFire.FIREBALL_THROW_TIME_DELAY)
				{
					this.lastFireballThrownTimer = 0f;
					this.numFireballsThrown++;
					this.ThrowFireball();
				}
			}
		}

		private void ThrowFireball()
		{
			List<UnitHealthy> list = new List<UnitHealthy>();
			foreach (Enemy enemy in this.world.activeChallenge.enemies)
			{
				if (enemy.IsAlive())
				{
					list.Add(enemy);
				}
			}
			if (list.Count > 0)
			{
				UnitHealthy unitHealthy = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
				double amount = this.world.GetHeroTeamDps() * this.fireballDamage;
				Damage damage = new Damage(amount, false, false, false, false);
				damage.type = DamageType.SKILL;
				this.world.DamageFuture(null, unitHealthy, damage, 0.18f);
				BuffDataDefense buffDataDefense = new BuffDataDefense();
				buffDataDefense.isStackable = false;
				buffDataDefense.id = 318;
				buffDataDefense.damageTakenFactor = (double)(1f + this.defReduction);
				buffDataDefense.visuals |= 32;
				buffDataDefense.dur = float.MaxValue;
				unitHealthy.AddBuff(buffDataDefense, 0, false);
				Vector3 posStart = new Vector3(GameMath.GetRandomFloat(-1.3f, -1.6f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(0.6f, 1f, GameMath.RandType.NoSeed), 0f);
				VisualLinedEffect item = new VisualLinedEffect(VisualLinedEffect.Type.TOTEM_FIRE, posStart, unitHealthy.pos + new Vector3(0.2f, 0f, 0f), 0.25f);
				this.world.visualLinedEffects.Add(item);
				SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, "totemFire", false, 0f, new SoundTotemThrow(1f));
				this.world.AddSoundEvent(e);
				return;
			}
		}

		public override void OnWavePassed()
		{
			this.AddProgress(this.pic);
		}

		public int numFireballs;

		public double fireballDamage;

		public float defReduction;

		private int numFireballsThrown;

		private float lastFireballThrownTimer;

		public static float FIREBALL_THROW_TIME_DELAY = 0.25f;
	}
}
