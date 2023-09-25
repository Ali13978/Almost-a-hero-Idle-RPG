using System;

namespace Simulation
{
	public class CharmBuffSpellShield : CharmBuff
	{
		protected override bool TryActivating()
		{
			foreach (Hero hero in this.world.heroes)
			{
				if (hero.IsAlive())
				{
					hero.GainShield((double)this.healthAdd, float.MaxValue);
					BuffData buffData = new BuffData();
					buffData.dur = 3f;
					buffData.id = 272;
					buffData.visuals |= 256;
					hero.AddBuff(buffData, 0, false);
				}
			}
			return true;
		}

		public override void OnAbilityCast(Skill skill)
		{
			this.AddProgress(this.pic);
		}

		public float healthAdd;
	}
}
