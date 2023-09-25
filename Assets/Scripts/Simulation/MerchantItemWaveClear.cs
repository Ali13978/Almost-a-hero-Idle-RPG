using System;
using System.Collections.Generic;

namespace Simulation
{
	public class MerchantItemWaveClear : MerchantItem
	{
		public override string GetId()
		{
			return "WAVE_CLEAR";
		}

		public override string GetTitleString()
		{
			return LM.Get("MERCHANT_ITEM_WAVE_CLEAR");
		}

		public override string GetDescriptionString(Simulator sim, int count)
		{
			if (count > 1)
			{
				return string.Format(LM.Get("MERCHANT_ITEM_DESC_WAVE_CLEAR_NEXT"), count);
			}
			return LM.Get("MERCHANT_ITEM_DESC_WAVE_CLEAR");
		}

		public override bool CanEvaulate(World world)
		{
			List<Enemy> enemies = world.activeChallenge.enemies;
			if (enemies.Count > 0 && world.lastUsedMerchantItemDuration > 1f)
			{
				foreach (Enemy enemy in enemies)
				{
					if (!enemy.IsOutOfDeathlessZone() && !enemy.IsAlive())
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		public override void Apply(Simulator sim)
		{
			base.Apply(sim);
			World activeWorld = sim.GetActiveWorld();
			foreach (Enemy enemy in activeWorld.activeChallenge.enemies)
			{
				activeWorld.DamageFuture(activeWorld.totem, enemy, new Damage(enemy.GetHealthMax() * 2.0, false, false, false, false), 0.5f);
				VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_THUNDERBOLT_PURPLE, 1f);
				visualEffect.pos = enemy.pos;
				activeWorld.visualEffects.Add(visualEffect);
			}
			SoundVariedSimple sound = new SoundVariedSimple(SoundArchieve.inst.thunderbolts, GameMath.GetRandomFloat(0.6f, 0.85f, GameMath.RandType.NoSeed));
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, "world", false, 0f, sound);
			activeWorld.AddSoundEvent(e);
		}

		public static int BASE_COUNT = 2;

		public static int BASE_COST = 30;
	}
}
