using System;
using Simulation;

namespace Ui
{
	public class UiCommandUpgradeSkill : UiCommand
	{
		private bool IsForUlti()
		{
			return this.branchIndex < 0;
		}

		public override void Apply(Simulator sim)
		{
			sim.TryUpgradeSkill(this.hero, this.branchIndex, this.skillIndex);
		}

		public Hero hero;

		public int branchIndex;

		public int skillIndex;
	}
}
