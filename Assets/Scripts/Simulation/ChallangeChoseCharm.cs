using System;

namespace Simulation
{
	public class ChallangeChoseCharm : ChallengeUpgrade
	{
		public ChallangeChoseCharm(int stage)
		{
			this.waveReq = stage;
		}

		public override void Apply(World world, ChallengeUpgradesTotal worldUpgradesTotal)
		{
		}

		public override string GetDescription(World world)
		{
			return "I am charming (H.J)";
		}
	}
}
