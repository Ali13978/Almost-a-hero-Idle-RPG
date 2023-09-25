using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class EnemyData : UnitHealthyData
	{
		public EnemyData(EnemyDataBase dataBase, double power, bool isRiftEnemy)
		{
			this.dataBase = dataBase;
			if (isRiftEnemy)
			{
				this.SetPowerForRift(power);
			}
			else
			{
				this.SetPower(power);
			}
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

		public void SetPower(double power)
		{
			power *= 0.5;
			this.healthMax = this.dataBase.healthMax * UnitMath.GetEnemyHealthForPower(power);
			this.damage = this.dataBase.damage * UnitMath.GetDamageForPower(power);
			this.goldToDrop = this.dataBase.goldToDrop * UnitMath.GetGoldToDropForPower(power);
			this.healthRegen = this.dataBase.healthRegen;
			this.damageTakenFactor = this.dataBase.damageTakenFactor;
		}

		public void SetPowerForRift(double power)
		{
			power *= 0.5;
			this.healthMax = this.dataBase.healthMax * UnitMath.GetHealthForPowerGog(power);
			this.damage = this.dataBase.damage * UnitMath.GetDamageForPowerGog(power);
			this.goldToDrop = this.dataBase.goldToDrop * UnitMath.GetGoldToDropForPowerGog(power);
			this.healthRegen = this.dataBase.healthRegen;
			this.damageTakenFactor = this.dataBase.damageTakenFactor;
		}

		public float durAttackActive
		{
			get
			{
				return this.dataBase.durAttackActive;
			}
		}

		public float durAttackWait
		{
			get
			{
				return this.dataBase.durAttackWait;
			}
		}

		public float timeDamage
		{
			get
			{
				return this.dataBase.timeDamage;
			}
		}

		public float durSpawnTranslate
		{
			get
			{
				return this.dataBase.durSpawnTranslate;
			}
		}

		public float durSpawnDrop
		{
			get
			{
				return this.dataBase.durSpawnDrop;
			}
		}

		public float durCorpse
		{
			get
			{
				return this.dataBase.durCorpse;
			}
		}

		public float deathEffectTime
		{
			get
			{
				return this.dataBase.deathEffectTime;
			}
		}

		public float deathEffectScale
		{
			get
			{
				return this.dataBase.deathEffectScale;
			}
		}

		public float durLeave
		{
			get
			{
				return this.dataBase.durLeave;
			}
		}

		public float durSpawnDropTranslate
		{
			get
			{
				return this.dataBase.durSpawnDropTranslate;
			}
		}

		public Sound soundSpawn
		{
			get
			{
				return this.dataBase.soundSpawn;
			}
		}

		public List<TimedSound> soundsAttack
		{
			get
			{
				return this.dataBase.soundsAttack;
			}
		}

		public Sound soundDeath
		{
			get
			{
				return this.dataBase.soundDeath;
			}
		}

		public Sound soundHurt
		{
			get
			{
				return this.dataBase.soundHurt;
			}
		}

		public EnemyVoices Voices
		{
			get
			{
				return this.dataBase.voices;
			}
		}

		public float GetMissChance()
		{
			return this.dataBase.missChance;
		}

		public EnemyDataBase dataBase;

		public double goldToDrop;
	}
}
