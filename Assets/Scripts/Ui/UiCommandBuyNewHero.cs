using System;
using Simulation;

namespace Ui
{
	public class UiCommandBuyNewHero : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (sim.TryBuyNewHero(this.heroId))
			{
				Hero hero = sim.GetActiveWorld().heroes.Find((Hero h) => h.GetId() == this.heroId);
				hero.needsInitialization = true;
				hero.canBeRendered = !this.loadHeroMainAssets;
				Main.IgnorePendingAssetsLoaded = true;
				if (this.loadHeroMainAssets)
				{
					Main.instance.LoadHeroMainAssets(hero.GetId(), delegate
					{
						hero.canBeRendered = true;
						Main.instance.LoadGameAssets();
					});
				}
				else
				{
					Main.instance.LoadGameAssets();
				}
			}
			else if (this.panelNewHero != null)
			{
				this.panelNewHero.inputBlocker.SetActive(false);
			}
		}

		public string heroId;

		public PanelNewHero panelNewHero;

		public bool loadHeroMainAssets;
	}
}
