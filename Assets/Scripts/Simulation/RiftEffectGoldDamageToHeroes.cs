using System;
using UnityEngine;

namespace Simulation
{
	public class RiftEffectGoldDamageToHeroes : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return -0.5f;
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectGoldDamageToHeroes();
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_GOLD_DAMAGE_TO_HEROES"), GameMath.GetPercentString(0.0099999997764825821, false));
		}

		public override void OnCollectDrop(Drop drop, World world)
		{
			DropCurrency dropCurrency = drop as DropCurrency;
			if (world.activeChallenge.state == Challenge.State.ACTION && dropCurrency != null && dropCurrency.currencyType == CurrencyType.GOLD)
			{
				Hero randomAliveHero = world.GetRandomAliveHero();
				if (randomAliveHero != null)
				{
					world.DamageFuture(null, randomAliveHero, new Damage(0.0099999997764825821 * randomAliveHero.GetHealthMax(), false, false, false, false)
					{
						canNotBeDodged = true
					}, 0.1f);
					VisualLinedEffect item = new VisualLinedEffect(VisualLinedEffect.Type.TOTEM_LIGHTNING, drop.pos, randomAliveHero.pos + new Vector3(0f, randomAliveHero.GetHeight() * 0.5f, 0f), 0.25f);
					world.visualLinedEffects.Add(item);
					SoundVariedSimple sound = new SoundVariedSimple(SoundArchieve.inst.lightnings, GameMath.GetRandomFloat(0.5f, 0.85f, GameMath.RandType.NoSeed));
					SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, "totemLightning", false, 0f, sound);
					world.AddSoundEvent(e);
				}
			}
		}

		private const double percentage = 0.0099999997764825821;
	}
}
