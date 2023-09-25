using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class CharmBuffQuackatoa : CharmBuff
	{
		protected override bool TryActivating()
		{
			bool flag = false;
			foreach (Enemy enemy in this.world.activeChallenge.enemies)
			{
				if (enemy.IsAlive())
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return false;
			}
			this.numDucksThrown = 0;
			this.lastDuckTimer = 0f;
			return true;
		}

		protected override void OnUpdate(float dt)
		{
			if (this.state == EnchantmentBuffState.ACTIVE && this.numDucksThrown < this.totalNumDucks)
			{
				this.lastDuckTimer += dt;
				if (this.lastDuckTimer >= CharmBuffQuackatoa.DUCK_TIME_DELAY)
				{
					this.lastDuckTimer = 0f;
					this.numDucksThrown++;
					this.ThrowDuck();
				}
			}
		}

		private void ThrowDuck()
		{
			List<UnitHealthy> list = new List<UnitHealthy>();
			foreach (Enemy enemy in this.world.activeChallenge.enemies)
			{
				if (enemy.IsAlive())
				{
					list.Add(enemy);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			UnitHealthy unitHealthy = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
			VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.DUCK_CHARM, 1f);
			visualEffect.scale = 0.8f;
			visualEffect.dirX = ((!GameMath.GetProbabilityOutcome(0.5f, GameMath.RandType.NoSeed)) ? 1f : -1f);
			visualEffect.pos = unitHealthy.pos;
			visualEffect.pos += new Vector3(0f, unitHealthy.GetHeight() * 0.9f, 0f);
			this.world.visualEffects.Add(visualEffect);
			BuffDataStun buffDataStun = new BuffDataStun();
			buffDataStun.id = 308;
			buffDataStun.isPermenant = false;
			buffDataStun.isStackable = false;
			buffDataStun.visuals |= 512;
			buffDataStun.dur = this.stunDuration;
			unitHealthy.AddBuff(buffDataStun, 0, false);
			this.world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, "charm", false, 0f, new SoundDelayed(0.2f, SoundArchieve.inst.uiDuck, 0.1f)));
		}

		public override void OnAnyCharmTriggered(World world, CharmBuff charmBuff)
		{
			if (charmBuff is CharmBuffQuackatoa)
			{
				return;
			}
			this.AddProgress(this.pic);
		}

		public float stunDuration;

		public int totalNumDucks;

		private int numDucksThrown;

		private float lastDuckTimer;

		public static float DUCK_TIME_DELAY = 0.25f;

		private const float DUCK_DUR_TIME = 1f;
	}
}
