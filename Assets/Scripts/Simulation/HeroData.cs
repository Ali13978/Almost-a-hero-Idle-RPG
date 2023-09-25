using System;
using UnityEngine;

namespace Simulation
{
	public class HeroData : UnitHealthyData
	{
		public HeroData(HeroDataBase dataBase, int progress, int level)
		{
			this.dataBase = dataBase;
			this.SetProgress(progress, level);
		}

		public override float GetHeight()
		{
			return this.dataBase.height;
		}

		public override float GetScaleHealthBar()
		{
			return this.dataBase.scaleHealthBar;
		}

		public override float GetScaleBuffVisual()
		{
			return this.dataBase.scaleBuffVisual;
		}

		public override Vector3 GetProjectileTargetOffset()
		{
			return this.dataBase.projectileTargetOffset;
		}

		public override float GetProjectileTargetRandomness()
		{
			return this.dataBase.projectileTargetRandomness;
		}

		public void SetProgress(int progress, int level)
		{
			this.damageTakenFactor = this.dataBase.damageTakenFactor;
			this.damage = this.GetDamageFromProgress(progress);
			this.healthMax = this.GetHealthMaxFromProgress(progress);
			this.critChance = this.dataBase.critChance;
			this.critFactor = this.dataBase.critFactor;
			this.durRevive = this.dataBase.durRevive + (float)level * 3f;
		}

		public string nameKey
		{
			get
			{
				return this.dataBase.nameKey;
			}
		}

		public SkillTree skillTree
		{
			get
			{
				return this.dataBase.skillTree;
			}
		}

		public Weapon weapon
		{
			get
			{
				return this.dataBase.weapon;
			}
		}

		public double GetDamageFromProgress(int progress)
		{
			return this.dataBase.damage * GameMath.PowInt(1.3, progress) * GameMath.PowInt(1.5, this.dataBase.evolveLevel);
		}

		public double GetHealthMaxFromProgress(int progress)
		{
			return this.dataBase.healthMax * GameMath.PowInt(1.3, progress) * GameMath.PowInt(1.5, this.dataBase.evolveLevel);
		}

		public bool IsSameDataBase(HeroDataBase other)
		{
			return this.dataBase == other;
		}

		public HeroDataBase GetDataBase()
		{
			return this.dataBase;
		}

		public Sound soundDeath
		{
			get
			{
				return this.dataBase.soundDeath;
			}
		}

		public Sound soundRevive
		{
			get
			{
				return this.dataBase.soundRevive;
			}
		}

		public Sound soundVoDeath
		{
			get
			{
				return this.dataBase.soundVoDeath;
			}
		}

		public Sound soundVoRevive
		{
			get
			{
				return this.dataBase.soundVoRevive;
			}
		}

		public Sound soundVoSpawn
		{
			get
			{
				return this.dataBase.soundVoSpawn;
			}
		}

		public Sound soundVoLevelUp
		{
			get
			{
				return this.dataBase.soundVoLevelUp;
			}
		}

		public AudioClipPromise[] soundVoItem
		{
			get
			{
				return this.dataBase.soundVoItem;
			}
		}

		public Sound soundVoWelcome
		{
			get
			{
				return this.dataBase.soundVoWelcome;
			}
		}

		public Sound soundVoEnvChange
		{
			get
			{
				return this.dataBase.soundVoEnvChange;
			}
		}

		public AudioClipPromise[] soundVoCheer
		{
			get
			{
				return this.dataBase.soundVoCheer;
			}
		}

		public string GetId()
		{
			return this.dataBase.GetId();
		}

		public float GetMissChance()
		{
			return this.dataBase.missChance;
		}

		public const double EVOLVE_MULT = 1.5;

		private HeroDataBase dataBase;

		public float durRevive;
	}
}
