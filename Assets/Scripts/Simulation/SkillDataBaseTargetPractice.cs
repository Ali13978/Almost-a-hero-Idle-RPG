using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseTargetPractice : SkillActiveDataBase
	{
		public SkillDataBaseTargetPractice()
		{
			this.nameKey = "SKILL_NAME_TARGET_PRACTICE";
			this.descKey = "SKILL_DESC_TARGET_PRACTICE";
			this.requiredHeroLevel = 0;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 0f;
			data.durInvulnurability = 3f;
			data.cooldownMax = SkillDataBaseTargetPractice.COOLDOWN;
			data.events = new List<SkillEvent>();
			float duration = this.GetDuration(level);
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = 0f;
			BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
			buffDataAttackSpeed.id = 8;
			skillEventBuffSelf.buff = buffDataAttackSpeed;
			buffDataAttackSpeed.dur = duration;
			buffDataAttackSpeed.attackSpeedAdd = SkillDataBaseTargetPractice.SPEED_INCREASE;
			SkillEventBuffSelf skillEventBuffSelf2 = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf2);
			skillEventBuffSelf2.time = 0f;
			BuffDataDamageAdd buffDataDamageAdd = new BuffDataDamageAdd();
			buffDataDamageAdd.id = 36;
			skillEventBuffSelf2.buff = buffDataDamageAdd;
			buffDataDamageAdd.dur = duration;
			buffDataDamageAdd.damageAdd = SkillDataBaseTargetPractice.DAMAGE_INCREASE;
			SkillEventBuffSelf skillEventBuffSelf3 = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf3);
			skillEventBuffSelf3.time = 0f;
			BuffDataMissChanceFactor buffDataMissChanceFactor = new BuffDataMissChanceFactor();
			buffDataMissChanceFactor.id = 131;
			skillEventBuffSelf3.buff = buffDataMissChanceFactor;
			buffDataMissChanceFactor.dur = duration;
			buffDataMissChanceFactor.missChanceFactorAdd = SkillDataBaseTargetPractice.MISS_CHANCE_ADD;
			SkillEventBuffSelf skillEventBuffSelf4 = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf4);
			skillEventBuffSelf4.time = 0f;
			BuffData buffData = new BuffData();
			skillEventBuffSelf4.buff = buffData;
			buffData.dur = duration;
			buffData.events = new List<BuffEvent>();
			BuffEventChangeWeaponToTemp buffEventChangeWeaponToTemp = new BuffEventChangeWeaponToTemp();
			buffData.events.Add(buffEventChangeWeaponToTemp);
			buffEventChangeWeaponToTemp.time = 0f;
			buffEventChangeWeaponToTemp.durChange = 1.36666667f;
			WeaponWoodRandomRanged weaponWoodRandomRanged = new WeaponWoodRandomRanged();
			weaponWoodRandomRanged.id = 107;
			buffEventChangeWeaponToTemp.weapon = weaponWoodRandomRanged;
			weaponWoodRandomRanged.SetTiming(0.6f, 0.233333334f, 0f);
			weaponWoodRandomRanged.projectileType = Projectile.Type.BLIND_ARCHER_ULTI;
			weaponWoodRandomRanged.targetType = Projectile.TargetType.SINGLE_ENEMY;
			weaponWoodRandomRanged.damageType = DamageType.SKILL;
			weaponWoodRandomRanged.durFly = 0.24f;
			weaponWoodRandomRanged.projectilePath = new ProjectilePathBomb
			{
				heightAddMax = 0.1f
			};
			weaponWoodRandomRanged.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.40625f, new SoundVariedSimple(SoundArchieve.inst.liaUltiAttacks, 1f))
			};
			BuffEventChangeWeaponToOrig buffEventChangeWeaponToOrig = new BuffEventChangeWeaponToOrig();
			buffData.events.Add(buffEventChangeWeaponToOrig);
			buffEventChangeWeaponToOrig.time = buffData.dur;
			buffEventChangeWeaponToOrig.durChange = 0.7f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), new object[]
			{
				AM.csa(GameMath.GetPercentString(SkillDataBaseTargetPractice.BASE_MISS_CHANCE, false)),
				AM.csa(GameMath.GetPercentString(SkillDataBaseTargetPractice.BASE_MISS_CHANCE + SkillDataBaseTargetPractice.MISS_CHANCE_ADD, false)),
				AM.csa(GameMath.GetPercentString(SkillDataBaseTargetPractice.SPEED_INCREASE, false)),
				AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(0)))
			}) + AM.GetCooldownText(SkillDataBaseTargetPractice.COOLDOWN, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), new object[]
			{
				AM.csa(GameMath.GetPercentString(SkillDataBaseTargetPractice.BASE_MISS_CHANCE, false)),
				AM.csa(GameMath.GetPercentString(SkillDataBaseTargetPractice.BASE_MISS_CHANCE + SkillDataBaseTargetPractice.MISS_CHANCE_ADD, false)),
				AM.csa(GameMath.GetPercentString(SkillDataBaseTargetPractice.SPEED_INCREASE, false)),
				AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(SkillDataBaseTargetPractice.DURATION_LEVEL) + ")")
			}) + AM.GetCooldownText(SkillDataBaseTargetPractice.COOLDOWN, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), new object[]
			{
				AM.csa(GameMath.GetPercentString(SkillDataBaseTargetPractice.BASE_MISS_CHANCE, false)),
				AM.csa(GameMath.GetPercentString(SkillDataBaseTargetPractice.BASE_MISS_CHANCE + SkillDataBaseTargetPractice.MISS_CHANCE_ADD, false)),
				AM.csa(GameMath.GetPercentString(SkillDataBaseTargetPractice.SPEED_INCREASE, false)),
				AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level)))
			}) + AM.GetCooldownText(SkillDataBaseTargetPractice.COOLDOWN, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.liaUltiStart, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voLiaUlti, 1f));
		}

		public float GetDuration(int level)
		{
			return SkillDataBaseTargetPractice.DURATION_INIT + SkillDataBaseTargetPractice.DURATION_LEVEL * (float)level;
		}

		public static float COOLDOWN = 150f;

		public static float DURATION_INIT = 8f;

		public static float DURATION_LEVEL = 2f;

		public static float SPEED_INCREASE = 2.5f;

		public static double DAMAGE_INCREASE = 2.5;

		public static float BASE_MISS_CHANCE = 0.2f;

		public static float MISS_CHANCE_ADD = 0.4f;
	}
}
