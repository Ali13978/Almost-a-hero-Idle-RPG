using System;
using Simulation;

namespace Ui
{
	public class UiCommandSkinSelected : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			SkinData skinData = sim.GetAllSkins().Find((SkinData s) => s.id == this.skinId);
			if (skinData != null && skinData.IsUnlockRequirementSatisfied(sim))
			{
				skinData.isNew = false;
			}
		}

		public int skinId;
	}
}
