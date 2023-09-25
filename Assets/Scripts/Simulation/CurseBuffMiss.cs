using System;

namespace Simulation
{
	public class CurseBuffMiss : CurseBuff
	{
		protected override void OnUpdate(float dt)
		{
			this.world.buffTotalEffect.heroMissChance += this.missProbability;
		}

		public override void OnAbilityCast(Skill skill)
		{
			this.AddProgress(this.pic);
		}

		public float missProbability;
	}
}
