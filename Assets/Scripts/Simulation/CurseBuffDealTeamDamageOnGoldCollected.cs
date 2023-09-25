using System;
using UnityEngine;

namespace Simulation
{
	public class CurseBuffDealTeamDamageOnGoldCollected : CurseBuff
	{
		public override void OnCollectDrop(Drop drop)
		{
			if (this.state == EnchantmentBuffState.INACTIVE)
			{
				return;
			}
			DropCurrency dropCurrency = drop as DropCurrency;
			if (dropCurrency == null || dropCurrency.currencyType != CurrencyType.GOLD)
			{
				return;
			}
			Damage damage = new Damage(this.world.GetHeroTeamDps() * this.damageFactor, false, false, false, false)
			{
				canNotBeDodged = true
			};
			foreach (Hero hero in this.world.heroes)
			{
				if (hero.IsAlive())
				{
					this.world.DamageFuture(null, hero, damage.GetCopy(), 0.1f);
					VisualLinedEffect item = new VisualLinedEffect(VisualLinedEffect.Type.TOTEM_LIGHTNING, drop.pos, hero.pos + new Vector3(0f, hero.GetHeight() * 0.5f, 0f), 0.25f);
					this.world.visualLinedEffects.Add(item);
				}
			}
			SoundVariedSimple sound = new SoundVariedSimple(SoundArchieve.inst.lightnings, GameMath.GetRandomFloat(0.5f, 0.85f, GameMath.RandType.NoSeed));
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, "totemLightning", false, 0f, sound);
			this.world.AddSoundEvent(e);
		}

		public override void OnAnyCharmTriggered(World world, CharmBuff charmBuff)
		{
			this.AddProgress(this.pic);
		}

		public double damageFactor;
	}
}
