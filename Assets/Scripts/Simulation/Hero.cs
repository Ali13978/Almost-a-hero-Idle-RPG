using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class Hero : UnitHealthy
	{
		public Hero(HeroDataBase dataBase, int[] levelJumps, int level, int xp, World world, List<Gear> gears) : base(world)
		{
			this.gears = gears;
			this.level = level;
			this.xp = xp;
			this.levelJumps = levelJumps;
			this.duplicateIndex = -1;
			int progress = this.GetProgress(level, xp);
			this.data = new HeroData(dataBase, progress, level);
			this.canBeRendered = true;
			this.numUnspentSkillPoints = 0;
			this.skillTreeProgressGained = new SkillTreeProgress(dataBase.skillTree);
			this.skillTreeProgressGeared = new SkillTreeProgress(dataBase.skillTree);
			this.skillTreeProgressGeared.InitAsAdditional();
			this.skillTreeProgressTotal = new SkillTreeProgress(dataBase.skillTree);
			this.UpdateSkillTreeProgressTotal();
			this.duringSkillInvulnerability = new BuffDataInvulnerability();
			this.duringSkillInvulnerability.id = 321;
			this.duringSkillInvulnerability.visuals |= 128;
			this.state = Hero.State.IDLE;
			this.runningSkill = null;
			this.needsInitialization = false;
			this.activeSkills = new List<SkillActiveData>();
			this.passiveSkills = new List<SkillPassiveData>();
			this.skillEnhancers = new List<SkillEnhancer>();
			this.skillCooldowns = new List<float>();
			this.weapon = this.data.weapon.Clone();
			this.weapon.Init(this, world);
			this.UpdateStats(0f);
			base.SetHealthFull();
			this.OnGearsChanged();
		}

		public Trinket trinket
		{
			get
			{
				return this.data.GetDataBase().trinket;
			}
		}

		public void Refresh()
		{
			this.UpdateData();
			this.RefreshAllSkills();
		}

		public void SetLevel(int newLevel, int newXp)
		{
			this.level = newLevel;
			this.xp = newXp;
			this.UpdateData();
			this.RefreshAllSkills();
		}

		public void UpdateData()
		{
			int progress = this.GetProgress(this.level, this.xp);
			this.data.SetProgress(progress, this.level);
		}

		public Weapon GetWeapon()
		{
			return this.weapon;
		}

		public double GetUpgradeCost(bool takeFreeUpgradeIntoAccount = true)
		{
			if (takeFreeUpgradeIntoAccount && this.nextUpgradeFree)
			{
				return 0.0;
			}
			if (this.level < 0 || this.level >= HeroDataBase.LEVEL_XPS.Length - 3)
			{
				return double.PositiveInfinity;
			}
			bool flag = false;
			if (this.xp + 1 == this.world.GetRequiredXpToLevelHero(this.level))
			{
				flag = true;
			}
			double x = 1.6;
			double num = 0.65;
			if (this.world.activeChallenge is ChallengeRift)
			{
				x = 1.42;
				num = 1.0;
			}
			double y = (double)this.GetProgress(this.level, 0) * num + (double)this.xp;
			double num2 = 10.0 * Math.Pow(x, y);
			if (flag)
			{
				num2 *= 5.0;
			}
			return num2 * (this.statCache.upgradeCostFactor * this.costMultiplier);
		}

		public void Upgrade()
		{
			double healthMax = base.GetHealthMax();
			this.xp++;
			if (this.xp == this.GetXpNeedForNextLevel())
			{
				this.level++;
				this.xp = 0;
				this.numUnspentSkillPoints++;
				if (GameMath.GetProbabilityOutcome(0.25f, GameMath.RandType.NoSeed))
				{
					SoundEventSound e = new SoundEventSound(SoundType.UI, this.GetId(), true, 0f, this.soundVoLevelUp);
					this.world.AddSoundEvent(e);
				}
			}
			this.Refresh();
			this.UpdateStats(0f);
			if (base.IsAlive())
			{
				double healthMax2 = base.GetHealthMax();
				double healthRatio = base.GetHealthRatio();
				double healRatio = (1.0 - healthRatio) * (healthMax2 - healthMax) / healthMax2;
				this.HealWithoutCallback(healRatio);
			}
			foreach (Buff buff in this.buffs)
			{
				buff.OnUpgradedSelf();
			}
			this.costMultiplier = 1.0;
			this.RefreshAllSkills();
		}

		public bool CanEvolve()
		{
			HeroDataBase dataBase = this.data.GetDataBase();
			return dataBase.CanEvolve(this.gears);
		}

		public void Evolve()
		{
			this.data.GetDataBase().evolveLevel++;
			this.UpdateData();
			this.world.shouldSave = true;
		}

		public int GetLevel()
		{
			return this.level;
		}

		public void SetLevel(int level)
		{
			this.level = level;
		}

		public int GetXp()
		{
			return this.xp;
		}

		public void SetXp(int xp)
		{
			this.xp = xp;
		}

		public int GetEvolveLevel()
		{
			return this.data.GetDataBase().evolveLevel;
		}

		public int GetEquippedSkinIndex()
		{
			return this.data.GetDataBase().equippedSkin.index;
		}

		public int GetProgress(int level, int xp)
		{
			return this.levelJumps[GameMath.GetMinInt(level, this.levelJumps.Length - 1)] + xp;
		}

		public int GetNextProgress(int level, int xp)
		{
			int num = level;
			int num2 = xp + 1;
			if (num2 == this.world.GetRequiredXpToLevelHero(level))
			{
				num++;
				num2 = 0;
			}
			return this.GetProgress(num, num2);
		}

		public int GetXpNeedForNextLevel()
		{
			return this.world.GetRequiredXpToLevelHero(this.level);
		}

		public int GetNumUnspentSkillPoints()
		{
			return this.numUnspentSkillPoints;
		}

		public void SetNumUnspentSkillPoints(int numUnspentSkillPoints)
		{
			this.numUnspentSkillPoints = numUnspentSkillPoints;
		}

		public void IncrementNumUnspentSkillPoints(int numUnspentSkillPoints)
		{
			this.numUnspentSkillPoints += numUnspentSkillPoints;
		}

		public override string GetId()
		{
			return this.data.GetId();
		}

		public string GetDistinctId()
		{
			string text = string.Empty;
			if (this.duplicateIndex >= 0)
			{
				text += this.duplicateIndex.ToString();
			}
			return this.data.GetId() + text;
		}

		public override float GetScaleHealthBar()
		{
			return this.data.GetScaleHealthBar();
		}

		public override float GetScaleBuffVisual()
		{
			return this.data.GetScaleBuffVisual();
		}

		public override float GetHeight()
		{
			return this.data.GetHeight() + this.heightOffset;
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
			return this.weapon.GetDps();
		}

		public override double GetDpsTeam()
		{
			return this.world.GetHeroTeamDps();
		}

		public double GetHealthMaxNextUpgrade()
		{
			int nextProgress = this.GetNextProgress(this.level, this.xp);
			double healthFactor = this.world.activeChallenge.totalGainedUpgrades.healthFactor;
			return this.data.GetHealthMaxFromProgress(nextProgress) * healthFactor;
		}

		public override void Heal(double healRatio)
		{
			base.Heal(healRatio * this.world.buffTotalEffect.heroHealFactor);
		}

		public void HealWithoutCallback(double healRatio)
		{
			base.Heal(healRatio * this.world.buffTotalEffect.heroHealFactor);
		}

		public double GetDamageAll()
		{
			return this.statCache.damage;
		}

		public double GetDamageDifUpgrade()
		{
			int progress = this.GetProgress(this.level, this.xp);
			int nextProgress = this.GetNextProgress(this.level, this.xp);
			double num = this.data.GetDamageFromProgress(nextProgress) / this.data.GetDamageFromProgress(progress);
			return this.statCache.damage * num - this.statCache.damage;
		}

		public double GetDamageNonBuffedNextUpgrade()
		{
			int nextProgress = this.GetNextProgress(this.level, this.xp);
			double heroDamageFactor = this.world.activeChallenge.totalGainedUpgrades.heroDamageFactor;
			return this.data.GetDamageFromProgress(nextProgress) * heroDamageFactor;
		}

		public override void TakeDamage(Damage damage, Unit by, double minHealth = 0.0)
		{
			bool flag = !base.IsAlive();
			base.TakeDamage(damage, by, minHealth);
			bool flag2 = !base.IsAlive();
			if (!flag && flag2)
			{
				this.weapon.OnDied();
			}
		}

		public int GetWeaponLoadExtra()
		{
			return this.statCache.weaponLoadExtra;
		}

		public float GetBlackCurtainRatio()
		{
			if (this.blackCurtainTimeCounter < this.bcTimeFadeInStart)
			{
				return 0f;
			}
			if (this.blackCurtainTimeCounter < this.bcTimeFadeInEnd)
			{
				return (this.blackCurtainTimeCounter - this.bcTimeFadeInStart) / (this.bcTimeFadeInEnd - this.bcTimeFadeInStart);
			}
			if (this.blackCurtainTimeCounter < this.bcTimeFadeOutStart)
			{
				return 1f;
			}
			if (this.blackCurtainTimeCounter < this.bcTimeFadeOutEnd)
			{
				return (this.bcTimeFadeOutEnd - this.blackCurtainTimeCounter) / (this.bcTimeFadeOutEnd - this.bcTimeFadeOutStart);
			}
			return 0f;
		}

		public bool IsInFrontBlackCurtain()
		{
			return this.blackCurtainTimeCounter < this.bcDurStayInFront;
		}

		public bool CanBeSlowedByBlackCurtain()
		{
			return this.blackCurtainTimeCounter >= this.bcDurNonslowable;
		}

		public override void UpdateBuffTotalWorldEffect(BuffTotalWorldEffect buffTotalEffect)
		{
			buffTotalEffect.ringDamageEvolveFactor += (GameMath.PowInt(1.5, this.data.GetDataBase().evolveLevel) - 1.0) * 0.05;
			base.UpdateBuffTotalWorldEffect(buffTotalEffect);
		}

		public override void UpdateStats(float dt)
		{
			base.UpdateStats(dt);
			this.statCache.UpdateHero(this.data, this.buffTotalEffect, this.world.buffTotalEffect, this.world.universalBonus, this.world.activeChallenge.totalGainedUpgrades, this.world.GetNumHeroesOfClassInTeam(HeroClass.ATTACKER), this.world.GetNumHeroesOfClassInTeam(HeroClass.DEFENDER));
		}

		public override void Update(float dt, int addVisuals)
		{
			base.Update(dt, addVisuals);
			this.inStateTimeCounter += dt;
			this.blackCurtainTimeCounter += dt;
			if (this.needsInitialization)
			{
				return;
			}
			if (this.state != Hero.State.DEAD && !base.IsAlive())
			{
				if (this.runningSkill != null)
				{
					this.castedSkillDuringDeath = this.runningSkill;
					this.runningSkill.Cancel();
					this.runningSkill = null;
				}
				base.ClearNonpermenantBuffs();
				this.UpdateState(Hero.State.DEAD);
				SoundEventCancelBy e = new SoundEventCancelBy(this.GetId());
				this.world.AddSoundEvent(e);
				SoundEventSound e2 = new SoundEventSound(SoundType.GAMEPLAY, this.GetId(), false, 0f, this.data.soundDeath);
				this.world.AddSoundEvent(e2);
				if (GameMath.GetProbabilityOutcome(0.4f, GameMath.RandType.NoSeed))
				{
					SoundEventSound e3 = new SoundEventSound(SoundType.GAMEPLAY, this.GetId(), true, 999f, this.data.soundVoDeath);
					this.world.AddSoundEvent(e3);
				}
				this.world.OnHeroDead();
			}
			if (this.state == Hero.State.DEAD)
			{
				this.inStateTimeCounter += dt * (this.buffTotalEffect.reviveSpeedFactor - 1f) + dt * (this.world.buffTotalEffect.reviveSpeed - 1f);
				if (this.world.activeChallenge.heroesCanRevive && this.inStateTimeCounter >= this.GetReviveDuration())
				{
					this.Revive();
				}
				this.weapon.UpdatePassive(dt);
				if (this.inStateTimeCounter - dt <= this.GetReviveDuration() - 1f && this.GetReviveDuration() - 1f < this.inStateTimeCounter)
				{
					SoundEventSound e4 = new SoundEventSound(SoundType.GAMEPLAY, this.GetId(), false, 0f, this.data.soundRevive);
					this.world.AddSoundEvent(e4);
					if (GameMath.GetProbabilityOutcome(0.3f, GameMath.RandType.NoSeed))
					{
						SoundEventSound e5 = new SoundEventSound(SoundType.GAMEPLAY, this.GetId(), true, 0f, this.data.soundVoRevive);
						this.world.AddSoundEvent(e5);
					}
				}
				return;
			}
			if (this.state == Hero.State.SKILL && base.IsSilenced())
			{
				this.runningSkill.Cancel();
				this.runningSkill = null;
				this.UpdateState(Hero.State.IDLE);
			}
			if (this.state != Hero.State.SKILL && this.runningSkill != null)
			{
				this.UpdateState(Hero.State.SKILL);
			}
			if (this.state != Hero.State.STUN && base.HasBuffStun())
			{
				if (this.state == Hero.State.SKILL)
				{
					this.runningSkill.Cancel();
					this.runningSkill = null;
				}
				this.UpdateState(Hero.State.STUN);
			}
			if (this.state == Hero.State.STUN && !base.HasBuffStun())
			{
				this.UpdateState(Hero.State.IDLE);
			}
			if ((this.state == Hero.State.WEAPON_CHANGE_TO_TEMP || this.state == Hero.State.WEAPON_CHANGE_TO_ORIG) && this.inStateTimeCounter >= this.durWeaponChange)
			{
				this.UpdateState(Hero.State.IDLE);
			}
			if (this.state == Hero.State.IDLE)
			{
				if (this.isToChangeToOrigWeapon)
				{
					this.isToChangeToOrigWeapon = false;
					this.weapon = this.data.weapon.Clone();
					this.weapon.Init(this, this.world);
					this.UpdateState(Hero.State.WEAPON_CHANGE_TO_ORIG);
				}
				if (!this.world.isRainingGlory && !this.world.isRainingDuck && this.world.activeChallenge.state == Challenge.State.ACTION)
				{
					this.weapon.TryActivate();
					if (this.weapon.IsActive())
					{
						this.UpdateState(Hero.State.ATTACK);
					}
					else
					{
						this.weapon.UpdatePassive(dt);
					}
				}
			}
			if (this.state == Hero.State.ATTACK)
			{
				this.weapon.UpdateActive(dt);
				if (!this.weapon.IsActive())
				{
					this.UpdateState(Hero.State.IDLE);
				}
			}
			if (this.state == Hero.State.IDLE && !this.world.isRainingGlory && !this.world.isRainingDuck && !base.IsSilenced())
			{
				int num = 1;
				for (int i = this.activeSkills.Count - 1; i >= num; i--)
				{
					if (this.skillCooldowns[i] == 0f)
					{
						if (this.activeSkills[i].castCondition != null)
						{
							if (!this.activeSkills[i].castCondition.IsNotOkay(this))
							{
								this.ActivateSkill(i);
							}
						}
						else
						{
							this.ActivateSkill(i);
						}
						break;
					}
				}
			}
			this.UpdateActiveSkill(dt);
			this.UpdateSkillCooldowns(dt);
		}

		private void UpdateActiveSkill(float dt)
		{
			if (this.runningSkill == null)
			{
				return;
			}
			if (this.IsStunned())
			{
				if (this.runningSkill.data.dataBase.stunBehaviour == SkillActiveDataBase.OnStunBehaviour.REMOVE)
				{
					this.runningSkill.Cancel();
					this.runningSkill = null;
					this.UpdateState(Hero.State.IDLE);
					return;
				}
				if (this.runningSkill.data.dataBase.stunBehaviour == SkillActiveDataBase.OnStunBehaviour.PAUSE)
				{
					return;
				}
			}
			this.runningSkill.Update(dt);
			if (this.runningSkill.HasEnded())
			{
				this.runningSkill = null;
				this.UpdateState(Hero.State.IDLE);
			}
		}

		private void UpdateSkillCooldowns(float dt)
		{
			float num = dt * this.statCache.skillCoolFactor;
			for (int i = this.skillCooldowns.Count - 1; i > 0; i--)
			{
				if (this.runningSkill == null || this.runningSkill.data.dataBase != this.activeSkills[i].dataBase)
				{
					this.skillCooldowns[i] = GameMath.GetMaxFloat(0f, this.skillCooldowns[i] - num);
				}
			}
			num *= this.world.buffTotalEffect.heroUltiCoolFactor;
			num *= this.buffTotalEffect.ultiCoolFactor;
			if (this.runningSkill == null || this.runningSkill.data.dataBase != this.activeSkills[0].dataBase)
			{
				this.skillCooldowns[0] = GameMath.GetMaxFloat(0f, this.skillCooldowns[0] - num);
			}
		}

		public void UpdateState(Hero.State newState)
		{
			if (this.state == newState)
			{
				return;
			}
			if (this.state == Hero.State.ATTACK && newState != Hero.State.IDLE)
			{
				this.weapon.OnInterrupted();
			}
			if (this.state == Hero.State.IDLE && GameMath.GetProbabilityOutcome(1f, GameMath.RandType.NoSeed))
			{
				this.idleChanger++;
			}
			this.state = newState;
			this.inStateTimeCounter = 0f;
		}

		private void Revive()
		{
			base.SetHealthFull();
			this.weapon = this.data.weapon.Clone();
			this.weapon.Init(this, this.world);
			this.AddBuff(this.world.activeChallenge.buffDataReviveInvulnerability, 0, false);
			this.UpdateState(Hero.State.IDLE);
			this.world.OnHeroRevived(this);
			this.castedSkillDuringDeath = null;
			foreach (UnitHealthy unitHealthy in this.GetAllies())
			{
				if (unitHealthy != this)
				{
					unitHealthy.OnReviveAlly(this);
				}
			}
		}

		private void ActivateSkill(int skillIndex)
		{
			SkillActiveData skillActiveData = this.activeSkills[skillIndex];
			if (this.runningSkill != null && this.runningSkill.data == skillActiveData)
			{
				this.runningSkill.Cancel();
				this.inStateTimeCounter = 0f;
			}
			this.skillCooldowns[skillIndex] = this.GetSkillCoolDownMax(skillIndex);
			this.runningSkill = new Skill(skillActiveData, this);
			this.UpdateState(Hero.State.SKILL);
			this.duringSkillInvulnerability.dur = skillActiveData.durInvulnurability;
			base.OnCastSpellSelf(this.runningSkill);
			foreach (UnitHealthy unitHealthy in this.GetAllies())
			{
				unitHealthy.OnCastSpellAlly(this, this.runningSkill);
			}
			if (this.world.totem != null)
			{
				this.world.totem.OnCastSpellAlly(this, this.runningSkill);
			}
			if (skillIndex == 0)
			{
				this.AddBuff(this.duringSkillInvulnerability, 0, false);
				this.world.dailyQuestUltiSkillCounter++;
				this.blackCurtainTimeCounter = 0f;
				SkillActiveDataBase dataBase = skillActiveData.dataBase;
				this.bcTimeFadeInStart = dataBase.timeFadeInStart;
				this.bcTimeFadeInEnd = dataBase.timeFadeInEnd;
				this.bcTimeFadeOutStart = dataBase.timeFadeOutStart;
				this.bcTimeFadeOutEnd = dataBase.timeFadeOutEnd;
				this.bcDurStayInFront = dataBase.durStayInFrontCurtain;
				this.bcDurNonslowable = dataBase.durNonslowable;
				TutorialManager.OnUltiUsed(this);
			}
			this.world.OnSpellCast(this.runningSkill, skillIndex);
			this.runningSkill.PlaySound(this.world);
		}

		public void CancelSkills()
		{
			if (this.runningSkill != null)
			{
				this.runningSkill.Cancel();
				this.runningSkill = null;
			}
			for (int i = this.skillCooldowns.Count - 1; i > 0; i--)
			{
				this.skillCooldowns[i] = this.GetSkillCoolDownMax(i);
			}
			this.skillCooldowns[0] = 0f;
		}

		public override void AddProjectile(Projectile newProjectile)
		{
			newProjectile.visualVariation = this.data.GetDataBase().equippedSkin.index;
			base.AddProjectile(newProjectile);
		}

		public void ChangeWeaponToTemp(float dur, Weapon tempWeapon, float durIfAlreadyTemWeapon = -1f)
		{
			this.isToChangeToOrigWeapon = false;
			if (durIfAlreadyTemWeapon > 0f && this.IsUsingTempWeapon())
			{
				this.durWeaponChange = durIfAlreadyTemWeapon;
				tempWeapon.isRepeatedTempWeapon = true;
			}
			else
			{
				tempWeapon.isRepeatedTempWeapon = false;
				this.durWeaponChange = dur;
			}
			this.weapon = tempWeapon;
			this.weapon.Init(this, this.world);
			this.UpdateState(Hero.State.WEAPON_CHANGE_TO_TEMP);
		}

		public void ChangeWeaponToOrig(float dur)
		{
			this.isToChangeToOrigWeapon = true;
			this.durWeaponChange = dur;
		}

		public void InterruptWeapon()
		{
			this.weapon.OnInterrupted();
		}

		public void OnGearsChanged()
		{
			this.RefreshAllSkills();
		}

		private void RefreshAllSkills()
		{
			this.UpdateSkillTreeProgress();
			this.RefreshSkillsAccordingToProgress();
		}

		private void UpdateSkillTreeProgress()
		{
			this.skillTreeProgressGeared.InitAsAdditional();
			foreach (Gear gear in this.gears)
			{
				this.UpdateSkillProgressWithGear(gear);
			}
			this.UpdateSkillTreeProgressTotal();
		}

		private void UpdateSkillProgressWithGear(Gear gear)
		{
			SkillTree skillTree = this.data.skillTree;
			SkillTreeNode skillToLevelUp = gear.data.skillToLevelUp;
			if (skillTree.ulti == skillToLevelUp)
			{
				this.skillTreeProgressGeared.ulti += gear.GetSkillLevelIncAmount();
				return;
			}
			for (int i = skillTree.branches.Length - 1; i >= 0; i--)
			{
				SkillTreeNode[] array = skillTree.branches[i];
				for (int j = array.Length - 1; j >= 0; j--)
				{
					SkillTreeNode skillTreeNode = array[j];
					if (skillTreeNode == skillToLevelUp)
					{
						this.skillTreeProgressGeared.branches[i][j] += gear.GetSkillLevelIncAmount();
						return;
					}
				}
			}
		}

		public void UpdateSkillTreeProgressTotal()
		{
			this.skillTreeProgressTotal.ulti = this.skillTreeProgressGained.ulti + this.skillTreeProgressGeared.ulti;
			for (int i = this.skillTreeProgressTotal.branches.Length - 1; i >= 0; i--)
			{
				int[] array = this.skillTreeProgressTotal.branches[i];
				int[] array2 = this.skillTreeProgressGained.branches[i];
				int[] array3 = this.skillTreeProgressGeared.branches[i];
				int j = 0;
				int num = array.Length;
				while (j < num)
				{
					array[j] = array2[j];
					if (this.IsSkillUpgradableByGear(i, j))
					{
						array[j] += array3[j];
					}
					j++;
				}
			}
		}

		private bool IsSkillUpgradableByGear(int branchIndex, int skillIndex)
		{
			SkillTreeNode skillTreeNode = this.data.skillTree.branches[branchIndex][skillIndex];
			int num = skillTreeNode.requiredHeroLevel - this.world.universalBonus.heroLevelRequiredForSkillDecrease - this.world.activeChallenge.totalGainedUpgrades.heroLevelRequiredForSkillDecrease;
			if (num > this.level)
			{
				return false;
			}
			int[] array = this.skillTreeProgressTotal.branches[branchIndex];
			for (int i = 0; i < skillIndex; i++)
			{
				if (array[i] < 0)
				{
					return false;
				}
			}
			return true;
		}

		private void RefreshSkillsAccordingToProgress()
		{
			this.RefreshSkillEnhancers();
			this.RefreshActiveAndPassiveSkills();
			this.RefreshPermanentBuffs();
		}

		private void RefreshSkillEnhancers()
		{
			SkillTree skillTree = this.data.skillTree;
			SkillTreeNode[][] branches = skillTree.branches;
			this.skillEnhancers.Clear();
			for (int i = branches.Length - 1; i >= 0; i--)
			{
				SkillTreeNode[] array = branches[i];
				int[] array2 = this.skillTreeProgressTotal.branches[i];
				for (int j = array.Length - 1; j >= 0; j--)
				{
					int num = array2[j];
					if (num >= 0)
					{
						SkillTreeNode skillTreeNode = array[j];
						if (skillTreeNode is SkillEnhancerBase)
						{
							SkillEnhancerBase enhancerBase = (SkillEnhancerBase)skillTreeNode;
							SkillEnhancer item = new SkillEnhancer(enhancerBase, num);
							this.skillEnhancers.Add(item);
						}
					}
				}
			}
		}

		private void RefreshActiveAndPassiveSkills()
		{
			SkillTree skillTree = this.data.skillTree;
			Dictionary<SkillActiveDataBase, float> dictionary = new Dictionary<SkillActiveDataBase, float>();
			for (int i = this.activeSkills.Count - 1; i >= 0; i--)
			{
				dictionary[this.activeSkills[i].dataBase] = this.skillCooldowns[i];
			}
			this.skillCooldowns.Clear();
			this.activeSkills.Clear();
			this.passiveSkills.Clear();
			SkillActiveDataBase skillActiveDataBase = (SkillActiveDataBase)skillTree.ulti;
			int ulti = this.skillTreeProgressTotal.ulti;
			SkillActiveData item = new SkillActiveData(skillActiveDataBase, this.skillEnhancers, ulti);
			this.activeSkills.Add(item);
			this.skillCooldowns.Add((!dictionary.ContainsKey(skillActiveDataBase)) ? 0f : dictionary[skillActiveDataBase]);
			for (int j = skillTree.branches.Length - 1; j >= 0; j--)
			{
				for (int k = skillTree.branches[j].Length - 1; k >= 0; k--)
				{
					int num = this.skillTreeProgressTotal.branches[j][k];
					if (num >= 0)
					{
						SkillTreeNode skillTreeNode = skillTree.branches[j][k];
						if (skillTreeNode.IsActive())
						{
							SkillActiveDataBase skillActiveDataBase2 = (SkillActiveDataBase)skillTreeNode;
							SkillActiveData item2 = new SkillActiveData(skillActiveDataBase2, this.skillEnhancers, num);
							this.activeSkills.Add(item2);
							this.skillCooldowns.Add((!dictionary.ContainsKey(skillActiveDataBase2)) ? 0f : dictionary[skillActiveDataBase2]);
						}
						else if (skillTreeNode.IsPassive())
						{
							SkillPassiveDataBase dataBase = (SkillPassiveDataBase)skillTreeNode;
							SkillPassiveData item3 = new SkillPassiveData(dataBase, this.skillEnhancers, num);
							this.passiveSkills.Add(item3);
						}
					}
				}
			}
		}

		public void RefreshPermanentBuffs()
		{
			Dictionary<int, int> dictionary;
			Dictionary<int, float> dictionary2;
			Dictionary<int, bool> dictionary3;
			base.ClearPermenantBuffs(out dictionary, out dictionary2, out dictionary3);
			foreach (SkillPassiveData skillPassiveData in this.passiveSkills)
			{
				BuffData passiveBuff = skillPassiveData.passiveBuff;
				int genericCounter = (!dictionary.ContainsKey(passiveBuff.id)) ? 0 : dictionary[passiveBuff.id];
				passiveBuff.genericTimer = ((!dictionary2.ContainsKey(passiveBuff.id)) ? 0f : dictionary2[passiveBuff.id]);
				passiveBuff.genericFlag = (dictionary3.ContainsKey(passiveBuff.id) && dictionary3[passiveBuff.id]);
				this.AddBuff(passiveBuff, genericCounter, false);
			}
			if (this.trinket != null)
			{
				foreach (TrinketEffect trinketEffect in this.trinket.effects)
				{
					trinketEffect.Apply(this, dictionary, dictionary2, dictionary3);
				}
			}
		}

		public bool CanUpgradeSkillUlti()
		{
			return this.numUnspentSkillPoints != 0 && !this.HasSkillUltiReachedMaxLevel();
		}

		public bool HasSkillUltiReachedMaxLevel()
		{
			SkillTreeNode ulti = this.data.skillTree.ulti;
			int ulti2 = this.skillTreeProgressGained.ulti;
			return ulti.HasReachedMaxLevel(ulti2);
		}

		public bool HasAllSkillsReachedMaxLevel()
		{
			if (!this.HasSkillUltiReachedMaxLevel())
			{
				return false;
			}
			for (int i = 0; i < this.GetSkillBranchLength(0); i++)
			{
				if (!this.HasSkillReachedMaxLevel(0, i))
				{
					return false;
				}
			}
			for (int j = 0; j < this.GetSkillBranchLength(1); j++)
			{
				if (!this.HasSkillReachedMaxLevel(1, j))
				{
					return false;
				}
			}
			return true;
		}

		public bool CanUpgradeSkill(int branchIndex, int skillIndex)
		{
			return this.numUnspentSkillPoints != 0 && !this.HasSkillReachedMaxLevel(branchIndex, skillIndex) && this.CanSeeSkillInSkillScreen(branchIndex, skillIndex);
		}

		public bool HasSkillReachedMaxLevel(int branchIndex, int skillIndex)
		{
			SkillTreeNode skillTreeNode = this.data.skillTree.branches[branchIndex][skillIndex];
			int num = this.skillTreeProgressGained.branches[branchIndex][skillIndex];
			return skillTreeNode.HasReachedMaxLevel(num);
		}

		public int GetSkillBranchLength(int branchIndex)
		{
			return this.data.skillTree.branches[branchIndex].Length;
		}

		public bool CanSeeSkillInSkillScreen(int branchIndex, int skillIndex)
		{
			SkillTreeNode skillTreeNode = this.data.skillTree.branches[branchIndex][skillIndex];
			int num = skillTreeNode.requiredHeroLevel - this.world.universalBonus.heroLevelRequiredForSkillDecrease - this.world.activeChallenge.totalGainedUpgrades.heroLevelRequiredForSkillDecrease;
			if (num > this.level)
			{
				return false;
			}
			int[] array = this.skillTreeProgressTotal.branches[branchIndex];
			for (int i = 0; i < skillIndex; i++)
			{
				if (array[i] < 0)
				{
					return false;
				}
			}
			HeroDataBase dataBase = this.data.GetDataBase();
			if (dataBase.skillBranchesEverUnlocked[branchIndex] < skillIndex)
			{
				dataBase.skillBranchesEverUnlocked[branchIndex] = skillIndex;
			}
			return true;
		}

		public void TryUpgradeSkillUlti()
		{
			if (this.CanUpgradeSkillUlti())
			{
				this.numUnspentSkillPoints--;
				this.skillTreeProgressGained.ulti++;
				this.skillTreeProgressTotal.ulti++;
				int ulti = this.skillTreeProgressTotal.ulti;
				SkillActiveData skillActiveData = this.activeSkills[0];
				skillActiveData.SetLevel(ulti, this.skillEnhancers);
				TutorialManager.UpgradedSkill();
			}
		}

		public void TryUpgradeSkill(int branchIndex, int skillIndex)
		{
			if (this.CanUpgradeSkill(branchIndex, skillIndex))
			{
				this.numUnspentSkillPoints--;
				SkillTreeNode node = this.data.skillTree.branches[branchIndex][skillIndex];
				this.skillTreeProgressGained.branches[branchIndex][skillIndex]++;
				this.skillTreeProgressTotal.branches[branchIndex][skillIndex]++;
				int newLevel = this.skillTreeProgressTotal.branches[branchIndex][skillIndex];
				this.SetSkillTreeNodeLevel(node, newLevel);
				int num = this.skillTreeProgressTotal.branches[branchIndex].Length;
				for (int i = skillIndex + 1; i < num; i++)
				{
					if (!this.IsSkillUpgradableByGear(branchIndex, i))
					{
						break;
					}
					if (this.skillTreeProgressTotal.branches[branchIndex][i] >= 0)
					{
						break;
					}
					if (this.skillTreeProgressGeared.branches[branchIndex][i] == 0)
					{
						break;
					}
					int num2 = this.skillTreeProgressGeared.branches[branchIndex][i] - 1;
					this.skillTreeProgressTotal.branches[branchIndex][i] = num2;
					SkillTreeNode node2 = this.data.skillTree.branches[branchIndex][i];
					this.SetSkillTreeNodeLevel(node2, num2);
				}
				TutorialManager.UpgradedSkill();
			}
		}

		private void SetSkillTreeNodeLevel(SkillTreeNode node, int newLevel)
		{
			if (newLevel < 0)
			{
				throw new ArgumentException();
			}
			if (!this.IsSkillTreeNodeUnlocked(node))
			{
				this.UnlockSkillNode(node, newLevel);
				return;
			}
			SkillData skillData = this.GetSkillData(node);
			skillData.SetLevel(newLevel, this.skillEnhancers);
			if (node.IsEnhancer())
			{
				this.OnChangedSkillEnhancer();
			}
			else if (node.IsPassive())
			{
				this.RefreshPermanentBuffs();
			}
		}

		public SkillData GetSkillData(SkillTreeNode nodeSkill)
		{
			if (nodeSkill.IsActive())
			{
				return this.GetSkillDataActive(nodeSkill);
			}
			if (nodeSkill.IsPassive())
			{
				return this.GetSkillDataPassive(nodeSkill);
			}
			if (nodeSkill.IsEnhancer())
			{
				return this.GetSkillEnhancer(nodeSkill);
			}
			throw new ArgumentException();
		}

		private bool IsSkillTreeNodeUnlocked(SkillTreeNode node)
		{
			if (node is SkillEnhancerBase)
			{
				foreach (SkillEnhancer skillEnhancer in this.skillEnhancers)
				{
					if (node == skillEnhancer.enhancerBase)
					{
						return true;
					}
				}
				return false;
			}
			if (node.IsActive())
			{
				foreach (SkillActiveData skillActiveData in this.activeSkills)
				{
					if (node == skillActiveData.dataBase)
					{
						return true;
					}
				}
				return false;
			}
			if (node.IsPassive())
			{
				foreach (SkillPassiveData skillPassiveData in this.passiveSkills)
				{
					if (node == skillPassiveData.dataBase)
					{
						return true;
					}
				}
				return false;
			}
			if (node.IsEnhancer())
			{
			}
			throw new ArgumentException();
		}

		private void UnlockSkillNode(SkillTreeNode node, int newLevel)
		{
			if (node.IsActive())
			{
				SkillActiveDataBase dataBase = (SkillActiveDataBase)node;
				SkillActiveData item = new SkillActiveData(dataBase, this.skillEnhancers, newLevel);
				this.activeSkills.Add(item);
				float item2 = (this.activeSkills.Count - 1 != 0) ? 10f : 0f;
				this.skillCooldowns.Add(item2);
			}
			else if (node.IsPassive())
			{
				SkillPassiveDataBase dataBase2 = (SkillPassiveDataBase)node;
				SkillPassiveData skillPassiveData = new SkillPassiveData(dataBase2, this.skillEnhancers, newLevel);
				this.passiveSkills.Add(skillPassiveData);
				this.AddBuff(skillPassiveData.passiveBuff, 0, false);
			}
			else
			{
				if (!node.IsEnhancer())
				{
					throw new ArgumentException();
				}
				SkillEnhancerBase enhancerBase = (SkillEnhancerBase)node;
				SkillEnhancer item3 = new SkillEnhancer(enhancerBase, newLevel);
				this.skillEnhancers.Add(item3);
				this.OnChangedSkillEnhancer();
			}
		}

		private bool IsSkillFromRightNode(SkillTreeNode node)
		{
			return this.data.skillTree.branches[0][0] == node;
		}

		public bool IsSkillNodeMaxMinusOne(int branchIndex, int skillIndex, int itemLevel)
		{
			SkillData skillData;
			SkillTreeNode skillTreeNode;
			if (branchIndex == -1)
			{
				skillData = this.activeSkills[0];
				skillTreeNode = this.activeSkills[0].dataBase;
			}
			else
			{
				skillTreeNode = this.data.skillTree.branches[branchIndex][skillIndex];
				skillData = this.GetSkillData(skillTreeNode);
			}
			return skillData != null && skillData.level == skillTreeNode.maxLevel + itemLevel - 1;
		}

		private SkillEnhancer GetSkillEnhancer(SkillTreeNode enhancerBase)
		{
			foreach (SkillEnhancer skillEnhancer in this.skillEnhancers)
			{
				if (skillEnhancer.enhancerBase == enhancerBase)
				{
					return skillEnhancer;
				}
			}
			return null;
		}

		private SkillPassiveData GetSkillDataPassive(SkillTreeNode node)
		{
			foreach (SkillPassiveData skillPassiveData in this.passiveSkills)
			{
				if (skillPassiveData.dataBase == node)
				{
					return skillPassiveData;
				}
			}
			return null;
		}

		private SkillActiveData GetSkillDataActive(SkillTreeNode node)
		{
			foreach (SkillActiveData skillActiveData in this.activeSkills)
			{
				if (skillActiveData.dataBase == node)
				{
					return skillActiveData;
				}
			}
			return null;
		}

		private int GetSkillDataActiveIndex(SkillTreeNode node)
		{
			int num = 0;
			foreach (SkillActiveData skillActiveData in this.activeSkills)
			{
				if (skillActiveData.dataBase == node)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		private void OnChangedSkillEnhancer()
		{
			foreach (SkillData skillData in this.activeSkills)
			{
				skillData.SetLevel(skillData.level, this.skillEnhancers);
			}
			foreach (SkillData skillData2 in this.passiveSkills)
			{
				skillData2.SetLevel(skillData2.level, this.skillEnhancers);
			}
			this.RefreshPermanentBuffs();
		}

		public string GetSkillDescUlti()
		{
			int ulti = this.skillTreeProgressGained.ulti;
			int maxLevel = this.data.skillTree.ulti.maxLevel;
			if (ulti == maxLevel)
			{
				return this.activeSkills[0].GetDescFull();
			}
			return this.activeSkills[0].GetDesc();
		}

		public string GetSkillDesc(int branchIndex, int skillIndex)
		{
			SkillTreeNode skillTreeNode = this.data.skillTree.branches[branchIndex][skillIndex];
			int num = this.skillTreeProgressGained.branches[branchIndex][skillIndex];
			int maxLevel = skillTreeNode.maxLevel;
			bool flag = num == maxLevel;
			if (this.IsSkillTreeNodeUnlocked(skillTreeNode))
			{
				SkillData skillData = this.GetSkillData(skillTreeNode);
				return (!flag) ? skillData.GetDesc() : skillData.GetDescFull();
			}
			return skillTreeNode.GetDescZero();
		}

		public int GetSkillCooldownIndex(Type skillDataBaseType)
		{
			for (int i = this.activeSkills.Count - 1; i >= 0; i--)
			{
				if (this.activeSkills[i].dataBase.GetType() == skillDataBaseType)
				{
					return i;
				}
			}
			return -1;
		}

		public void DecreaseSkillCooldown(Type skillDataBaseType, float time)
		{
			for (int i = this.activeSkills.Count - 1; i >= 0; i--)
			{
				if (this.activeSkills[i].dataBase.GetType() == skillDataBaseType)
				{
					this.skillCooldowns[i] = GameMath.GetMaxFloat(0f, this.skillCooldowns[i] - time);
				}
			}
		}

		public void DecreaseAllSkillCooldowns(float time)
		{
			SkillActiveData skillActiveData = (this.runningSkill != null) ? this.runningSkill.data : null;
			for (int i = this.activeSkills.Count - 1; i >= 0; i--)
			{
				if (this.activeSkills[i] != skillActiveData)
				{
					this.skillCooldowns[i] = GameMath.GetMaxFloat(0f, this.skillCooldowns[i] - time);
				}
			}
		}

		public void ZeroSkillCooldown(Type skillDataBaseType)
		{
			for (int i = this.activeSkills.Count - 1; i >= 0; i--)
			{
				if (this.activeSkills[i].dataBase.GetType() == skillDataBaseType)
				{
					this.skillCooldowns[i] = 0f;
				}
			}
		}

		public void ZeroUltiCooldown()
		{
			this.skillCooldowns[0] = 0f;
		}

		public void ZeroSkillCooldowns()
		{
			for (int i = this.skillCooldowns.Count - 1; i >= 0; i--)
			{
				this.skillCooldowns[i] = 0f;
			}
		}

		public override void CoolWeapon(float coolRatio)
		{
			this.weapon.Cool(coolRatio);
		}

		public override void AttackImmediately(UnitHealthy unit)
		{
			this.weapon.AttackImmediately(unit);
		}

		public override void CancelCurrentOverheat()
		{
			this.weapon.CancelCurrentOverheat();
		}

		public SkillTree GetSkillTree()
		{
			return this.data.skillTree;
		}

		public SkillTreeProgress GetSkillTreeProgress()
		{
			return this.skillTreeProgressTotal;
		}

		public SkillTreeProgress GetSkillTreeProgressGained()
		{
			return this.skillTreeProgressGained;
		}

		public void SetSkillTreeProgressGained(SkillTreeProgress newSkillTreeProgressGained)
		{
			for (int i = 0; i < newSkillTreeProgressGained.branches.Length; i++)
			{
				for (int j = 0; j < newSkillTreeProgressGained.branches[i].Length; j++)
				{
					int num = newSkillTreeProgressGained.branches[i][j];
					int maxLevel = this.data.skillTree.branches[i][j].maxLevel;
					if (num > maxLevel)
					{
						newSkillTreeProgressGained.branches[i][j] = maxLevel;
					}
				}
			}
			this.skillTreeProgressGained = newSkillTreeProgressGained;
		}

		public bool CanActivateSkill(int skillIndex)
		{
			return (this.activeSkills[skillIndex].isToggle && this.IsRunningSkill(skillIndex)) || this.skillCooldowns[skillIndex] <= 0f;
		}

		private bool IsRunningSkill(int skillIndex)
		{
			return this.runningSkill != null && this.runningSkill.data.dataBase == this.activeSkills[skillIndex].dataBase;
		}

		public bool IsSkillActive(int skillIndex)
		{
			return this.runningSkill != null && this.runningSkill.GetDataBase() == this.activeSkills[skillIndex].dataBase;
		}

		public bool IsSkillTogglable(int skillIndex)
		{
			return this.activeSkills[skillIndex].isToggle;
		}

		public float GetToggleDelta(int skillIndex)
		{
			return (this.runningSkill == null || this.runningSkill.data != this.activeSkills[skillIndex]) ? 0f : this.runningSkill.toggleOffTimeDelta;
		}

		public bool IsSkillToggling(int skillIndex)
		{
			return this.runningSkill != null && this.runningSkill.IsTogglingOff();
		}

		public float GetMainSkillCooldownTimeRatio()
		{
			return this.skillCooldowns[0] / this.GetMainSkillCooldownMax();
		}

		public float GetAutoSkillCooldownTimeRatio(int index)
		{
			return this.skillCooldowns[index + 1] / this.GetAutoActiveSkillCooldownMax(index);
		}

		public int GetNumAutoActiveSkills()
		{
			return this.activeSkills.Count - 1;
		}

		public float GetRightSideAutoAtiveColldownRate()
		{
			if (this.skillTreeProgressTotal.branches[1][0] < 0)
			{
				return float.PositiveInfinity;
			}
			SkillTreeNode node = this.data.skillTree.branches[1][0];
			int skillDataActiveIndex = this.GetSkillDataActiveIndex(node);
			return this.skillCooldowns[skillDataActiveIndex] / this.GetSkillCoolDownMax(skillDataActiveIndex);
		}

		public float GetLeftSideAutoAtiveColldownRate()
		{
			if (this.skillTreeProgressTotal.branches[0][0] < 0)
			{
				return float.PositiveInfinity;
			}
			SkillTreeNode node = this.data.skillTree.branches[0][0];
			int skillDataActiveIndex = this.GetSkillDataActiveIndex(node);
			return this.skillCooldowns[skillDataActiveIndex] / this.GetSkillCoolDownMax(skillDataActiveIndex);
		}

		public float GetAutoActiveSkillCoolDownTimeRatio(int autoActiveSkillIndex)
		{
			int skillIndex = autoActiveSkillIndex + 1;
			return this.GetAutoActiveSkillCooldown(autoActiveSkillIndex) / this.GetSkillCoolDownMax(skillIndex);
		}

		private float GetAutoActiveSkillCooldown(int autoActiveSkillIndex)
		{
			int num = autoActiveSkillIndex + 1;
			if (num >= this.skillCooldowns.Count)
			{
				return float.PositiveInfinity;
			}
			return this.skillCooldowns[num];
		}

		public float GetMainSkillCooldownMax()
		{
			return this.GetSkillCoolDownMax(0);
		}

		public float GetAutoActiveSkillCooldownMax(int autoActiveSkillIndex)
		{
			int skillIndex = autoActiveSkillIndex + 1;
			return this.GetSkillCoolDownMax(skillIndex);
		}

		public float GetSkillCoolDownMax(int skillIndex)
		{
			if (this.activeSkills.Count - 1 < skillIndex)
			{
				return 0f;
			}
			SkillActiveData skillActiveData = this.activeSkills[skillIndex];
			float num = skillActiveData.cooldownMax;
			if (skillIndex == 0)
			{
				num *= this.world.universalBonus.ultiCoolDownMaxFactor;
				num *= this.world.buffTotalEffect.heroUltiCooldownMaxFactor;
			}
			return num;
		}

		public Type GetUltiSkillType()
		{
			return this.GetSkillType(0);
		}

		public Type GetSkillType(int skillIndex)
		{
			return this.activeSkills[skillIndex].dataBase.GetType();
		}

		public float GetTillReviveTime()
		{
			if (!this.IsDead())
			{
				return 0f;
			}
			return this.GetReviveDuration() - this.inStateTimeCounter;
		}

		public void SetTillReviveTime(float tillReviveTime)
		{
			if (tillReviveTime > 0f)
			{
				this.state = Hero.State.DEAD;
				this.inStateTimeCounter = this.GetReviveDuration() - tillReviveTime;
			}
		}

		public float GetReviveDuration()
		{
			return this.statCache.reviveDur;
		}

		public bool HasMainSkillAvailable()
		{
			return !this.IsDead() && this.skillCooldowns[0] == 0f;
		}

		public void TryActivateSkill(int skillIndex)
		{
			if (this.IsDead())
			{
				return;
			}
			if (this.skillCooldowns[skillIndex] > 0f)
			{
				if (this.IsRunningSkill(skillIndex) && this.runningSkill.data.isToggle && !this.runningSkill.IsTogglingOff())
				{
					this.runningSkill.ToggleOff();
					this.runningSkill.StopSound(this.world);
				}
			}
			else
			{
				this.ActivateSkill(skillIndex);
			}
		}

		public void TryActivateMainSkill()
		{
			this.TryActivateSkill(0);
		}

		public override void DamageAll(Damage damage)
		{
			foreach (Enemy damaged in this.world.activeChallenge.enemies)
			{
				this.world.DamageMain(this, damaged, damage.GetCopy());
			}
		}

		public override IEnumerable<UnitHealthy> GetAllies()
		{
			foreach (Hero hero in this.world.heroes)
			{
				yield return hero;
			}
			yield break;
		}

		public IEnumerable<Hero> GetHeroAllies()
		{
			foreach (Hero hero in this.world.heroes)
			{
				yield return hero;
			}
			yield break;
		}

		public override IEnumerable<UnitHealthy> GetOpponents()
		{
			foreach (Enemy enemy in this.world.activeChallenge.enemies)
			{
				yield return enemy;
			}
			yield break;
		}

		public string GetNameKey()
		{
			return this.data.nameKey;
		}

		public Sound soundDeath
		{
			get
			{
				return this.data.soundDeath;
			}
		}

		public Sound soundRevive
		{
			get
			{
				return this.data.soundRevive;
			}
		}

		public Sound soundVoDeath
		{
			get
			{
				return this.data.soundVoDeath;
			}
		}

		public Sound soundVoRevive
		{
			get
			{
				return this.data.soundVoRevive;
			}
		}

		public Sound soundVoSpawn
		{
			get
			{
				return this.data.soundVoSpawn;
			}
		}

		public Sound soundVoLevelUp
		{
			get
			{
				return this.data.soundVoLevelUp;
			}
		}

		public AudioClipPromise[] soundVoItem
		{
			get
			{
				return this.data.soundVoItem;
			}
		}

		public Sound soundVoWelcome
		{
			get
			{
				return this.data.soundVoWelcome;
			}
		}

		public Sound soundVoEnvChange
		{
			get
			{
				return this.data.soundVoEnvChange;
			}
		}

		public AudioClipPromise[] soundVoCheer
		{
			get
			{
				return this.data.soundVoCheer;
			}
		}

		public bool IsReviving()
		{
			return this.IsDead() && this.GetTillReviveTime() < 0.65f;
		}

		public bool IsDead()
		{
			return this.state == Hero.State.DEAD;
		}

		public bool IsDeadForGood()
		{
			return this.IsDead() && !this.world.activeChallenge.heroesCanRevive;
		}

		public bool IsIdle()
		{
			return this.state == Hero.State.IDLE || this.state == Hero.State.STUN;
		}

		public bool IsStunned()
		{
			return this.state == Hero.State.STUN;
		}

		public bool IsChangingWeaponToTemp()
		{
			return this.state == Hero.State.WEAPON_CHANGE_TO_TEMP;
		}

		public bool IsChangingWeaponToOrig()
		{
			return this.state == Hero.State.WEAPON_CHANGE_TO_ORIG;
		}

		public bool IsUsingTempWeapon()
		{
			return this.weapon.id != this.data.weapon.id;
		}

		public bool IsAttacking()
		{
			return this.state == Hero.State.ATTACK;
		}

		public int GetNumHits()
		{
			return this.weapon.GetNumHits();
		}

		public bool IsUsingSkill()
		{
			return this.state == Hero.State.SKILL;
		}

		public SkillActiveDataBase GetRunningSkillDataBase()
		{
			return this.runningSkill.GetDataBase();
		}

		public SkillActiveDataBase TryGetRunningSkillDataBase()
		{
			if (this.runningSkill == null)
			{
				return null;
			}
			return this.runningSkill.GetDataBase();
		}

		public int GetRunningSkillAnimIndex()
		{
			return this.runningSkill.GetAnimIndex();
		}

		public float GetRunningSkillCurAnimTimeRatio()
		{
			return this.runningSkill.GetCurAnimTimeRatio();
		}

		public float GetRunningSkillCurAnimDur()
		{
			return this.runningSkill.GetCurAnimDur();
		}

		public float GetRunningSkillTime()
		{
			return this.runningSkill.GetTime();
		}

		public float GetActiveSkillDuration()
		{
			return this.runningSkill.data.dur;
		}

		public float GetAttackBarTimeRatio()
		{
			return this.weapon.GetBarTimeRatio();
		}

		public float GetAttackAnimTimeRatio()
		{
			return this.weapon.GetAnimTimeRatio();
		}

		public bool IsOverheated()
		{
			return this.weapon.IsOverheated();
		}

		public float GetOverheatTimeLeft()
		{
			return this.weapon.GetOverheatTimeLeft();
		}

		public bool IsReloading()
		{
			return this.weapon.IsReloading();
		}

		public float GetReloadTimeRatio()
		{
			return this.weapon.GetReloadTimeRatio();
		}

		public bool HasDataBase(HeroDataBase dataBase)
		{
			return this.data.IsSameDataBase(dataBase);
		}

		public List<float> GetSkillCooldowns()
		{
			return this.skillCooldowns;
		}

		public void LoadSkillCooldowns(List<float> newSkillCooldowns)
		{
			int count = this.skillCooldowns.Count;
			int count2 = newSkillCooldowns.Count;
			int minInt = GameMath.GetMinInt(count, count2);
			for (int i = 0; i < minInt; i++)
			{
				this.skillCooldowns[i] = newSkillCooldowns[i];
			}
			for (int j = count2; j < count; j++)
			{
				this.skillCooldowns[j] = this.GetSkillCoolDownMax(j) * GameMath.GetRandomFloat(0.2f, 1f, GameMath.RandType.NoSeed);
			}
		}

		public float GetUltiCooldown()
		{
			return this.skillCooldowns[0];
		}

		public void DecreaseUltiCooldown(float timeDecrement)
		{
			this.skillCooldowns[0] = GameMath.GetMaxFloat(0f, this.skillCooldowns[0] - timeDecrement);
		}

		public void DecreaseAllCooldowns(float timeDecrement)
		{
			this.DecreaseSkillCooldowns(timeDecrement);
		}

		public void DecreaseDeadWaitTime(float timeDecrement)
		{
			if (this.IsDead())
			{
				this.inStateTimeCounter += timeDecrement;
			}
		}

		public void DecreaseSkillCooldowns(float timeDecrement)
		{
			for (int i = this.skillCooldowns.Count - 1; i >= 0; i--)
			{
				this.skillCooldowns[i] = GameMath.GetMaxFloat(0f, this.skillCooldowns[i] - timeDecrement);
			}
		}

		public HeroData GetData()
		{
			return this.data;
		}

		public HeroClass GetHeroClass()
		{
			return this.data.GetDataBase().heroClass;
		}

		public override bool IsOnWorld()
		{
			foreach (Unit unit in this.world.heroes)
			{
				if (unit == this)
				{
					return true;
				}
			}
			return false;
		}

		public void SetDuplicateIndex(int val)
		{
			this.duplicateIndex = val;
		}

		public bool IsDuplicate()
		{
			return this.duplicateIndex >= 0;
		}

		public bool IsUpgradeCostMin()
		{
			return false;
		}

		public override void OnDeathAlly(UnitHealthy dead)
		{
			base.OnDeathAlly(dead);
			if (this.GetId() == "JIM" && base.IsAlive())
			{
				this.world.OnJimAllyDeath();
			}
		}

		public override void OnKilled(UnitHealthy killed)
		{
			if (this.GetId() == "KIND_LENNY" && killed != null && killed.HasBuffStun())
			{
				this.world.OnLennyKillStunned();
			}
			if (this.GetId() == "TAM" && killed != null && killed.statCache.missChance > 0f)
			{
				this.world.OnTamKillBlind();
			}
			base.OnKilled(killed);
		}

		public override bool AddBuff(BuffData buffData, int genericCounter = 0, bool lateAdd = false)
		{
			if (!base.AddBuff(buffData, genericCounter, lateAdd))
			{
				return false;
			}
			if ((buffData.visuals & 64) != 0 && this.GetId() == "DEREK")
			{
				this.world.OnWendleHealed();
			}
			else if (buffData.tag == BuffTags.LENNY_HEAL && this.GetId() != "KIND_LENNY")
			{
				this.world.OnLennyHealAlly();
			}
			return true;
		}

		public override void OnPostDamage(UnitHealthy damaged, Damage damage)
		{
			base.OnPostDamage(damaged, damage);
			if (damage.isCrit)
			{
				if (this.GetId() == "HORATIO")
				{
					this.world.OnHiltCrit();
				}
				else if (this.GetId() == "DRUID")
				{
					this.world.dailyQuestRonLandedCritHit++;
				}
			}
		}

		private List<Gear> gears;

		public const int LEVEL_CAP = 90;

		private Hero.State state;

		public double costMultiplier = 1.0;

		public bool isToChangeToOrigWeapon;

		public float durWeaponChange;

		private Weapon weapon;

		private HeroData data;

		public int[] levelJumps;

		private int level;

		private int xp;

		private int numUnspentSkillPoints;

		private SkillTreeProgress skillTreeProgressGained;

		private SkillTreeProgress skillTreeProgressGeared;

		private SkillTreeProgress skillTreeProgressTotal;

		private Skill runningSkill;

		private List<float> skillCooldowns;

		private float blackCurtainTimeCounter;

		private float bcTimeFadeInStart;

		private float bcTimeFadeInEnd;

		private float bcTimeFadeOutStart;

		private float bcTimeFadeOutEnd;

		private float bcDurStayInFront;

		private float bcDurNonslowable;

		private List<SkillActiveData> activeSkills;

		private List<SkillPassiveData> passiveSkills;

		private List<SkillEnhancer> skillEnhancers;

		private BuffDataInvulnerability duringSkillInvulnerability;

		public bool nextUpgradeFree;

		private int duplicateIndex;

		public int idleChanger;

		public bool needsInitialization;

		public bool canBeRendered;

		public Skill castedSkillDuringDeath;

		public float heightOffset;

		public enum State
		{
			IDLE,
			WEAPON_CHANGE_TO_TEMP,
			WEAPON_CHANGE_TO_ORIG,
			ATTACK,
			SKILL,
			DEAD,
			STUN
		}
	}
}
