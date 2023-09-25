using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class CharmBuffBouncingBolt : CharmBuff
	{
		protected override bool TryActivating()
		{
			bool flag = this.CastLightning(null, out this.lastHitUnit);
			if (flag)
			{
				this.numBounced = 0;
				this.lastBounceTimer = 0f;
			}
			return flag;
		}

		protected override void OnUpdate(float dt)
		{
			if (this.state == EnchantmentBuffState.ACTIVE && this.numBounced < this.totalNumBounce)
			{
				this.lastBounceTimer += dt;
				if (this.lastBounceTimer >= CharmBuffBouncingBolt.BOUNCE_TIME_DELAY)
				{
					this.lastBounceTimer = 0f;
					this.numBounced++;
					UnitHealthy start = this.lastHitUnit;
					if (!this.CastLightning(start, out this.lastHitUnit))
					{
						this.numBounced = this.totalNumBounce;
					}
				}
			}
		}

		public override void OnWeaponUsed(Hero hero)
		{
			this.AddProgress(this.pic);
		}

		private bool CastLightning(UnitHealthy start, out UnitHealthy unitHit)
		{
			unitHit = null;
			List<UnitHealthy> list = new List<UnitHealthy>();
			foreach (Enemy enemy in this.world.activeChallenge.enemies)
			{
				if (enemy.IsAlive() && enemy != start)
				{
					list.Add(enemy);
				}
			}
			if (list.Count <= 0)
			{
				return false;
			}
			unitHit = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
			double amount = this.world.GetHeroTeamDps() * this.teamDamageToDeal;
			Damage damage = new Damage(amount, false, false, false, false);
			this.world.DamageMain(null, unitHit, damage);
			Vector3 posStart = default(Vector3);
			if (start == null)
			{
				posStart = new Vector3(GameMath.GetRandomFloat(-1.5f, -2f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(0.6f, 1f, GameMath.RandType.NoSeed), 0f);
			}
			else
			{
				posStart = start.pos + new Vector3(0f, start.GetHeight() * 0.5f, 0f);
			}
			VisualLinedEffect item = new VisualLinedEffect(VisualLinedEffect.Type.TOTEM_LIGHTNING, posStart, unitHit.pos + new Vector3(0f, unitHit.GetHeight() * 0.5f, 0f), 0.33f);
			this.world.visualLinedEffects.Add(item);
			SoundVariedSimple sound = new SoundVariedSimple(SoundArchieve.inst.lightnings, GameMath.GetRandomFloat(0.5f, 0.85f, GameMath.RandType.NoSeed));
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, "totemLightning", false, 0f, sound);
			this.world.AddSoundEvent(e);
			return true;
		}

		public double teamDamageToDeal;

		public int totalNumBounce;

		private int numBounced;

		private float lastBounceTimer;

		private UnitHealthy lastHitUnit;

		public static float BOUNCE_TIME_DELAY = 0.11f;
	}
}
