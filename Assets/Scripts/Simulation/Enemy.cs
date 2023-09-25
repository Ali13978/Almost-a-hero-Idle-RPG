using System;
using System.Collections.Generic;
using Render;
using UnityEngine;

namespace Simulation
{
	public class Enemy : UnitHealthy
	{
		static Enemy()
		{
			Enemy.BUFF_INV_MINIONS.id = 321;
			Enemy.BUFF_INV_MINIONS.visuals |= 128;
			Enemy.BUFF_INV_MINIONS.isStackable = false;
		}

		public Enemy(EnemyDataBase dataBase, double power, World world) : base(world)
		{
			Enemy.totNumEnemiesCounter += 1u;
			this.id = "enemy" + Enemy.totNumEnemiesCounter.ToString("D10");
			this.isRiftEnemy = (world.gameMode == GameMode.RIFT);
			this.data = new EnemyData(dataBase, power, this.isRiftEnemy);
			this.state = Enemy.State.SPAWN_NONEXISTANCE;
			this.attackTarget = null;
			this.timeCounterAttackActive = 0f;
			this.timeCounterAttackWait = 0f;
			this.hasDamagedThisCycle = false;
			this.numHits = 0;
			this.atAttackSound = 0;
			this.extraSpawnSpeed = 0f;
			this.hasReleasedDeathEffect = (dataBase.name == "BOSS WISE SNAKE");
			this.isChestRaid = false;
			this.genericRandom = GameMath.GetRandomInt(0, 1073741823, GameMath.RandType.NoSeed);
			if (dataBase.spawnedMinionsCount > 0 && dataBase.isInmuneWithMinions)
			{
				this.invulnerabilityVisualEffect = new WiseSnakeInvulnerabilityVisualEffect(dataBase.spawnedMinionsCount, world, dataBase.spawnedMinion.durCorpse, this.id);
				this.AddBuff(Enemy.BUFF_INV_MINIONS, 0, false);
			}
			this.UpdateStats(0f);
			base.SetHealthFull();
		}

		public override string GetId()
		{
			return this.id;
		}

		public override float GetHeight()
		{
			return this.data.GetHeight();
		}

		public override float GetScaleHealthBar()
		{
			return this.data.GetScaleHealthBar();
		}

		public override float GetScaleBuffVisual()
		{
			return this.data.GetScaleBuffVisual();
		}

		public override Vector3 GetProjectileTargetOffset()
		{
			return this.data.GetProjectileTargetOffset();
		}

		public override float GetProjectileTargetRandomness()
		{
			return this.data.GetProjectileTargetRandomness();
		}

		public override double GetDps()
		{
			return base.GetDamage() / (double)this.data.durAttackWait;
		}

		public override double GetDpsTeam()
		{
			return this.world.GetEnemyTeamDps();
		}

		public float GetDurSpawnTranslate()
		{
			return this.data.durSpawnTranslate;
		}

		public float GetDurSpawnDrop()
		{
			return this.data.durSpawnDrop;
		}

		public float GetAnimTimeRatioSpawnTranslate()
		{
			return this.inStateTimeCounter / this.GetDurSpawnTranslate();
		}

		public float GetAnimTimeRatioSpawnDrop()
		{
			return this.inStateTimeCounter / this.GetDurSpawnDrop();
		}

		public float GetDurCorpse()
		{
			return this.data.durCorpse;
		}

		public bool IsToBeRemoved()
		{
			return this.HasLeft() || (this.IsDead() && this.inStateTimeCounter > this.GetDurCorpse());
		}

		public override void UpdateStats(float dt)
		{
			base.UpdateStats(dt);
			if (this.IsRegular())
			{
				this.statCache.UpdateEnemyRegular(this.data, this.buffTotalEffect, this.world.buffTotalEffect, this.world.universalBonus, this.world.GetNumHeroesOfClassInTeam(HeroClass.SUPPORTER));
			}
			else
			{
				if (!this.IsBoss())
				{
					throw new NotImplementedException();
				}
				this.statCache.UpdateEnemyBoss(this.data, this.buffTotalEffect, this.world.buffTotalEffect, this.world.universalBonus, this.world.GetNumHeroesOfClassInTeam(HeroClass.SUPPORTER));
			}
		}

		public override void Update(float dt, int addVisuals)
		{
			base.Update(dt, addVisuals);
			this.inStateTimeCounter += dt;
			if (this.state == Enemy.State.LEAVING)
			{
				if (this.inStateTimeCounter >= this.data.durLeave)
				{
					this.UpdateState(Enemy.State.LEFT);
				}
				return;
			}
			if (this.state == Enemy.State.LEFT)
			{
				return;
			}
			if (this.state != Enemy.State.DEAD && !base.IsAlive())
			{
				if (this.IsBoss())
				{
					this.world.OnBossKilled();
				}
				else if (this.IsChest())
				{
					this.world.OnChestKilled();
				}
				else
				{
					this.world.OnEnemyKilled();
				}
				this.UpdateState(Enemy.State.DEAD);
				base.RemoveBuffsOnDeath();
				SoundEventCancelBy e = new SoundEventCancelBy(this.id);
				this.world.AddSoundEvent(e);
				bool flag = this.world.activeChallenge.HasTargetTotWave() && this.world.activeChallenge.GetTargetTotWave() == this.world.activeChallenge.GetTotWave();
				Sound sound = (this.data.soundHurt != null && !flag) ? this.data.soundHurt : this.data.soundDeath;
				SoundEventSound e2 = new SoundEventSound(SoundType.GAMEPLAY, this.id, false, 0f, sound);
				this.world.AddSoundEvent(e2);
				if (this.invulnerabilityVisualEffect != null)
				{
					this.invulnerabilityVisualEffect.OnDead();
				}
				if (this.data.Voices != null)
				{
					if (flag)
					{
						this.data.Voices.death.Play(this.world, this.id);
					}
					else
					{
						this.data.Voices.hurt.Play(this.world, this.id);
					}
				}
			}
			if (this.state == Enemy.State.DEAD)
			{
				if (!this.hasReleasedDeathEffect && this.inStateTimeCounter > this.data.deathEffectTime)
				{
					this.hasReleasedDeathEffect = true;
					VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.ENEMY_DEATH, 0.6f);
					visualEffect.pos = this.pos + AttachmentOffsets.GetDeathEffect(this.GetName());
					visualEffect.scale = this.data.deathEffectScale;
					visualEffect.skinIndex = this.data.dataBase.GetCorruptednessSkinIndex();
					this.world.visualEffects.Add(visualEffect);
				}
				if (this.invulnerabilityVisualEffect != null)
				{
					this.invulnerabilityVisualEffect.Update(dt, 0);
				}
				return;
			}
			if (this.data.dataBase.spawnedMinionsCount > 0)
			{
				int num = 0;
				int count = this.world.activeChallenge.enemies.Count;
				for (int i = 1; i < count; i++)
				{
					Enemy enemy = this.world.activeChallenge.enemies[i];
					if (!enemy.IsDead() && !enemy.IsSpawnNonexisting() && !enemy.IsSpawnTranslating())
					{
						num++;
					}
				}
				if (this.invulnerabilityVisualEffect != null)
				{
					this.invulnerabilityVisualEffect.Update(dt, num);
				}
				if (this.data.dataBase.isInmuneWithMinions)
				{
					bool flag2 = base.IsInvulnerable();
					if (flag2 && num == 0 && this.state != Enemy.State.SPAWN_MINIONS && this.firstMinionsSpawned)
					{
						base.RemoveBuff(Enemy.BUFF_INV_MINIONS);
					}
					else if (!flag2 && num > 0)
					{
						this.AddBuff(Enemy.BUFF_INV_MINIONS, 0, false);
					}
				}
				if (this.state != Enemy.State.SPAWN_NONEXISTANCE && this.state != Enemy.State.SPAWN_TRANSLATE && this.state != Enemy.State.SPAWN_DROP && num == 0 && this.data.dataBase.spawnMinionsIfAloneAfterSeconds > 0f)
				{
					this.secondsToNextMinionSpawn -= dt;
					if (this.secondsToNextMinionSpawn <= 0f)
					{
						this.secondsToNextMinionSpawn = this.data.dataBase.spawnMinionsIfAloneAfterSeconds;
						this.minionsSpawnedCount = 0;
						this.UpdateState(Enemy.State.SPAWN_MINIONS);
						this.firstMinionsSpawned = true;
					}
				}
			}
			if (this.state == Enemy.State.SPAWN_NONEXISTANCE)
			{
				this.pos = this.posSpawnStart;
				this.inStateTimeCounter += dt * this.extraSpawnSpeed;
				if (this.inStateTimeCounter >= this.durSpawnNonexistent)
				{
					this.UpdateState(Enemy.State.SPAWN_TRANSLATE);
				}
			}
			if (this.state == Enemy.State.SPAWN_TRANSLATE)
			{
				this.inStateTimeCounter += dt * this.extraSpawnSpeed;
				if (this.inStateTimeCounter >= this.GetDurSpawnTranslate())
				{
					this.pos = this.posSpawnEnd;
					this.UpdateState(Enemy.State.SPAWN_DROP);
					if (!this.doNotPlaySpawnSound)
					{
						if (this.data.Voices != null)
						{
							this.data.Voices.spawn.Play(this.world, this.id);
						}
						SoundEventSound e3 = new SoundEventSound(SoundType.GAMEPLAY, this.id, false, 0f, this.data.soundSpawn);
						this.world.AddSoundEvent(e3);
					}
				}
				else
				{
					float spawnTranslateTimeRatio = this.inStateTimeCounter / (this.GetDurSpawnTranslate() + this.data.durSpawnDropTranslate * 0.3f);
					this.pos = this.GetSpawnPos(spawnTranslateTimeRatio);
				}
			}
			if (this.state == Enemy.State.SPAWN_DROP)
			{
				this.inStateTimeCounter += dt * this.extraSpawnSpeed;
				if (this.inStateTimeCounter >= this.data.durSpawnDropTranslate)
				{
					this.pos = this.posSpawnEnd;
				}
				else
				{
					float num2 = this.GetDurSpawnTranslate() / (this.GetDurSpawnTranslate() + this.data.durSpawnDropTranslate * 0.3f);
					num2 += (1f - num2) * Mathf.Sqrt(this.inStateTimeCounter / this.data.durSpawnDropTranslate);
					this.pos = this.GetSpawnPos(num2);
				}
				if (this.inStateTimeCounter >= this.GetDurSpawnDrop())
				{
					this.pos = this.posSpawnEnd;
					this.UpdateState(Enemy.State.IDLE);
				}
			}
			if (this.state != Enemy.State.STUN && base.HasBuffStun() && this.state != Enemy.State.SPAWN_NONEXISTANCE && this.state != Enemy.State.SPAWN_TRANSLATE && this.state != Enemy.State.SPAWN_DROP)
			{
				this.UpdateState(Enemy.State.STUN);
			}
			if (this.state == Enemy.State.STUN)
			{
				if (!base.HasBuffStun())
				{
					this.UpdateState(Enemy.State.IDLE);
				}
				return;
			}
			this.UpdateAttackWaitTime(dt);
			if (this.state == Enemy.State.ATTACK)
			{
				float num3 = base.GetAttackSpeed();
				if (num3 < 0.5f)
				{
					num3 = 0.5f;
				}
				this.timeCounterAttackActive += dt * num3;
				if (!this.hasDamagedThisCycle && this.timeCounterAttackActive > this.data.timeDamage)
				{
					this.OnAttackDamage();
				}
				if (this.timeCounterAttackActive > this.data.durAttackActive)
				{
					this.UpdateState(Enemy.State.IDLE);
					this.numHits++;
				}
				this.PlayTimedAttackSounds(this.timeCounterAttackActive / this.data.durAttackActive);
			}
			if (this.state == Enemy.State.IDLE && this.timeCounterAttackWait >= this.data.durAttackWait)
			{
				this.timeCounterAttackActive = 0f;
				float num4 = this.data.durAttackWait * 0.1f;
				this.timeCounterAttackWait = GameMath.GetRandomFloat(-num4, num4, GameMath.RandType.NoSeed);
				this.attackTarget = this.world.GetHeroToAttack();
				if (this.attackTarget != null && this.attackTarget.IsAttackable())
				{
					this.UpdateState(Enemy.State.ATTACK);
					this.atAttackSound = 0;
				}
			}
			if (this.state == Enemy.State.SPAWN_MINIONS)
			{
				if (this.minionsSpawnedCount < this.data.dataBase.spawnedMinionsCount && this.inStateTimeCounter >= this.data.dataBase.spawnMinionsTime && this.inStateTimeCounter - this.data.dataBase.spawnMinionsTime >= this.data.dataBase.secondsBetweenEachMinionSpawn * (float)this.minionsSpawnedCount)
				{
					this.minionsSpawnedCount++;
					if (this.data.dataBase.isInmuneWithMinions)
					{
						BuffDataInvulnerability spawnBuff = new BuffDataInvulnerability
						{
							id = 321,
							isStackable = false,
							dur = this.data.dataBase.spawnMinionsAnimDuration - this.inStateTimeCounter,
							visuals = 128
						};
						this.world.activeChallenge.SpawnOneMinion(this.data.dataBase.spawnedMinion, this.data.dataBase.spawnedMinionsCount, this.minionsSpawnedCount, spawnBuff);
					}
					else
					{
						this.world.activeChallenge.SpawnOneMinion(this.data.dataBase.spawnedMinion, this.data.dataBase.spawnedMinionsCount, this.minionsSpawnedCount, null);
					}
				}
				if (this.inStateTimeCounter >= this.data.dataBase.spawnMinionsAnimDuration)
				{
					this.UpdateState(Enemy.State.IDLE);
				}
			}
		}

		public bool IsSpawning(float stateTime)
		{
			if (this.state == Enemy.State.SPAWN_TRANSLATE)
			{
				float num = this.inStateTimeCounter / (this.GetDurSpawnTranslate() + this.data.durSpawnDropTranslate * 0.3f);
				if (num > stateTime)
				{
					return false;
				}
			}
			return this.state == Enemy.State.SPAWN_DROP || this.state == Enemy.State.SPAWN_NONEXISTANCE || this.state == Enemy.State.SPAWN_TRANSLATE;
		}

		private void UpdateAttackWaitTime(float dt)
		{
			float num = base.GetAttackSpeed();
			if (num < 0f)
			{
				num = 0f;
			}
			this.timeCounterAttackWait += dt * num;
		}

		public void UpdateState(Enemy.State newState)
		{
			if (newState == this.state)
			{
				return;
			}
			this.state = newState;
			this.inStateTimeCounter = 0f;
			this.hasDamagedThisCycle = false;
			if (newState == Enemy.State.SPAWN_TRANSLATE && this.data.dataBase.stunHereosWhenSpawn)
			{
				BuffDataStun buffDataStun = new BuffDataStun();
				buffDataStun.id = 319;
				buffDataStun.isPermenant = false;
				buffDataStun.isStackable = false;
				buffDataStun.visuals |= 512;
				buffDataStun.dur = this.data.dataBase.stunDurationInSeconds;
				foreach (Hero hero in this.world.heroes)
				{
					hero.AddBuff(buffDataStun, 0, false);
				}
			}
			else if (newState == Enemy.State.SPAWN_MINIONS && this.firstMinionsSpawned)
			{
				this.world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, this.id, false, 0f, this.data.dataBase.soundSummonMinions));
				if (this.data.Voices != null)
				{
					this.data.Voices.summonMinions.Play(this.world, this.id);
				}
			}
		}

		public void Leave()
		{
			this.UpdateState(Enemy.State.LEAVING);
			Enemy.BUFF_INV_LEAVING.id = 117;
			this.AddBuff(Enemy.BUFF_INV_LEAVING, 0, false);
		}

		private void OnAttackDamage()
		{
			this.hasDamagedThisCycle = true;
			if (this.data.dataBase.projectile == null || (this.data.dataBase.useProjectileOnNumHit != null && !this.data.dataBase.useProjectileOnNumHit[this.numHits % this.data.dataBase.useProjectileOnNumHit.Length]))
			{
				bool flag = false;
				float missChance = base.GetMissChance();
				if (GameMath.GetProbabilityOutcome(missChance, GameMath.RandType.NoSeed))
				{
					flag = true;
					Damage damage = new Damage(0.0, false, false, true, false);
					GlobalPastDamage pastDamage = new GlobalPastDamage(this, damage);
					this.world.AddPastDamage(pastDamage);
				}
				Damage damage2 = new Damage(base.GetDamage(), false, false, flag, false);
				if (flag)
				{
					base.OnMissed(this.attackTarget, damage2);
				}
				this.world.DamageMain(this, this.attackTarget, damage2);
			}
			else
			{
				Projectile projectile = this.data.dataBase.projectile;
				Projectile projectile2 = new Projectile(this, projectile.type, projectile.targetType, this.attackTarget, projectile.durFly, projectile.path);
				bool flag2 = false;
				float missChance2 = base.GetMissChance();
				if (GameMath.GetProbabilityOutcome(missChance2, GameMath.RandType.NoSeed))
				{
					flag2 = true;
					Damage damage3 = new Damage(0.0, false, false, true, false);
					GlobalPastDamage pastDamage2 = new GlobalPastDamage(this, damage3);
					this.world.AddPastDamage(pastDamage2);
				}
				Damage damage4 = new Damage(base.GetDamage(), false, false, flag2, false);
				if (flag2)
				{
					base.OnMissed(this.attackTarget, damage4);
				}
				projectile2.damage = damage4;
				if (projectile.visualEffect != null)
				{
					projectile2.visualEffect = projectile.visualEffect.GetCopy();
				}
				if (this.data.dataBase.soundImpact != null)
				{
					projectile2.soundImpact = new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, this.data.dataBase.soundImpact);
				}
				this.AddProjectile(projectile2);
			}
		}

		public bool IsOutOfDeathlessZone()
		{
			return this.pos.x <= Enemy.DEATHLESS_ZONE_LIMIT_X;
		}

		public override void TakeDamage(Damage damage, Unit by, double minHealth = 0.0)
		{
			bool flag = !base.IsAlive();
			if (this.pos.x > Enemy.DEATHLESS_ZONE_LIMIT_X)
			{
				minHealth = 0.001;
			}
			base.TakeDamage(damage, by, minHealth);
			bool flag2 = !base.IsAlive();
			if (!flag && flag2)
			{
				this.DropLoot();
				TutorialManager.OnEnemyKilled(this, by);
			}
			TutorialManager.OnEnemyTookDamage(damage, by, this);
		}

		private void DropLoot()
		{
			this.DropLootGold();
			this.DropLootMythstone();
			if (this.world.canDropChestCandies && this.world.gameMode != GameMode.RIFT && (this.data.dataBase.type != EnemyDataBase.Type.CHEST || this.canDropCandies))
			{
				this.DropLootCandy();
			}
			if (GameMath.GetProbabilityOutcome(this.world.universalBonus.powerupNonCritDamageDropChance, GameMath.RandType.NoSeed))
			{
				Vector3 velStart = new Vector3(GameMath.GetRandomFloat(0.8f, -0.8f, GameMath.RandType.NoSeed) - this.pos.x, GameMath.GetRandomFloat(1.2f, 1.8f, GameMath.RandType.NoSeed), 0f);
				DropPowerupNonCritDamage dropPowerupNonCritDamage = new DropPowerupNonCritDamage();
				dropPowerupNonCritDamage.InitVel(this.data.dataBase.durLoot, this.pos, this.pos.y, velStart);
				dropPowerupNonCritDamage.durNonExistence = 1f;
				this.world.drops.Add(dropPowerupNonCritDamage);
			}
			if (GameMath.GetProbabilityOutcome(this.world.universalBonus.powerupCooldownDropChance, GameMath.RandType.NoSeed))
			{
				Vector3 velStart2 = new Vector3(GameMath.GetRandomFloat(0.8f, -0.8f, GameMath.RandType.NoSeed) - this.pos.x, GameMath.GetRandomFloat(1.2f, 1.8f, GameMath.RandType.NoSeed), 0f);
				DropPowerupCooldown dropPowerupCooldown = new DropPowerupCooldown();
				dropPowerupCooldown.InitVel(this.data.dataBase.durLoot, this.pos, this.pos.y, velStart2);
				dropPowerupCooldown.durNonExistence = 1f;
				this.world.drops.Add(dropPowerupCooldown);
			}
			if (GameMath.GetProbabilityOutcome(this.world.universalBonus.powerupReviveDropChance, GameMath.RandType.NoSeed))
			{
				Vector3 velStart3 = new Vector3(GameMath.GetRandomFloat(0.8f, -0.8f, GameMath.RandType.NoSeed) - this.pos.x, GameMath.GetRandomFloat(1.2f, 1.8f, GameMath.RandType.NoSeed), 0f);
				DropPowerupRevive dropPowerupRevive = new DropPowerupRevive();
				dropPowerupRevive.InitVel(this.data.dataBase.durLoot, this.pos, this.pos.y, velStart3);
				dropPowerupRevive.durNonExistence = 1f;
				this.world.drops.Add(dropPowerupRevive);
			}
		}

		public double GetTotalLootToDrop()
		{
			double num = this.percentageDamageTakenFromHeroes / 0.01;
			double num2 = 1.0 + num * this.world.universalBonus.bountyIncreasePerDamageTakenFromHero;
			double num3 = this.statCache.goldToDrop * this.world.buffTotalEffect.goldFactor * this.world.buffTotalEffect.goldFactorTE * this.world.buffTotalEffect.goldBoostFactor * this.world.activeChallenge.totalGainedUpgrades.goldFactor * num2;
			if (GameMath.GetProbabilityOutcome(this.world.GetGoldMultTenChance(), GameMath.RandType.NoSeed))
			{
				num3 *= 10.0;
			}
			if (this.data.dataBase.type == EnemyDataBase.Type.CHEST)
			{
				num3 *= this.world.universalBonus.goldChestFactor + this.world.buffTotalEffect.goldChestFactor - 1.0;
				if (this.isChestRaid)
				{
					num3 *= this.world.universalBonus.goldChestRaidFactor;
				}
			}
			return num3;
		}

		private void DropLootGold()
		{
			double totalLootToDrop = this.GetTotalLootToDrop();
			int randomInt = GameMath.GetRandomInt(this.data.dataBase.numDropMin, this.data.dataBase.numDropMax, GameMath.RandType.NoSeed);
			Vector3 pos = this.pos;
			if (this.world.buffTotalEffect.noGoldDrop)
			{
				return;
			}
			float height = this.GetHeight();
			pos.x += GameMath.GetRandomFloat(-height * 0.25f, height * 0.25f, GameMath.RandType.NoSeed);
			pos.y += this.GetHeight() * 0.5f + GameMath.GetRandomFloat(-height * 0.25f, height * 0.25f, GameMath.RandType.NoSeed);
			for (int i = 0; i < randomInt; i++)
			{
				Vector3 velStart = new Vector3(GameMath.GetRandomFloat(0.4f, -0.4f, GameMath.RandType.NoSeed) - pos.x * 0.25f, GameMath.GetRandomFloat(1.2f, 2.2f, GameMath.RandType.NoSeed), 0f);
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.GOLD, 0.0, this.world, false);
				if (this.world.gameMode == GameMode.CRUSADE)
				{
					dropCurrency.invPos = RenderManager.POS_GOLD_INV_TIMECHALLENGE;
				}
				dropCurrency.InitVel(this.data.dataBase.durLoot / (float)randomInt * (float)i, pos, this.pos.y, velStart);
				double num = totalLootToDrop / (double)randomInt * GameMath.GetRandomDouble(0.95, 1.05, GameMath.RandType.NoSeed);
				if (num > 0.0)
				{
					dropCurrency.amount = num;
					this.world.drops.Add(dropCurrency);
				}
			}
		}

		private void DropLootMythstone()
		{
			if (this.mythstone <= 0.0)
			{
				return;
			}
			int num;
			if (this.mythstone > 5.0)
			{
				num = 5;
			}
			else
			{
				num = (int)this.mythstone;
			}
			double amount = this.mythstone / (double)num;
			for (int i = 0; i < num; i++)
			{
				Vector3 velStart = new Vector3(GameMath.GetRandomFloat(0.8f, -0.8f, GameMath.RandType.NoSeed) - this.pos.x, GameMath.GetRandomFloat(1.2f, 1.8f, GameMath.RandType.NoSeed), 0f);
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.MYTHSTONE, amount, this.world, false);
				dropCurrency.InitVel(this.data.dataBase.durLoot / (float)num * (float)i, this.pos, this.pos.y, velStart);
				dropCurrency.durNonExistence = 1f;
				this.world.drops.Add(dropCurrency);
			}
		}

		private void DropLootCandy()
		{
			if (this.data.dataBase.type != EnemyDataBase.Type.CHEST && this.data.dataBase.name != "BOSS SNOWMAN")
			{
				return;
			}
			int num;
			if (this.data.dataBase.type == EnemyDataBase.Type.CHEST)
			{
				if (this.isChestRaid)
				{
					num = GameMath.GetRandomInt(4, 4, GameMath.RandType.NoSeed);
				}
				else
				{
					num = GameMath.GetRandomInt(4, 4, GameMath.RandType.NoSeed);
				}
				if (this.isRiftEnemy)
				{
					num = GameMath.CeilToInt((float)num * 0.35f);
				}
			}
			else
			{
				num = GameMath.GetRandomInt(25, 30, GameMath.RandType.NoSeed);
			}
			for (int i = 0; i < num; i++)
			{
				Vector3 velStart = new Vector3(GameMath.GetRandomFloat(0.8f, -0.8f, GameMath.RandType.NoSeed) - this.pos.x, GameMath.GetRandomFloat(1.2f, 1.8f, GameMath.RandType.NoSeed), 0f);
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.CANDY, 1.0, this.world, true);
				dropCurrency.InitVel(this.data.dataBase.durLoot / (float)num * (float)i, this.pos, this.pos.y, velStart);
				dropCurrency.durNonExistence = 0.1f;
				this.world.drops.Add(dropCurrency);
			}
		}

		public override void DamageAll(Damage damage)
		{
			foreach (Hero damaged in this.world.heroes)
			{
				this.world.DamageMain(this, damaged, damage);
			}
		}

		public override IEnumerable<UnitHealthy> GetAllies()
		{
			foreach (Enemy enemy in this.world.activeChallenge.enemies)
			{
				yield return enemy;
			}
			yield break;
		}

		public override IEnumerable<UnitHealthy> GetOpponents()
		{
			foreach (Hero hero in this.world.heroes)
			{
				yield return hero;
			}
			yield break;
		}

		public IEnumerable<Hero> GetHeroOpponents()
		{
			foreach (Hero hero in this.world.heroes)
			{
				yield return hero;
			}
			yield break;
		}

		public bool IsRegular()
		{
			return this.data.dataBase.type == EnemyDataBase.Type.REGULAR || this.data.dataBase.type == EnemyDataBase.Type.CHEST;
		}

		public bool IsBoss()
		{
			return this.data.dataBase.type == EnemyDataBase.Type.BOSS || this.data.dataBase.type == EnemyDataBase.Type.EPIC;
		}

		public bool IsEpic()
		{
			return this.data.dataBase.type == EnemyDataBase.Type.EPIC;
		}

		public bool IsChest()
		{
			return this.data.dataBase.type == EnemyDataBase.Type.CHEST;
		}

		public bool IsLeaving()
		{
			return this.state == Enemy.State.LEAVING;
		}

		public bool HasLeft()
		{
			return this.state == Enemy.State.LEFT;
		}

		public bool IsDead()
		{
			return this.state == Enemy.State.DEAD;
		}

		public bool IsSpawnNonexisting()
		{
			return this.state == Enemy.State.SPAWN_NONEXISTANCE;
		}

		public bool IsSpawnTranslating()
		{
			return this.state == Enemy.State.SPAWN_TRANSLATE;
		}

		public bool IsSpawnDropping()
		{
			return this.state == Enemy.State.SPAWN_DROP;
		}

		public bool IsIdle()
		{
			return this.state == Enemy.State.IDLE;
		}

		public bool IsStunned()
		{
			return this.state == Enemy.State.STUN;
		}

		public bool IsAttacking()
		{
			return this.state == Enemy.State.ATTACK;
		}

		public bool IsSpawningMinions()
		{
			return this.state == Enemy.State.SPAWN_MINIONS;
		}

		public Vector3 GetPosAfterTime(float timePass)
		{
			if (!this.IsSpawnNonexisting() && !this.IsSpawnTranslating())
			{
				return this.pos;
			}
			float num = this.inStateTimeCounter + timePass;
			if (this.IsSpawnNonexisting())
			{
				num -= this.durSpawnNonexistent;
			}
			if (num < 0f)
			{
				return this.posSpawnStart;
			}
			if (num >= this.GetDurSpawnTranslate())
			{
				return this.posSpawnEnd;
			}
			float spawnTranslateTimeRatio = num / this.GetDurSpawnTranslate();
			return this.GetSpawnPos(spawnTranslateTimeRatio);
		}

		private Vector3 GetSpawnPos(float spawnTranslateTimeRatio)
		{
			return GameMath.Lerp(this.posSpawnStart, this.posSpawnEnd, spawnTranslateTimeRatio);
		}

		public override bool IsOnWorld()
		{
			foreach (Unit unit in this.world.activeChallenge.enemies)
			{
				if (unit == this)
				{
					return true;
				}
			}
			return false;
		}

		private void PlayTimedAttackSounds(float attackTimeRatio)
		{
			List<TimedSound> soundsAttack = this.data.soundsAttack;
			while (this.atAttackSound < soundsAttack.Count && soundsAttack[this.atAttackSound].time <= attackTimeRatio)
			{
				Sound sound = soundsAttack[this.atAttackSound].sound;
				if (sound is SoundVaried)
				{
					SoundVaried soundVaried = (SoundVaried)sound;
					soundVaried.SetVariation(this.numHits);
				}
				SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, this.GetId(), false, 0f, sound);
				this.world.AddSoundEvent(e);
				this.atAttackSound++;
			}
		}

		public string GetName()
		{
			return this.data.dataBase.name;
		}

		public override bool AddBuff(BuffData buffData, int genericCounter = 0, bool lateAdd = false)
		{
			if (!base.AddBuff(buffData, genericCounter, lateAdd))
			{
				return false;
			}
			if ((buffData.visuals & 512) != 0)
			{
				this.world.OnEnemyStunned(this);
			}
			return true;
		}

		public static float DEATHLESS_ZONE_LIMIT_X = 1f;

		private static BuffDataInvulnerability BUFF_INV_LEAVING = new BuffDataInvulnerability(float.PositiveInfinity);

		private static BuffDataInvulnerability BUFF_INV_MINIONS = new BuffDataInvulnerability(float.PositiveInfinity);

		public static uint totNumEnemiesCounter;

		public Enemy.State state;

		public string id;

		public EnemyData data;

		public double mythstone;

		public float durSpawnNonexistent;

		public Vector3 posSpawnStart;

		public Vector3 posSpawnEnd;

		public UnitHealthy attackTarget;

		public float timeCounterAttackActive;

		public float timeCounterAttackWait;

		public bool hasDamagedThisCycle;

		public int numHits;

		public int atAttackSound;

		public double percentageDamageTakenFromHeroes;

		public bool hasReleasedDeathEffect;

		public int genericRandom;

		public float extraSpawnSpeed;

		public bool isChestRaid;

		public bool canDropCandies;

		public bool doNotPlaySpawnSound;

		public WiseSnakeInvulnerabilityVisualEffect invulnerabilityVisualEffect;

		private float secondsToNextMinionSpawn;

		private float spawnMinionsIfAloneEverySeconds;

		private int minionsSpawnedCount;

		private bool firstMinionsSpawned;

		private bool isRiftEnemy;

		public enum State
		{
			SPAWN_NONEXISTANCE,
			SPAWN_TRANSLATE,
			SPAWN_DROP,
			IDLE,
			ATTACK,
			STUN,
			SPAWN_MINIONS,
			DEAD,
			LEAVING,
			LEFT
		}
	}
}
