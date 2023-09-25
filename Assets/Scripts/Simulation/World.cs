using System;
using System.Collections.Generic;
using Render;
using Static;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class World
	{
		public int GetRequiredXpToLevelHero(int level)
		{
			return HeroDataBase.LEVEL_XPS[level];
		}

		public int GetRequiredXpToLevelRing(int level)
		{
			return TotemDataBase.LEVEL_XPS[level];
		}

		public void Init(UniversalTotalBonus universalBonus, GameMode gameMode, Challenge newActiveChallenge, Unlock unlockMode)
		{

			this.gameMode = gameMode;
			this.universalBonus = universalBonus;
			this.unlockMode = unlockMode;
			this.givenSkillPoints = 0;
			this.idleDuration = 0f;
			this.pastDamageTimer = 0f;
			this.numPastDamages = 0;
			this.highestDamageBefore = 0.0;
			this.autoUpgradeTimer = 0f;
			this.offlineGold = 0.0;
			this.skippedStageNo = -1;
			this.timeWarpTimeLeft = 0f;
			this.autoTapTimeLeft = 0f;
			this.canProgressStage = true;
			this.isRainingGlory = false;
			this.isRainingDuck = false;
			this.doTransition = false;
			this.isTransitioning = false;
			this.willEnd = false;
			this.maxStageReached = 0;
			this.maxHeroLevelReached = 0;
			this.unlocks = new List<Unlock>();
			this.earnedMythstone = 0.0;
			this.gold = new Currency(CurrencyType.GOLD);
			this.activeChallenge = newActiveChallenge;
			this.activeChallenge.Reset();
			this.totem = null;
			this.heroes = new List<Hero>();

			this.projectiles = new List<Projectile>();

            this.swarmDragons = new List<SwarmDragon>();
			this.stampedes = new List<Stampede>();
			this.supportAnimals = new List<SupportAnimal>();
			this.treasureDrops = new List<TreasureDrop>();
			this.drops = new List<Drop>();
			this.futureDamages = new List<FutureDamage>();
			this.currencyDragons = new List<CurrencyDragon>();
			this.pastDamages = new Queue<GlobalPastDamage>();
			this.pastHeals = new Queue<GlobalPastHeal>();
			this.visualEffects = new List<VisualEffect>();
			this.sounds = new List<SoundEvent>();
			this.visualLinedEffects = new List<VisualLinedEffect>();
			this.buffTotalEffect = new BuffTotalWorldEffect();
			this.buffTotalEffect.Init();
			this.selfTaps = new List<Vector3>();
			this.merchantItemsToEvaluate = new List<MerchantItem>();
			this.InitData();
			this.InitMerchantItems();
			foreach (MerchantItem merchantItem in this.merchantItems)
			{
				merchantItem.SetNumUsed(0);
			}
			this.adDragonState = World.AdDragonState.NONEXISTANCE;
			this.adDragonWait = GameMath.GetRandomFloat(this.adDragonWaitMin, this.adDragonWaitMax, GameMath.RandType.NoSeed);
			this.adDragonTimeCounter = 0f;
			this.adDragonPos = new Vector3(-0.7f, 0.6f, 0f);
			this.adDragonDir = 1f;
		}

		private void InitData()
		{
			this.adDragonReqMaxStage = 0;
			if (this.gameMode == GameMode.STANDARD)
			{
				this.adDragonReqMaxStage = 5;
			}
			this.newHeroCosts = new double[]
			{
				50.0,
				5000.0,
				100000.0,
				20000000.0,
				1000000000.0,
				double.PositiveInfinity
			};
			this.heroLevelJumps = new int[5][];
			if (this.gameMode == GameMode.STANDARD)
			{
				for (int i = 0; i < 5; i++)
				{
					this.heroLevelJumps[i] = new int[91];
					this.heroLevelJumps[i][0] = 0;
					this.heroLevelJumps[i][1] = HeroDataBase.LEVEL_XPS[0] + 5;
					for (int j = 2; j < 91; j++)
					{
						int num = this.heroLevelJumps[i][j - 1] + HeroDataBase.LEVEL_XPS[j] + 5;
						this.heroLevelJumps[i][j] = num + Mathf.FloorToInt(Mathf.Pow((float)j, 0.2f));
					}
				}
			}
			else if (this.gameMode == GameMode.CRUSADE)
			{
				for (int k = 0; k < 5; k++)
				{
					this.heroLevelJumps[k] = new int[91];
					this.heroLevelJumps[k][0] = 0;
					this.heroLevelJumps[k][1] = HeroDataBase.LEVEL_XPS[0] + 5;
					for (int l = 2; l < 91; l++)
					{
						int num2 = this.heroLevelJumps[k][l - 1] + HeroDataBase.LEVEL_XPS[l] + 5;
						this.heroLevelJumps[k][l] = num2 + Mathf.FloorToInt((float)l);
					}
				}
			}
			else
			{
				if (this.gameMode != GameMode.RIFT)
				{
					throw new NotImplementedException();
				}
				for (int m = 0; m < 5; m++)
				{
					this.heroLevelJumps[m] = new int[91];
					this.heroLevelJumps[m][0] = 0;
					this.heroLevelJumps[m][1] = HeroDataBase.LEVEL_XPS[0] + 5;
					for (int n = 2; n < 91; n++)
					{
						int num3 = this.heroLevelJumps[m][n - 1] + HeroDataBase.LEVEL_XPS[n] + 5;
						this.heroLevelJumps[m][n] = num3 + Mathf.FloorToInt(Mathf.Pow((float)n, 0.5f));
					}
				}
			}
			this.totemLevelJumps = new int[91];
			this.totemLevelJumps[0] = 0;
			this.totemLevelJumps[1] = TotemDataBase.LEVEL_XPS[0] + 5;
			for (int num4 = 2; num4 < 91; num4++)
			{
				int num5 = this.totemLevelJumps[num4 - 1] + TotemDataBase.LEVEL_XPS[num4] + 5;
				this.totemLevelJumps[num4] = num5 + Mathf.FloorToInt(Mathf.Pow((float)num4, 0.2f));
			}
			this.heroPoses = new Vector3[]
			{
				new Vector3(-0.65f, -0.15f),
				new Vector3(-0.4f, 0.1f),
				new Vector3(-0.4f, -0.41f),
				new Vector3(-0.735f, -0.55f),
				new Vector3(-0.735f, 0.25f)
			};
			this.enemyPoses = new Vector3[][]
			{
				new Vector3[]
				{
					new Vector3(0.43f, -0.15f, 0f)
				},
				new Vector3[]
				{
					new Vector3(0.4f, 0.1f, 0f),
					new Vector3(0.4f, -0.41f, 0f)
				},
				new Vector3[]
				{
					new Vector3(0.65f, -0.15f, 0f),
					new Vector3(0.4f, 0.1f, 0f),
					new Vector3(0.4f, -0.4f, 0f)
				},
				new Vector3[]
				{
					new Vector3(0.4f, 0.1f, 0f),
					new Vector3(0.4f, -0.4f, 0f),
					new Vector3(0.735f, -0.55f, 0f),
					new Vector3(0.735f, 0.25f, 0f)
				},
				new Vector3[]
				{
					new Vector3(0.65f, -0.15f, 0f),
					new Vector3(0.4f, 0.1f, 0f),
					new Vector3(0.4f, -0.4f, 0f),
					new Vector3(0.735f, -0.55f, 0f),
					new Vector3(0.735f, 0.25f, 0f)
				},
				new Vector3[]
				{
					new Vector3(0.5f, 0.2f, 0f),
					new Vector3(0.5f, -0.5f, 0f),
					new Vector3(0.72f, -0.03f, 0f),
					new Vector3(0.72f, -0.27f, 0f),
					new Vector3(0.28f, -0.03f, 0f),
					new Vector3(0.28f, -0.27f, 0f)
				}
			};
			int num6 = 0;
			int num7 = this.heroPoses.Length;
			while (num6 < num7)
			{
				Vector3[] array = this.heroPoses;
				int num8 = num6;
				array[num8].x = array[num8].x + AspectRatioOffset.HERO_X;
				num6++;
			}
			int num9 = 0;
			int num10 = this.enemyPoses.Length;
			while (num9 < num10)
			{
				int num11 = 0;
				int num12 = this.enemyPoses[num9].Length;
				while (num11 < num12)
				{
					Vector3[] array2 = this.enemyPoses[num9];
					int num13 = num11;
					array2[num13].x = array2[num13].x + AspectRatioOffset.ENEMY_X;
					num11++;
				}
				num9++;
			}
		}

		public void OnPreAttack(Hero by, Damage newDamage, Projectile projectile)
		{
			if (this.activeChallenge is ChallengeRift)
			{
				ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
				foreach (RiftEffect riftEffect in challengeRift.riftEffects)
				{
					riftEffect.OnPreAttack(by, newDamage, projectile);
				}
			}
		}

		public void OnWeaponUsed(Hero by)
		{
			if (this.activeChallenge is ChallengeRift)
			{
				ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
				foreach (EnchantmentBuff enchantmentBuff in challengeRift.allEnchantments)
				{
					enchantmentBuff.OnWeaponUsed(by);
				}
			}
		}

		public bool IsModeUnlocked()
		{
			return this.unlockMode == null || this.unlockMode.isCollected;
		}

		public void SetModeLockedIfPossible()
		{
			if (this.unlockMode != null)
			{
				this.unlockMode.isCollected = false;
			}
		}

		private void InitMerchantItems()
		{
			if (this.gameMode == GameMode.STANDARD)
			{
				MerchantItemGoldBag merchantItemGoldBag = new MerchantItemGoldBag();
				merchantItemGoldBag.SetNumUsed(0);
				merchantItemGoldBag.SetNumMaxBase(MerchantItemGoldBag.BASE_COUNT);
				merchantItemGoldBag.SetPrice((double)MerchantItemGoldBag.BASE_COST);
				merchantItemGoldBag.SetLevel(0);
				MerchantItemAutoTap merchantItemAutoTap = new MerchantItemAutoTap();
				merchantItemAutoTap.SetNumUsed(0);
				merchantItemAutoTap.SetNumMaxBase(MerchantItemAutoTap.BASE_COUNT);
				merchantItemAutoTap.SetPrice((double)MerchantItemAutoTap.BASE_COST);
				MerchantItemTimeWarp merchantItemTimeWarp = new MerchantItemTimeWarp();
				merchantItemTimeWarp.SetNumUsed(0);
				merchantItemTimeWarp.SetNumMaxBase(MerchantItemTimeWarp.BASE_COUNT);
				merchantItemTimeWarp.SetPrice((double)MerchantItemTimeWarp.BASE_COST);
				MerchantItemShield merchantItemShield = new MerchantItemShield();
				merchantItemShield.SetNumUsed(0);
				merchantItemShield.SetNumMaxBase(MerchantItemShield.BASE_COUNT);
				merchantItemShield.SetPrice((double)MerchantItemShield.BASE_COST);
				MerchantItemGoldBoost merchantItemGoldBoost = new MerchantItemGoldBoost();
				merchantItemGoldBoost.SetNumUsed(0);
				merchantItemGoldBoost.SetNumMaxBase(MerchantItemGoldBoost.BASE_COUNT);
				merchantItemGoldBoost.SetPrice((double)MerchantItemGoldBoost.BASE_COST);
				MerchantItemWaveClear merchantItemWaveClear = new MerchantItemWaveClear();
				merchantItemWaveClear.SetNumUsed(0);
				merchantItemWaveClear.SetNumMaxBase(MerchantItemWaveClear.BASE_COUNT);
				merchantItemWaveClear.SetPrice((double)MerchantItemWaveClear.BASE_COST);
				this.merchantItems = new List<MerchantItem>
				{
					merchantItemGoldBag,
					merchantItemAutoTap,
					merchantItemTimeWarp,
					merchantItemShield,
					merchantItemGoldBoost,
					merchantItemWaveClear
				};
				MerchantItemBlizzard item = new MerchantItemBlizzard();
				MerchantItemHotCocoa item2 = new MerchantItemHotCocoa();
				MerchantItemOrnamentDrop item3 = new MerchantItemOrnamentDrop();
				this.eventMerchantItems = new List<MerchantItem>
				{
					item,
					item2,
					item3
				};
			}
			else if (this.gameMode == GameMode.CRUSADE)
			{
				MerchantItemPowerUp merchantItemPowerUp = new MerchantItemPowerUp();
				merchantItemPowerUp.SetNumUsed(0);
				merchantItemPowerUp.SetNumMaxBase(MerchantItemPowerUp.BASE_COUNT);
				merchantItemPowerUp.SetPrice((double)MerchantItemPowerUp.BASE_COST);
				merchantItemPowerUp.SetLevel(0);
				MerchantItemClock merchantItemClock = new MerchantItemClock();
				merchantItemClock.SetNumUsed(0);
				merchantItemClock.SetNumMaxBase(MerchantItemClock.BASE_COUNT);
				merchantItemClock.SetPrice((double)MerchantItemClock.BASE_COST);
				MerchantItemRefresherOrb merchantItemRefresherOrb = new MerchantItemRefresherOrb();
				merchantItemRefresherOrb.SetNumUsed(0);
				merchantItemRefresherOrb.SetNumMaxBase(MerchantItemRefresherOrb.BASE_COUNT);
				merchantItemRefresherOrb.SetPrice((double)MerchantItemRefresherOrb.BASE_COST);
				this.merchantItems = new List<MerchantItem>
				{
					merchantItemPowerUp,
					merchantItemRefresherOrb,
					merchantItemClock
				};
			}
			else
			{
				if (this.gameMode != GameMode.RIFT)
				{
					throw new NotImplementedException();
				}
				MerchantItemEmergencyCharm merchantItemEmergencyCharm = new MerchantItemEmergencyCharm();
				merchantItemEmergencyCharm.SetNumUsed(0);
				merchantItemEmergencyCharm.SetNumMaxBase(MerchantItemEmergencyCharm.BASE_COUNT);
				merchantItemEmergencyCharm.SetPrice((double)MerchantItemEmergencyCharm.BASE_COST);
				MerchantItemCatalyst merchantItemCatalyst = new MerchantItemCatalyst();
				merchantItemCatalyst.SetNumUsed(0);
				merchantItemCatalyst.SetNumMaxBase(MerchantItemCatalyst.BASE_COUNT);
				merchantItemCatalyst.SetPrice((double)MerchantItemCatalyst.BASE_COST);
				MerchantItemVariety merchantItemVariety = new MerchantItemVariety();
				merchantItemVariety.SetNumUsed(0);
				merchantItemVariety.SetNumMaxBase(MerchantItemVariety.BASE_COUNT);
				merchantItemVariety.SetPrice((double)MerchantItemVariety.BASE_COST);
				MerchantItemPickRandomCharms merchantItemPickRandomCharms = new MerchantItemPickRandomCharms();
				merchantItemPickRandomCharms.SetNumUsed(0);
				merchantItemPickRandomCharms.SetNumMaxBase(MerchantItemPickRandomCharms.BASE_COUNT);
				merchantItemPickRandomCharms.SetPrice((double)MerchantItemPickRandomCharms.BASE_COST);
				this.merchantItems = new List<MerchantItem>
				{
					merchantItemEmergencyCharm,
					merchantItemCatalyst,
					merchantItemVariety,
					merchantItemPickRandomCharms
				};
			}
		}

		public void DEBUGreset()
		{
			this.ResetPrestige();
			this.DEBUGresetUnlocks();
			this.activeChallenge.DEBUGreset();
		}

		public void ResetPrestige()
		{
			this.isRainingGlory = false;
			this.isRainingDuck = false;
			this.doTransition = false;
			this.isTransitioning = false;
			if (this.activeChallenge is ChallengeStandard)
			{
				this.activeChallenge = this.allChallenges[0];
				this.activeChallenge.Reset();
			}
			this.offlineGold = 0.0;
			this.givenSkillPoints = 0;
			this.gold.InitZero();
			this.ResetMerchantItems();
			this.activeChallenge.Prestige();
			this.totem = null;
			this.heroes.Clear();
			this.projectiles.Clear();
			this.treasureDrops.Clear();
			this.swarmDragons.Clear();
			this.stampedes.Clear();
			this.supportAnimals.Clear();
			this.currencyDragons.Clear();
			this.drops.Clear();
		}

		public void ResetMerchantItems()
		{
			foreach (MerchantItem merchantItem in this.merchantItems)
			{
				merchantItem.SetNumUsed(0);
			}
			this.timeWarpTimeLeft = 0f;
			this.autoTapTimeLeft = 0f;
			this.powerUpTimeLeft = 0f;
			this.goldBoostTimeLeft = 0f;
			this.refresherOrbTimeLeft = 0f;
			this.shieldTimeLeft = 0f;
			this.catalystTimeLeft = 0f;
			this.numCharmSelectionAdd = 0;
			this.pickRandomCharms = false;
			this.blizzardTimeLeft = 0f;
			this.hotCocoaTimeLeft = 0f;
			this.ornamentDropTimeLeft = 0f;
			this.powerupCooldownTimeLeft = 0f;
			this.powerupNonCritDamageTimeLeft = 0f;
			this.powerupReviveTimeLeft = 0f;
		}

		public void Setup(TotemDataBase totem, List<Rune> wornRunes, HeroDataBase[] heroesData, List<Gear> gears)
		{
			if (totem != null)
			{
				this.LoadTotem(totem, wornRunes);
			}
			foreach (HeroDataBase newBoughtHero in heroesData)
			{
				this.LoadNewHero(newBoughtHero, gears, false);
			}
			this.activeChallenge.OnSetupComplete(totem, wornRunes, heroesData, gears);
			this.willEnd = false;
			this.isTransitioning = false;
			this.shouldSave = true;
		}

		public void Update(float dt, Taps taps)
		{
			float dt2 = dt;
			this.dtUnwarp = dt;
			if (this.timeWarpTimeLeft > 0f)
			{
				this.timeWarpTimeLeft = Mathf.Max(this.timeWarpTimeLeft - dt, 0f);
				dt *= this.timeWarpSpeed;
			}
			dt *= this.buffTotalEffect.timeUpdateFactor;
			this.hasJustActivatedAdDragon = false;
			this.hasJustActivatedCurencyDragon = false;
			if (this.adDragonState == World.AdDragonState.IDLE && taps != null && taps.HasAtLeastOneNew())
			{
				Vector3 anyNew = taps.GetAnyNew();
				if (GameMath.D2xy(anyNew, this.adDragonPos) < 0.1f)
				{
					this.hasJustActivatedAdDragon = true;
				}
			}
			this.timeSinceStartup += dt;
			this.pastDamageTimer -= dt;
			if (this.pastDamageTimer < 0f)
			{
				this.numPastDamages = 0;
				this.pastDamageTimer = 0f;
			}
			if (this.highestDamageBefore > 100.0)
			{
				this.highestDamageBefore *= 0.997;
			}
			this.ringUltraCritTimer += dt;
			this.blackCurtainRatio = 0f;
			foreach (Hero hero in this.heroes)
			{
				this.blackCurtainRatio = GameMath.GetMaxFloat(this.blackCurtainRatio, hero.GetBlackCurtainRatio());
			}
			float num = 1f - 0.75f * this.blackCurtainRatio;
			if (taps != null)
			{
				foreach (Vector3 newPos in this.selfTaps)
				{
					taps.AddNewSimPos(newPos);
				}
				this.selfTaps.Clear();
			}
			this.UpdateCurrencyDragons(dt2, taps);
			bool hasAnyRingTap = taps != null && taps.HasAtLeastOneNew() && !this.hasJustActivatedAdDragon && !this.hasJustActivatedCurencyDragon;
			this.UpdateBuffEffect(dt * num, hasAnyRingTap);
			this.UpdateRiftEffects(dt * num);
			if (this.activeChallenge is ChallengeRift)
			{
				this.UpdateEnchantmentBuffs(dt * num);
			}
			this.UpdateUnitStats(dt * num);
			this.activeChallenge.Update(dt * num, this.dtUnwarp);
			this.UpdateAdDragon(dt2);
			this.UpdateTotem(dt * num, (!this.hasJustActivatedCurencyDragon && !this.hasJustActivatedAdDragon && this.activeChallenge.state == Challenge.State.ACTION) ? taps : null);
			this.UpdateHeroes(dt, num);
			this.UpdateProjectiles(dt * num);
			this.UpdateTreasureDrops(dt * num);
			this.UpdateSwarmDragons(dt * num);
			this.UpdateDruidStampedes(dt * num);
			this.UpdateSupportAnimals(dt * num);
			this.UpdateDrops(dt * num, taps);
			this.UpdatePastDamages(dt * num);
			this.UpdateFutureDamages(dt * num);
			this.UpdateVisualEffects(dt * num);
			this.UpdateVisualLineEffects(dt * num);
			this.UpdateMaxStageReached();
			if (this.throwOrnamentDropsCount > 0)
			{
				this.ThrowOrnamentDrops();
			}
			if (this.isRainingGlory && this.drops.Count == 0)
			{
				this.OnRainGloryComplete();
			}
			if (this.isRainingDuck && !this.HasActiveDuck())
			{
				if (this.duckInitTimer < 1f)
				{
					this.duckInitTimer += dt;
					if (this.duckInitTimer >= 1f)
					{
						Vector3 pos = Vector3.zero;
						if (this.heroes.Count > 0)
						{
							int randomInt = GameMath.GetRandomInt(0, this.heroes.Count, GameMath.RandType.NoSeed);
							Hero hero2 = this.heroes[randomInt];
							pos = hero2.pos;
							pos.y += hero2.GetHeight() * 0.9f;
						}
						VisualEffect visualEffect = new VisualEffect(VisualEffect.Type.DUCK, 1f);
						visualEffect.pos = pos;
						this.visualEffects.Add(visualEffect);
						SoundEventSound e = new SoundEventSound(SoundType.UI, string.Empty, false, 0f, new SoundDelayed(0.4f, SoundArchieve.inst.uiDuck, 1f));
						this.AddSoundEvent(e);
					}
				}
				else
				{
					this.OnDuckComplete();
				}
			}
		}

		private void UpdateEnchantmentBuffs(float dt)
		{
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			foreach (CharmBuff charmBuff in challengeRift.charmBuffs)
			{
				charmBuff.Update(dt);
			}
			foreach (CurseBuff curseBuff in challengeRift.curseBuffs)
			{
				if (curseBuff.state != EnchantmentBuffState.INACTIVE)
				{
					curseBuff.Update(dt);
				}
			}
		}

		private void UpdateCurrencyDragons(float dt, Taps taps)
		{
			for (int i = this.currencyDragons.Count - 1; i >= 0; i--)
			{
				CurrencyDragon currencyDragon = this.currencyDragons[i];
				currencyDragon.stateTime += dt;
				if (currencyDragon.state == CurrencyDragon.State.ENTER)
				{
					currencyDragon.SetState(CurrencyDragon.State.IDLE);
				}
				else if (currencyDragon.state == CurrencyDragon.State.IDLE)
				{
					CurrencyDragon currencyDragon2 = currencyDragon;
					currencyDragon2.pos.x = currencyDragon2.pos.x + currencyDragon.speed * this.GetTimeSpeedFactor() * currencyDragon.direction * dt;
					currencyDragon.pos.y = 0.55f + GameMath.Sin((currencyDragon.stateTime + currencyDragon.stateOffset) * 6.23f) * 0.0175f + currencyDragon.yOffset;
					if ((currencyDragon.pos.x > 0.7f && currencyDragon.direction > 0f) || (currencyDragon.pos.x < -0.7f && currencyDragon.direction < 0f))
					{
						currencyDragon.direction = -currencyDragon.direction;
					}
					if (taps != null && taps.HasAtLeastOneNew())
					{
						Vector3 anyNew = taps.GetAnyNew();
						if (GameMath.D2xy(anyNew, currencyDragon.pos) < 0.0333333351f)
						{
							currencyDragon.SetState(CurrencyDragon.State.DROP);
							currencyDragon.PlayActivateSound(this);
							this.hasJustActivatedCurencyDragon = true;
						}
					}
					if (currencyDragon.stateTime * this.GetTimeSpeedFactor() >= currencyDragon.maxTime)
					{
						currencyDragon.SetState(CurrencyDragon.State.DROP);
						currencyDragon.PlayActivateSound(this);
					}
				}
				else if (currencyDragon.state == CurrencyDragon.State.DROP)
				{
					if (!currencyDragon.dropsSpawned && (double)currencyDragon.stateTime >= 0.2)
					{
						currencyDragon.dropsSpawned = true;
						int num = 15;
						if (currencyDragon.dropAmount < (double)num)
						{
							num = (int)currencyDragon.dropAmount;
						}
						double amount = currencyDragon.dropAmount / (double)num;
						for (int j = 0; j < num; j++)
						{
							Vector3 pos = currencyDragon.pos;
							Vector3 vector = new Vector3(currencyDragon.pos.x + GameMath.GetRandomFloat(-0.3f, 0.3f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.4f, 0.2f, GameMath.RandType.NoSeed), 0f);
							Vector3 velStart = new Vector3(GameMath.GetRandomFloat(-0.8f, 0.8f, GameMath.RandType.NoSeed) - pos.x * 0.5f, GameMath.GetRandomFloat(0f, -0.5f, GameMath.RandType.NoSeed), 0f);
							DropCurrency dropCurrency = new DropCurrency(currencyDragon.dropCurrency, amount, this, false);
							dropCurrency.InitVel(0f, pos, vector.y, velStart);
							dropCurrency.durNonExistence = (float)j * 0.02f;
							dropCurrency.amount = amount;
							this.drops.Add(dropCurrency);
						}
					}
					if (currencyDragon.stateTime > 1.3f)
					{
						CurrencyDragon currencyDragon3 = currencyDragon;
						currencyDragon3.pos.y = currencyDragon3.pos.y + 3f * currencyDragon.speed * dt;
					}
					if (currencyDragon.stateTime > 1.8f)
					{
						this.currencyDragons.RemoveAt(i);
					}
				}
				else
				{
					if (currencyDragon.state != CurrencyDragon.State.EXIT)
					{
						throw new Exception(currencyDragon.state.ToString());
					}
					CurrencyDragon currencyDragon4 = currencyDragon;
					currencyDragon4.pos.x = currencyDragon4.pos.x + currencyDragon.speed * currencyDragon.direction * dt;
					if (currencyDragon.stateTime > 0.6f)
					{
						CurrencyDragon currencyDragon5 = currencyDragon;
						currencyDragon5.pos.y = currencyDragon5.pos.y + 3f * currencyDragon.speed * dt;
					}
					if (currencyDragon.stateTime > 1.2f)
					{
						this.currencyDragons.RemoveAt(i);
					}
				}
			}
		}

		private void UpdateAdDragon(float dt)
		{
			if (this.gameMode != GameMode.STANDARD)
			{
				return;
			}
			if (this.adDragonState == World.AdDragonState.NONEXISTANCE)
			{
				this.adDragonTimeCounter += dt * this.universalBonus.dragonSpawnRateFactor;
				if (this.adDragonReqMaxStage <= this.maxStageReached && this.adDragonTimeCounter > this.adDragonWait && !this.isRainingGlory)
				{
					this.adDragonState = World.AdDragonState.IDLE;
					this.adDragonPos.x = -1.1f;
					this.adDragonDir = 1f;
					this.adDragonTimeCounter = 0f;
					this.adDragonWait = GameMath.GetRandomFloat(this.adDragonWaitMin, this.adDragonWaitMax, GameMath.RandType.NoSeed);
					this.sounds.Add(new SoundEventCancelBy("adDragon"));
					this.sounds.Add(new SoundEventSound(SoundType.GAMEPLAY, "adDragon", false, 0f, new SoundLooped(SoundArchieve.inst.adDragonLoop, 1f)));
				}
			}
			else
			{
				this.adDragonTimeCounter += dt;
				if (this.adDragonState == World.AdDragonState.IDLE)
				{
					if (this.adDragonDir > 0f && this.adDragonPos.x > 0.7f)
					{
						this.adDragonDir *= -1f;
					}
					else if (this.adDragonDir < 0f && this.adDragonPos.x < -0.7f)
					{
						this.adDragonDir *= -1f;
					}
					this.adDragonPos.x = this.adDragonPos.x + this.adDragonDir * 0.3f * dt;
					if (this.hasJustActivatedAdDragon)
					{
						this.ActivateAdDragon();
						PlayerStats.OnAdDragonCatch();
					}
					if (this.adDragonTimeCounter > this.adDragonIdleDur)
					{
						this.adDragonState = World.AdDragonState.ESCAPE;
						this.adDragonTimeCounter = 0f;
						this.sounds.Add(new SoundEventCancelBy("adDragon"));
						this.sounds.Add(new SoundEventSound(SoundType.GAMEPLAY, "adDragon", false, 0f, new SoundSimple(SoundArchieve.inst.adDragonLeave, 1f, float.MaxValue)));
						PlayerStats.OnAdDragonMiss();
					}
				}
				else if (this.adDragonState == World.AdDragonState.WAIT_UI)
				{
					if (RewardedAdManager.inst == null || RewardedAdManager.inst.hasFailed || (!this.showAdOffer && !this.showingAdOffer && !RewardedAdManager.inst.IsWatchingAnyAd() && !RewardedAdManager.inst.shouldGiveReward && !RewardedAdManager.inst.shouldGiveRewardCapped))
					{
						RewardedAdManager.inst.hasFailed = false;
						this.adDragonState = World.AdDragonState.ESCAPE;
						this.adDragonTimeCounter = 0f;
						this.sounds.Add(new SoundEventSound(SoundType.GAMEPLAY, "adDragon", false, 0f, new SoundSimple(SoundArchieve.inst.adDragonLeave, 1f, float.MaxValue)));
					}
				}
				else if (this.adDragonState == World.AdDragonState.ESCAPE)
				{
					this.adDragonPos.x = this.adDragonPos.x + this.adDragonDir * 0.3f * dt;
					if (this.adDragonTimeCounter > 3f)
					{
						this.adDragonState = World.AdDragonState.NONEXISTANCE;
						this.adDragonTimeCounter = 0f;
					}
				}
				else if (this.adDragonState == World.AdDragonState.ACTIVATE && this.adDragonTimeCounter > 3f)
				{
					this.adDragonState = World.AdDragonState.NONEXISTANCE;
					this.adDragonTimeCounter = 0f;
				}
			}
		}

		public int GetCharmSelectionNum()
		{
			return 3 + this.numCharmSelectionAdd;
		}

		private void ActivateAdDragon()
		{
			this.hasJustActivatedAdDragon = true;
			this.dailyQuestDragonCatchCounter++;
			this.sounds.Add(new SoundEventCancelBy("adDragon"));
			this.sounds.Add(new SoundEventSound(SoundType.GAMEPLAY, "adDragon", false, 0f, new SoundSimple(SoundArchieve.inst.adDragonTap, 1f, float.MaxValue)));
			if (TutorialManager.IsShopTabUnlocked() && this.gameMode != GameMode.CRUSADE && RewardedAdManager.inst != null && RewardedAdManager.inst.IsRewardedVideoAvailable() && GameMath.GetProbabilityOutcome(PlayfabManager.GetChanceAd(), GameMath.RandType.NoSeed))
			{
				this.adDragonState = World.AdDragonState.WAIT_UI;
				this.showAdOffer = true;
				this.adRewardCurrencyType = PlayfabManager.GetRewardTypeAd();
				if (this.adRewardCurrencyType == CurrencyType.GOLD)
				{
					this.adRewardAmount = PlayfabManager.titleData.adDragonRewardAdGoldFactor * World.GetAdDragonBaseGoldRewardAmount(this);
				}
				else if (this.adRewardCurrencyType == CurrencyType.TOKEN)
				{
					this.adRewardAmount = PlayfabManager.titleData.adDragonRewardAdToken;
				}
				else if (this.adRewardCurrencyType == CurrencyType.GEM)
				{
					this.adRewardAmount = PlayfabManager.titleData.adDragonRewardAdCredits;
				}
				else if (this.adRewardCurrencyType == CurrencyType.SCRAP)
				{
					this.adRewardAmount = PlayfabManager.titleData.adDragonRewardAdScrap;
				}
				else if (this.adRewardCurrencyType == CurrencyType.MYTHSTONE)
				{
					this.adRewardAmount = PlayfabManager.titleData.adDragonRewardAdMyth;
				}
				else
				{
					if (this.adRewardCurrencyType != CurrencyType.AEON)
					{
						throw new NotImplementedException();
					}
					this.adRewardAmount = PlayfabManager.titleData.adDragonRewardAdAeon;
				}
			}
			else
			{
				this.adDragonState = World.AdDragonState.ACTIVATE;
				this.adDragonTimeCounter = 0f;
				CurrencyType currencyType = TutorialManager.IsShopTabUnlocked() ? PlayfabManager.GetRewardTypeDirect(this.gameMode) : CurrencyType.GEM;
				double num;
				if (currencyType == CurrencyType.GOLD)
				{
					num = PlayfabManager.titleData.adDragonRewardDirectGoldFactor * World.GetAdDragonBaseGoldRewardAmount(this);
				}
				else if (currencyType == CurrencyType.TOKEN)
				{
					num = PlayfabManager.titleData.adDragonRewardDirectToken;
				}
				else if (currencyType == CurrencyType.GEM)
				{
					num = PlayfabManager.titleData.adDragonRewardDirectCredits;
				}
				else if (currencyType == CurrencyType.SCRAP)
				{
					num = PlayfabManager.titleData.adDragonRewardDirectScrap;
				}
				else if (currencyType == CurrencyType.MYTHSTONE)
				{
					num = PlayfabManager.titleData.adDragonRewardDirectMyth;
				}
				else
				{
					if (currencyType != CurrencyType.AEON)
					{
						throw new NotImplementedException();
					}
					num = PlayfabManager.titleData.adDragonRewardDirectAeon;
				}
				int num2 = 15;
				if (num < (double)num2)
				{
					num2 = (int)num;
				}
				double amount = num / (double)num2;
				for (int i = 0; i < num2; i++)
				{
					Vector3 startPos = this.adDragonPos;
					Vector3 vector = new Vector3(this.adDragonPos.x + GameMath.GetRandomFloat(-0.3f, 0.3f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.4f, 0.2f, GameMath.RandType.NoSeed), 0f);
					Vector3 velStart = new Vector3(GameMath.GetRandomFloat(-0.8f, 0.8f, GameMath.RandType.NoSeed) - startPos.x * 0.5f, GameMath.GetRandomFloat(0f, -0.5f, GameMath.RandType.NoSeed), 0f);
					DropCurrency dropCurrency = new DropCurrency(currencyType, amount, this, false);
					dropCurrency.InitVel(0f, startPos, vector.y, velStart);
					dropCurrency.durNonExistence = (float)i * 0.02f;
					dropCurrency.amount = amount;
					this.drops.Add(dropCurrency);
				}
				if (this.canDropCandies)
				{
					int randomInt = GameMath.GetRandomInt(6, 12, GameMath.RandType.NoSeed);
					for (int j = 0; j < randomInt; j++)
					{
						Vector3 startPos2 = this.adDragonPos;
						Vector3 vector2 = new Vector3(this.adDragonPos.x + GameMath.GetRandomFloat(-0.3f, 0.3f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.4f, 0.2f, GameMath.RandType.NoSeed), 0f);
						Vector3 velStart2 = new Vector3(GameMath.GetRandomFloat(-0.8f, 0.8f, GameMath.RandType.NoSeed) - startPos2.x * 0.5f, GameMath.GetRandomFloat(0f, -0.5f, GameMath.RandType.NoSeed), 0f);
						DropCurrency dropCurrency2 = new DropCurrency(CurrencyType.CANDY, 1.0, this, true);
						dropCurrency2.InitVel(0f, startPos2, vector2.y, velStart2);
						dropCurrency2.durNonExistence = (float)j * 0.02f;
						dropCurrency2.amount = 1.0;
						this.drops.Add(dropCurrency2);
					}
				}
			}
		}

		public static double GetAdDragonBaseGoldRewardAmount(World world)
		{
			int num = world.GetStageNumber();
			if (world.activeChallenge is ChallengeStandard)
			{
				ChallengeStandard challengeStandard = (ChallengeStandard)world.activeChallenge;
				if (ChallengeStandard.IsBossWave(challengeStandard.totWave))
				{
					num--;
				}
			}
			double power = (double)num * 0.5;
			return UnitMath.GetGoldToDropForPower(power) * world.universalBonus.goldFactor * world.universalBonus.gearGoldFactor * world.universalBonus.mineGoldFactor * (double)world.universalBonus.goldBagValueFactor * world.universalBonus.goldBagAdDragonFactor * world.activeChallenge.totalGainedUpgrades.goldFactor * (world.universalBonus.bountyIncreasePerDamageTakenFromHero * 50.0 + 1.0) * world.universalBonus.GetGoldFactorForCurrentSupportersInTeam(world.GetNumHeroesOfClassInTeam(HeroClass.SUPPORTER));
		}

		public void OnAdCancel()
		{
			this.adDragonState = World.AdDragonState.ESCAPE;
			this.adDragonTimeCounter = 0f;
			this.sounds.Add(new SoundEventSound(SoundType.GAMEPLAY, "adDragon", false, 0f, new SoundSimple(SoundArchieve.inst.adDragonLeave, 1f, float.MaxValue)));
		}

		private void UpdateFutureDamages(float dt)
		{
			int num = this.futureDamages.Count;
			for (int i = num - 1; i >= 0; i--)
			{
				FutureDamage futureDamage = this.futureDamages[i];
				futureDamage.timeLeft -= dt;
				if (futureDamage.timeLeft <= 0f)
				{
					this.DamageMain(futureDamage.damager, futureDamage.damaged, futureDamage.damage);
					this.futureDamages[i] = this.futureDamages[--num];
					this.futureDamages.RemoveAt(num);
				}
			}
		}

		private void UpdateVisualLineEffects(float dt)
		{
			int num = this.visualLinedEffects.Count;
			for (int i = num - 1; i >= 0; i--)
			{
				VisualLinedEffect visualLinedEffect = this.visualLinedEffects[i];
				visualLinedEffect.Update(dt);
				if (visualLinedEffect.IsToBeRemoved())
				{
					this.visualLinedEffects[i] = this.visualLinedEffects[--num];
					this.visualLinedEffects.RemoveAt(num);
				}
			}
		}

		private void UpdateMaxStageReached()
		{
			int stageNumber = this.GetStageNumber();
			int waveNumber = this.activeChallenge.GetWaveNumber();
			if (stageNumber > this.maxStageReached && waveNumber > 0)
			{
				this.maxStageReached = stageNumber;
				if (this.gameMode == GameMode.STANDARD && !this.currentSim.maxStageReachedInCurrentAdventure && this.currentSim.numPrestiges > 0)
				{
					this.currentSim.maxStageReachedInCurrentAdventure = true;
					UiManager.adventureMaxStageJustReached = true;
				}
			}
		}

		private void UpdateVisualEffects(float dt)
		{
			int num = this.visualEffects.Count;
			for (int i = num - 1; i >= 0; i--)
			{
				VisualEffect visualEffect = this.visualEffects[i];
				visualEffect.Update(dt);
				if (visualEffect.IsToBeRemoved())
				{
					this.visualEffects[i] = this.visualEffects[--num];
					this.visualEffects.RemoveAt(num);
				}
			}
		}

		private void UpdatePastDamages(float dt)
		{
			int count = this.pastDamages.Count;
			float dt2 = Mathf.Pow((float)count, 0.2f) * dt;
			foreach (GlobalPastDamage globalPastDamage in this.pastDamages)
			{
				globalPastDamage.Update(dt2);
			}
			while (this.pastDamages.Count > 0 && this.pastDamages.Peek().time > this.pastDamages.Peek().totTime)
			{
				this.pastDamages.Dequeue();
			}
			foreach (GlobalPastHeal globalPastHeal in this.pastHeals)
			{
				globalPastHeal.Update(dt);
			}
			while (this.pastHeals.Count > 0 && this.pastHeals.Peek().time > this.pastHeals.Peek().totTime)
			{
				this.pastHeals.Dequeue();
			}
		}

		private void UpdateRiftEffects(float dt)
		{
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			if (challengeRift != null)
			{
				foreach (RiftEffect riftEffect in challengeRift.riftEffects)
				{
					riftEffect.Apply(this, dt);
				}
			}
		}

		private void UpdateBuffEffect(float dt, bool hasAnyRingTap)
		{
			this.buffTotalEffect.Init();
			if (this.gameMode == GameMode.RIFT)
			{
				this.buffTotalEffect.constantHeroReviveTime = World.RIFT_REVIVE_TIME;
			}
			this.powerUpTimeLeft = Mathf.Max(0f, this.powerUpTimeLeft - dt);
			if (this.powerUpTimeLeft > 0f)
			{
				this.buffTotalEffect.heroDamageFactor += this.powerUpDamageFactorAdd;
			}
			this.goldBoostTimeLeft = Mathf.Max(0f, this.goldBoostTimeLeft - dt);
			if (this.goldBoostTimeLeft > 0f)
			{
				this.buffTotalEffect.goldBoostFactor *= this.goldBoostFactor;
			}
			this.powerupNonCritDamageTimeLeft = Mathf.Max(0f, this.powerupNonCritDamageTimeLeft - dt);
			if (this.powerupNonCritDamageTimeLeft > 0f)
			{
				this.buffTotalEffect.damageNonCritFactor *= this.universalBonus.powerupNonCritDamageFactorBonus;
			}
			this.powerupCooldownTimeLeft = Mathf.Max(0f, this.powerupCooldownTimeLeft - dt);
			if (this.powerupCooldownTimeLeft > 0f)
			{
				this.buffTotalEffect.heroUltiCoolFactor *= 1f + this.universalBonus.powerupCooldownUltiAdd;
			}
			this.powerupReviveTimeLeft = Mathf.Max(0f, this.powerupReviveTimeLeft - dt);
			if (this.powerupReviveTimeLeft > 0f)
			{
				this.buffTotalEffect.reviveSpeed *= 1f + this.universalBonus.powerupReviveSpeedAdd;
			}
			this.shieldTimeLeft = Mathf.Max(0f, this.shieldTimeLeft - dt);
			if (this.shieldTimeLeft > 0f)
			{
				float dur = 1f;
				foreach (Hero hero in this.heroes)
				{
					BuffDataInvulnerability buffDataInvulnerability = new BuffDataInvulnerability(dur);
					buffDataInvulnerability.id = 116;
					buffDataInvulnerability.visuals |= 128;
					bool flag = false;
					BuffDataReviveSpeed buffDataReviveSpeed = new BuffDataReviveSpeed();
					buffDataReviveSpeed.id = 154;
					buffDataReviveSpeed.reviveSpeedFactorAdd = 10f;
					buffDataReviveSpeed.dur = dur;
					foreach (Buff buff in hero.buffs)
					{
						if (buff.HasSameId(buffDataInvulnerability))
						{
							buff.ResetTime();
							flag = true;
						}
						else if (buff.HasSameId(buffDataReviveSpeed))
						{
							buff.ResetTime();
							flag = true;
						}
					}
					if (!flag)
					{
						hero.AddBuff(buffDataReviveSpeed, 0, false);
						hero.AddBuff(buffDataInvulnerability, 0, false);
					}
				}
			}
			this.refresherOrbTimeLeft = Mathf.Max(0f, this.refresherOrbTimeLeft - dt);
			if (this.refresherOrbTimeLeft > 0f)
			{
				this.buffTotalEffect.heroUltiCoolFactor += this.refresherOrbSkillCoolFactor;
			}
			this.catalystTimeLeft = Mathf.Max(0f, this.catalystTimeLeft - dt);
			if (this.catalystTimeLeft > 0f)
			{
				this.catalystActTimer += dt;
				while (this.catalystActTimer >= 1f)
				{
					this.catalystActTimer -= 1f;
					ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
					if (challengeRift != null)
					{
						List<CharmBuff> charmsThatAreNotAlwaysActive = challengeRift.GetCharmsThatAreNotAlwaysActive();
						if (charmsThatAreNotAlwaysActive.Count > 0)
						{
							CharmBuff charmBuff = charmsThatAreNotAlwaysActive[GameMath.GetRandomInt(0, charmsThatAreNotAlwaysActive.Count, GameMath.RandType.NoSeed)];
							charmBuff.AddProgress(this.catalystProgressPercentage);
						}
					}
				}
			}
			this.isIdleGainActive = false;
			bool flag2 = this.autoTapTimeLeft > 0f;
			if (!hasAnyRingTap)
			{
				this.noRingTabDur += dt;
			}
			else
			{
				this.noRingTabDur = 0f;
			}
			if (this.universalBonus.goldIdleFactor > 1.0)
			{
				if (this.universalBonus.idleBonusAfter <= 0f || (!flag2 && !hasAnyRingTap))
				{
					this.idleDuration += dt;
					if (this.idleDuration > this.universalBonus.idleBonusAfter)
					{
						this.buffTotalEffect.goldBoostFactor *= this.universalBonus.goldIdleFactor;
						this.isIdleGainActive = true;
					}
				}
				else
				{
					this.idleDuration = 0f;
				}
			}
			else
			{
				this.idleDuration = 0f;
			}
			this.blizzardTimeLeft = GameMath.GetMaxFloat(0f, this.blizzardTimeLeft - dt);
			this.hotCocoaTimeLeft = GameMath.GetMaxFloat(0f, this.hotCocoaTimeLeft - dt);
			if (this.hotCocoaTimeLeft > 0f)
			{
				this.buffTotalEffect.heroSkillCoolFactor *= this.hotCocoaCooldownReductionFactor;
				this.buffTotalEffect.heroUltiCoolFactor *= this.hotCocoaCooldownReductionFactor;
				this.buffTotalEffect.heroDamageFactor *= (double)this.hotCocoaDamageFactor;
			}
			this.ornamentDropTimeLeft = GameMath.GetMaxFloat(0f, this.ornamentDropTimeLeft - dt);
			if (this.ornamentDropTimeLeft > 0f)
			{
				this.ornamentDropCurrentTime += dt;
				if ((hasAnyRingTap || (flag2 && this.totem.CanAutoTapOnThisFrame(dt))) && this.ornamentDropCurrentTime >= this.ornamentDropTargetTime)
				{
					this.throwOrnamentDropsCount = this.ornamentDropProjectilesCount;
				}
			}
			else
			{
				this.ornamentDropCurrentTime = 0f;
				this.throwOrnamentDropsCount = 0;
			}
			if (!this.autoUpgradeDisabled && this.gameMode == GameMode.STANDARD && this.universalBonus.autoUpgradeMaxCost > 0.0)
			{
				this.autoUpgradeTimer += dt;
				if (this.autoUpgradeTimer > 0.07f)
				{
					this.autoUpgradeTimer = 0f;
					double num = double.MaxValue;
					Hero hero2 = null;
					foreach (Hero hero3 in this.heroes)
					{
						if (hero3.GetUpgradeCost(false) <= this.universalBonus.autoUpgradeMaxCost)
						{
							if (hero3.GetUpgradeCost(true) < num)
							{
								num = hero3.GetUpgradeCost(true);
								hero2 = hero3;
							}
						}
					}
					double num2 = double.MaxValue;
					if (this.totem != null && this.totem.GetUpgradeCost(false) <= this.universalBonus.autoUpgradeMaxCost)
					{
						num2 = this.GetTotemUpgradeCost();
						if (num2 < 0.0)
						{
							num2 = double.MaxValue;
						}
					}
					if (hero2 != null && num <= num2)
					{
						if (this.gold.CanAfford(num))
						{
							this.TryUpgradeHero(hero2);
							UiManager.stateJustChanged = true;
						}
					}
					else if (num2 != 1.7976931348623157E+308 && num2 <= num && this.gold.CanAfford(num2))
					{
						this.TryUpgradeTotem();
						UiManager.stateJustChanged = true;
					}
					if (this.autoUpgradeMilestones)
					{
						double nextChallangeUpgradeCost = this.GetNextChallangeUpgradeCost();
						if (this.universalBonus.autoUpgradeMaxCost >= nextChallangeUpgradeCost && this.CanBuyWorldUpgrade() && this.CanAffordWorldUpgrade())
						{
							this.TryBuyWorldUpgrade();
						}
					}
				}
			}
			if (this.totem != null)
			{
				this.totem.UpdateBuffTotalWorldEffect(this.buffTotalEffect);
			}
			foreach (Hero hero4 in this.heroes)
			{
				hero4.UpdateBuffTotalWorldEffect(this.buffTotalEffect);
			}
			foreach (Enemy enemy in this.activeChallenge.enemies)
			{
				enemy.UpdateBuffTotalWorldEffect(this.buffTotalEffect);
			}
		}

		private void ThrowOrnamentDrops()
		{
			this.cachedEnemyList.Clear();
			for (int i = this.activeChallenge.enemies.Count - 1; i >= 0; i--)
			{
				if (this.activeChallenge.enemies[i].IsAlive())
				{
					Enemy enemy = this.activeChallenge.enemies[i];
					this.cachedEnemyList.Add(enemy);
					if (!this.cachedEnemyHits.ContainsKey(enemy))
					{
						this.cachedEnemyHits.Add(enemy, 1);
					}
				}
			}
			int count = this.cachedEnemyList.Count;
			if (count > 0)
			{
				double num = this.GetHeroTeamDps() * (double)this.ornamentDropTeamDamageFactor;
				int num2 = this.throwOrnamentDropsCount - 1;
				while (this.throwOrnamentDropsCount > 0)
				{
					Enemy randomListElement = this.cachedEnemyList.GetRandomListElement<Enemy>();
					this.ornamentDropCurrentTime = 0f;
					Vector3 posSpawnEnd = randomListElement.posSpawnEnd;
					posSpawnEnd.y += randomListElement.GetHeight();
					Vector3 posStart = new Vector3(posSpawnEnd.x + GameMath.GetRandomFloat(-0.2f, 0.2f, GameMath.RandType.NoSeed), 1.7f + (float)(num2 - this.throwOrnamentDropsCount) * 0.3f, posSpawnEnd.z);
					float num3 = Mathf.Abs((posSpawnEnd.y - posStart.y) / 2.5f);
					this.ornamentDropCurrentTime = -num3;
					ProjectilePathLinear pathToCopy = new ProjectilePathLinear();
					Projectile projectile = new Projectile(null, Projectile.Type.CHRISTMAS_ORNAMENT, Projectile.TargetType.SINGLE_ENEMY, randomListElement, num3, pathToCopy, posSpawnEnd);
					projectile.InitPath(posStart, posSpawnEnd);
					projectile.damageMomentTimeRatio = 1f;
					projectile.damage = new Damage(num, false, false, false, false);
					projectile.visualEffect = new VisualEffect(VisualEffect.Type.ORNAMENT_DROP, 0.333f);
					projectile.rotateSpeed = GameMath.GetRandomFloat(0f, 360f, GameMath.RandType.NoSeed);
					projectile.targetLocked = true;
					projectile.soundImpact = new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, new SoundVariedSimple(SoundArchieve.inst.ornamentDropImpacts, 1f));
					projectile.visualVariation = GameMath.GetRandomInt(1, 4, GameMath.RandType.NoSeed);
					this.projectiles.Add(projectile);
					this.throwOrnamentDropsCount--;
					double num4 = randomListElement.GetHealth() + randomListElement.GetShield();
					double num5 = num;
					Dictionary<Enemy, int> dictionary;
					Enemy key;
					int num6;
					(dictionary = this.cachedEnemyHits)[key = randomListElement] = (num6 = dictionary[key]) + 1;
					if (num4 < num5 * (double)num6)
					{
						this.cachedEnemyList.Remove(randomListElement);
						this.cachedEnemyHits.Remove(randomListElement);
						if (this.cachedEnemyList.Count == 0)
						{
							break;
						}
					}
				}
				this.cachedEnemyHits.Clear();
			}
		}

		public void UpdateUnitStats(float dt)
		{
			if (this.totem != null)
			{
				this.totem.UpdateStats(dt);
			}
			foreach (Hero hero in this.heroes)
			{
				hero.UpdateStats(dt);
			}
			foreach (Enemy enemy in this.activeChallenge.enemies)
			{
				enemy.UpdateStats(dt);
			}
		}

		public void OnGearsChanged(List<Gear> gears)
		{
			foreach (Hero hero in this.heroes)
			{
				hero.OnGearsChanged();
			}
		}

		public void OnUniversalBonusChanged()
		{
			foreach (MerchantItem merchantItem in this.merchantItems)
			{
				if (merchantItem is MerchantItemAutoTap)
				{
					merchantItem.SetNumMaxAdd(this.universalBonus.autoTapCountAdd);
				}
				else if (merchantItem is MerchantItemGoldBag)
				{
					merchantItem.SetNumMaxAdd(this.universalBonus.goldBagCountAdd);
				}
				else if (merchantItem is MerchantItemTimeWarp)
				{
					merchantItem.SetNumMaxAdd(this.universalBonus.timeWarpCountAdd);
				}
				else if (merchantItem is MerchantItemPowerUp)
				{
					merchantItem.SetNumMaxAdd(this.universalBonus.powerUpCountAdd);
				}
				else if (merchantItem is MerchantItemRefresherOrb)
				{
					merchantItem.SetNumMaxAdd(this.universalBonus.refresherOrbCountAdd);
				}
				else if (merchantItem is MerchantItemShield)
				{
					merchantItem.SetNumMaxAdd(this.universalBonus.shieldCountAdd);
				}
				else if (merchantItem is MerchantItemGoldBoost)
				{
					merchantItem.SetNumMaxAdd(this.universalBonus.horseshoeCountAdd);
				}
				else if (merchantItem is MerchantItemWaveClear)
				{
					merchantItem.SetNumMaxAdd(this.universalBonus.destructionCountAdd);
				}
			}
			this.RefreshChallengeUpgrades(this.activeChallenge.totalGainedUpgrades.numBought);
		}

		private void InitRainResult()
		{
			this.activeChallenge.enemies.Clear();
			this.stampedes.Clear();
			this.supportAnimals.Clear();
			this.swarmDragons.Clear();
			this.treasureDrops.Clear();
			this.projectiles.Clear();
			foreach (Hero hero in this.heroes)
			{
				Hero hero2 = hero;
				hero2.pos.x = hero2.pos.x + 0.3f;
				hero.buffs.Clear();
				hero.SetHealthFull();
				hero.CancelSkills();
				hero.UpdateState(Hero.State.IDLE);
				hero.InterruptWeapon();
			}
		}

		private void InitRainGlory()
		{
			this.isRainingGlory = true;
			this.willEnd = true;
		}

		private void InitDuck()
		{
			this.isRainingDuck = true;
			this.willEnd = true;
			this.duckInitTimer = 0f;
		}

		private bool HasActiveDuck()
		{
			foreach (VisualEffect visualEffect in this.visualEffects)
			{
				if (visualEffect.type == VisualEffect.Type.DUCK)
				{
					return true;
				}
			}
			return false;
		}

		private void Prestige(bool isMega)
		{
			this.InitRainResult();
			this.InitRainGlory();
			if (this.adDragonState != World.AdDragonState.ESCAPE && this.adDragonState != World.AdDragonState.ACTIVATE)
			{
				this.adDragonTimeCounter = 0f;
			}
			if (this.adDragonState == World.AdDragonState.IDLE)
			{
				this.adDragonState = World.AdDragonState.ESCAPE;
				this.sounds.Add(new SoundEventCancelBy("adDragon"));
				this.sounds.Add(new SoundEventSound(SoundType.GAMEPLAY, "adDragon", false, 0f, new SoundSimple(SoundArchieve.inst.adDragonLeave, 1f, float.MaxValue)));
			}
			if (UiManager.DEBUGDontShowTransitionInEditor)
			{
				this.AddMythstone(this.GetNumMythstonesOnPrestige(isMega));
			}
			else
			{
				this.CreatePrestigeRain(isMega);
			}
			if (this.adDragonState == World.AdDragonState.WAIT_UI || this.adDragonState == World.AdDragonState.ACTIVATE || this.adDragonState == World.AdDragonState.IDLE)
			{
				this.adDragonState = World.AdDragonState.ESCAPE;
				this.adDragonTimeCounter = 0f;
				this.sounds.Add(new SoundEventCancelBy("adDragon"));
				this.sounds.Add(new SoundEventSound(SoundType.GAMEPLAY, "adDragon", false, 0f, new SoundSimple(SoundArchieve.inst.adDragonLeave, 1f, float.MaxValue)));
			}
			this.shouldSave = true;
			TutorialManager.Prestiged();
		}

		public void CreatePrestigeRain(bool isMega)
		{
			double numMythstonesOnPrestige = this.GetNumMythstonesOnPrestige(isMega);
			int num = 50;
			if (numMythstonesOnPrestige < (double)num)
			{
				num = (int)numMythstonesOnPrestige;
			}
			double amount = numMythstonesOnPrestige / (double)num;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = new Vector3(GameMath.GetRandomFloat(-0.9f, 0.9f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.4f, 0.4f, GameMath.RandType.NoSeed), 0f);
				Vector3 startPos = vector;
				startPos.y = (float)(i + 2);
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.MYTHSTONE, amount, this, false);
				dropCurrency.InitVel(0f, startPos, vector.y, Vector3.zero);
				this.drops.Add(dropCurrency);
			}
		}

		public void OnRainGloryComplete()
		{
			if (this.activeChallenge.CanPrestigeNowExceptRainingGlory())
			{
				this.ResetPrestige();
			}
			else if (this.activeChallenge is ChallengeRift)
			{
				ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
				this.activeChallenge.Reset();
				if (this.advanceToNextRiftAfterRainGlory)
				{
					if (challengeRift.IsCursed())
					{
						this.currentSim.PickRandomCurses();
						this.SelectNextCursedRiftOrRegular();
					}
					else
					{
						this.AdvanceToNextRiftChallenge();
					}
				}
				this.ResetPrestige();
			}
			else
			{
				this.SetupNextChallenge();
				this.ResetPrestige();
			}
			this.isTransitioning = true;
		}

		public void OnDuckComplete()
		{
			this.activeChallenge.Reset();
			this.ResetPrestige();
			this.isTransitioning = true;
		}

		public double GetNumMythstonesOnPrestige(bool isMega)
		{
			return this.activeChallenge.GetNumMythstonesOnPrestige() * ((!isMega) ? 1.0 : 2.0);
		}

		public double GetNumMythstonesOnPrestigePure()
		{
			return this.activeChallenge.GetNumMythstonesOnPrestigePure();
		}

		public double GetNumMythstonesOnPrestigeArtifactBonus()
		{
			return this.activeChallenge.GetNumMythstonesOnPrestigeArtifactBonus();
		}

		public void SetupNextChallenge()
		{
			this.activeChallenge = null;
			for (int i = 0; i < this.allChallenges.Count; i++)
			{
				Challenge challenge = this.allChallenges[i];
				if (challenge.state != Challenge.State.WON)
				{
					this.activeChallenge = challenge;
					this.activeChallenge.Reset();
					break;
				}
			}
			if (this.activeChallenge == null)
			{
				this.activeChallenge = this.allChallenges[0];
				if (this.activeChallenge is ChallengeRift)
				{
					this.isCompleted = false;
				}
				else
				{
					this.isCompleted = true;
				}
			}
		}

		public void SelectNextCursedRiftOrRegular()
		{
			int num = this.cursedChallenges.IndexOf(this.activeChallenge);
			this.cursedChallenges.RemoveAt(num);
			if (this.cursedChallenges.Count > 0)
			{
				this.activeChallenge = this.cursedChallenges[(num >= this.cursedChallenges.Count) ? 0 : num];
			}
			else
			{
				int num2 = this.currentSim.lastSelectedRegularGateIndex;
				if (num2 >= this.allChallenges.Count)
				{
					num2 = this.allChallenges.Count - 1;
				}
				else if ((this.allChallenges[num2] as ChallengeRift).discoveryIndex > this.currentSim.riftDiscoveryIndex)
				{
					do
					{
						num2--;
					}
					while ((this.allChallenges[num2] as ChallengeRift).discoveryIndex > this.currentSim.riftDiscoveryIndex);
				}
				this.activeChallenge = this.allChallenges[num2];
			}
		}

		public Challenge GetNextCursedRiftOrRegular()
		{
			if (this.cursedChallenges.Count > 0)
			{
				return this.cursedChallenges[0];
			}
			int num = this.GetLatestBeatenRiftChallengeIndex() + 1;
			if (num >= this.allChallenges.Count)
			{
				num = this.allChallenges.Count - 1;
			}
			else if ((this.allChallenges[num] as ChallengeRift).discoveryIndex > this.currentSim.riftDiscoveryIndex)
			{
				num--;
			}
			return this.allChallenges[num];
		}

		public void AdvanceToNextRiftChallenge()
		{
			int num = this.allChallenges.IndexOf(this.activeChallenge);
			if (num >= this.allChallenges.Count - 1)
			{
				this.activeChallenge = this.allChallenges.GetLastItem<Challenge>();
			}
			else
			{
				ChallengeRift challengeRift = this.allChallenges[num + 1] as ChallengeRift;
				if (challengeRift.discoveryIndex > this.currentSim.riftDiscoveryIndex)
				{
					return;
				}
				this.activeChallenge = challengeRift;
			}
		}

		public void OnChallengeWon()
		{
			int num = 0;
			int i = 0;
			int count = this.allChallenges.Count;
			while (i < count)
			{
				if (this.allChallenges[i].state != Challenge.State.WON)
				{
					break;
				}
				num++;
				i++;
			}
			PlayerStats.OnChallengeWin(num);
			this.AddSoundEvent(new SoundEventUiVariedVoiceSimple(SoundArchieve.inst.voGreenManTimeChallengeComplete, "GREEN_MAN", 1f));
			this.InitRainResult();
		}

		public void OnChallengeUiConfirmWin()
		{
			if (this.activeChallenge is ChallengeWithTime)
			{
				ChallengeWithTime challengeWithTime = (ChallengeWithTime)this.activeChallenge;
				if (challengeWithTime.unlock.DoesRewardCurrency() && !UiManager.DEBUGDontShowTransitionInEditor)
				{
					this.advanceToNextRiftAfterRainGlory = (this.activeChallenge is ChallengeRift && !(this.activeChallenge as ChallengeRift).unlock.isCollected);
					this.InitRainGlory();
					if (!challengeWithTime.unlock.isCollected)
					{
						challengeWithTime.unlock.RainCurrency();
						if (this.activeChallenge is ChallengeRift)
						{
							this.currentSim.RecalculateUnlockedCursedRiftSlots();
						}
					}
					if (this.activeChallenge is ChallengeRift)
					{
						ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
						if (challengeRift.IsCursed())
						{
							this.currentSim.cursedGatesBeaten++;
						}
					}
					return;
				}
				this.earnedUnlock = challengeWithTime.unlock;
				if (this.activeChallenge is ChallengeRift)
				{
					ChallengeRift challengeRift2 = this.activeChallenge as ChallengeRift;
					if (challengeRift2.IsCursed())
					{
						this.SelectNextCursedRiftOrRegular();
					}
					else
					{
						this.activeChallenge.Reset();
						if (!challengeRift2.unlock.isCollected)
						{
							this.AdvanceToNextRiftChallenge();
						}
					}
				}
				else
				{
					this.SetupNextChallenge();
				}
				this.ResetPrestige();
			}
			this.shouldSave = true;
		}

		public void OnRiftWon()
		{
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			this.AddSoundEvent(new SoundEventUiVariedVoiceSimple(SoundArchieve.inst.voRiftComplete, "GREEN_MAN", 1f));
			this.sameRiftLoseCount = 0;
			this.InitRainResult();
			if (!challengeRift.unlock.isCollected && !challengeRift.IsCursed())
			{
				this.cachedLatestBeatenRiftChallengeIndex++;
				if (this.cachedLatestBeatenRiftChallengeIndex + 1 < this.allChallenges.Count)
				{
					this.cachedLatestUnlockedRiftChallengeIndex++;
				}
				if (this.allChallenges.IndexOf(challengeRift) == 4 && this.currentSim.ratingState <= RatingState.AskLater)
				{
					this.currentSim.shouldAskForRate = true;
				}
			}
			PlayerStats.numRiftWon++;
			if (challengeRift.IsCursed())
			{
				PlayerStats.numCursedRiftWon++;
				PlayfabManager.SendPlayerEvent(PlayfabEventId.GOG_CURSED_GATE_COMPLETED, new Dictionary<string, object>
				{
					{
						"num_cursed_gate_completed",
						PlayerStats.numCursedRiftWon
					}
				}, null, null, true);
			}
		}

		public void OnChallengeLost()
		{
			this.AddSoundEvent(new SoundEventUiVariedVoiceSimple(SoundArchieve.inst.voGreenManTimeChallengeFail, "GREEN_MAN", 1f));
			this.InitRainResult();
			if (this.gameMode == GameMode.CRUSADE)
			{
				int num = this.allChallenges.IndexOf(this.activeChallenge);
				PlayfabManager.SendPlayerEvent(PlayfabEventId.TIME_CHALLENGE_FAILED, new Dictionary<string, object>
				{
					{
						"index",
						num
					}
				}, null, null, true);
			}
		}

		public void OnRiftLost()
		{
			this.AddSoundEvent(new SoundEventUiVariedVoiceSimple(SoundArchieve.inst.voRiftFail, "GREEN_MAN", 1f));
		}

		public void OnRiftNoCompleted(int challengeId)
		{
			if (challengeId != this.lastRiftIdLost)
			{
				this.sameRiftLoseCount = 1;
				this.lastRiftIdLost = challengeId;
			}
			else
			{
				this.sameRiftLoseCount++;
			}
			this.InitRainResult();
		}

		public void OnTimeChallengeNotCompleted(int challengeId)
		{
			if (challengeId != this.lastTimeChallengeIdLost)
			{
				this.sameTimeChallengeLoseCount = 1;
				this.lastTimeChallengeIdLost = challengeId;
			}
			else
			{
				this.sameTimeChallengeLoseCount++;
			}
			this.currentSim.timeChallengesLostCount++;
			this.InitRainResult();
		}

		public void OnChallengeUiConfirmLose()
		{
			this.InitDuck();
			this.shouldSave = true;
		}

		private void UpdateTotem(float dt, Taps taps)
		{
			if (this.totem != null)
			{
				bool autoTap = this.autoTapTimeLeft > 0f;
				this.autoTapTimeLeft = Mathf.Max(0f, this.autoTapTimeLeft - dt);
				this.totem.UpdateTotem(dt, taps, autoTap);
			}
		}

		public Enemy GetClosestEnemy(Vector3 simPos)
		{
			Enemy result = null;
			float num = float.PositiveInfinity;
			foreach (Enemy enemy in this.activeChallenge.enemies)
			{
				if (!enemy.IsDead())
				{
					if (!enemy.IsInvulnerable())
					{
						float num2 = GameMath.D2xy(simPos, enemy.pos);
						if (num2 < num)
						{
							num = num2;
							result = enemy;
						}
					}
				}
			}
			return result;
		}

		public Hero GetClosestHero(Vector3 simPos)
		{
			Hero result = null;
			float num = float.PositiveInfinity;
			foreach (Hero hero in this.heroes)
			{
				if (!hero.IsDead())
				{
					if (!hero.IsInvulnerable())
					{
						float num2 = GameMath.D2xy(simPos, hero.pos);
						if (num2 < num)
						{
							num = num2;
							result = hero;
						}
					}
				}
			}
			return result;
		}

		private void UpdateHeroes(float dt, float blackCurtainTimeFactor)
		{
			foreach (Hero hero in this.heroes)
			{
				float num = dt;
				if (hero.CanBeSlowedByBlackCurtain())
				{
					num *= blackCurtainTimeFactor;
				}
				hero.Update(num, (this.hotCocoaTimeLeft <= 0f) ? 0 : 8);
				if (this.autoSkillDistribute && hero.GetNumUnspentSkillPoints() > 0)
				{
					List<World.SkillIndex> list = new List<World.SkillIndex>();
					if (hero.CanUpgradeSkillUlti())
					{
						list.Add(World.SkillIndex.Create(-1, 0));
					}
					int skillBranchLength = hero.GetSkillBranchLength(0);
					for (int i = 0; i < skillBranchLength; i++)
					{
						if (hero.CanUpgradeSkill(0, i))
						{
							list.Add(World.SkillIndex.Create(0, i));
						}
						if (hero.CanUpgradeSkill(1, i))
						{
							list.Add(World.SkillIndex.Create(1, i));
						}
					}
					if (list.Count > 0)
					{
						World.SkillIndex randomListElement = list.GetRandomListElement<World.SkillIndex>();
						if (randomListElement.branch == -1)
						{
							hero.TryUpgradeSkillUlti();
						}
						else
						{
							hero.TryUpgradeSkill(randomListElement.branch, randomListElement.index);
						}
					}
				}
			}
		}

		public float GetWaveSkipChance()
		{
			return this.universalBonus.waveSkipChanceAdd;
		}

		public LevelSkipPairs GetLevelSkipPairs()
		{
			return this.universalBonus.stageSkipPair;
		}

		public void DamageFuture(Unit damager, UnitHealthy damaged, Damage damage, float time)
		{
			FutureDamage item = new FutureDamage(damager, damaged, damage, time);
			this.futureDamages.Add(item);
		}

		public void DamageMain(Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (!damage.isExact)
			{
				damage.amount = GameMath.GetRandomDouble(damage.amount * 0.85, damage.amount * 1.15, GameMath.RandType.NoSeed);
			}
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			if (challengeRift != null)
			{
				foreach (RiftEffect riftEffect in challengeRift.riftEffects)
				{
					riftEffect.OnPreDamage(this, damager, damaged, damage);
				}
				foreach (EnchantmentBuff enchantmentBuff in challengeRift.allEnchantments)
				{
					enchantmentBuff.OnPreDamage(damager, damaged, damage);
				}
			}
			if (damager != null)
			{
				damager.OnPreDamage(damaged, damage);
			}
			damaged.TakeDamage(damage, damager, 0.0);
			if (challengeRift != null)
			{
				foreach (RiftEffect riftEffect2 in challengeRift.riftEffects)
				{
					riftEffect2.OnPostDamage(this, damager, damaged, damage);
				}
				foreach (EnchantmentBuff enchantmentBuff2 in challengeRift.allEnchantments)
				{
					enchantmentBuff2.OnPostDamage(damager, damaged, damage);
				}
			}
			if (damager != null)
			{
				damager.OnPostDamage(damaged, damage);
			}
		}

		public void AddProjectile(Projectile newProjectile)
		{
			this.projectiles.Add(newProjectile);
		}

		private void UpdateProjectiles(float dt)
		{
			int num = this.projectiles.Count;
			for (int i = num - 1; i >= 0; i--)
			{
				Projectile projectile = this.projectiles[i];
				projectile.Update(dt);
				if (!projectile.hasDamaged && projectile.GetFlyRatio() >= projectile.damageMomentTimeRatio)
				{
					projectile.hasDamaged = true;
					this.OnProjectileReached(projectile);
				}
				if (projectile.HasReached())
				{
					this.projectiles[i] = this.projectiles[--num];
					this.projectiles.RemoveAt(num);
				}
			}
		}

		private void UpdateTreasureDrops(float dt)
		{
			int count = this.treasureDrops.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				TreasureDrop treasureDrop = this.treasureDrops[i];
				treasureDrop.Update(dt);
				if (treasureDrop.state == TreasureDrop.State.REMOVE)
				{
					this.treasureDrops.RemoveAt(i);
				}
			}
		}

		private void UpdateSwarmDragons(float dt)
		{
			int num = this.swarmDragons.Count;
			for (int i = num - 1; i >= 0; i--)
			{
				SwarmDragon swarmDragon = this.swarmDragons[i];
				swarmDragon.Update(dt);
				if (swarmDragon.target == null || !swarmDragon.target.IsAlive())
				{
					swarmDragon.target = this.GetRandomAliveEnemyNontransitioning();
				}
				if (swarmDragon.target != null)
				{
					swarmDragon.targetPos = swarmDragon.target.pos;
					swarmDragon.targetDirection = swarmDragon.targetPos - swarmDragon.pos;
					swarmDragon.seekTime = 0f;
				}
				else
				{
					swarmDragon.seekTime += dt;
					swarmDragon.targetDirection = swarmDragon.targetPos - swarmDragon.pos;
				}
				Vector2 normalized = swarmDragon.targetDirection.normalized;
				if (swarmDragon.state == SwarmDragon.State.DEAD)
				{
					this.swarmDragons[i] = this.swarmDragons[--num];
					this.swarmDragons.RemoveAt(num);
				}
				else if (swarmDragon.state == SwarmDragon.State.INITIAL_KICK)
				{
					SwarmDragon swarmDragon2 = swarmDragon;
					swarmDragon2.pos.x = swarmDragon2.pos.x + normalized.x * dt * swarmDragon.speed;
					if (swarmDragon.stateTime >= 0.2f)
					{
						swarmDragon.currentRotation = GameMath.GetRandomFloat(-1.57079637f, 1.57079637f, GameMath.RandType.NoSeed);
						swarmDragon.SetState(SwarmDragon.State.HEADING);
					}
				}
				else if (swarmDragon.state == SwarmDragon.State.HEADING)
				{
					swarmDragon.targetRotation = Mathf.Atan2(swarmDragon.targetDirection.y, swarmDragon.targetDirection.x) * 57.29578f;
					float num2 = 1f + swarmDragon.stateTime * 1.6f;
					if (swarmDragon.target == null)
					{
						num2 = 1f;
					}
					swarmDragon.currentRotation = Mathf.LerpAngle(swarmDragon.currentRotation * 57.29578f, swarmDragon.targetRotation, dt * swarmDragon.rotationSpeed * num2) * 0.0174532924f;
					swarmDragon.direction = new Vector2(Mathf.Cos(swarmDragon.currentRotation), Mathf.Sin(swarmDragon.currentRotation));
					Vector2 direction = swarmDragon.direction;
					if (swarmDragon.target == null)
					{
						swarmDragon.speedMult -= dt * 2f;
						if (swarmDragon.speedMult < 0.4f)
						{
							swarmDragon.speedMult = 0.4f;
						}
					}
					else
					{
						swarmDragon.speedMult += dt * 2f;
						if (swarmDragon.speedMult > 1f)
						{
							swarmDragon.speedMult = 1f;
						}
					}
					SwarmDragon swarmDragon3 = swarmDragon;
					swarmDragon3.pos.x = swarmDragon3.pos.x + direction.x * dt * swarmDragon.speed * swarmDragon.speedMult;
					SwarmDragon swarmDragon4 = swarmDragon;
					swarmDragon4.pos.y = swarmDragon4.pos.y + direction.y * dt * swarmDragon.speed * swarmDragon.speedMult;
					float magnitude = swarmDragon.targetDirection.magnitude;
					if (magnitude <= 0.1f && swarmDragon.target != null)
					{
						this.DamageMain(swarmDragon.by, swarmDragon.target, swarmDragon.damage);
						swarmDragon.SetState(SwarmDragon.State.HIT);
					}
					else if (swarmDragon.totalTime > 15f)
					{
						swarmDragon.SetState(SwarmDragon.State.DISAPPEAR);
					}
				}
				else if (swarmDragon.state == SwarmDragon.State.HIT)
				{
					float magnitude2 = swarmDragon.targetDirection.magnitude;
					if (magnitude2 <= 0.1f && swarmDragon.target != null)
					{
						swarmDragon.pos = GameMath.Lerp(swarmDragon.pos, swarmDragon.targetPos, swarmDragon.stateTime / 0.3f);
					}
					if (swarmDragon.stateTime >= 0.3f)
					{
						swarmDragon.SetState(SwarmDragon.State.DISAPPEAR);
					}
				}
				else if (swarmDragon.state == SwarmDragon.State.DISAPPEAR && swarmDragon.stateTime >= 0.4f)
				{
					swarmDragon.SetState(SwarmDragon.State.DEAD);
				}
			}
		}

		private void UpdateDruidStampedes(float dt)
		{
			int num = this.stampedes.Count;
			for (int i = num - 1; i >= 0; i--)
			{
				Stampede stampede = this.stampedes[i];
				stampede.Update(dt);
				switch (stampede.state)
				{
				case Stampede.State.HEADING:
					stampede.pos += stampede.movement * dt;
					if (stampede.stateTime >= stampede.timeToReachTarget)
					{
						stampede.SetState(Stampede.State.HIT);
					}
					break;
				case Stampede.State.HIT:
					for (int j = this.activeChallenge.enemies.Count - 1; j >= 0; j--)
					{
						this.DamageMain(stampede.by, this.activeChallenge.enemies[j], stampede.damage.GetCopy());
					}
					stampede.SetState(Stampede.State.DISAPPEAR);
					break;
				case Stampede.State.DISAPPEAR:
					stampede.pos += stampede.movement * dt;
					if (stampede.stateTime >= 1.5f)
					{
						stampede.SetState(Stampede.State.DEAD);
					}
					break;
				case Stampede.State.DEAD:
					this.stampedes[i] = this.stampedes[--num];
					this.stampedes.RemoveAt(num);
					break;
				default:
					throw new NotImplementedException();
				}
			}
		}

		private void UpdateSupportAnimals(float dt)
		{
			int num = this.supportAnimals.Count;
			for (int i = num - 1; i >= 0; i--)
			{
				SupportAnimal supportAnimal = this.supportAnimals[i];
				supportAnimal.Update(dt);
				switch (supportAnimal.state)
				{
				case SupportAnimal.State.SELECTING_TARGET:
					supportAnimal.target = this.GetRandomAliveHero();
					if (supportAnimal.target != null)
					{
						Vector3 a = new Vector3(supportAnimal.target.pos.x + 0.15f, supportAnimal.target.pos.y - 0.01f);
						Vector3 vector = a - supportAnimal.pos;
						supportAnimal.movement = vector.normalized * supportAnimal.speed;
						supportAnimal.timeToReachTarget = vector.magnitude / supportAnimal.speed;
						supportAnimal.SetState(SupportAnimal.State.HEADING);
						if (supportAnimal.spawnSound != null)
						{
							this.AddSoundEvent(supportAnimal.spawnSound);
						}
					}
					else
					{
						supportAnimal.SetState(SupportAnimal.State.DEAD);
					}
					break;
				case SupportAnimal.State.HEADING:
					if (supportAnimal.stateTime >= supportAnimal.timeToReachTarget)
					{
						supportAnimal.SetState(SupportAnimal.State.GIVING_BUFF);
						supportAnimal.pos = new Vector3(supportAnimal.target.pos.x + 0.15f, supportAnimal.target.pos.y - 0.01f);
					}
					else
					{
						supportAnimal.pos += supportAnimal.movement * dt;
					}
					break;
				case SupportAnimal.State.GIVING_BUFF:
					if (supportAnimal.stateTime >= 0.3f)
					{
						if (!supportAnimal.buffGiven)
						{
							supportAnimal.target.AddBuff(supportAnimal.buff, 0, false);
							supportAnimal.buffGiven = true;
						}
						if (supportAnimal.stateTime >= 0.733f)
						{
							supportAnimal.SetState(SupportAnimal.State.RETURNING);
							if (supportAnimal.disappearSound != null)
							{
								this.AddSoundEvent(supportAnimal.disappearSound);
							}
						}
					}
					break;
				case SupportAnimal.State.RETURNING:
					if (supportAnimal.stateTime >= supportAnimal.timeToReachTarget)
					{
						supportAnimal.SetState(SupportAnimal.State.DEAD);
						supportAnimal.pos = supportAnimal.initialPos;
					}
					else
					{
						supportAnimal.pos -= supportAnimal.movement * dt;
					}
					break;
				case SupportAnimal.State.DEAD:
					this.supportAnimals[i] = this.supportAnimals[--num];
					this.supportAnimals.RemoveAt(num);
					break;
				default:
					throw new NotImplementedException();
				}
			}
		}

		private void OnProjectileReached(Projectile projectile)
		{
			if (projectile.TargetsSingleAlly())
			{
				Vector3 posEnd = projectile.GetPosEnd();
				UnitHealthy unitHealthy;
				if (projectile.targetLocked)
				{
					unitHealthy = projectile.target;
				}
				else
				{
					unitHealthy = this.GetProjectileHitHero(posEnd);
				}
				if (unitHealthy != null)
				{
					this.ApplyProjectileEffectDirect(projectile, unitHealthy, projectile.damage);
				}
			}
			else if (projectile.TargetNone())
			{
				this.ApplyProjectileEffectDirect(projectile, null, projectile.damage);
			}
			else if (projectile.TargetsSingleEnemy())
			{
				Vector3 posEnd2 = projectile.GetPosEnd();
				UnitHealthy unitHealthy2;
				if (projectile.targetLocked)
				{
					unitHealthy2 = projectile.target;
				}
				else
				{
					unitHealthy2 = this.GetProjectileHitEnemy(posEnd2);
				}
				if (unitHealthy2 != null)
				{
					this.ApplyProjectileEffectDirect(projectile, unitHealthy2, projectile.damage);
				}
			}
			else
			{
				if (!projectile.TargetsAllEnemies())
				{
					throw new NotImplementedException();
				}
				foreach (Enemy damaged in this.activeChallenge.enemies)
				{
					this.ApplyProjectileEffectDirect(projectile, damaged, projectile.damage);
				}
			}
			if (projectile.damageArea != null)
			{
				Vector3 posEnd3 = projectile.GetPosEnd();
				float r = projectile.damageAreaR * projectile.damageAreaR;
				foreach (UnitHealthy unitHealthy3 in this.activeChallenge.enemies)
				{
					if (unitHealthy3 != projectile.target)
					{
						if (GameMath.AreInsideRangeXY(posEnd3, unitHealthy3.pos, r))
						{
							this.ApplyProjectileEffectDirect(projectile, unitHealthy3, projectile.damageArea);
						}
					}
				}
			}
			if (projectile.visualEffect != null)
			{
				VisualEffect visualEffect = projectile.visualEffect;
				if (visualEffect.skinIndex == -1)
				{
					visualEffect.skinIndex = projectile.visualVariation;
				}
				visualEffect.pos = projectile.GetPosEnd();
				Vector3 vector = GameMath.ConvertToScreenSpace(projectile.GetDir());
				float rot = Mathf.Atan2(vector.y, vector.x) * 180f / 3.14159274f;
				visualEffect.rot = rot;
				visualEffect.time = 0f;
				this.visualEffects.Add(visualEffect);
			}
			if (projectile.soundImpact != null)
			{
				this.AddSoundEvent(projectile.soundImpact);
			}
		}

		private void ApplyProjectileEffectDirect(Projectile projectile, UnitHealthy damaged, Damage damage)
		{
			Unit by = projectile.by;
			if (damage != null)
			{
				Damage copy = damage.GetCopy();
				this.DamageMain(by, damaged, copy);
			}
			List<BuffData> buffs = projectile.buffs;
			if (buffs != null)
			{
				foreach (BuffData buffData in buffs)
				{
					damaged.AddBuff(buffData, 0, false);
				}
			}
			if (projectile.onHit != null)
			{
				projectile.onHit(projectile.target);
			}
		}

		public Enemy GetProjectileHitEnemy(Vector3 projectilePos)
		{
			Enemy result = null;
			float num = float.PositiveInfinity;
			foreach (Enemy enemy in this.activeChallenge.enemies)
			{
				Vector3 b = enemy.pos + enemy.GetProjectileTargetOffset();
				float num2 = GameMath.D2xy(projectilePos, b);
				float projectileTargetRandomness = enemy.GetProjectileTargetRandomness();
				if (num2 <= projectileTargetRandomness * projectileTargetRandomness)
				{
					if (num2 < num)
					{
						num = num2;
						result = enemy;
					}
				}
			}
			return result;
		}

		public Hero GetProjectileHitHero(Vector3 projectilePos)
		{
			Hero result = null;
			float num = float.PositiveInfinity;
			foreach (Hero hero in this.heroes)
			{
				Vector3 b = hero.pos + hero.GetProjectileTargetOffset();
				float num2 = GameMath.D2xy(projectilePos, b);
				float projectileTargetRandomness = hero.GetProjectileTargetRandomness();
				if (num2 <= projectileTargetRandomness * projectileTargetRandomness + 0.1f)
				{
					if (num2 < num)
					{
						num = num2;
						result = hero;
					}
				}
			}
			return result;
		}

		private void UpdateDrops(float dt, Taps taps)
		{
			this.finishedDrops.Clear();
			foreach (Drop drop in this.drops)
			{
				Drop.State state = drop.state;
				if (taps != null && drop.state == Drop.State.GROUND)
				{
					int num = taps.GetNumNew();
					for (int i = 0; i < num; i++)
					{
						this.CheckTap(taps.GetNewSimTap(i), drop);
					}
					num = taps.GetNumOld();
					for (int j = 0; j < num; j++)
					{
						this.CheckTap(taps.GetOldSimTap(j), drop);
					}
				}
				drop.Update(dt);
				if (state == Drop.State.GROUND && drop.state == Drop.State.FLY_TO_INV)
				{
					this.OnDropCollected(drop);
				}
				if (drop.IsTimeToCollect())
				{
					this.finishedDrops.Add(drop);
					if (drop is DropPowerup)
					{
						SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, this.soundPowerup);
						this.AddSoundEvent(e);
					}
					else if (drop is DropCurrency)
					{
						DropCurrency dropCurrency = drop as DropCurrency;
						SoundEventSound soundEventSound;
						switch (dropCurrency.currencyType)
						{
						case CurrencyType.GOLD:
							soundEventSound = new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, this.soundGoldCollect);
							break;
						case CurrencyType.SCRAP:
							soundEventSound = new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, this.soundScrapsCollect);
							break;
						case CurrencyType.MYTHSTONE:
							soundEventSound = new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, this.soundMythstonesCollect);
							break;
						case CurrencyType.GEM:
							soundEventSound = new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, this.soundCreditsCollect);
							break;
						case CurrencyType.TOKEN:
							soundEventSound = new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, this.soundTokensCollect);
							break;
						case CurrencyType.AEON:
							soundEventSound = new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, this.soundTokensCollect);
							break;
						case CurrencyType.CANDY:
							soundEventSound = new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, this.soundMythstonesCollect);
							break;
						case CurrencyType.TRINKET_BOX:
							soundEventSound = null;
							break;
						default:
							throw new NotImplementedException();
						}
						if (soundEventSound != null)
						{
							this.AddSoundEvent(soundEventSound);
						}
					}
				}
			}
			foreach (Drop drop2 in this.finishedDrops)
			{
				drop2.Apply(this);
				this.drops.Remove(drop2);
			}
		}

		private void CheckTap(Vector3 tapPos, Drop drop)
		{
			float num = GameMath.D2xy(tapPos, drop.pos);
			if (num <= 0.0324f)
			{
				drop.CollectManually();
			}
		}

		private void OnDropCollected(Drop drop)
		{
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			if (challengeRift == null)
			{
				return;
			}
			foreach (RiftEffect riftEffect in challengeRift.riftEffects)
			{
				riftEffect.OnCollectDrop(drop, this);
			}
			foreach (EnchantmentBuff enchantmentBuff in challengeRift.allEnchantments)
			{
				enchantmentBuff.OnCollectDrop(drop);
			}
		}

		public void AddGold(double amount)
		{
			if (this.activeChallenge is ChallengeRift)
			{
				ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
				if (!this.isCollectingDropsInImmediateMode)
				{
					foreach (EnchantmentBuff enchantmentBuff in challengeRift.allEnchantments)
					{
						enchantmentBuff.OnCollectGold();
					}
				}
			}
			TutorialManager.OnGoldCollected(amount);
			this.gold.Increment(amount);
		}

		public float GetGoldMultTenChance()
		{
			return this.universalBonus.goldMultTenChanceAdd;
		}

		public double GetEpicBossMythstoneDrop(int stage)
		{
			return (double)stage * 0.03 * GameMath.PowDouble(1.03, (double)(stage - 80) * 0.5) * this.universalBonus.epicBossDropMythstonesFactor;
		}

		public List<bool> GetMainSkillIsActives()
		{
			List<bool> list = new List<bool>();
			this.FillMainSkillIsActives(list);
			return list;
		}

		public void FillMainSkillIsActives(List<bool> res)
		{
			foreach (Hero hero in this.heroes)
			{
				res.Add(hero.IsSkillActive(0));
			}
		}

		public List<bool> GetMainSkillIsTogglable()
		{
			List<bool> list = new List<bool>();
			this.FillMainSkillIsTogglable(list);
			return list;
		}

		public void FillMainSkillIsTogglable(List<bool> res)
		{
			foreach (Hero hero in this.heroes)
			{
				res.Add(hero.IsSkillTogglable(0));
			}
		}

		public List<float> GetMainSkillToggleDelta()
		{
			List<float> list = new List<float>();
			this.FillMainSkillToggleDelta(list);
			return list;
		}

		public void FillMainSkillToggleDelta(List<float> res)
		{
			foreach (Hero hero in this.heroes)
			{
				res.Add(hero.GetToggleDelta(0));
			}
		}

		public List<bool> GetMainSkillIsToggling()
		{
			List<bool> list = new List<bool>();
			this.FillMainSkillIsToggling(list);
			return list;
		}

		public void FillMainSkillIsToggling(List<bool> res)
		{
			foreach (Hero hero in this.heroes)
			{
				res.Add(hero.IsSkillToggling(0));
			}
		}

		public List<bool> GetMainSkillCanActivates()
		{
			List<bool> list = new List<bool>();
			this.FillMainSkillCanActivates(list);
			return list;
		}

		public void FillMainSkillCanActivates(List<bool> res)
		{
			foreach (Hero hero in this.heroes)
			{
				res.Add(hero.CanActivateSkill(0));
			}
		}

		public List<float> GetMainSkillCooldownRatios()
		{
			List<float> list = new List<float>();
			this.FillMainSkillCooldownRatios(list);
			return list;
		}

		public void FillMainSkillCooldownRatios(List<float> res)
		{
			foreach (Hero hero in this.heroes)
			{
				res.Add(hero.GetMainSkillCooldownTimeRatio());
			}
		}

		public List<float> GetAutoSkillCooldownRatios(int index)
		{
			List<float> list = new List<float>();
			this.FillAutoSkillCooldownRatios(index, list);
			return list;
		}

		public void FillAutoSkillCooldownRatios(int index, List<float> res)
		{
			foreach (Hero hero in this.heroes)
			{
				if (index == 1)
				{
					res.Add(hero.GetRightSideAutoAtiveColldownRate());
				}
				else
				{
					if (index != 0)
					{
						throw new Exception("GetAutoSkillCooldownRatios failed, index: " + index);
					}
					res.Add(hero.GetLeftSideAutoAtiveColldownRate());
				}
			}
		}

		public List<float> GetMainSkillCooldownMaxes()
		{
			List<float> list = new List<float>();
			this.FillMainSkillCooldownMaxes(list);
			return list;
		}

		public void FillMainSkillCooldownMaxes(List<float> res)
		{
			foreach (Hero hero in this.heroes)
			{
				res.Add(hero.GetMainSkillCooldownMax());
			}
		}

		public List<float> GetHeroReviveTimes()
		{
			List<float> list = new List<float>();
			this.FillHeroReviveTimes(list);
			return list;
		}

		public void FillHeroReviveTimes(List<float> res)
		{
			foreach (Hero hero in this.heroes)
			{
				res.Add(hero.GetTillReviveTime());
			}
		}

		public List<float> GetHeroReviveTimeMaxes()
		{
			List<float> list = new List<float>();
			this.FillHeroReviveTimeMaxes(list);
			return list;
		}

		public void FillHeroReviveTimeMaxes(List<float> res)
		{
			foreach (Hero hero in this.heroes)
			{
				res.Add(hero.GetReviveDuration());
			}
		}

		public List<bool> GetHeroRechargeBuffs()
		{
			List<bool> list = new List<bool>();
			this.FillHeroRechargeBuffs(list);
			return list;
		}

		public void FillHeroRechargeBuffs(List<bool> res)
		{
			foreach (Hero hero in this.heroes)
			{
				res.Add((hero.statCache.skillCoolFactor > 1f || this.buffTotalEffect.heroUltiCoolFactor > 1f) && !hero.CanActivateSkill(0) && !hero.IsSkillActive(0));
			}
		}

		public List<bool> GetHeroStunnedBuffs()
		{
			List<bool> list = new List<bool>();
			this.FillHeroStunnedBuffs(list);
			return list;
		}

		public void FillHeroStunnedBuffs(List<bool> res)
		{
			foreach (Hero hero in this.heroes)
			{
				res.Add(hero.IsStunned());
			}
		}

		public void FillHeroSilencedBuffs(List<bool> res)
		{
			foreach (Hero hero in this.heroes)
			{
				res.Add(hero.IsSilenced());
			}
		}

		public List<HeroDataBase.UltiCatagory> GetUltiCategories()
		{
			List<HeroDataBase.UltiCatagory> list = new List<HeroDataBase.UltiCatagory>();
			foreach (Hero hero in this.heroes)
			{
				list.Add(hero.GetData().GetDataBase().ultiCatagory);
			}
			return list;
		}

		public List<Type> GetSkillTypes()
		{
			List<Type> list = new List<Type>();
			this.FillSkillTypes(list);
			return list;
		}

		public void FillSkillTypes(List<Type> res)
		{
			foreach (Hero hero in this.heroes)
			{
				res.Add(hero.GetUltiSkillType());
			}
		}

		public double GetTotEnemyHealth()
		{
			double num = 0.0;
			foreach (Enemy enemy in this.activeChallenge.enemies)
			{
				num += enemy.GetHealth();
			}
			return num;
		}

		public double GetTotEnemyHealthMax()
		{
			return this.activeChallenge.waveTotHealthMax;
		}

		public string GetTotemName()
		{
			if (this.totem == null)
			{
				return "$none$";
			}
			return this.totem.name;
		}

		public int GetTotemLevel()
		{
			if (this.totem == null)
			{
				return -1;
			}
			return this.totem.GetLevel();
		}

		public int GetTotemXp()
		{
			if (this.totem == null)
			{
				return -1;
			}
			return this.totem.GetXp();
		}

		public void SetTotemLevel(int level, int xp)
		{
			if (this.totem == null)
			{
				return;
			}
			this.totem.SetLevel(level, xp);
		}

		public int GetTotemXpNeedForNextLevel()
		{
			if (this.totem == null)
			{
				return -1;
			}
			return this.totem.GetXpNeedForNextLevel();
		}

		public double GetTotemDamageNonBuffed()
		{
			if (this.totem == null)
			{
				return -1.0;
			}
			return this.totem.GetData().damage;
		}

		public double GetTotemDamageAll()
		{
			if (this.totem == null)
			{
				return -1.0;
			}
			return this.totem.GetDamageAll();
		}

		public double GetTotemDamageDifUpgrade()
		{
			if (this.totem == null)
			{
				return -1.0;
			}
			return this.totem.GetDamageDifUpgrade();
		}

		public double GetTotemUpgradeCost()
		{
			if (this.totem == null)
			{
				return -1.0;
			}
			return this.totem.GetUpgradeCost(true);
		}

		public bool CanAffordTotemUpgrade()
		{
			return this.gold.CanAfford(this.GetTotemUpgradeCost());
		}

		public int GetNumAliveEnemies()
		{
			int num = 0;
			foreach (Enemy enemy in this.activeChallenge.enemies)
			{
				if (!enemy.IsDead())
				{
					num++;
				}
			}
			return num;
		}

		public int GetNumHeroesAliveExcluding(Unit excluded)
		{
			int num = 0;
			foreach (Hero hero in this.heroes)
			{
				if (!hero.IsDead() && hero != (Hero)excluded)
				{
					num++;
				}
			}
			return num;
		}

		public double GetHeroTeamDps()
		{
			double num = 0.0;
			foreach (Hero hero in this.heroes)
			{
				num += hero.GetDps();
			}
			return num;
		}

		public double GetEnemyTeamDps()
		{
			double num = 0.0;
			foreach (Enemy enemy in this.activeChallenge.enemies)
			{
				num += enemy.GetDps();
			}
			return num;
		}

		public Hero GetRandomHero()
		{
			int count = this.heroes.Count;
			if (count == 0)
			{
				return null;
			}
			int randomInt = GameMath.GetRandomInt(0, count, GameMath.RandType.NoSeed);
			return this.heroes[randomInt];
		}

		public Hero GetRandomAliveHero()
		{
			this.heroCandidates.Clear();
			foreach (Hero hero in this.heroes)
			{
				if (hero.IsAlive())
				{
					this.heroCandidates.Add(hero);
				}
			}
			int count = this.heroCandidates.Count;
			if (count == 0)
			{
				return null;
			}
			int randomInt = GameMath.GetRandomInt(0, count, GameMath.RandType.NoSeed);
			return this.heroCandidates[randomInt];
		}

		public Hero GetRandomAliveHeroExcluding(Unit excluded)
		{
			this.heroCandidates.Clear();
			foreach (Hero hero in this.heroes)
			{
				if (hero.IsAlive() && hero != excluded)
				{
					this.heroCandidates.Add(hero);
				}
			}
			int count = this.heroCandidates.Count;
			if (count == 0)
			{
				return null;
			}
			int randomInt = GameMath.GetRandomInt(0, count, GameMath.RandType.NoSeed);
			return this.heroCandidates[randomInt];
		}

		public List<Hero> GetNumberOfRandomAliveHeroExcluding(Unit excluded, int count)
		{
			List<Hero> list = new List<Hero>();
			this.heroCandidates.Clear();
			foreach (Hero hero in this.heroes)
			{
				if (hero.IsAlive() && hero != excluded)
				{
					this.heroCandidates.Add(hero);
				}
			}
			while (this.heroCandidates.Count > 0 && count > 0)
			{
				count--;
				int randomInt = GameMath.GetRandomInt(0, this.heroCandidates.Count, GameMath.RandType.NoSeed);
				list.Add(this.heroCandidates[randomInt]);
				this.heroCandidates.RemoveAt(randomInt);
			}
			return list;
		}

		public Hero GetRandomAliveHeroExcludingList(List<Unit> excluded)
		{
			this.heroCandidates.Clear();
			foreach (Hero hero in this.heroes)
			{
				if (hero.IsAlive() && !excluded.Contains(hero))
				{
					this.heroCandidates.Add(hero);
				}
			}
			int count = this.heroCandidates.Count;
			if (count == 0)
			{
				return null;
			}
			int randomInt = GameMath.GetRandomInt(0, count, GameMath.RandType.NoSeed);
			return this.heroCandidates[randomInt];
		}

		public Hero GetAliveHeroWithMinHealthExcluding(Unit excluded)
		{
			Hero hero = null;
			foreach (Hero hero2 in this.heroes)
			{
				if (!hero2.IsDead())
				{
					if (hero2 != excluded)
					{
						if (hero == null || hero2.GetHealthRatio() < hero.GetHealthRatio())
						{
							hero = hero2;
						}
					}
				}
			}
			return hero;
		}

		public int GetNumAttackableHeroes()
		{
			int num = 0;
			foreach (Hero hero in this.heroes)
			{
				if (hero.IsAttackable())
				{
					num++;
				}
			}
			return num;
		}

		public Hero GetHeroToAttack()
		{
			List<Hero> list = new List<Hero>();
			foreach (Hero hero in this.heroes)
			{
				if (hero.IsAttackable())
				{
					list.Add(hero);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			float num = 0f;
			foreach (Hero hero2 in list)
			{
				num += hero2.GetTaunt();
			}
			float num2 = GameMath.GetRandomFloat(0f, num, GameMath.RandType.NoSeed);
			foreach (Hero hero3 in list)
			{
				num2 -= hero3.GetTaunt();
				if (num2 <= 0f)
				{
					return hero3;
				}
			}
			Hero hero4 = null;
			foreach (Hero hero5 in list)
			{
				if (hero4 == null || hero4.GetTaunt() < hero5.GetTaunt())
				{
					hero4 = hero5;
				}
			}
			return hero4;
		}

		public Enemy GetRandomEnemy()
		{
			int count = this.activeChallenge.enemies.Count;
			if (count == 0)
			{
				return null;
			}
			int randomInt = GameMath.GetRandomInt(0, count, GameMath.RandType.NoSeed);
			return this.activeChallenge.enemies[randomInt];
		}

		public Enemy GetRandomAliveEnemy()
		{
			this.enemyCandidates.Clear();
			foreach (Enemy enemy in this.activeChallenge.enemies)
			{
				if (enemy.IsAlive())
				{
					this.enemyCandidates.Add(enemy);
				}
			}
			int count = this.enemyCandidates.Count;
			if (count == 0)
			{
				return null;
			}
			int randomInt = GameMath.GetRandomInt(0, count, GameMath.RandType.NoSeed);
			return this.enemyCandidates[randomInt];
		}

		public Enemy GetRandomAliveEnemyNontransitioning()
		{
			this.enemyCandidates.Clear();
			foreach (Enemy enemy in this.activeChallenge.enemies)
			{
				if (enemy.IsAlive() && !enemy.IsSpawning(0.5f))
				{
					this.enemyCandidates.Add(enemy);
				}
			}
			int count = this.enemyCandidates.Count;
			if (count == 0)
			{
				return null;
			}
			int randomInt = GameMath.GetRandomInt(0, count, GameMath.RandType.NoSeed);
			return this.enemyCandidates[randomInt];
		}

		public float GetBossTimeScale()
		{
			return this.activeChallenge.GetBossTimeRatio();
		}

		public float GetBossTimePassed()
		{
			return this.activeChallenge.GetBossTimePassed();
		}

		public bool CanPrestigeNow()
		{
			return !this.isRainingGlory && this.activeChallenge.CanPrestigeNowExceptRainingGlory();
		}

		public int GetPrestigeReqStageNo()
		{
			return this.activeChallenge.GetPrestigeReqStageNo();
		}

		public double GetNewHeroCost()
		{
			return this.newHeroCosts[this.heroes.Count];
		}

		public bool CanAffordNewHero()
		{
			return this.gold.CanAfford(this.GetNewHeroCost());
		}

		public bool IsThereAnyAliveHero()
		{
			foreach (Hero hero in this.heroes)
			{
				if (hero.IsAlive())
				{
					return true;
				}
			}
			return false;
		}

		public bool IsEnvironmentForest()
		{
			return true;
		}

		public bool IsEnvironmentVillage()
		{
			return false;
		}

		public bool IsEnvironmentCity()
		{
			return false;
		}

		public bool IsEnvironmentMountains()
		{
			return false;
		}

		public void AddMythstone(double amount)
		{
			this.earnedMythstone += amount;
		}

		public void AddCredit(double amount)
		{
			this.earnedCredit += amount;
		}

		public void AddToken(double amount)
		{
			this.earnedToken += amount;
		}

		public void AddScrap(double amount)
		{
			this.earnedScrap += amount;
		}

		public void AddAeon(double amount)
		{
			this.earnedAeon += amount;
		}

		public void AddCandy(double amount)
		{
			this.earnedCandy += amount;
		}

		public void AddCandyCapped(double amount)
		{
			this.earnedCandyCapped += amount;
		}

		public void AddTrinketBox(int amount)
		{
			this.earnedTrinketBoxes += amount;
		}

		public double GetAndZeroEarnedMythstone()
		{
			double result = this.earnedMythstone;
			this.earnedMythstone = 0.0;
			return result;
		}

		public double GetAndZeroEarnedCredit()
		{
			double result = this.earnedCredit;
			this.earnedCredit = 0.0;
			return result;
		}

		public double GetAndZeroEarnedToken()
		{
			double result = this.earnedToken;
			this.earnedToken = 0.0;
			return result;
		}

		public double GetAndZeroEarnedAeon()
		{
			double result = this.earnedAeon;
			this.earnedAeon = 0.0;
			return result;
		}

		public double GetAndZeroEarnedCandy()
		{
			double result = this.earnedCandy;
			this.earnedCandy = 0.0;
			return result;
		}

		public double GetAndZeroEarnedCandyCapped()
		{
			double result = this.earnedCandyCapped;
			this.earnedCandyCapped = 0.0;
			return result;
		}

		public void AddEarnedRiftPoints(double amount)
		{
			this.earnedRiftPoints += amount;
		}

		public double GetAndZeroEarnedRiftPoints()
		{
			double result = this.earnedRiftPoints;
			this.earnedRiftPoints = 0.0;
			return result;
		}

		public int GetAndZeroEarnedTrinketBox()
		{
			int result = this.earnedTrinketBoxes;
			this.earnedTrinketBoxes = 0;
			return result;
		}

		public double GetAndZeroEarnedScrap()
		{
			double result = this.earnedScrap;
			this.earnedScrap = 0.0;
			return result;
		}

		public Unlock GetAndNullEarnedUnlock()
		{
			Unlock result = this.earnedUnlock;
			this.earnedUnlock = null;
			return result;
		}

		private Hero GetHero(string heroId)
		{
			Hero result = null;
			foreach (Hero hero in this.heroes)
			{
				if (hero.GetId() == heroId)
				{
					result = hero;
					break;
				}
			}
			return result;
		}

		public void TryUseSkill(int skillIndex)
		{
			if (skillIndex < 0 || skillIndex >= this.heroes.Count)
			{
				return;
			}
			Hero hero = this.heroes[skillIndex];
			hero.TryActivateMainSkill();
			TutorialManager.SkillUsed();
		}

		public void TryUpgradeTotem()
		{
			if (this.totem == null)
			{
				return;
			}
			if (this.CanAffordTotemUpgrade())
			{
				this.gold.Decrement(this.GetTotemUpgradeCost());
				this.totem.Upgrade();
				bool flag = this.totem.GetXp() + 1 == this.GetRequiredXpToLevelRing(this.totem.GetLevel());
				if (!this.totem.nextUpgradeFree && !flag && GameMath.GetProbabilityOutcome(this.universalBonus.freeUpgradeChance, GameMath.RandType.NoSeed))
				{
					this.totem.nextUpgradeFree = true;
				}
				else
				{
					this.totem.nextUpgradeFree = false;
				}
				TutorialManager.RingUpgraded();
			}
		}

		public void TryUpgradeHeroWithIndex(int index)
		{
			if (index >= this.heroes.Count)
			{
				return;
			}
			this.TryUpgradeHero(this.heroes[index]);
		}

		public void TryUpgradeHero(Hero hero)
		{
			double upgradeCost = hero.GetUpgradeCost(true);
			if (this.gold.CanAfford(upgradeCost))
			{
				this.gold.Decrement(upgradeCost);
				hero.Upgrade();
				bool flag = hero.GetXp() + 1 == hero.GetXpNeedForNextLevel();
				if (!hero.nextUpgradeFree && !flag && GameMath.GetProbabilityOutcome(this.universalBonus.freeUpgradeChance, GameMath.RandType.NoSeed))
				{
					hero.nextUpgradeFree = true;
				}
				else
				{
					hero.nextUpgradeFree = false;
				}
				this.maxHeroLevelReached = GameMath.GetMaxInt(this.maxHeroLevelReached, hero.GetLevel());
				TutorialManager.HeroUpgraded();
			}
		}

		public void TryLeaveBoss()
		{
			if (this.activeChallenge.CanLeaveBoss())
			{
				this.activeChallenge.LeaveBoss();
			}
		}

		public void TryGoToBoss()
		{
			if (this.activeChallenge.CanGoToBoss())
			{
				this.activeChallenge.GoToBoss();
			}
		}

		public bool TryPrestige(bool isMega)
		{
			if (this.CanPrestigeNow())
			{
				this.Prestige(isMega);
				return true;
			}
			return false;
		}

		public void BuyNewHero(HeroDataBase newBoughtHero, List<Gear> gears)
		{
			this.gold.Decrement(this.GetNewHeroCost());
			this.LoadNewHero(newBoughtHero, gears, false);
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, newBoughtHero.GetId(), true, 0f, newBoughtHero.soundVoSpawn);
			this.AddSoundEvent(e);
		}

		public void LoadNewHero(HeroDataBase newBoughtHero, List<Gear> gears, bool applyRandomSkinIfNecessary)
		{

			int count = this.heroes.Count;
			Hero hero = new Hero(newBoughtHero, this.heroLevelJumps[count], 0, 0, this, gears);
			hero.IncrementNumUnspentSkillPoints(this.activeChallenge.totalGainedUpgrades.skillPointsAdd);
			if (this.gameMode == GameMode.STANDARD)
			{
				hero.SetLevel(World.ADVENTURE_INTIAL_HERO_LEVELS[count]);
				hero.IncrementNumUnspentSkillPoints(World.ADVENTURE_INTIAL_HERO_LEVELS[count]);
				hero.Refresh();
			}
			else if (this.gameMode == GameMode.RIFT)
			{
				ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
				if (challengeRift == null)
				{
					throw new Exception("rift challenge not found");
				}
				hero.SetLevel(challengeRift.heroStartLevel);
				hero.IncrementNumUnspentSkillPoints(challengeRift.heroStartLevel + 1);
				hero.Refresh();
			}
			hero.pos = this.heroPoses[count];
			int num = -1;
			foreach (Hero hero2 in this.heroes)
			{
				if (hero2.GetId() == newBoughtHero.GetId())
				{
					num++;
				}
			}
			hero.SetDuplicateIndex(num);
			if (applyRandomSkinIfNecessary && newBoughtHero.randomSkinsEnabled)
			{
				newBoughtHero.equippedSkin = this.currentSim.GetHeroBoughtSkins(newBoughtHero.id).GetRandomListElement<SkinData>();
			}
			this.heroes.Add(hero);
		}

		public void ClearHeroes()
		{
			this.heroes.Clear();
		}

		public void LoadTotem(TotemDataBase dataBase, List<Rune> wornRunes)
		{
			this.totem = Totem.CreateTotem(dataBase, this.totemLevelJumps, 0, 0, this);
			this.totem.RefreshRunes(wornRunes);
			if (this.gameMode == GameMode.RIFT)
			{
				ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
				if (challengeRift == null)
				{
					throw new Exception("rift challenge not found");
				}
				this.totem.SetLevel(challengeRift.heroStartLevel);
				this.totem.Refresh();
			}
		}

		public void TryUpgradeSkill(Hero hero, int branchIndex, int skillIndex)
		{
			if (branchIndex < 0)
			{
				hero.TryUpgradeSkillUlti();
			}
			else
			{
				hero.TryUpgradeSkill(branchIndex, skillIndex);
			}
		}

		public bool CanUpgradeSkill(string heroId, int branchIndex, int skillIndex)
		{
			Hero hero = this.GetHero(heroId);
			if (branchIndex == -1)
			{
				return hero.CanUpgradeSkillUlti();
			}
			return hero.CanUpgradeSkill(branchIndex, skillIndex);
		}

		public void DEBUGchangeStage(int numStageChange)
		{
			this.activeChallenge.DEBUGchangeStage(numStageChange);
		}

		public int GetStageNumber()
		{
			return this.activeChallenge.GetStageNumber();
		}

		public void ZeroUltiCooldowns()
		{
			foreach (Hero hero in this.heroes)
			{
				hero.ZeroUltiCooldown();
			}
		}

		public void ZeroSkillCooldowns()
		{
			foreach (Hero hero in this.heroes)
			{
				hero.ZeroSkillCooldowns();
			}
		}

		public List<Unlock> GetUnlocks()
		{
			return this.unlocks;
		}

		public Unlock GetNextUnhiddenUnlock()
		{
			int i = 0;
			int count = this.unlocks.Count;
			while (i < count)
			{
				if ((!this.unlocks[i].isHidden && !this.unlocks[i].isCollected) || i == count - 1)
				{
					return this.unlocks[i];
				}
				i++;
			}
			return null;
		}

		public int GetNextUnlockStage()
		{
			int num = this.activeChallenge.GetStageNumber();
			if (this.activeChallenge is ChallengeStandard && ChallengeStandard.IsBossWave(this.activeChallenge.GetTotWave()))
			{
				num--;
			}
			int i = 0;
			int count = this.unlocks.Count;
			while (i < count)
			{
				Unlock unlock = this.unlocks[i];
				int nextUnlockStage = unlock.GetNextUnlockStage(num);
				if (nextUnlockStage >= 0)
				{
					return nextUnlockStage;
				}
				i++;
			}
			return -1;
		}

		public void DEBUGresetUnlocks()
		{
			this.SetModeLockedIfPossible();
			foreach (Unlock unlock in this.unlocks)
			{
				unlock.DEBUGreset();
			}
			this.maxStageReached = 0;
		}

		public int GetNumCollectedUnlocks()
		{
			if (this.activeChallenge is ChallengeStandard)
			{
				int num = 0;
				int i = 0;
				int count = this.unlocks.Count;
				while (i < count)
				{
					if (this.unlocks[i].isCollected)
					{
						num++;
					}
					i++;
				}
				return num;
			}
			if (this.activeChallenge is ChallengeWithTime)
			{
				for (int j = 0; j < this.allChallenges.Count; j++)
				{
					Challenge challenge = this.allChallenges[j];
					if (challenge.state != Challenge.State.WON)
					{
						return j;
					}
				}
				return this.allChallenges.Count;
			}
			throw new NotImplementedException();
		}

		public void LoadMaxStageReached(int stageNo)
		{
			this.maxStageReached = stageNo;
		}

		public int GetMaxStageReached()
		{
			return this.maxStageReached;
		}

		public void SetMaxHeroLevelReached(int level)
		{
			this.maxHeroLevelReached = level;
		}

		public int GetMaxHeroLevelReached()
		{
			return this.maxHeroLevelReached;
		}

		public float GetTimeSpeedFactor()
		{
			return (this.timeWarpTimeLeft <= 0f) ? 1f : this.timeWarpSpeed;
		}

		public void AddSoundEvent(SoundEvent e)
		{
			this.sounds.Add(e);
		}

		public string GetId()
		{
			return World.GetIdFromMode(this.gameMode);
		}

		public static string GetIdFromMode(GameMode gameMode)
		{
			if (gameMode == GameMode.STANDARD)
			{
				return "STANDARD";
			}
			if (gameMode == GameMode.SOLO)
			{
				return "SOLO";
			}
			if (gameMode == GameMode.CRUSADE)
			{
				return "CRUSADE";
			}
			if (gameMode == GameMode.RIFT)
			{
				return "RIFT";
			}
			throw new NotImplementedException();
		}

		public ChallengeUpgrade GetNextChallangeUpgrade()
		{
			int numBought = this.activeChallenge.totalGainedUpgrades.numBought;
			if (numBought >= this.activeChallenge.allUpgrades.Count)
			{
				return null;
			}
			return this.activeChallenge.allUpgrades[numBought];
		}

		public ChallengeUpgrade GetPrevChallangeUpgrade()
		{
			int numBought = this.activeChallenge.totalGainedUpgrades.numBought;
			if (numBought == 0 || this.activeChallenge.totalGainedUpgrades.numBought > this.activeChallenge.allUpgrades.Count)
			{
				return null;
			}
			return this.activeChallenge.allUpgrades[numBought - 1];
		}

		public double GetNextChallangeUpgradeCost()
		{
			ChallengeUpgrade nextChallangeUpgrade = this.GetNextChallangeUpgrade();
			if (nextChallangeUpgrade == null)
			{
				return double.PositiveInfinity;
			}
			return nextChallangeUpgrade.GetCost(this);
		}

		public bool IsNextWorldUpgradeUnlocked()
		{
			ChallengeUpgrade nextChallangeUpgrade = this.GetNextChallangeUpgrade();
			if (nextChallangeUpgrade == null)
			{
				return false;
			}
			if (this.gameMode == GameMode.STANDARD)
			{
				int stageReq = nextChallangeUpgrade.stageReq;
				int stageNumber = this.GetStageNumber();
				return stageReq < stageNumber || (stageReq == stageNumber && this.activeChallenge.GetWaveNumber() > 0);
			}
			if (this.gameMode == GameMode.CRUSADE)
			{
				int waveReq = nextChallangeUpgrade.waveReq;
				int totWave = this.activeChallenge.GetTotWave();
				return waveReq < totWave;
			}
			if (this.gameMode == GameMode.RIFT)
			{
				int waveReq2 = nextChallangeUpgrade.waveReq;
				int totWave2 = this.activeChallenge.GetTotWave();
				return waveReq2 < totWave2;
			}
			throw new NotImplementedException();
		}

		public bool CanAffordWorldUpgrade()
		{
			double nextChallangeUpgradeCost = this.GetNextChallangeUpgradeCost();
			return this.gold.CanAfford(nextChallangeUpgradeCost);
		}

		public bool CanBuyWorldUpgrade()
		{
			return this.IsNextWorldUpgradeUnlocked() && this.CanAffordWorldUpgrade();
		}

		public void TryBuyWorldUpgrade()
		{
			if (this.CanBuyWorldUpgrade())
			{
				double nextChallangeUpgradeCost = this.GetNextChallangeUpgradeCost();
				this.gold.Decrement(nextChallangeUpgradeCost);
				ChallengeUpgrade nextChallangeUpgrade = this.GetNextChallangeUpgrade();
				nextChallangeUpgrade.Apply(this, this.activeChallenge.totalGainedUpgrades);
				this.activeChallenge.totalGainedUpgrades.numBought++;
			}
		}

		public void TryCollectOfflineEarnings()
		{
			this.RainGold(this.offlineGold);
			this.offlineGold = 0.0;
		}

		public void RefreshChallengeUpgrades(int numBought)
		{
			this.activeChallenge.totalGainedUpgrades.Init();
			int i = 0;
			int count = this.activeChallenge.allUpgrades.Count;
			while (i < count)
			{
				if (i >= numBought)
				{
					break;
				}
				this.activeChallenge.allUpgrades[i].Apply(this, this.activeChallenge.totalGainedUpgrades);
				i++;
			}
			this.activeChallenge.totalGainedUpgrades.numBought = numBought;
		}

		public int GetNumNotificationsHeroesTab()
		{
			int num = 0;
			if (this.CanBuyWorldUpgrade())
			{
				num++;
			}
			foreach (Hero hero in this.heroes)
			{
				if (hero.GetXpNeedForNextLevel() == hero.GetXp() + 1 && this.gold.CanAfford(hero.GetUpgradeCost(true)))
				{
					num++;
				}
			}
			if (this.CanAffordTotemUpgrade() && this.totem != null && this.totem.GetXpNeedForNextLevel() == this.totem.GetXp() + 1)
			{
				num++;
			}
			return num;
		}

		public void OnEnvironmentIsToChange()
		{
			this.doTransition = true;
			this.isTransitioning = true;
		}

		public void OnEnvironmentTransitionUiWindowClosed()
		{
			if (this.activeChallenge != null && this.activeChallenge.enemies != null)
			{
				foreach (Enemy enemy in this.activeChallenge.enemies)
				{
					if (!enemy.IsAlive())
					{
						enemy.inStateTimeCounter = 99999f;
					}
				}
			}
			this.isTransitioning = false;
			Hero randomAliveHero = this.GetRandomAliveHero();
			if (randomAliveHero != null && (randomAliveHero.GetId() != "DRUID" || !randomAliveHero.IsUsingTempWeapon()))
			{
				SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, randomAliveHero.GetId(), true, 0f, randomAliveHero.soundVoEnvChange);
				this.AddSoundEvent(e);
			}
		}

		public void StartAutoTap(float dur)
		{
			this.autoTapTimeLeft += dur;
		}

		public void StartTimeWarp(float dur, float speed)
		{
			this.timeWarpTimeLeft += dur;
			this.timeWarpSpeed = speed;
		}

		public void StartPowerUp(float dur, double damageFactorAdd)
		{
			this.powerUpTimeLeft += dur;
			this.powerUpDamageFactorAdd = damageFactorAdd;
		}

		public void StartGoldBoost(float dur, double goldFactorAdd)
		{
			this.goldBoostTimeLeft += dur;
			this.goldBoostFactor = 1.0 + goldFactorAdd;
		}

		public void StartRefresherOrb(float dur, float skillCoolFactor)
		{
			this.refresherOrbTimeLeft += dur;
			this.refresherOrbSkillCoolFactor = skillCoolFactor;
		}

		public void StartCatalyst(float dur, float progressPercentage)
		{
			this.catalystTimeLeft += dur;
			this.catalystProgressPercentage = progressPercentage;
		}

		public void StartVariety()
		{
			this.numCharmSelectionAdd = 1;
		}

		public void EnableRandomCharmsPicker()
		{
			this.pickRandomCharms = true;
		}

		public void StartEmergencyCharm()
		{
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			if (challengeRift == null)
			{
				return;
			}
			challengeRift.numCharmSelection++;
			this.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, new SoundSimple(SoundArchieve.inst.charmSelectionAvailable, 1f, float.MaxValue)));
		}

		public void StartShield(float dur)
		{
			this.shieldTimeLeft += dur;
		}

		public void StartBlizzard(float dur, float slowFactor)
		{
			if (this.blizzardTimeLeft <= 0f)
			{
				this.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, string.Empty, false, 0f, new SoundSimple(SoundArchieve.inst.blizzardStartSound, 1f, 2f)));
			}
			this.blizzardTimeLeft += dur;
			this.blizzardSlowFactor = slowFactor;
			this.AddBlizzardBuffToEnemies(dur);
		}

		public void AddBlizzardBuffToEnemies(float dur)
		{
			foreach (Enemy enemy in this.activeChallenge.enemies)
			{
				if (enemy.data.dataBase.type != EnemyDataBase.Type.CHEST)
				{
					Buff buffWithId = enemy.GetBuffWithId(352);
					if (buffWithId != null)
					{
						buffWithId.IncreaseDuration(dur);
					}
					else
					{
						BuffDataAttackSpeedFactor buffData = new BuffDataAttackSpeedFactor
						{
							id = 352,
							attackSpeedFactor = this.blizzardSlowFactor,
							visuals = 2,
							dur = dur
						};
						enemy.AddBuff(buffData, 0, false);
					}
				}
			}
		}

		public void StartHotCocoa(float dur, float cooldownReductionFactor, float damageFactor)
		{
			this.hotCocoaTimeLeft += dur;
			this.hotCocoaCooldownReductionFactor = 1f + cooldownReductionFactor;
			this.hotCocoaDamageFactor = 1f + damageFactor;
		}

		public void StartOrnamentDrop(float dur, float teamDamageFactor, float targetTime, int projectilesCount)
		{
			this.ornamentDropTimeLeft += dur;
			this.ornamentDropTeamDamageFactor = teamDamageFactor;
			this.ornamentDropTargetTime = targetTime;
			this.ornamentDropProjectilesCount = projectilesCount;
		}

		public void RainGold(double amount)
		{
			int num = 30;
			if (amount < (double)num)
			{
				num = (int)amount;
			}
			double amount2 = amount / (double)num;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = new Vector3(GameMath.GetRandomFloat(-0.6f, 0.6f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.25f, 0.15f, GameMath.RandType.NoSeed), 0f);
				Vector3 startPos = vector;
				startPos.y = (float)i * 0.2f + 1.15f;
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.GOLD, amount2, this, false);
				if (this.gameMode == GameMode.CRUSADE)
				{
					dropCurrency.invPos = RenderManager.POS_GOLD_INV_TIMECHALLENGE;
				}
				dropCurrency.InitVel(0f, startPos, vector.y, Vector3.zero);
				this.drops.Add(dropCurrency);
			}
		}

		public void RainMythstone(double amount)
		{
			int num = 30;
			if (amount < (double)num)
			{
				num = (int)amount;
			}
			double amount2 = amount / (double)num;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = new Vector3(GameMath.GetRandomFloat(-0.6f, 0.6f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.25f, 0.15f, GameMath.RandType.NoSeed), 0f);
				Vector3 startPos = vector;
				startPos.y = (float)i * 0.2f + 1.15f;
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.MYTHSTONE, amount2, this, false);
				if (this.gameMode == GameMode.CRUSADE)
				{
					dropCurrency.invPos = RenderManager.POS_GOLD_INV_TIMECHALLENGE;
				}
				dropCurrency.InitVel(0f, startPos, vector.y, Vector3.zero);
				this.drops.Add(dropCurrency);
			}
		}

		public void RainTokens(double amount)
		{
			int num = 30;
			if (amount < (double)num)
			{
				num = (int)amount;
			}
			double amount2 = amount / (double)num;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = new Vector3(GameMath.GetRandomFloat(-0.6f, 0.6f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.25f, 0.15f, GameMath.RandType.NoSeed), 0f);
				Vector3 startPos = vector;
				startPos.y = (float)i * 0.2f + 1.15f;
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.TOKEN, amount2, this, false);
				dropCurrency.InitVel(0f, startPos, vector.y, Vector3.zero);
				this.drops.Add(dropCurrency);
			}
		}

		public void RainCurrencyOnUi(UiState state, CurrencyType currencyType, double amount, DropPosition dropPos, int numDrops = 30, float delay = 0f, float durExtraStay = 0f, float randomizationPower = 1f, Transform targetToScaleAnim = null, float durExtraStayEachDropIncrease = 0f)
		{
			if (amount < (double)numDrops || currencyType == CurrencyType.TRINKET_BOX)
			{
				numDrops = (int)amount;
			}
			double amount2 = amount / (double)numDrops;
			for (int i = 0; i < numDrops; i++)
			{
				Vector3 startPos = dropPos.startPos + new Vector3(GameMath.GetRandomFloat(-0.05f, 0.05f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.05f, 0.05f, GameMath.RandType.NoSeed), 0f) * randomizationPower;
				Vector3 endPos = dropPos.endPos + new Vector3(GameMath.GetRandomFloat(-0.2f, 0.2f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.07f, 0.07f, GameMath.RandType.NoSeed), 0f) * randomizationPower;
				DropCurrency dropCurrency = new DropCurrency(currencyType, amount2, this, false);
				dropCurrency.InitReach((float)i * 0.02f + delay, startPos, endPos, dropPos.invPos, 0.25f, Drop.EaseType.EASE_IN_OUT, 1.5f);
				dropCurrency.durStayOnGround = durExtraStay + durExtraStayEachDropIncrease * (float)i;
				dropCurrency.showSideCurrency = dropPos.showSideCurrency;
				dropCurrency.targetToScaleOnReach = dropPos.targetToScaleOnReach;
				dropCurrency.durFlyToInv = 1.6f;
				dropCurrency.scaleDuration = 0.09f;
				dropCurrency.scaleDurationBeforeReach = 0.3f;
				dropCurrency.uiState = state;
				this.drops.Add(dropCurrency);
			}
		}

		public void RainScraps(double amount)
		{
			int num = 30;
			if (amount < (double)num)
			{
				num = (int)amount;
			}
			double amount2 = amount / (double)num;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = new Vector3(GameMath.GetRandomFloat(-0.6f, 0.6f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.25f, 0.15f, GameMath.RandType.NoSeed), 0f);
				Vector3 startPos = vector;
				startPos.y = (float)i * 0.2f + 1.15f;
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.SCRAP, amount2, this, false);
				dropCurrency.InitVel(0f, startPos, vector.y, Vector3.zero);
				this.drops.Add(dropCurrency);
			}
		}

		public void RainCredits(double amount)
		{
			int num = 30;
			if (amount < (double)num)
			{
				num = (int)amount;
			}
			double amount2 = amount / (double)num;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = new Vector3(GameMath.GetRandomFloat(-0.6f, 0.6f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.25f, 0.15f, GameMath.RandType.NoSeed), 0f);
				Vector3 startPos = vector;
				startPos.y = (float)i * 0.2f + 1.15f;
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.GEM, amount2, this, false);
				dropCurrency.InitVel(0f, startPos, vector.y, Vector3.zero);
				this.drops.Add(dropCurrency);
			}
		}

		public void RainCandies(double amount)
		{
			int num = 30;
			if (amount < (double)num)
			{
				num = (int)amount;
			}
			double amount2 = amount / (double)num;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = new Vector3(GameMath.GetRandomFloat(-0.6f, 0.6f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.25f, 0.15f, GameMath.RandType.NoSeed), 0f);
				Vector3 startPos = vector;
				startPos.y = (float)i * 0.2f + 1.15f;
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.CANDY, amount2, this, false);
				dropCurrency.InitVel(0f, startPos, vector.y, Vector3.zero);
				this.drops.Add(dropCurrency);
			}
		}

		public void RainTrinketPacks(int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				Vector3 vector = new Vector3(GameMath.GetRandomFloat(-0.6f, 0.6f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.25f, 0.15f, GameMath.RandType.NoSeed), 0f);
				Vector3 startPos = vector;
				startPos.y = (float)i * 0.2f + 1.15f;
				DropCurrency dropCurrency = new DropCurrency(CurrencyType.TRINKET_BOX, 1.0, this, false);
				dropCurrency.zOffsetInventory -= 105f;
				dropCurrency.InitVel(0f, startPos, vector.y, Vector3.zero);
				this.drops.Add(dropCurrency);
			}
		}

		public void OnBossKilled()
		{
			Hero randomAliveHero = this.GetRandomAliveHero();
			if (randomAliveHero != null && (randomAliveHero.GetId() != "DRUID" || !randomAliveHero.IsUsingTempWeapon()))
			{
				SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, randomAliveHero.GetId(), true, 0f, new SoundVariedSimple(randomAliveHero.soundVoCheer, 1f));
				this.AddSoundEvent(e);
			}
			PlayerStats.OnEnemyKilled();
		}

		public void OnEnemyKilled()
		{
			PlayerStats.OnEnemyKilled();
		}

		public void OnHeroDead()
		{
			PlayerStats.OnHeroDead();
		}

		public void AddCurrencyDragon(CurrencyDragon dragon)
		{
			dragon.PlayLoopSound(this);
			this.currencyDragons.Add(dragon);
		}

		public void StopStageProgression()
		{
			this.canProgressStage = false;
		}

		public float GetTimeWarpTimeLeft()
		{
			return this.timeWarpTimeLeft;
		}

		public float GetTimeWarpTimeMax()
		{
			return 150f;
		}

		public float GetAutoTapTimeLeft()
		{
			return this.autoTapTimeLeft;
		}

		public float GetAutoTapTimeMax()
		{
			return 300f;
		}

		public float GetPowerUpTimeLeft()
		{
			return this.powerUpTimeLeft;
		}

		public float GetPowerUpTimeMax()
		{
			return 60f;
		}

		public float GetGoldBoostTimeLeft()
		{
			return this.goldBoostTimeLeft;
		}

		public float GetGoldBoostTimeMax()
		{
			return 300f;
		}

		public float GetTimeHolderTimeLeft()
		{
			if (this.activeChallenge is ChallengeWithTime)
			{
				return ((ChallengeWithTime)this.activeChallenge).timeShield;
			}
			return -1f;
		}

		public float GetTimeHolderTimeMax()
		{
			return 600f;
		}

		public float GetRefresherOrbTimeLeft()
		{
			return this.refresherOrbTimeLeft;
		}

		public float GetRefresherOrbTimeMax()
		{
			return 10f;
		}

		public float GetShieldTimeLeft()
		{
			return this.shieldTimeLeft;
		}

		public float GetCatalystTimeLeft()
		{
			return this.catalystTimeLeft;
		}

		public float GetCatalystTimeMax()
		{
			return 15f;
		}

		public float GetBlizzardTimeLeft()
		{
			return this.blizzardTimeLeft;
		}

		public float GetHotCocoaTimeLeft()
		{
			return this.hotCocoaTimeLeft;
		}

		public float GetOrnamentDropTimeLeft()
		{
			return this.ornamentDropTimeLeft;
		}

		public void AddPastDamage(GlobalPastDamage pastDamage)
		{
			this.numPastDamages++;
			this.pastDamageTimer = 1f;
			if (!double.IsPositiveInfinity(pastDamage.damage.amount) && pastDamage.damage.amount > 10.0 && pastDamage.damage.amount > this.highestDamageBefore)
			{
				this.highestDamageBefore = pastDamage.damage.amount;
				if (this.numPastDamages > 10)
				{
					pastDamage.highlight = !pastDamage.damage.doNotHighlight;
				}
			}
			pastDamage.Initialize();
			this.pastDamages.Enqueue(pastDamage);
		}

		public void AddPastDamage(GlobalPastHeal pastHeal)
		{
			pastHeal.Initialize();
			this.pastHeals.Enqueue(pastHeal);
		}

		public void SetSkippedStage(int stageNo)
		{
			this.skippedStageNo = stageNo;
		}

		public int GetSpawnSpeedBelowStage()
		{
			return this.universalBonus.fastEnemySpawnBelow;
		}

		public bool IsIdleGainActive()
		{
			return this.isIdleGainActive;
		}

		public void CollectDropsImmidiately()
		{
			this.isCollectingDropsInImmediateMode = true;
			foreach (Drop drop in this.drops)
			{
				drop.Apply(this);
			}
			this.drops.Clear();
			this.isCollectingDropsInImmediateMode = false;
		}

		public bool IsRaining()
		{
			return this.isRainingGlory || this.isRainingDuck;
		}

		public void RefreshTrinketEffects()
		{
			foreach (Hero hero in this.heroes)
			{
				hero.RefreshPermanentBuffs();
			}
		}

		public bool RefreshTrinketEffects(HeroDataBase heroDataBase)
		{
			foreach (Hero hero in this.heroes)
			{
				if (hero.HasDataBase(heroDataBase))
				{
					hero.RefreshPermanentBuffs();
					return true;
				}
			}
			return false;
		}

		public void AddPowerupNonCritDamage()
		{
			this.powerupNonCritDamageTimeLeft = GameMath.GetMinFloat(600.99f, this.powerupNonCritDamageTimeLeft + this.universalBonus.powerupNonCritDamageDuration);
		}

		public void AddPowerupCooldown()
		{
			this.powerupCooldownTimeLeft = GameMath.GetMinFloat(600.99f, this.powerupCooldownTimeLeft + this.universalBonus.powerupCooldownDuration);
		}

		public void AddPowerupRevive()
		{
			this.powerupReviveTimeLeft = GameMath.GetMinFloat(600.99f, this.powerupReviveTimeLeft + this.universalBonus.powerupReviveDuration);
			foreach (Hero hero in this.heroes)
			{
				if (hero.IsAlive())
				{
					hero.AddBuff(new BuffDataHealthRegen
					{
						id = 101,
						healthRegenAdd = this.universalBonus.powerupReviveHealthRegen / 4.0,
						dur = 4f
					}, 0, false);
				}
			}
		}

		public void OnHiltDodge()
		{
			this.dailyQuestHiltDodgeCounter++;
		}

		public void OnBellylarfAnger(float dt)
		{
			this.dailyQuestBellylarfAngerTimer += dt;
		}

		public void OnVexxCool()
		{
			this.dailyQuestVexxCoolCounter++;
		}

		public void OnLennyKillStunned()
		{
			this.dailyQuestLennyKillCounter++;
		}

		public void OnWendleCastSpell()
		{
			this.dailyQuestWendleCastCounter++;
		}

		public void OnWendleHealed()
		{
			this.dailyQuestWendleHealCounter++;
		}

		public void OnVStealFromBoss()
		{
			this.dailyQuestVStealBossCounter++;
		}

		public void OnBoomerBoom()
		{
			this.dailyQuestBoomerBoomCounter++;
		}

		public void OnSamShield()
		{
			this.dailyQuestSamShieldCounter++;
		}

		public void OnLiaMiss()
		{
			this.dailyQuestLiaMissCounter++;
		}

		public void OnJimAllyDeath()
		{
			this.dailyQuestJimAllyDeathCounter++;
		}

		public void OnTamKillBlind()
		{
			this.dailyQuestTamKillBlindedCounter++;
		}

		public void OnDeathAny(Unit unit)
		{
			if (this.activeChallenge is ChallengeRift)
			{
				ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
				foreach (RiftEffect riftEffect in challengeRift.riftEffects)
				{
					riftEffect.OnDeathAny(unit);
				}
				foreach (EnchantmentBuff enchantmentBuff in challengeRift.allEnchantments)
				{
					enchantmentBuff.OnDeathAny(unit);
				}
			}
		}

		public void OnSpellCast(Skill skill, int skillIndex)
		{
			if (this.activeChallenge is ChallengeRift)
			{
				ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
				foreach (EnchantmentBuff enchantmentBuff in challengeRift.allEnchantments)
				{
					enchantmentBuff.OnAbilityCast(skill);
				}
			}
			if (skillIndex == 0)
			{
				PlayerStats.OnUltimateUsed();
			}
			else
			{
				PlayerStats.OnSecondaryAbilityCasted();
			}
		}

		public void OnChestKilled()
		{
			if (this.HasHeroInTeam("SHEELA"))
			{
				this.dailyQuestVTreasureChestKill++;
			}
			if (this.HasHeroInTeam("GOBLIN"))
			{
				this.dailyQuestGoblinKillTreasureCount++;
			}
			if (this.gameMode == GameMode.STANDARD)
			{
				ChallengeStandard challengeStandard = this.activeChallenge as ChallengeStandard;
				if (challengeStandard.chestCandyDropTimer >= 10f)
				{
					challengeStandard.chestCandyDropTimer = 0f;
				}
			}
			PlayerStats.OnGoblinChestDestroyed();
		}

		public void OnGoblinSummonDragon()
		{
			this.dailyQuestGoblinDragonSummonCount++;
		}

		public void OnLennyHealAlly()
		{
			this.dailyQuestLennyHealAlly++;
		}

		public void OnBabuHealAlly()
		{
			this.dailyQuestBabuHealAlly++;
		}

		public void OnTamHitMarkedTargets()
		{
			this.dailyQuestTamHitMarkedTargets++;
		}

		public void OnLiaStealHealth()
		{
			this.dailyQuestLiaStealHealth++;
		}

		public void OnWarlockBlindEnemy()
		{
			this.dailyQuestWarlockBlindEnemy++;
		}

		public void OnWarlockRedirectDamage()
		{
			this.dailyQuestWarlockRedirectDamage++;
		}

		public void OnGoblinMiss()
		{
			this.dailyQuestGoblinMissCount++;
		}

		private bool HasHeroInTeam(string id)
		{
			foreach (Hero hero in this.heroes)
			{
				if (hero.GetId() == id)
				{
					return true;
				}
			}
			return false;
		}

		public void OnHiltCrit()
		{
			this.dailyQuestHiltCriticalHit++;
		}

		public void OnBabuGetHit()
		{
			this.dailyQuestBabuGetHitCounter++;
		}

		public void ResetDailyQuestProgress()
		{
			this.dailyQuestBellylarfAngerTimer = 0f;
			this.dailyQuestPassStageCounter = 0;
			this.dailyQuestKilledEnemyCounter = 0;
			this.dailyQuestUltiSkillCounter = 0;
			this.dailyQuestDragonCatchCounter = 0;
			this.dailyQuestHiltDodgeCounter = 0;
			this.dailyQuestVexxCoolCounter = 0;
			this.dailyQuestLennyKillCounter = 0;
			this.dailyQuestWendleCastCounter = 0;
			this.dailyQuestVStealBossCounter = 0;
			this.dailyQuestBoomerBoomCounter = 0;
			this.dailyQuestSamShieldCounter = 0;
			this.dailyQuestLiaMissCounter = 0;
			this.dailyQuestJimAllyDeathCounter = 0;
			this.dailyQuestTamKillBlindedCounter = 0;
			this.dailyQuestWendleHealCounter = 0;
			this.dailyQuestVTreasureChestKill = 0;
			this.dailyQuestLennyHealAlly = 0;
			this.dailyQuestHiltCriticalHit = 0;
			this.dailyQuestTamHitMarkedTargets = 0;
			this.dailyQuestEnemyStunned = 0;
			this.dailyQuestLiaStealHealth = 0;
			this.dailyQuestWarlockBlindEnemy = 0;
			this.dailyQuestWarlockRedirectDamage = 0;
			this.dailyQuestGoblinMissCount = 0;
			this.dailyQuestGoblinKillTreasureCount = 0;
			this.dailyQuestBabuHealAlly = 0;
			this.dailyQuestBabuGetHitCounter = 0;
		}

		public void AddRingTap(Vector3 tapPositions)
		{
			this.selfTaps.Add(tapPositions);
		}

		public int GetActiveChallengeIndex()
		{
			int num = (this.cursedChallenges != null) ? this.cursedChallenges.IndexOf(this.activeChallenge) : -1;
			if (num == -1)
			{
				return this.allChallenges.IndexOf(this.activeChallenge);
			}
			return num;
		}

		public void SetActiveChallengeByIndex(int index, bool isCursed = false)
		{
			this.activeChallenge = this.allChallenges[index];
		}

		public bool IsRiftChallengeUnlocked(int index, int discoveryIndex)
		{
			if (index == 0)
			{
				return true;
			}
			ChallengeRift challengeRift = this.allChallenges[index] as ChallengeRift;
			if (challengeRift.discoveryIndex > discoveryIndex)
			{
				return false;
			}
			int latestBeatenRiftChallengeIndex = this.GetLatestBeatenRiftChallengeIndex();
			return index <= latestBeatenRiftChallengeIndex + 1;
		}

		public void CacheLatestBeatenRiftChallengeIndex()
		{
			this.cachedLatestBeatenRiftChallengeIndex = -1;
			int count = this.allChallenges.Count;
			for (int i = 0; i < count; i++)
			{
				ChallengeRift challengeRift = this.allChallenges[i] as ChallengeRift;
				if (challengeRift.unlock.isCollected)
				{
					this.cachedLatestBeatenRiftChallengeIndex = i;
				}
			}
			this.cachedLatestUnlockedRiftChallengeIndex = GameMath.Clamp(this.cachedLatestBeatenRiftChallengeIndex + 1, 0, count - 1);
		}

		public int GetLatestBeatenRiftChallengeIndex()
		{
			return this.cachedLatestBeatenRiftChallengeIndex;
		}

		public int GetLatestUnlockedRiftChallengeIndex()
		{
			return this.cachedLatestUnlockedRiftChallengeIndex;
		}

		public bool IsRiftBeaten(int id)
		{
			ChallengeRift challengeRift = this.allChallenges.Find((Challenge r) => (r as ChallengeRift).id == id) as ChallengeRift;
			return challengeRift.unlock.isCollected;
		}

		public bool AreAllChallengesBeaten()
		{
			return this.allChallenges.Count == 0 || (this.allChallenges[this.allChallenges.Count - 1] as ChallengeRift).unlock.isCollected;
		}

		public void OnAnyCharmTriggered(CharmBuff charmBuff)
		{
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			if (challengeRift != null)
			{
				foreach (EnchantmentBuff enchantmentBuff in challengeRift.allEnchantments)
				{
					enchantmentBuff.OnAnyCharmTriggered(this, charmBuff);
				}
			}
		}

		public void OnUnitDodged(UnitHealthy dodger, Unit attacker, Damage damage)
		{
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			if (challengeRift != null)
			{
				foreach (EnchantmentBuff enchantmentBuff in challengeRift.allEnchantments)
				{
					enchantmentBuff.OnDodgeAny(dodger);
				}
			}
		}

		public void OnHeroRevived(Hero hero)
		{
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			if (challengeRift != null)
			{
				foreach (EnchantmentBuff enchantmentBuff in challengeRift.allEnchantments)
				{
					enchantmentBuff.OnHeroRevived(hero);
				}
			}
		}

		public void OnUnitShielded(UnitHealthy unitHealthy)
		{
			Hero hero = unitHealthy as Hero;
			if (hero == null)
			{
				return;
			}
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			if (challengeRift != null)
			{
				foreach (EnchantmentBuff enchantmentBuff in challengeRift.allEnchantments)
				{
					enchantmentBuff.OnHeroShielded(hero);
				}
			}
		}

		public void OnUnitPreHeal(UnitHealthy unitHealthy, ref double ratioHealed)
		{
			Hero hero = unitHealthy as Hero;
			if (hero == null)
			{
				return;
			}
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			if (challengeRift != null)
			{
				foreach (RiftEffect riftEffect in challengeRift.riftEffects)
				{
					riftEffect.OnHeroPreHeal(hero, ref ratioHealed);
				}
			}
		}

		public void OnUnitHealed(UnitHealthy unitHealthy, double ratioHealed)
		{
			Hero hero = unitHealthy as Hero;
			if (hero == null)
			{
				return;
			}
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			if (challengeRift != null)
			{
				foreach (EnchantmentBuff enchantmentBuff in challengeRift.allEnchantments)
				{
					enchantmentBuff.OnHeroHealed(hero, ratioHealed);
				}
			}
		}

		public void OnEnemyStunned(Enemy enemy)
		{
			this.dailyQuestEnemyStunned++;
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			if (challengeRift != null)
			{
				foreach (EnchantmentBuff enchantmentBuff in challengeRift.allEnchantments)
				{
					enchantmentBuff.OnEnemyStunned(enemy);
				}
				foreach (RiftEffect riftEffect in challengeRift.riftEffects)
				{
					riftEffect.OnEnemyStunned(enemy, this);
				}
			}
		}

		public int GetChallengeIndex(Challenge challenge)
		{
			return this.allChallenges.IndexOf(challenge);
		}

		public bool IsActiveChallengeCursed()
		{
			ChallengeRift challengeRift = this.activeChallenge as ChallengeRift;
			return challengeRift != null && challengeRift.IsCursed();
		}

		public bool DoesActiveChallengeAllowRepeatedHeroes()
		{
			return this.activeChallenge.AreRepeatedHeroesAllowed();
		}

		public int GetNumHeroesOfClassInTeam(HeroClass heroClass)
		{
			int num = 0;
			for (int i = this.heroes.Count - 1; i >= 0; i--)
			{
				if (this.heroes[i].GetHeroClass() == heroClass)
				{
					num++;
				}
			}
			return num;
		}

		public bool CanRingUltraCrit()
		{
			return this.ringUltraCritTimer > this.universalBonus.ringUltraCritCd;
		}

		public void OnRingUltraCrit()
		{
			this.ringUltraCritTimer = 0f;
		}

		public static int[] ADVENTURE_INTIAL_HERO_LEVELS = new int[]
		{
			0,
			1,
			2,
			3,
			4
		};

		public static float RIFT_REVIVE_TIME = 60f;

		public const float POWERUP_DURATION_MAX = 600.99f;

		public const float AD_DRAGON_MAX_X = 0.7f;

		public const float AD_DRAGON_MIN_X = -0.7f;

		public const float AD_DRAGON_SPEED = 0.3f;

		public const float AD_DRAGON_TAP_R2 = 0.1f;

		public const float AD_DRAGON_TAP_R2_QUART = 0.0333333351f;

		public const float CURSET_RIFT_INTERVAL = 14400f;

		public const int MAX_CURSED_RIFT_COUNT = 10;

		public const float LeftPosOffScreen = -1.5f;

		public static Vector3 ENEMY_CENTER = new Vector3(0.5f, 0f, 0f);

		public const float WORLD_MAX_DROP_HEIGHT = 0.28f;

		public Vector3[] heroPoses;

		public Vector3[][] enemyPoses;

		public Simulator currentSim;

		public GameMode gameMode;

		private Unlock earnedUnlock;

		private double earnedMythstone;

		private double earnedCredit;

		private double earnedToken;

		private double earnedScrap;

		private double earnedAeon;

		private double earnedCandy;

		private double earnedCandyCapped;

		private double earnedRiftPoints;

		private int earnedTrinketBoxes;

		public bool shouldSave;

		public bool shouldSoftSave;

		public Currency gold;

		public double offlineGold;

		public double[] newHeroCosts;

		public int[][] heroLevelJumps;

		public int[] totemLevelJumps;

		public List<Challenge> allChallenges;

		public List<Challenge> cursedChallenges;

		public Challenge activeChallenge;

		public Totem totem;

		public List<Hero> heroes;

		public List<Projectile> projectiles;

		public List<Drop> drops;

		public List<FutureDamage> futureDamages;

		public List<CurrencyDragon> currencyDragons;

		public List<SwarmDragon> swarmDragons;

		public List<Stampede> stampedes;

		public List<SupportAnimal> supportAnimals;

		public List<TreasureDrop> treasureDrops;

		public UniversalTotalBonus universalBonus;

		public BuffTotalWorldEffect buffTotalEffect;

		public float blackCurtainRatio;

		public Queue<GlobalPastDamage> pastDamages;

		public Queue<GlobalPastHeal> pastHeals;

		public List<VisualEffect> visualEffects;

		public List<SoundEvent> sounds;

		public List<VisualLinedEffect> visualLinedEffects;

		private int maxStageReached;

		private int maxHeroLevelReached;

		private int cachedLatestBeatenRiftChallengeIndex = -1;

		private int cachedLatestUnlockedRiftChallengeIndex;

		public List<Unlock> unlocks;

		public Unlock unlockMode;

		public List<Vector3> selfTaps;

		public List<MerchantItem> merchantItems;

		public List<MerchantItem> eventMerchantItems;

		public float timeWarpTimeLeft;

		public float dtUnwarp;

		public float timeWarpSpeed;

		public float autoTapTimeLeft;

		public float powerUpTimeLeft;

		public double powerUpDamageFactorAdd;

		public float goldBoostTimeLeft;

		public double goldBoostFactor;

		public float refresherOrbTimeLeft;

		public float refresherOrbSkillCoolFactor;

		public float shieldTimeLeft;

		public float shieldTimeToAdd;

		public float powerupNonCritDamageTimeLeft;

		public float powerupCooldownTimeLeft;

		public float powerupReviveTimeLeft;

		public float catalystTimeLeft;

		public float catalystProgressPercentage;

		public float catalystActTimer;

		public int numCharmSelectionAdd;

		public bool pickRandomCharms;

		public float blizzardTimeLeft;

		public float blizzardSlowFactor;

		public float hotCocoaTimeLeft;

		public float hotCocoaCooldownReductionFactor;

		public float hotCocoaDamageFactor;

		public float ornamentDropTimeLeft;

		public float ornamentDropTargetTime;

		public float ornamentDropTeamDamageFactor;

		public int ornamentDropProjectilesCount;

		public float ornamentDropCurrentTime;

		private int throwOrnamentDropsCount;

		public int givenSkillPoints;

		public float ringUltraCritTimer;

		public float timeSinceStartup;

		private bool advanceToNextRiftAfterRainGlory;

		public bool isRainingGlory;

		public bool isRainingDuck;

		public float duckInitTimer;

		public const float duckInitPeriod = 1f;

		public World.AdDragonState adDragonState;

		public int adDragonReqMaxStage;

		public float adDragonWaitMin = 300f;

		public float adDragonWaitMax = 450f;

		public float adDragonWait;

		public float adDragonIdleDur = 25f;

		public float adDragonTimeCounter;

		public Vector3 adDragonPos;

		public float adDragonDir;

		public bool autoSkillDistribute;

		private bool hasJustActivatedAdDragon;

		private bool hasJustActivatedCurencyDragon;

		private float pastDamageTimer;

		private double highestDamageBefore;

		public List<MerchantItem> merchantItemsToEvaluate;

		private int numPastDamages;

		public bool doTransition;

		public bool isTransitioning;

		public bool willEnd;

		public bool showAdOffer;

		public bool showingAdOffer;

		public CurrencyType adRewardCurrencyType;

		public double adRewardAmount;

		private Sound soundGoldCollect = new SoundVariedSimple(SoundArchieve.inst.goldCollects, 0.5f);

		private Sound soundCreditsCollect = new SoundVariedSimple(SoundArchieve.inst.creditsCollects, 1f);

		private Sound soundScrapsCollect = new SoundVariedSimple(SoundArchieve.inst.scrapsCollects, 1f);

		private Sound soundMythstonesCollect = new SoundVariedSimple(SoundArchieve.inst.mythstonesCollects, 1f);

		private Sound soundTokensCollect = new SoundVariedSimple(SoundArchieve.inst.tokensCollects, 1f);

		private Sound soundPowerup = new SoundSimple(SoundArchieve.inst.uiHeroLevelUp, 1f, float.MaxValue);

		public int offlineWaveProgression;

		public int skippedStageNo;

		public bool isCompleted;

		public bool canProgressStage;

		private float idleDuration;

		public float noRingTabDur;

		private bool isIdleGainActive;

		private float autoUpgradeTimer;

		public bool autoUpgradeDisabled;

		public bool autoUpgradeMilestones;

		public bool raidChestDisabled;

		public float dailyQuestBellylarfAngerTimer;

		public int dailyQuestPassStageCounter;

		public int dailyQuestKilledEnemyCounter;

		public int dailyQuestUltiSkillCounter;

		public int dailyQuestDragonCatchCounter;

		public int dailyQuestHiltDodgeCounter;

		public int dailyQuestVexxCoolCounter;

		public int dailyQuestLennyKillCounter;

		public int dailyQuestWendleCastCounter;

		public int dailyQuestVStealBossCounter;

		public int dailyQuestBoomerBoomCounter;

		public int dailyQuestSamShieldCounter;

		public int dailyQuestLiaMissCounter;

		public int dailyQuestJimAllyDeathCounter;

		public int dailyQuestTamKillBlindedCounter;

		public int dailyQuestWendleHealCounter;

		public int dailyQuestVTreasureChestKill;

		public int dailyQuestLennyHealAlly;

		public int dailyQuestBabuHealAlly;

		public int dailyQuestHiltCriticalHit;

		public int dailyQuestTamHitMarkedTargets;

		public int dailyQuestEnemyStunned;

		public int dailyQuestLiaStealHealth;

		public int dailyQuestWarlockBlindEnemy;

		public int dailyQuestWarlockRedirectDamage;

		public int dailyQuestGoblinMissCount;

		public int dailyQuestGoblinDragonSummonCount;

		public int dailyQuestGoblinKillTreasureCount;

		public int dailyQuestBabuGetHitCounter;

		public int dailyQuestRonLandedCritHit;

		public int dailyQuestRonImpulsiveSkillTriggeredCounter;

		public bool canDropCandies;

		public bool canDropChestCandies;

		public bool hasValidCandyQuest;

		public bool isCollectingDropsInImmediateMode;

		public int lastRiftIdLost = -1;

		public int sameRiftLoseCount;

		public int lastTimeChallengeIdLost = -1;

		public int sameTimeChallengeLoseCount;

		public float lastUsedMerchantItemDuration;

		private List<Enemy> cachedEnemyList = new List<Enemy>();

		private Dictionary<Enemy, int> cachedEnemyHits = new Dictionary<Enemy, int>();

		private List<Drop> finishedDrops = new List<Drop>();

		private List<Hero> heroCandidates = new List<Hero>();

		private List<Enemy> enemyCandidates = new List<Enemy>();

		public enum AdDragonState
		{
			NONEXISTANCE,
			IDLE,
			WAIT_UI,
			ACTIVATE,
			ESCAPE
		}

		public struct SkillIndex
		{
			public static World.SkillIndex Create(int branch, int index)
			{
				return new World.SkillIndex
				{
					branch = branch,
					index = index
				};
			}

			public int branch;

			public int index;
		}
	}
}
