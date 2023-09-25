using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseAnger : SkillActiveDataBase
	{
		public SkillDataBaseAnger()
		{
			this.nameKey = "SKILL_NAME_ANGER";
			this.descKey = "SKILL_DESC_ANGER";
			this.requiredHeroLevel = 0;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			this.timeFadeInStart = 0.1f;
			this.timeFadeInEnd = 0.3f;
			this.timeFadeOutStart = 1.33333337f;
			this.timeFadeOutEnd = 1.53333342f;
			this.durNonslowable = 1.53333342f;
			this.durStayInFrontCurtain = 1.33333337f;
			data.dur = 0f;
			data.durInvulnurability = 5f;
			data.cooldownMax = 140f;
			data.events = new List<SkillEvent>();
			SkillEventZeroBuffGenericCounter skillEventZeroBuffGenericCounter = new SkillEventZeroBuffGenericCounter();
			skillEventZeroBuffGenericCounter.time = 0f;
			skillEventZeroBuffGenericCounter.buffType = typeof(BuffDataAngerManagement);
			data.events.Add(skillEventZeroBuffGenericCounter);
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = data.dur;
			BuffDataAnger buffDataAnger = new BuffDataAnger();
			buffDataAnger.id = 1;
			skillEventBuffSelf.buff = buffDataAnger;
			buffDataAnger.dur = 12f;
			buffDataAnger.damageFactor = this.GetDamage(level);
			buffDataAnger.attackSpeedFactor = 0.5f;
			buffDataAnger.events = new List<BuffEvent>();
			BuffEventChangeWeaponToTemp buffEventChangeWeaponToTemp = new BuffEventChangeWeaponToTemp();
			buffDataAnger.events.Add(buffEventChangeWeaponToTemp);
			buffEventChangeWeaponToTemp.time = 0f;
			buffEventChangeWeaponToTemp.durChange = 1.33333337f;
			WeaponWood weaponWood = new WeaponWood();
			weaponWood.id = 103;
			weaponWood.damageType = DamageType.SKILL;
			buffEventChangeWeaponToTemp.weapon = weaponWood;
			weaponWood.SetTiming(1.0333333f, 0.2580645f, 0.01f, 0f);
			weaponWood.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.2580645f, new SoundVariedSimple(SoundArchieve.inst.thourUltiAttacks, 1f))
			};
			BuffEventChangeWeaponToOrig buffEventChangeWeaponToOrig = new BuffEventChangeWeaponToOrig();
			buffDataAnger.events.Add(buffEventChangeWeaponToOrig);
			buffEventChangeWeaponToOrig.time = buffDataAnger.dur;
			buffEventChangeWeaponToOrig.durChange = 0.433333337f;
		}

		public double GetDamage(int level)
		{
			return SkillDataBaseAnger.DAMAGE_BONUSES[level];
		}

		public double GetDamageDif(int level)
		{
			return SkillDataBaseAnger.DAMAGE_BONUSES[level + 1] - SkillDataBaseAnger.DAMAGE_BONUSES[level];
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false)), AM.csa(GameMath.GetTimeInSecondsString(12f))) + AM.GetCooldownText(140f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(this.GetDamageDif(data.level), false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(12f))) + AM.GetCooldownText(140f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(12f))) + AM.GetCooldownText(140f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.thourUlti, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voThourUlti, 1f));
		}

		private static double[] DAMAGE_BONUSES = new double[]
		{
			2.0,
			3.0,
			4.0,
			5.0,
			6.0
		};

		private const float COOLDOWN = 140f;

		private const float DUR_START = 1.33333337f;

		private const float DURATION = 12f;
	}
}
