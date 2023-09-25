using System;
using Simulation;

namespace Ui
{
	public class UiCommandChallengeWinLose : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (this.isWin)
			{
				sim.GetActiveWorld().OnChallengeUiConfirmWin();
			}
			else
			{
				sim.GetActiveWorld().OnChallengeUiConfirmLose();
			}
		}

		public bool isWin;
	}
}
