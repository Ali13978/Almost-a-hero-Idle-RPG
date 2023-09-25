using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class Projectile
	{
		public Projectile()
		{
		}

		public Projectile(Unit by, Projectile.Type type, Projectile.TargetType targetType, UnitHealthy target, float durFly, ProjectilePath pathToCopy, Vector3 targetPos)
		{
			this.targetPosition = targetPos;
			this.by = by;
			this.type = type;
			this.targetType = targetType;
			this.target = target;
			this.durFly = durFly;
			this.damageMomentTimeRatio = 1f;
			this.hasDamaged = false;
			this.path = pathToCopy.GetCopy();
			this.timeOnAir = 0f;
			this.InitPath();
		}

		public Projectile(Unit by, Projectile.Type type, Projectile.TargetType targetType, UnitHealthy target, float durFly, ProjectilePath pathToCopy)
		{
			this.by = by;
			this.type = type;
			this.targetType = targetType;
			this.target = target;
			this.durFly = durFly;
			this.damageMomentTimeRatio = 1f;
			this.hasDamaged = false;
			this.path = pathToCopy.GetCopy();
			this.timeOnAir = 0f;
			this.InitPath();
		}

		public void InitPath()
		{
			Vector3 projectileStart = AttachmentOffsets.GetProjectileStart(this.type);
			Vector3 vector = projectileStart + this.startPointOffset;
			if (this.by != null)
			{
				vector += this.by.pos;
			}
			Vector3 vector2;
			if (this.TargetsSingleUnit())
			{
				if (this.target == null)
				{
					vector2 = World.ENEMY_CENTER;
				}
				else
				{
					Vector3 a;
					if (this.target is Enemy)
					{
						Enemy enemy = (Enemy)this.target;
						a = enemy.GetPosAfterTime(this.durFly);
					}
					else
					{
						a = this.target.pos;
					}
					Vector3 projectileTargetOffset = this.target.GetProjectileTargetOffset();
					float projectileTargetRandomness = this.target.GetProjectileTargetRandomness();
					Vector2 vector3 = GameMath.GetRandomPointInUnitCircle() * projectileTargetRandomness;
					projectileTargetOffset.x += vector3.x;
					projectileTargetOffset.y += vector3.y;
					vector2 = a + projectileTargetOffset;
				}
			}
			else if (this.TargetNone())
			{
				vector2 = this.targetPosition;
			}
			else
			{
				if (!this.TargetsAllEnemies())
				{
					throw new NotImplementedException();
				}
				vector2 = World.ENEMY_CENTER;
			}
			vector2 += this.endPointOffset;
			this.InitPath(vector, vector2);
		}

		public void InitPath(Vector3 posStart, Vector3 posEnd)
		{
			this.path.Init(posStart, posEnd);
		}

		public Projectile GetCopy()
		{
			Projectile projectile = new Projectile();
			projectile.by = this.by;
			projectile.type = this.type;
			projectile.targetType = this.targetType;
			projectile.target = this.target;
			projectile.durFly = this.durFly;
			projectile.timeOnAir = this.timeOnAir;
			projectile.path = this.path.GetCopy();
			projectile.damageMomentTimeRatio = this.damageMomentTimeRatio;
			if (this.damage != null)
			{
				projectile.damage = this.damage.GetCopy();
			}
			if (this.damageArea != null)
			{
				projectile.damageArea = this.damageArea.GetCopy();
			}
			projectile.buffs = this.buffs;
			projectile.damageAreaR = this.damageAreaR;
			projectile.targetLocked = this.targetLocked;
			projectile.targetPosition = this.targetPosition;
			projectile.visualVariation = this.visualVariation;
			return projectile;
		}

		public bool TargetsSingleEnemy()
		{
			return this.targetType == Projectile.TargetType.SINGLE_ENEMY;
		}

		public bool TargetsAllEnemies()
		{
			return this.targetType == Projectile.TargetType.ALL_ENEMIES;
		}

		public bool TargetsSingleAllyRandom()
		{
			return this.targetType == Projectile.TargetType.SINGLE_ALLY_ANY;
		}

		public bool TargetsSingleAllySelfRandom()
		{
			return this.targetType == Projectile.TargetType.SINGLE_ALLY_ANY_SELF;
		}

		public bool TargetsSingleAllyWithMinHealth()
		{
			return this.targetType == Projectile.TargetType.SINGLE_ALLY_MIN_HEALTH;
		}

		public bool TargetsSingleAlly()
		{
			return this.TargetsSingleAllySelfRandom() || this.TargetsSingleAllyRandom() || this.TargetsSingleAllyWithMinHealth();
		}

		public bool TargetsSingleUnit()
		{
			return this.TargetsSingleEnemy() || this.TargetsSingleAlly();
		}

		public bool TargetNone()
		{
			return this.targetType == Projectile.TargetType.NONE;
		}

		public void Update(float dt)
		{
			this.timeOnAir += dt;
		}

		public bool HasReached()
		{
			return this.timeOnAir >= this.durFly;
		}

		public float GetFlyRatio()
		{
			return this.timeOnAir / this.durFly;
		}

		public float GetEstimatedTime()
		{
			return this.durFly - this.timeOnAir;
		}

		public Vector3 GetPos()
		{
			return this.path.GetPos(this.GetFlyRatio());
		}

		public Vector3 GetDir()
		{
			return this.path.GetDir(this.GetFlyRatio());
		}

		public Vector3 GetPosEnd()
		{
			return this.path.posEnd;
		}

		public void ReTarget(UnitHealthy tar)
		{
			Vector3 pos = this.GetPos();
			this.target = tar;
			this.durFly -= this.timeOnAir;
			if (this.durFly < 0.2f)
			{
				this.durFly = 0.2f;
			}
			this.timeOnAir = 0f;
			this.path = new ProjectilePathBomb
			{
				heightAddMax = 0.1f
			};
			Vector3 posEnd;
			if (this.TargetsSingleUnit())
			{
				if (this.target == null)
				{
					posEnd = World.ENEMY_CENTER;
				}
				else
				{
					Vector3 a;
					if (this.target is Enemy)
					{
						Enemy enemy = (Enemy)this.target;
						a = enemy.GetPosAfterTime(this.durFly);
					}
					else
					{
						a = this.target.pos;
					}
					Vector3 projectileTargetOffset = this.target.GetProjectileTargetOffset();
					float projectileTargetRandomness = this.target.GetProjectileTargetRandomness();
					Vector2 vector = GameMath.GetRandomPointInUnitCircle() * projectileTargetRandomness;
					projectileTargetOffset.x += vector.x;
					projectileTargetOffset.y += vector.y;
					posEnd = a + projectileTargetOffset;
				}
			}
			else if (this.TargetNone())
			{
				posEnd = this.targetPosition;
			}
			else
			{
				if (!this.TargetsAllEnemies())
				{
					throw new NotImplementedException();
				}
				posEnd = World.ENEMY_CENTER;
			}
			this.InitPath(pos, posEnd);
		}

		public Unit by;

		public Projectile.Type type;

		public Projectile.TargetType targetType;

		public UnitHealthy target;

		public Vector3 targetPosition;

		public Vector3 startPointOffset;

		public Vector3 endPointOffset;

		public float durFly;

		public float damageMomentTimeRatio;

		public bool hasDamaged;

		public ProjectilePath path;

		public float timeOnAir;

		public Damage damage;

		public List<BuffData> buffs;

		public Action<UnitHealthy> onHit;

		public Damage damageArea;

		public float damageAreaR;

		public VisualEffect visualEffect;

		public int visualVariation;

		public float scale = 1f;

		public bool targetLocked;

		public int projectileAlternativeIndex = -1;

		public bool discarded;

		public SoundEventSound soundImpact;

		public float rotateSpeed;

		public enum Type
		{
			REVERSED_EXCALIBUR_MUD,
			BOSS,
			APPLE,
			APPLE_BOMBARD,
			APPLE_AID,
			DEREK_MAGIC_BALL,
			DEREK_BOOK,
			BOMBERMAN_DINAMIT,
			BOMBERMAN_FIREWORK,
			BOMBERMAN_FIRE_CRACKER,
			SHEELA,
			DWARF_CORRUPTED,
			HUMAN_CORRUPTED,
			SAM_AXE,
			SAM_BOTTLE,
			BLIND_ARCHER_ATTACK,
			BLIND_ARCHER_AUTO,
			BLIND_ARCHER_ULTI,
			ELF_CORRUPTED,
			TOTEM_ICE_SHARD,
			MANGOLIES,
			TOTEM_EARTH,
			TOTEM_EARTH_SMALL,
			TOTEM_EARTH_STAR,
			GOBLIN_SACK,
			GOBLIN_SMOKE_BOMB,
			WARLOCK_ATTACK,
			WARLOCK_SWARM,
			WARLOCK_REGRET,
			CHARM_DAGGER,
			METEOR,
			FLUTE,
			QUICK_STUDY,
			SNAKE,
			WISE_SNAKE,
			BABU_SOUP,
			BABU_TEA_CUP,
			CHRISTMAS_ORNAMENT
		}

		public enum TargetType
		{
			SINGLE_ENEMY,
			ALL_ENEMIES,
			SINGLE_ALLY_ANY,
			SINGLE_ALLY_MIN_HEALTH,
			SINGLE_ALLY_ANY_SELF,
			NONE
		}
	}
}
