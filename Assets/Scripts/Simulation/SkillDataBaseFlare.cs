using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseFlare : SkillActiveDataBase
	{
		public SkillDataBaseFlare()
		{
			this.nameKey = "SKILL_NAME_FLARE";
			this.descKey = "SKILL_DESC_FLARE";
			this.timeFadeInStart = 0.1f;
			this.timeFadeInEnd = 0.3f;
			this.timeFadeOutStart = 1f;
			this.timeFadeOutEnd = 1.2f;
			this.requiredHeroLevel = 1;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			float dur = this.GetDur(level);
			data.dur = 4f;
			data.cooldownMax = 150f;
			SkillEventVisualEffect skillEventVisualEffect = new SkillEventVisualEffect();
			skillEventVisualEffect.time = 4f;
			skillEventVisualEffect.visualEffect = new VisualEffect(VisualEffect.Type.TAM_FLARE, dur + 4f);
			BuffDataFlare buffDataFlare = new BuffDataFlare();
			buffDataFlare.dur = dur;
			buffDataFlare.isPermenant = false;
			buffDataFlare.isStackable = false;
			buffDataFlare.id = 84;
			buffDataFlare.defenseDebuff = new BuffDataDefense();
			buffDataFlare.defenseDebuff.id = 55;
			buffDataFlare.defenseDebuff.isPermenant = false;
			buffDataFlare.defenseDebuff.isStackable = false;
			buffDataFlare.defenseDebuff.dur = 1f;
			buffDataFlare.defenseDebuff.damageTakenFactor = (double)(1f + this.GetDamageFactor(level));
			buffDataFlare.defenseDebuff.visuals |= 32;
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			skillEventBuffSelf.time = 1.2f;
			skillEventBuffSelf.buff = buffDataFlare;
			data.events = new List<SkillEvent>
			{
				skillEventVisualEffect,
				skillEventBuffSelf
			};
		}

		private float GetDamageFactor(int level)
		{
			return 0.3f + (float)level * 0.05f;
		}

		private float GetDur(int level)
		{
			return 8f + 2f * (float)level;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(0)))) + AM.GetCooldownText(150f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05f, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(2f) + ")")) + AM.GetCooldownText(150f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(data.level)))) + AM.GetCooldownText(150f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voTamFlare, 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(0.55f, SoundArchieve.inst.tamFlare, 1f));
		}

		private const float DMG_TAKEN_FACTOR_BASE = 0.3f;

		private const float DMG_TAKEN_FACTOR_LEVEL = 0.05f;

		private const float DUR_START = 4f;

		private const float DUR_BASE = 8f;

		private const float DUR_LEVEL = 2f;

		private const float COOLDOWN = 150f;
	}
}
