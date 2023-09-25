using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseBittersweet : SkillActiveDataBase
	{
		public SkillDataBaseBittersweet()
		{
			this.nameKey = "SKILL_NAME_BITTERSWEET";
			this.descKey = "SKILL_DESC_BITTERSWEET";
			this.timeFadeInStart = 0.1f;
			this.timeFadeInEnd = 0.3f;
			this.timeFadeOutStart = 1f;
			this.timeFadeOutEnd = 1.2f;
			this.requiredHeroLevel = 0;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			float durMiddle = this.GetDurMiddle(level);
			data.dur = 1f + durMiddle + 1f;
			data.durInvulnurability = 5f;
			data.cooldownMax = 360f;
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			data.events = new List<SkillEvent>
			{
				skillEventBuffSelf
			};
			skillEventBuffSelf.time = 1f;
			BuffDataBittersweet buffDataBittersweet = new BuffDataBittersweet();
			buffDataBittersweet.id = 15;
			skillEventBuffSelf.buff = buffDataBittersweet;
			buffDataBittersweet.isPermenant = false;
			buffDataBittersweet.isStackable = false;
			buffDataBittersweet.dur = durMiddle;
			buffDataBittersweet.skillCooldownFactor = 1f + this.GetCdFactor(level);
			data.animEvents = new List<SkillAnimEvent>();
			int num = GameMath.RoundDoubleToInt((double)durMiddle / 1.333);
			float num2 = durMiddle / (float)num;
			for (int i = 0; i < num; i++)
			{
				SkillAnimEvent skillAnimEvent = new SkillAnimEvent();
				data.animEvents.Add(skillAnimEvent);
				skillAnimEvent.animIndex = 1;
				skillAnimEvent.time = 1f + num2 * (float)i;
			}
			SkillAnimEvent skillAnimEvent2 = new SkillAnimEvent();
			data.animEvents.Add(skillAnimEvent2);
			skillAnimEvent2.animIndex = 2;
			skillAnimEvent2.time = 1f + durMiddle;
		}

		private float GetCdFactor(int level)
		{
			return 5f;
		}

		private float GetDurMiddle(int level)
		{
			return 5f + 0.5f * (float)level;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCdFactor(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDurMiddle(0)))) + AM.GetCooldownText(360f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCdFactor(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDurMiddle(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(0.5f) + ")")) + AM.GetCooldownText(360f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCdFactor(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDurMiddle(data.level)))) + AM.GetCooldownText(360f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voJimUlti, 1f));
			base.AddSoundEvent(world, by, new SoundJimUlti(skillData, 1f));
		}

		private const float CD_FACTOR_BASE = 5f;

		private const float DUR_START = 1f;

		private const float DUR_MIDDLE_BASE = 5f;

		private const float DUR_MIDDLE_LEVEL = 0.5f;

		private const float DUR_END = 1f;

		private const float COOLDOWN = 360f;
	}
}
