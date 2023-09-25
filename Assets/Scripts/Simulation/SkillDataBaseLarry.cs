using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseLarry : SkillActiveDataBase
	{
		public SkillDataBaseLarry()
		{
			this.nameKey = "SKILL_NAME_LARRY";
			this.descKey = "SKILL_DESC_LARRY";
			this.requiredHeroLevel = 1;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			int num = -1;
			int num2 = -1;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerCurly)
				{
					num = skillEnhancer.level;
				}
				else if (skillEnhancer.enhancerBase is SkillEnhancerMoe)
				{
					num2 = skillEnhancer.level;
				}
			}
			data.dur = 3.667f;
			data.durInvulnurability = 3.667f;
			data.cooldownMax = 120f;
			data.events = new List<SkillEvent>();
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			skillEventBuffSelf.time = 2f;
			skillEventBuffSelf.buff = new BuffData();
			skillEventBuffSelf.buff.dur = 1.667f;
			skillEventBuffSelf.buff.events = new List<BuffEvent>();
			int num3 = (num <= -1 && num2 <= -1) ? 0 : 1;
			skillEventBuffSelf.buff.events.Add(new BuffEventSpawnSupportAnimal
			{
				buff = new BuffDataAttackSpeed
				{
					attackSpeedAdd = this.GetAttackSpeed(level),
					dur = 20f,
					id = 346,
					visuals = 1
				},
				skin = SupportAnimal.Skin.Larry,
				spawnSound = new SoundEventSound(SoundType.GAMEPLAY, "larry", false, 0f, new SoundSimple(SoundArchieve.inst.druidLarrySkillAnimalSpawn[num3], 1f)),
				disappearSound = new SoundEventSound(SoundType.GAMEPLAY, "larry", false, 0f, new SoundSimple(SoundArchieve.inst.druidLarrySkillAnimalDisappear[num3], 1f)),
				time = 0f
			});
			if (num > -1)
			{
				skillEventBuffSelf.buff.events.Add(new BuffEventSpawnSupportAnimal
				{
					buff = new BuffDataCritChance
					{
						critChanceAdd = SkillEnhancerCurly.GetCriticalDamageChanceAdd(num),
						dur = 20f,
						id = 347,
						visuals = 4
					},
					skin = SupportAnimal.Skin.Curly,
					time = 0.5f
				});
			}
			if (num2 > -1)
			{
				skillEventBuffSelf.buff.events.Add(new BuffEventSpawnSupportAnimal
				{
					buff = new BuffDataDamageAdd
					{
						damageAdd = SkillEnhancerMoe.GetDamage(num2),
						dur = 20f,
						id = 348,
						visuals = 8
					},
					skin = SupportAnimal.Skin.Moe,
					time = 1f
				});
			}
			data.events.Add(skillEventBuffSelf);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetAttackSpeed(0), false)), AM.csa(GameMath.GetTimeInSecondsString(20f))) + AM.GetCooldownText(120f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetAttackSpeed(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1f, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(20f))) + AM.GetCooldownText(120f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetAttackSpeed(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(20f))) + AM.GetCooldownText(120f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			bool flag = (by as Hero).IsUsingTempWeapon();
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.druidAutoSkills[(!flag) ? 2 : 3], 1f));
			if (flag)
			{
				base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voDruidLarryBeast, 1f));
			}
			else
			{
				base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voDruidLarry, 1f));
			}
		}

		public float GetAttackSpeed(int level)
		{
			return 0.2f + 0.1f * (float)level;
		}

		public const float COOLDOWN = 120f;

		public const float ATTACK_SPEED_INIT = 0.2f;

		public const float ATTACK_SPEED_PER_LEVEL = 0.1f;

		public const float BUFF_DURATION = 20f;

		private const float DUR_ANIM = 3.667f;

		private const float DUR_ANIM_START = 2f;
	}
}
