using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseWeepingSong : SkillActiveDataBase
	{
		public SkillDataBaseWeepingSong()
		{
			this.nameKey = "SKILL_NAME_WEEPING_SONG";
			this.descKey = "SKILL_DESC_WEEPING_SONG";
			this.timeFadeInStart = 0.1f;
			this.timeFadeInEnd = 0.3f;
			this.timeFadeOutStart = 0.1f;
			this.timeFadeOutEnd = 0.3f;
			this.requiredHeroLevel = 1;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			int num = -1;
			int num2 = -1;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseDepression)
				{
					num = skillEnhancer.level;
				}
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseNotSoFast)
				{
					num2 = skillEnhancer.level;
				}
			}
			float dur = 14f;
			data.dur = 12f;
			data.cooldownMax = this.GetCooldown(level);
			BuffDataWeepingSong buffDataWeepingSong = new BuffDataWeepingSong();
			buffDataWeepingSong.dur = dur;
			BuffDataDefense buffDataDefense = new BuffDataDefense();
			buffDataDefense.id = 56;
			buffDataDefense.isPermenant = false;
			buffDataDefense.isStackable = false;
			buffDataDefense.dur = 1f;
			buffDataDefense.damageTakenFactor = 1.0 + (double)this.GetDefReduction(level);
			buffDataDefense.visuals |= 32;
			buffDataWeepingSong.defenseDebuff = buffDataDefense;
			if (num >= 0)
			{
				BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
				buffDataAttackSpeed.id = 6;
				buffDataAttackSpeed.isPermenant = false;
				buffDataAttackSpeed.isStackable = false;
				buffDataAttackSpeed.dur = 1f;
				buffDataAttackSpeed.attackSpeedAdd = -SkillEnhancerBaseDepression.GetSlow(num);
				buffDataAttackSpeed.visuals |= 2;
				buffDataWeepingSong.attackSpeedDebuff = buffDataAttackSpeed;
			}
			if (num2 >= 0)
			{
				buffDataWeepingSong.dropGoldDebuff = new BuffDataDropGold
				{
					id = 135,
					isPermenant = false,
					isStackable = false,
					dur = 1f,
					dropGoldFactorAdd = (double)SkillEnhancerBaseNotSoFast.GetGold(num2)
				};
			}
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			skillEventBuffSelf.time = 0.1f;
			skillEventBuffSelf.buff = buffDataWeepingSong;
			data.events = new List<SkillEvent>
			{
				skillEventBuffSelf
			};
		}

		private float GetCooldown(int level)
		{
			return 160f - (float)level * 10f;
		}

		private float GetDefReduction(int level)
		{
			return 0.27f + (float)level * 0.06f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDefReduction(0), false)), AM.csa(GameMath.GetTimeInSecondsString(14f))) + AM.GetCooldownText(this.GetCooldown(0), -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDefReduction(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.06f, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(14f))) + AM.GetCooldownText(this.GetCooldown(data.level), 10f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDefReduction(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(14f))) + AM.GetCooldownText(this.GetCooldown(data.level), -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voJimSkillWeepingsong, 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(1.6f, SoundArchieve.inst.jimWeepingSong[0], 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(3.6f, SoundArchieve.inst.jimWeepingSong[1], 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(5.8f, SoundArchieve.inst.jimWeepingSong[2], 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(7.8f, SoundArchieve.inst.jimWeepingSong[3], 1f));
		}

		private const float COOLDOWN = 160f;

		private const float COOLDOWN_DEC = 10f;

		private const float DEF_REDUCTION_BASE = 0.27f;

		private const float DEF_REDUCTION_LEVEL = 0.06f;

		private const float BUFF_DURATION = 14f;

		private const float DUR_BEGIN = 0.1f;

		private const float DUR_MIDDLE = 12f;

		private const float DUR_END = 0.1f;
	}
}
