using System;
using System.Collections.Generic;

namespace Simulation
{
	public class RiftEffectRandomHeroes : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 2f;
			}
		}

		public override void OnRiftSetup(World world, ChallengeRift riftChallenge, TotemDataBase totem, List<Rune> wornRunes, HeroDataBase[] heroesData, List<Gear> gears)
		{
			world.ClearHeroes();
			List<HeroDataBase> availableHeroes = world.currentSim.GetAvailableHeroes();
			Utility.SuffleList<HeroDataBase>(availableHeroes);
			if (availableHeroes.Count >= this.heroCount)
			{
				for (int i = 0; i < this.heroCount; i++)
				{
					HeroDataBase newBoughtHero = availableHeroes[i];
					world.LoadNewHero(newBoughtHero, gears, false);
				}
				return;
			}
			throw new Exception("Not enough available heroes");
		}

		public override string GetDesc()
		{
			return LM.Get("RIFT_EFFECT_RANDOM_HEROES");
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectRandomHeroes
			{
				heroCount = this.heroCount
			};
		}

		public int heroCount = 2;
	}
}
