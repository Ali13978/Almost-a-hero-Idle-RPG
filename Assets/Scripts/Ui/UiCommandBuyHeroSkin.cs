using System;
using Simulation;

namespace Ui
{
	public class UiCommandBuyHeroSkin : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			SkinData skinData = sim.GetAllSkins().Find((SkinData s) => s.id == this.skinId);
			if (skinData != null)
			{
				sim.TryBuySkin(this.skinId);
				return;
			}
			throw new EntryPointNotFoundException(string.Format("skin with id: {0} not found", this.skinId));
		}

		public int skinId;
	}
}
