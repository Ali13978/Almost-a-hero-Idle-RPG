using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseBombard : SkillActiveDataBase
	{
		public SkillDataBaseBombard()
		{
			this.nameKey = "SKILL_NAME_BOMBARD";
			this.descKey = "SKILL_DESC_BOMBARD";
			this.requiredHeroLevel = 1;
			this.maxLevel = 2;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.cooldownMax = 25f;
			int numAttacks = this.GetNumAttacks(level);
			data.dur = 2.9333334f + (float)numAttacks * 0.6f + 0.533333361f;
			data.durInvulnurability = 0f;
			data.cooldownMax = this.GetCooldown(level);
			data.events = new List<SkillEvent>();
			for (int i = 0; i < numAttacks; i++)
			{
				SkillEventProjectile skillEventProjectile = new SkillEventProjectile();
				data.events.Add(skillEventProjectile);
				skillEventProjectile.time = 2.9333334f + 0.6f * (float)i + 0.06666667f;
				skillEventProjectile.targetType = Projectile.TargetType.ALL_ENEMIES;
				skillEventProjectile.projectileType = Projectile.Type.APPLE_BOMBARD;
				skillEventProjectile.damageType = DamageType.SKILL;
				skillEventProjectile.visualEffect = new VisualEffect(VisualEffect.Type.BOMBERMAN_DINAMIT, 0.5f);
				skillEventProjectile.visualEffect.skinIndex = 0;
				skillEventProjectile.path = new ProjectilePathBomb
				{
					heightAddMax = 0.5f
				};
				skillEventProjectile.damageInDps = 2.0;
				skillEventProjectile.durFly = 0.7f;
			}
			data.animEvents = new List<SkillAnimEvent>();
			for (int j = 0; j < numAttacks; j++)
			{
				SkillAnimEvent skillAnimEvent = new SkillAnimEvent();
				data.animEvents.Add(skillAnimEvent);
				skillAnimEvent.animIndex = 1;
				skillAnimEvent.time = 2.9333334f + 0.6f * (float)j;
			}
			SkillAnimEvent skillAnimEvent2 = new SkillAnimEvent();
			data.animEvents.Add(skillAnimEvent2);
			skillAnimEvent2.animIndex = 2;
			skillAnimEvent2.time = data.events[numAttacks - 1].time + 0.6f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetNumAttacks(0).ToString())) + AM.GetCooldownText(this.GetCooldown(0), -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetNumAttacks(data.level).ToString()) + AM.csl(" (+" + 2.ToString() + ")")) + AM.GetCooldownText(this.GetCooldown(data.level), 20f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetNumAttacks(data.level).ToString())) + AM.GetCooldownText(this.GetCooldown(data.level), -1f);
		}

		public int GetNumAttacks(int level)
		{
			return 4 + level * 2;
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundDelayed(0.2f, SoundArchieve.inst.lennyAutoSkills[1], 1f));
			int i = 0;
			int num = SoundArchieve.inst.lennyBombardAttacks.Length;
			int count = skillData.events.Count;
			while (i < count)
			{
				base.AddSoundEvent(world, by, new SoundDelayed(skillData.events[i].time, SoundArchieve.inst.lennyBombardAttacks[i % num], 1f));
				i++;
			}
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voLennySkillAuto2, 1f));
		}

		public float GetCooldown(int level)
		{
			return 120f - (float)level * 20f;
		}

		private const float COOLDOWN = 120f;

		private const float COOLDOWN_DEC = 20f;

		private const int INITIAL_NUM_ATTACKS = 4;

		private const int LEVEL_NUM_ATTACKS = 2;

		private const float DUR_START = 2.9333334f;

		private const float DUR_ATTACK = 0.6f;

		private const float DUR_END = 0.533333361f;

		private const float PROJ_TIME_OFFSET = 0.06666667f;
	}
}
