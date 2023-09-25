using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseFastCheerful : SkillActiveDataBase
	{
		public SkillDataBaseFastCheerful()
		{
			this.nameKey = "SKILL_NAME_FAST_AND_CHEERFUL";
			this.descKey = "SKILL_DESC_FAST_AND_CHEERFUL";
			this.requiredHeroLevel = 1;
			this.maxLevel = 3;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 2.0333333f;
			data.durInvulnurability = 0f;
			data.cooldownMax = this.GetCooldown(level);
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = 1.73333335f;
			BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
			buffDataAttackSpeed.id = 4;
			skillEventBuffSelf.buff = buffDataAttackSpeed;
			buffDataAttackSpeed.isStackable = true;
			buffDataAttackSpeed.dur = this.GetDuration(level);
			buffDataAttackSpeed.attackSpeedAdd = 1f;
			buffDataAttackSpeed.visuals |= 1;
			SkillEventBuffSelf skillEventBuffSelf2 = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf2);
			skillEventBuffSelf2.time = data.dur;
			BuffDataWeaponAntiHeat buffDataWeaponAntiHeat = new BuffDataWeaponAntiHeat();
			buffDataWeaponAntiHeat.id = 192;
			skillEventBuffSelf2.buff = buffDataWeaponAntiHeat;
			buffDataWeaponAntiHeat.dur = this.GetDuration(level);
			BuffEventCancelCurrentOverheat buffEventCancelCurrentOverheat = new BuffEventCancelCurrentOverheat();
			buffDataWeaponAntiHeat.events = new List<BuffEvent>();
			buffDataWeaponAntiHeat.events.Add(buffEventCancelCurrentOverheat);
			buffEventCancelCurrentOverheat.time = 0f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(1f, false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(0)))) + AM.GetCooldownText(140f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(1f, false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInSecondsString(3f) + ")")) + AM.GetCooldownText(this.GetCooldown(data.level), 30f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(1f, false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level)))) + AM.GetCooldownText(this.GetCooldown(data.level), -1f);
		}

		public float GetDuration(int level)
		{
			return 12f + 3f * (float)level;
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.idaAutoSkills[0], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voIdaSkillAuto1, 1f));
		}

		public float GetCooldown(int level)
		{
			return 140f - (float)level * 30f;
		}

		private const float COOLDOWN = 140f;

		private const float COOLDOWN_DEC = 30f;

		private const float INITIAL_DURATION = 12f;

		private const float LEVEL_DURATION = 3f;

		private const float INITIAL_SPEED = 1f;
	}
}
