using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseRevenge : SkillActiveDataBase
	{
		public SkillDataBaseRevenge()
		{
			this.nameKey = "SKILL_NAME_REVENGE";
			this.descKey = "SKILL_DESC_REVENGE";
			this.requiredHeroLevel = 0;
			this.maxLevel = 3;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			double num = 0.0;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerLetThemCome)
				{
					num += SkillEnhancerLetThemCome.GetDamageAdd(skillEnhancer.level);
				}
			}
			data.dur = 0f;
			data.cooldownMax = 210f;
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = 0f;
			BuffDataRainOfAxes buffDataRainOfAxes = new BuffDataRainOfAxes();
			buffDataRainOfAxes.id = 197;
			skillEventBuffSelf.buff = buffDataRainOfAxes;
			buffDataRainOfAxes.damageBonusPerDeadHero = num;
			buffDataRainOfAxes.dur = 15f;
			buffDataRainOfAxes.durChangeWeaponToOrig = 0.75f;
			buffDataRainOfAxes.damageInTeamDps = this.GetDamage(level);
			BuffEventChangeWeaponToTemp buffEventChangeWeaponToTemp = new BuffEventChangeWeaponToTemp();
			buffDataRainOfAxes.events = new List<BuffEvent>
			{
				buffEventChangeWeaponToTemp
			};
			buffEventChangeWeaponToTemp.time = 0f;
			buffEventChangeWeaponToTemp.durChange = 1.3f;
			WeaponWoodRandomRanged weaponWoodRandomRanged = new WeaponWoodRandomRanged();
			weaponWoodRandomRanged.id = 108;
			weaponWoodRandomRanged.damageType = DamageType.SKILL;
			buffEventChangeWeaponToTemp.weapon = weaponWoodRandomRanged;
			weaponWoodRandomRanged.SetTiming(0.966666639f, 0.3f, 0.01f);
			weaponWoodRandomRanged.projectileType = Projectile.Type.SAM_BOTTLE;
			weaponWoodRandomRanged.targetType = Projectile.TargetType.SINGLE_ENEMY;
			weaponWoodRandomRanged.durFly = 0.23f;
			weaponWoodRandomRanged.projectilePath = new ProjectilePathBomb
			{
				heightAddMax = 0.2f
			};
			weaponWoodRandomRanged.impactSound = new SoundVariedSimple(SoundArchieve.inst.samAttackImpacts, 1f);
			weaponWoodRandomRanged.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0f, new SoundVariedSimple(SoundArchieve.inst.samAttacks, 1f))
			};
			SkillEventBuffSelf skillEventBuffSelf2 = new SkillEventBuffSelf();
			skillEventBuffSelf2.time = 0f;
			BuffDataInvulnerability buffDataInvulnerability = new BuffDataInvulnerability();
			buffDataInvulnerability.id = 111;
			buffDataInvulnerability.visuals |= 128;
			skillEventBuffSelf2.buff = buffDataInvulnerability;
			buffDataInvulnerability.dur = 15f;
			data.events.Add(skillEventBuffSelf2);
			BuffEventChangeWeaponToOrig buffEventChangeWeaponToOrig = new BuffEventChangeWeaponToOrig();
			buffDataRainOfAxes.events.Add(buffEventChangeWeaponToOrig);
			buffEventChangeWeaponToOrig.time = buffDataRainOfAxes.dur;
			buffEventChangeWeaponToOrig.durChange = 0.8333333f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(15f)), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false))) + AM.GetCooldownText(210f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(15f)), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.5, false) + ")")) + AM.GetCooldownText(210f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(15f)), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false))) + AM.GetCooldownText(210f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.samUltiStart, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voSamUlti, 1f));
		}

		public double GetDamage(int level)
		{
			return 2.0 + 0.5 * (double)level;
		}

		private const float COOLDOWN = 210f;

		private const float DURATION = 15f;

		private const double INIT_DAMAGE = 2.0;

		private const double LEVEL_DAMAGE = 0.5;

		private const float DUR_SETUP = 0.8f;

		private const float DUR_THROW = 0.25f;
	}
}
