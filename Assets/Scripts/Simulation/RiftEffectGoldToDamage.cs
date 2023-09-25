using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class RiftEffectGoldToDamage : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 2f;
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectGoldToDamage
			{
				damagePerGold = this.damagePerGold
			};
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_GOLD_TO_DAMAGE"), GameMath.GetPercentString(this.damagePerGold, false));
		}

		public override void OnCollectDrop(Drop drop, World world)
		{
			List<Enemy> list = new List<Enemy>();
			foreach (Enemy enemy in world.activeChallenge.enemies)
			{
				if (!enemy.IsDead())
				{
					list.Add(enemy);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			Damage damage = new Damage(world.GetHeroTeamDps() * this.damagePerGold, false, false, false, false);
			Enemy enemy2 = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
			world.DamageFuture(null, enemy2, damage, 0.1f);
			VisualLinedEffect item = new VisualLinedEffect(VisualLinedEffect.Type.TOTEM_LIGHTNING, drop.pos, enemy2.pos + new Vector3(0f, enemy2.GetHeight() * 0.5f, 0f), 0.25f);
			world.visualLinedEffects.Add(item);
			SoundVariedSimple sound = new SoundVariedSimple(SoundArchieve.inst.lightnings, GameMath.GetRandomFloat(0.5f, 0.85f, GameMath.RandType.NoSeed));
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, "totemLightning", false, 0f, sound);
			world.AddSoundEvent(e);
		}

		public double damagePerGold = 0.25;
	}
}
