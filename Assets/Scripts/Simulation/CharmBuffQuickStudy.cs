using System;

namespace Simulation
{
	public class CharmBuffQuickStudy : CharmBuffPermanent
	{
		protected override bool TryActivating()
		{
			this.secondsTillNextBook = this.bookEverySeconds;
			return true;
		}

		protected override void OnUpdate(float dt)
		{
			this.stateTime += dt;
			this.secondsTillNextBook -= dt;
			if (this.IsComplete() && !this.throwSecondPhase)
			{
				return;
			}
			if (this.secondsTillNextBook <= 0f)
			{
				this.secondsTillNextBook += this.bookEverySeconds;
				this.progress += this.progressPerBook;
				this.droppedBooks++;
				Hero randomHero = this.world.GetRandomHero();
				Projectile projectile = new Projectile(null, Projectile.Type.QUICK_STUDY, Projectile.TargetType.SINGLE_ALLY_ANY, randomHero, 0.4f, new ProjectilePathFromAbove
				{
					speedVertical = 2f
				});
				projectile.onHit = new Action<UnitHealthy>(this.OnProjectileHitHero);
				projectile.InitPath();
				this.world.AddProjectile(projectile);
				if (this.droppedBooks >= this.bookCountToDrop && !this.throwSecondPhase)
				{
					this.throwSecondPhase = GameMath.GetProbabilityOutcome(this.secondPhaseCance, GameMath.RandType.NoSeed);
					if (this.throwSecondPhase)
					{
						this.secondsTillNextBook += this.bookEverySeconds;
						this.droppedBooks = 0;
					}
				}
				else if (this.droppedBooks >= this.bookCountToDrop)
				{
					this.throwSecondPhase = false;
				}
			}
		}

		private bool IsComplete()
		{
			return (float)this.bookCountToDrop * this.progressPerBook <= this.progress + 0.01f;
		}

		private void OnProjectileHitHero(UnitHealthy hero)
		{
			(hero as Hero).IncrementNumUnspentSkillPoints(1);
		}

		public int bookCountToDrop;

		public float secondPhaseCance;

		public float progressPerBook = 0.1f;

		private float bookEverySeconds = 0.2f;

		private float secondsTillNextBook;

		private int droppedBooks;

		private bool throwSecondPhase;
	}
}
