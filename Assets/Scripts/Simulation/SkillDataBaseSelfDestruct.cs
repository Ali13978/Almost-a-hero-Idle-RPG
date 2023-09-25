using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class SkillDataBaseSelfDestruct : SkillActiveDataBase
	{
		public SkillDataBaseSelfDestruct()
		{
			this.nameKey = "SKILL_NAME_KAMIKAZE";
			this.descKey = "SKILL_DESC_KAMIKAZE";
			this.requiredHeroLevel = 0;
			this.maxLevel = 10;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 3.2f;
			data.durInvulnurability = 3.2f;
			data.cooldownMax = 130f;
			data.events = new List<SkillEvent>();
			SkillEventDamageAll skillEventDamageAll = new SkillEventDamageAll();
			skillEventDamageAll.canCrit = true;
			data.events.Add(skillEventDamageAll);
			skillEventDamageAll.time = data.dur * 0.65f;
			skillEventDamageAll.damageType = DamageType.SKILL;
			skillEventDamageAll.damageInDps = this.GetDamage(level);
			SkillEventDamageSelfWithoutKilling skillEventDamageSelfWithoutKilling = new SkillEventDamageSelfWithoutKilling();
			data.events.Add(skillEventDamageSelfWithoutKilling);
			skillEventDamageSelfWithoutKilling.time = skillEventDamageAll.time;
			skillEventDamageSelfWithoutKilling.healthRatio = 0.5;
			SkillEventVisualEffectByPos skillEventVisualEffectByPos = new SkillEventVisualEffectByPos
			{
				visualEffect = new VisualEffect(VisualEffect.Type.BOMBERMAN_DINAMIT, 0.5f),
				relativePos = new Vector3(0.85f, 0f)
			};
			data.events.Add(skillEventVisualEffectByPos);
			skillEventVisualEffectByPos.time = skillEventDamageAll.time;
		}

		public override string GetDescZero()
		{
			double damage = this.GetDamage(0);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(130f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			double damage = this.GetDamage(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damage, false)) + AM.csl(" (+" + GameMath.GetPercentString(15.0, false) + ")")) + AM.GetCooldownText(130f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			double damage = this.GetDamage(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(130f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.bombermanUlti, 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(skillData.events[0].time, SoundArchieve.inst.bombermanUltiExplosion, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voBombermanUlti, 1f));
		}

		public double GetDamage(int level)
		{
			return 30.0 + 15.0 * (double)level;
		}

		private const float COOLDOWN = 130f;

		private const double DMG_INITIAL = 30.0;

		private const double DMG_LEVEL = 15.0;
	}
}
