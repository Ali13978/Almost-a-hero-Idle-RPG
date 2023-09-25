using System;
using Simulation;

namespace Ui
{
	public class UiCommandChangeHeroSkin : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			SkinData skinData = sim.GetBoughtSkins().Find((SkinData s) => s.id == this.skinId);
			if (skinData != null)
			{
				HeroDataBase heroDataBase = sim.GetAllHeroes().Find((HeroDataBase he) => he.id == this.heroId);
				heroDataBase.equippedSkin = skinData;
				heroDataBase.randomSkinsEnabled = this.random;
				return;
			}
			throw new EntryPointNotFoundException(string.Format("skin with id: {0} for hero: {1} not found", this.skinId, this.heroId));
		}

		public int skinId;

		public string heroId;

		public bool random;
	}
}
