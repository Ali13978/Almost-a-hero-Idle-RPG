using System;
using System.Collections.Generic;

namespace Simulation
{
	public class EnemyDataBase : UnitHealthyDataBase
	{
		public EnemyDataBase()
		{
			this.critChance = 0f;
			this.critFactor = 1.0;
			this.damageTakenFactor = 1.0;
			this.durLoot = 0.25f;
			this.scale = 1f;
			this.deathEffectScale = 1f;
			this.spawnProb = 1f;
			this.numDropMin = 2;
			this.numDropMax = 4;
		}

		public int GetCorruptednessSkinIndex()
		{
			string text = this.name;
			switch (text)
			{
			case "BANDIT":
			case "WOLF":
			case "SPIDER":
			case "BAT":
			case "CHEST":
			case "BOSS":
			case "BOSS WISE SNAKE":
			case "BOSS SNOWMAN":
				return 0;
			case "SEMI CORRUPTED ELF":
			case "SEMI CORRUPTED DWARF":
			case "SEMI CORRUPTED HUMAN":
				return 1;
			case "MANGOLIES":
			case "CORRUPTED ELF":
			case "CORRUPTED DWARF":
			case "CORRUPTED HUMAN":
			case "BOSS ELF":
			case "BOSS DWARF":
			case "BOSS HUMAN":
			case "BOSS MANGOLIES":
				return 2;
			case "SNAKE":
				return 3;
			}
			throw new NotImplementedException();
		}

		public EnemyDataBase.Type type;

		public string name;

		public float durAttackActive;

		public float durAttackWait;

		public float timeDamage;

		public float durSpawnTranslate;

		public float durSpawnDrop;

		public float durCorpse;

		public float durLeave;

		public float durSpawnDropTranslate;

		public float deathEffectTime;

		public float deathEffectScale;

		public double goldToDrop;

		public int numDropMin;

		public int numDropMax;

		public float durLoot;

		public float spawnWeight;

		public float spawnProb;

		public bool stunHereosWhenSpawn;

		public float stunDurationInSeconds;

		public float blindMissChance;

		public bool isInmuneWithMinions;

		public float spawnMinionsIfAloneAfterSeconds;

		public EnemyDataBase spawnedMinion;

		public int spawnedMinionsCount;

		public float spawnMinionsAnimDuration;

		public float spawnMinionsTime;

		public float secondsBetweenEachMinionSpawn;

		public Projectile projectile;

		public bool[] useProjectileOnNumHit;

		public Sound soundSpawn;

		public List<TimedSound> soundsAttack;

		public Sound soundDeath;

		public Sound soundHurt;

		public Sound soundImpact;

		public Sound soundSummonMinions;

		public float scale;

		public EnemyVoices voices;

		public enum Type
		{
			REGULAR,
			CHEST,
			BOSS,
			EPIC
		}
	}
}
