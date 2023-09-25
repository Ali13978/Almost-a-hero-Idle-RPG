using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseRoar : SkillActiveDataBase
	{
		public SkillDataBaseRoar()
		{
			this.nameKey = "SKILL_NAME_ROAR";
			this.descKey = "SKILL_DESC_ROAR";
			this.timeFadeInStart = 0.1f;
			this.timeFadeInEnd = 0.3f;
			this.timeFadeOutStart = 1f;
			this.timeFadeOutEnd = 1.2f;
			this.requiredHeroLevel = 0;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			float dur = this.GetDur(level);
			data.dur = 2.5f;
			data.cooldownMax = 360f;
			data.events = new List<SkillEvent>();
			SkillEventBuffAllAllies skillEventBuffAllAllies = new SkillEventBuffAllAllies();
			data.events.Add(skillEventBuffAllAllies);
			skillEventBuffAllAllies.time = 0.625f;
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events.Add(skillEventBuffSelf);
			skillEventBuffSelf.time = 0.625f;
			BuffDataRoar buffDataRoar = new BuffDataRoar();
			buffDataRoar.id = 155;
			skillEventBuffAllAllies.buff = buffDataRoar;
			skillEventBuffSelf.buff = buffDataRoar;
			buffDataRoar.isPermenant = false;
			buffDataRoar.isStackable = false;
			buffDataRoar.dur = dur;
			buffDataRoar.heroDamageFactor = 1f + this.GetDamageFactor(level);
			buffDataRoar.visuals |= 8;
			SkillEventBuffAllOpponents skillEventBuffAllOpponents = new SkillEventBuffAllOpponents();
			data.events.Add(skillEventBuffAllOpponents);
			skillEventBuffAllOpponents.time = 0.625f;
			BuffDataStun buffDataStun = new BuffDataStun();
			buffDataStun.id = 171;
			skillEventBuffAllOpponents.buff = buffDataStun;
			buffDataStun.isPermenant = false;
			buffDataStun.isStackable = false;
			buffDataStun.visuals |= 512;
			buffDataStun.dur = 5f;
		}

		private float GetDamageFactor(int level)
		{
			return 0.4f + (float)level * 0.2f;
		}

		private float GetDur(int level)
		{
			return 12f + 1f * (float)level;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(5f)), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(0)))) + AM.GetCooldownText(360f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(5f)), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.2f, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(1f) + ")")) + AM.GetCooldownText(360f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(5f)), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(data.level)))) + AM.GetCooldownText(360f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voTamUlti, 1f));
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.tamUlti, 1f));
		}

		private const float DMG_FACTOR_BASE = 0.4f;

		private const float DMG_FACTOR_LEVEL = 0.2f;

		private const float DUR_START = 2.5f;

		private const float DUR_BASE = 12f;

		private const float DUR_LEVEL = 1f;

		private const float COOLDOWN = 360f;

		private const float STUN_DURATION = 5f;
	}
}
