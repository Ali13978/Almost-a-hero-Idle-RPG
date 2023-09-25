using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseMasterThief : SkillActiveDataBase
	{
		public SkillDataBaseMasterThief()
		{
			this.nameKey = "SKILL_NAME_MASTER_THIEF";
			this.descKey = "SKILL_DESC_MASTER_THIEF";
			this.requiredHeroLevel = 0;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 0f;
			data.durInvulnurability = 5f;
			data.cooldownMax = 210f;
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = data.dur;
			BuffDataMasterThief buffDataMasterThief = new BuffDataMasterThief();
			buffDataMasterThief.id = 127;
			skillEventBuffSelf.buff = buffDataMasterThief;
			buffDataMasterThief.dur = this.GetDuration(level);
			buffDataMasterThief.lootIncBuff = new BuffDataDropGold();
			buffDataMasterThief.lootIncBuff.id = 356;
			buffDataMasterThief.lootIncBuff.dur = 20f;
			buffDataMasterThief.lootIncBuff.isStackable = true;
			buffDataMasterThief.events = new List<BuffEvent>();
			BuffEventChangeWeaponToTemp buffEventChangeWeaponToTemp = new BuffEventChangeWeaponToTemp();
			buffDataMasterThief.events.Add(buffEventChangeWeaponToTemp);
			buffEventChangeWeaponToTemp.time = 0f;
			buffEventChangeWeaponToTemp.durChange = 1.36666667f;
			WeaponWoodAllRanged weaponWoodAllRanged = new WeaponWoodAllRanged();
			weaponWoodAllRanged.id = 106;
			buffEventChangeWeaponToTemp.weapon = weaponWoodAllRanged;
			weaponWoodAllRanged.SetTiming(0.8666667f, 0.4f, 0f);
			weaponWoodAllRanged.projectileType = Projectile.Type.SHEELA;
			weaponWoodAllRanged.targetType = Projectile.TargetType.SINGLE_ENEMY;
			weaponWoodAllRanged.durFly = 0.25f;
			weaponWoodAllRanged.projectilePath = new ProjectilePathLinear();
			weaponWoodAllRanged.damageType = DamageType.SKILL;
			weaponWoodAllRanged.soundsAttack = new List<TimedSound>
			{
				new TimedSound(0.37931034f, new SoundVariedSimple(SoundArchieve.inst.sheelaAttacks, 1f))
			};
			BuffEventChangeWeaponToOrig buffEventChangeWeaponToOrig = new BuffEventChangeWeaponToOrig();
			buffDataMasterThief.events.Add(buffEventChangeWeaponToOrig);
			buffEventChangeWeaponToOrig.time = buffDataMasterThief.dur;
			buffEventChangeWeaponToOrig.durChange = 0.7f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(0))), AM.csa(GameMath.GetPercentString(0.35, false)), AM.csa(GameMath.GetTimeInSecondsString(20f))) + AM.GetCooldownText(210f, -1f);
		}

		private float GetDuration(int level)
		{
			return 12f + (float)level * 2f;
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInSecondsString(2f) + ")"), AM.csa(GameMath.GetPercentString(0.35, false)), AM.csa(GameMath.GetTimeInSecondsString(20f))) + AM.GetCooldownText(210f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level))), AM.csa(GameMath.GetPercentString(0.35, false)), AM.csa(GameMath.GetTimeInSecondsString(20f))) + AM.GetCooldownText(210f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.sheelaUlti, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voSheelaUlti, 1f));
		}

		private const float COOLDOWN = 210f;

		private const float DURATION = 12f;

		private const float LEVEL_DURATION = 2f;

		private const double LOOT_INIT = 0.35;

		private const float LOOT_DURATION = 20f;
	}
}
