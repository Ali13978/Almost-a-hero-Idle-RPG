using System;
using Simulation;

namespace Ui
{
	public class UiCommandEvolveHero : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryEvolveHero(this.heroId);
			HeroDataBase heroDataBase = sim.GetHeroDataBase(this.heroId);
			heroDataBase.equippedSkin = sim.GetHeroEvolveSkin(this.heroId, heroDataBase.evolveLevel);
		}

		public string heroId;
	}
}
