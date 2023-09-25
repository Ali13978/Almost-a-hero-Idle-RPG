using System;

namespace Simulation
{
	public class RiftEffectUpgradeCostReduction : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 3f;
			}
		}

		public override void Apply(World world, float dt)
		{
			this.timer += dt;
			while (this.timer >= 1f)
			{
				this.timer -= 1f;
				foreach (Hero hero in world.heroes)
				{
					hero.costMultiplier *= 1.0 - this.upgradeCostMultiplier;
				}
			}
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_UPGRADE_COST_REDUCTION"), GameMath.GetPercentString(this.upgradeCostMultiplier, false));
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectUpgradeCostReduction
			{
				heroCount = this.heroCount,
				upgradeCostMultiplier = this.upgradeCostMultiplier
			};
		}

		public int heroCount = 2;

		public double upgradeCostMultiplier = 0.05;

		private float timer;
	}
}
