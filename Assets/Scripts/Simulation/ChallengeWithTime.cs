using System;
using System.Collections.Generic;

namespace Simulation
{
	public abstract class ChallengeWithTime : Challenge
	{
		public override void Init(World world)
		{
			base.Init(world);
			this.numDuplicatesTotal = 0;
			this.doesUpdateInBg = false;
		}

		public override void Reset()
		{
			base.Reset();
			this.timeCounter = 0f;
		}

		public override void Update(float dt, float unwarpedDt)
		{
			if (this.state == Challenge.State.ACTION)
			{
				base.UpdateEnemies(dt);
				if (this.timerPaused)
				{
					this.timerPaused = false;
				}
				else
				{
					this.timeShield -= dt;
					if (this.timeShield < 0f)
					{
						this.timeCounter -= this.timeShield;
						this.timeShield = 0f;
					}
				}
				if (this.timeCounter >= this.dur)
				{
					this.state = Challenge.State.LOST;
					this.OnChallengeLost();
				}
			}
		}

		protected virtual void OnChallengeLost()
		{
			this.world.OnChallengeLost();
		}

		public override void OnSetupComplete(TotemDataBase totem, List<Rune> wornRunes, HeroDataBase[] heroesData, List<Gear> gears)
		{
			base.OnSetupComplete(totem, wornRunes, heroesData, gears);
			if (this.numDuplicatesTotal >= 0)
			{
				while (this.world.heroes.Count < this.numDuplicatesTotal)
				{
					foreach (HeroDataBase newBoughtHero in heroesData)
					{
						if (this.world.heroes.Count < this.numDuplicatesTotal)
						{
							this.world.LoadNewHero(newBoughtHero, gears, false);
						}
					}
				}
			}
		}

		public override bool CanPrestigeNowExceptRainingGlory()
		{
			return false;
		}

		public override void Prestige()
		{
			base.Prestige();
		}

		public override bool CanGoToBoss()
		{
			return false;
		}

		public override bool CanLeaveBoss()
		{
			return false;
		}

		public virtual void Abandon()
		{
			this.state = Challenge.State.LOST;
			this.enemies.Clear();
		}

		public void AddTimeShield(float timeRatio)
		{
			this.timeShield += timeRatio * this.dur;
		}

		public float dur;

		public Unlock unlock;

		public float timeCounter;

		public float timeShield;

		public int numDuplicatesTotal;

		public bool timerPaused;
	}
}
