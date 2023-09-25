using System;
using UnityEngine;

namespace Simulation
{
	public class CharmBuffThirstingFiends : CharmBuff
	{
		protected override bool TryActivating()
		{
			Hero hero = null;
			foreach (Hero hero2 in this.world.heroes)
			{
				if (!hero2.IsDead() && (hero == null || hero2.GetHealthRatio() > hero.GetHealthRatio()))
				{
					hero = hero2;
				}
			}
			if (hero == null)
			{
				return false;
			}
			hero.TakeDamage(new Damage(this.damageToHero * hero.GetHealthMax(), false, false, false, false)
			{
				isPure = true,
				isExact = true
			}, null, 0.01);
			this.selectedHero = hero;
			this.numSummoned = 0;
			this.lastSummonTimer = 0f;
			return true;
		}

		protected override void OnUpdate(float dt)
		{
			if (this.state == EnchantmentBuffState.ACTIVE && this.numSummoned < this.totalDragonsToSummon)
			{
				this.lastSummonTimer += dt;
				if (this.lastSummonTimer >= CharmBuffThirstingFiends.SUMMON_TIME_DELAY)
				{
					this.lastSummonTimer = 0f;
					this.numSummoned++;
					this.SummonDragon();
				}
			}
		}

		protected void SummonDragon()
		{
			SwarmDragon swarmDragon = new SwarmDragon();
			swarmDragon.rotationSpeed = 4f;
			swarmDragon.speed = 1.6f;
			swarmDragon.pos = this.selectedHero.pos + new Vector3(-0.2f, 1.1f) * 0.2f;
			swarmDragon.by = this.selectedHero;
			swarmDragon.damage = new Damage(this.world.GetHeroTeamDps() * this.teamDamageMul, false, false, false, false);
			swarmDragon.damage.type = DamageType.NONE;
			this.world.swarmDragons.Add(swarmDragon);
		}

		public override void OnAnyCharmTriggered(World world, CharmBuff charmBuff)
		{
			if (charmBuff is CharmBuffThirstingFiends)
			{
				return;
			}
			this.AddProgress(this.pic);
		}

		public static float SUMMON_TIME_DELAY = 0.15f;

		public double damageToHero;

		public double teamDamageMul;

		public int totalDragonsToSummon;

		private Hero selectedHero;

		private int numSummoned;

		private float lastSummonTimer;
	}
}
