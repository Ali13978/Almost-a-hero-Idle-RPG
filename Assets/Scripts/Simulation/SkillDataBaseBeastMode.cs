using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseBeastMode : SkillActiveDataBase
	{
		public SkillDataBaseBeastMode()
		{
			this.nameKey = "SKILL_NAME_BEAST_MODE";
			this.descKey = "SKILL_DESC_BEAST_MODE";
			this.requiredHeroLevel = 1;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 0f;
			data.durInvulnurability = 5.2f;
			data.cooldownMax = 240f;
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerMassivePaws)
				{
					num = skillEnhancer.level;
				}
				else if (skillEnhancer.enhancerBase is SkillEnhancerHunterInstinct)
				{
					num2 = skillEnhancer.level;
				}
				else if (skillEnhancer.enhancerBase is SkillEnhancerRageDriven)
				{
					num3 = skillEnhancer.level;
				}
			}
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			skillEventBuffSelf.time = 0f;
			skillEventBuffSelf.buff = new BuffDataBasicAttacksDamage
			{
				dur = 31.367f,
				damageFactor = this.GetDamageIncreaseRatio(level),
				id = 335
			};
			skillEventBuffSelf.buff.events = new List<BuffEvent>();
			BuffEventChangeWeaponToTemp buffEventChangeWeaponToTemp = new BuffEventChangeWeaponToTemp();
			skillEventBuffSelf.buff.events.Add(buffEventChangeWeaponToTemp);
			buffEventChangeWeaponToTemp.time = 0f;
			buffEventChangeWeaponToTemp.durChange = 3.2f;
			buffEventChangeWeaponToTemp.durChangeIfAlreadyUsingTempWeapon = 2f;
			WeaponWood weaponWood = new WeaponWood();
			weaponWood.id = 121;
			weaponWood.damageType = DamageType.NONE;
			buffEventChangeWeaponToTemp.weapon = weaponWood;
			weaponWood.SetTiming(1.0333333f, 0.2580645f, 0.4f, 0.3f);
			weaponWood.DoNotWaitForNextAttack();
			weaponWood.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0f, new SoundVariedSimple(SoundArchieve.inst.druidGrowAttacks, 1f)),
				new TimedSound(0.166666672f, new SoundVariedMultiple(0.65f, new SoundVariedMultiple.SoundsInfo[]
				{
					new SoundVariedMultiple.SoundsInfo
					{
						expectedResult = 0,
						module = 1,
						sounds = new SoundVariedSimple(SoundArchieve.inst.druidPawsAttacks, 1f),
						variationOffset = 0
					},
					new SoundVariedMultiple.SoundsInfo
					{
						expectedResult = 0,
						module = 10,
						sounds = new SoundVariedSimple(SoundArchieve.inst.druidTailAttacks, 1f),
						variationOffset = 1
					}
				}))
			};
			skillEventBuffSelf.buff.events.Add(new BuffEventChangeWeaponToOrig
			{
				durChange = 3.167f,
				time = 28.2f
			});
			data.events.Add(skillEventBuffSelf);
			SkillEventBuffSelf skillEventBuffSelf2 = new SkillEventBuffSelf();
			skillEventBuffSelf2.time = 0f;
			skillEventBuffSelf2.buff = new BuffDataPlaySoundOnDeath
			{
				dur = 28.2f,
				sound = new SoundEventSound(SoundType.GAMEPLAY, "beast", false, 0f, new SoundSimple(SoundArchieve.inst.druidUltiDeath, 1f))
			};
			data.events.Add(skillEventBuffSelf2);
			SkillEventBuffSelf skillEventBuffSelf3 = new SkillEventBuffSelf();
			skillEventBuffSelf3.time = 0f;
			skillEventBuffSelf3.buff = new BuffDataAttackSpeedFactor
			{
				dur = 28.2f,
				attackSpeedFactor = 1f - this.GetAttackSpeedReduction(),
				id = 336
			};
			data.events.Add(skillEventBuffSelf3);
			if (num > -1)
			{
				SkillEventBuffSelf skillEventBuffSelf4 = new SkillEventBuffSelf();
				skillEventBuffSelf4.time = 0f;
				skillEventBuffSelf4.buff = new BuffDataMultipleTargetsProbability
				{
					dur = 28.2f,
					id = 337,
					firstProbability = SkillEnhancerMassivePaws.GetFirstProbability(num),
					firstEnemiesAmount = SkillEnhancerMassivePaws.GetFirstEnemiesDamagedCount() - 1,
					secondProbability = SkillEnhancerMassivePaws.GetSecondProbability(num),
					secondEnemiesAmount = SkillEnhancerMassivePaws.GetSecondEnemiesDamagedCount() - 1
				};
				data.events.Add(skillEventBuffSelf4);
			}
			if (num2 > -1)
			{
				SkillEventBuffSelf skillEventBuffSelf5 = new SkillEventBuffSelf();
				skillEventBuffSelf5.time = 0f;
				skillEventBuffSelf5.buff = new BuffDataCritFactor
				{
					id = 342,
					dur = 28.2f,
					critFactorAdd = (double)SkillEnhancerHunterInstinct.GetCritDamage(num2)
				};
				data.events.Add(skillEventBuffSelf5);
				SkillEventBuffSelf skillEventBuffSelf6 = new SkillEventBuffSelf();
				skillEventBuffSelf6.time = 0f;
				skillEventBuffSelf6.buff = new BuffDataCritChance
				{
					id = 343,
					dur = 28.2f,
					critChanceAdd = SkillEnhancerHunterInstinct.GetCritChance(num2)
				};
				data.events.Add(skillEventBuffSelf6);
			}
			if (num3 > -1)
			{
				SkillEventBuffSelf skillEventBuffSelf7 = new SkillEventBuffSelf();
				skillEventBuffSelf7.time = 0f;
				skillEventBuffSelf7.buff = new BuffDataAttackSpeedOnHitsCountToSameTarget
				{
					id = 351,
					dur = 28.2f,
					buffDuration = SkillEnhancerRageDriven.GetBuffDuration(num3),
					hitsCount = 3,
					attackSpeed = 1f + SkillEnhancerRageDriven.GetAttackSpeed(num3)
				};
				data.events.Add(skillEventBuffSelf7);
			}
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(25f)), AM.csa(GameMath.GetPercentString(this.GetDamageIncreaseRatio(0), false)), AM.csa(GameMath.GetPercentString(this.GetAttackSpeedReduction(), false))) + AM.GetCooldownText(240f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(25f)), AM.csa(GameMath.GetPercentString(this.GetDamageIncreaseRatio(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(15.0, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetAttackSpeedReduction(), false))) + AM.GetCooldownText(240f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(25f)), AM.csa(GameMath.GetPercentString(this.GetDamageIncreaseRatio(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetAttackSpeedReduction(), false))) + AM.GetCooldownText(240f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			Hero hero = by as Hero;
			bool isRepeatedTempWeapon = hero.GetWeapon().isRepeatedTempWeapon;
			if (isRepeatedTempWeapon)
			{
				world.AddSoundEvent(new SoundEventCancelBy(by.GetId()));
			}
			base.AddSoundEvent(world, by, new SoundDruidUlti((!isRepeatedTempWeapon) ? SoundArchieve.inst.druidUltiStart : SoundArchieve.inst.druidUltiStartShort, 1f));
		}

		public double GetDamageIncreaseRatio(int level)
		{
			return 30.0 + 15.0 * (double)level;
		}

		public float GetAttackSpeedReduction()
		{
			return 0.2f;
		}

		public const float COOLDOWN = 240f;

		public const float DURATION = 25f;

		public const double DAMAGE_INC_INIT = 30.0;

		public const double DAMAGE_INC_PER_LEVEL = 15.0;

		public const float ATTACK_SPEED_REDUCTION = 0.2f;

		public const float DUR_ANIM_START = 3.2f;

		private const float DUR_ANIM_START_REPEAT = 2f;

		private const float DUR_ANIM_END = 3.167f;
	}
}
