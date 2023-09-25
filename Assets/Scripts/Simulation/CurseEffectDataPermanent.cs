using System;

namespace Simulation
{
	public abstract class CurseEffectDataPermanent : CurseEffectData
	{
		public override string GetConditionDescFormat()
		{
			return null;
		}

		public sealed override void IncrementLevel(ChallengeRift challenge)
		{
		}

		public sealed override void DecrementLevel(ChallengeRift challenge)
		{
		}

		protected sealed override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			return null;
		}
	}
}
