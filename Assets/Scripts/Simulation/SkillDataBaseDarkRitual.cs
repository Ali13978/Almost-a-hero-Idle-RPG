using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class SkillDataBaseDarkRitual : SkillActiveDataBase
	{
		public SkillDataBaseDarkRitual()
		{
			this.nameKey = "SKILL_NAME_DARK_RITUAL";
			this.descKey = "SKILL_DESC_DARK_RITUAL";
			this.timeFadeInStart = 0.1f;
			this.timeFadeInEnd = 0.3f;
			this.timeFadeOutStart = 2f;
			this.timeFadeOutEnd = 2.2f;
			this.requiredHeroLevel = 0;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			float num = 0f;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerMyPittyTeam)
				{
					SkillEnhancerMyPittyTeam skillEnhancerMyPittyTeam = skillEnhancer.enhancerBase as SkillEnhancerMyPittyTeam;
					num = skillEnhancerMyPittyTeam.GetIncrease(skillEnhancer.level);
				}
			}
			data.isToggle = true;
			data.dur = 26.6666679f;
			data.durInvulnurability = 0.5f;
			this.durNonslowable = data.dur;
			this.durStayInFrontCurtain = 2.3f;
			data.cooldownMax = 15f;
			data.events = new List<SkillEvent>();
			for (int i = 0; i < 30; i++)
			{
				SkillEventDrainAlliesDamageEnemies skillEventDrainAlliesDamageEnemies = new SkillEventDrainAlliesDamageEnemies();
				skillEventDrainAlliesDamageEnemies.damageType = DamageType.SKILL;
				skillEventDrainAlliesDamageEnemies.damagePerRatio = this.GetDamage(level);
				skillEventDrainAlliesDamageEnemies.allyDamage = this.GetAllyDamage(level);
				skillEventDrainAlliesDamageEnemies.regardlessPerDamage = (double)num;
				skillEventDrainAlliesDamageEnemies.attackIndex = (i + 1) % 2;
				skillEventDrainAlliesDamageEnemies.time = 2f + (float)i * 0.8f + 0.45f;
				if ((i + 1) % 2 == 0)
				{
					skillEventDrainAlliesDamageEnemies.projectileStartPointOffset = new Vector3(0f, 0.1f);
				}
				else
				{
					skillEventDrainAlliesDamageEnemies.projectileStartPointOffset = new Vector3(-0.2f, 0.1f);
				}
				data.events.Add(skillEventDrainAlliesDamageEnemies);
			}
			data.animEvents = new List<SkillAnimEvent>();
			SkillAnimEvent skillAnimEvent = new SkillAnimEvent();
			data.animEvents.Add(skillAnimEvent);
			skillAnimEvent.animIndex = 0;
			skillAnimEvent.time = 2f;
			for (int j = 0; j < 30; j++)
			{
				SkillAnimEvent skillAnimEvent2 = new SkillAnimEvent();
				data.animEvents.Add(skillAnimEvent2);
				if (j % 2 == 1)
				{
					skillAnimEvent2.animIndex = 3;
				}
				else
				{
					skillAnimEvent2.animIndex = 1;
				}
				skillAnimEvent2.time = data.events[j].time - 0.45f;
			}
			SkillAnimEvent skillAnimEvent3 = new SkillAnimEvent();
			data.animEvents.Add(skillAnimEvent3);
			skillAnimEvent3.animIndex = 2;
			skillAnimEvent3.time = data.events[29].time + 0.8f - 0.45f;
		}

		public override string GetDescZero()
		{
			double damage = this.GetDamage(0);
			double allyDamage = this.GetAllyDamage(0);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(allyDamage, false)), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(15f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			SkillActiveData skillActiveData = data as SkillActiveData;
			SkillEventDrainAlliesDamageEnemies skillEventDrainAlliesDamageEnemies = skillActiveData.events[0] as SkillEventDrainAlliesDamageEnemies;
			double damage = this.GetDamage(data.level);
			double number = this.GetDamage(data.level + 1) - this.GetDamage(data.level);
			double allyDamage = skillEventDrainAlliesDamageEnemies.allyDamage;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(allyDamage, false)), AM.csa(GameMath.GetPercentString(damage, false)) + AM.csl(" (+" + GameMath.GetPercentString(number, false) + ")")) + AM.GetCooldownText(15f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			SkillActiveData skillActiveData = data as SkillActiveData;
			SkillEventDrainAlliesDamageEnemies skillEventDrainAlliesDamageEnemies = skillActiveData.events[0] as SkillEventDrainAlliesDamageEnemies;
			double damage = this.GetDamage(data.level);
			double allyDamage = skillEventDrainAlliesDamageEnemies.allyDamage;
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(allyDamage, false)), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(15f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.warlockUltiStart, 1f));
			base.AddSoundEvent(world, by, new SoundWarlockUlti(skillData, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voWarlockUlti, 1f));
		}

		public override void StopSound(World world, Unit by)
		{
			base.StopSound(world, by);
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.warlockUltiEnd, 1f));
		}

		public double GetDamage(int level)
		{
			return 0.4 + (double)level * 0.05;
		}

		public double GetAllyDamage(int level)
		{
			return 0.1 + (double)level * 0.0;
		}

		private const float COOLDOWN_BASE = 15f;

		private const int NUM_PULSE = 30;

		private const double DAMAGE_INIT = 0.4;

		private const double DAMAGE_LEVEL = 0.05;

		private const double DAMAGE_ALLY_INIT = 0.1;

		private const double DAMAGE_ALLY_LEVEL = 0.0;

		private const float DUR_START = 2f;

		private const float DUR_ATTACK = 0.8f;

		private const float DUR_END = 0.6666667f;
	}
}
