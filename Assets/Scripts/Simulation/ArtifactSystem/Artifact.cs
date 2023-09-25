using System;
using System.Collections.Generic;

namespace Simulation.ArtifactSystem
{
	[Serializable]
	public class Artifact
	{
		public int Rarity
		{
			get
			{
				return this.UniqueEffectsIds.Count;
			}
		}

		public int Level;

		public int CommonEffectId;

		public List<int> UniqueEffectsIds;

		[NonSerialized]
		public int CraftIndex;
	}
}
