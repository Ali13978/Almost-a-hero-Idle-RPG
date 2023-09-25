using System;
using System.Collections.Generic;

namespace Simulation
{
	public class RiftData
	{
		public RiftData Clone()
		{
			RiftData riftData = new RiftData
			{
				id = this.id,
				enemySet = this.enemySet,
				setup = this.setup,
				numHeroes = this.numHeroes,
				hasRing = this.hasRing,
				startLevel = this.startLevel,
				discovery = this.discovery,
				cursesSetup = this.cursesSetup,
				difLevel = this.difLevel,
				rewardFactor = this.rewardFactor
			};
			riftData.effects = new List<RiftEffect>();
			foreach (RiftEffect riftEffect in this.effects)
			{
				riftData.effects.Add(riftEffect.Clone());
			}
			return riftData;
		}

		public int id;

		public RiftEnemySet enemySet;

		public RiftSetup setup;

		public List<RiftEffect> effects;

		public int numHeroes;

		public bool hasRing;

		public int startLevel;

		public int discovery;

		public RiftCursesSetup cursesSetup;

		public int difLevel;

		public double rewardFactor = 1.0;
	}
}
