using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class SkillDataBaseMiniCannon : SkillActiveDataBase
	{
		public SkillDataBaseMiniCannon()
		{
			this.nameKey = "SKILL_NAME_MINI_CANNON";
			this.descKey = "SKILL_DESC_MINI_CANNON";
			this.requiredHeroLevel = 0;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			int num = 0;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseConstitution)
				{
					num += AM.LinearEquationInteger(skillEnhancer.level, SkillEnhancerBaseConstitution.LEVEL_TIMES, SkillEnhancerBaseConstitution.INITIAL_TIMES);
				}
			}
			int num2 = 18 + num;
			int num3 = Mathf.CeilToInt((float)num2 * 1f / 6f);
			this.timeFadeOutStart = 1.23333335f;
			this.timeFadeOutEnd = 1.4333334f;
			this.durNonslowable = 1.33333337f + (float)num3 * 0.6f + 1.13333333f;
			this.durStayInFrontCurtain = this.durNonslowable;
			data.dur = 1.33333337f + (float)num3 * 0.6f + 1.13333333f;
			data.durInvulnurability = data.dur;
			data.cooldownMax = 90f;
			data.events = new List<SkillEvent>();
			for (int i = 0; i < num2; i++)
			{
				SkillEventProjectile skillEventProjectile = new SkillEventProjectile();
				skillEventProjectile.canCrit = true;
				data.events.Add(skillEventProjectile);
				skillEventProjectile.time = 1.33333337f + (float)num3 * 0.6f * ((float)i * 1f / (float)num2);
				skillEventProjectile.damageInDps = this.GetDamage(level);
				skillEventProjectile.damageType = DamageType.SKILL;
				skillEventProjectile.targetType = Projectile.TargetType.SINGLE_ENEMY;
				skillEventProjectile.projectileType = Projectile.Type.APPLE;
				skillEventProjectile.visualEffect = new VisualEffect(VisualEffect.Type.HIT, 0.2f);
				skillEventProjectile.path = new ProjectilePathLinear();
				skillEventProjectile.durFly = 0.12f;
			}
			data.animEvents = new List<SkillAnimEvent>();
			for (int j = 0; j < num3; j++)
			{
				SkillAnimEvent skillAnimEvent = new SkillAnimEvent();
				data.animEvents.Add(skillAnimEvent);
				skillAnimEvent.animIndex = 1;
				skillAnimEvent.time = 1.33333337f + 0.6f * (float)j;
			}
			SkillAnimEvent skillAnimEvent2 = new SkillAnimEvent();
			data.animEvents.Add(skillAnimEvent2);
			skillAnimEvent2.animIndex = 2;
			skillAnimEvent2.time = 1.33333337f + 0.6f * (float)num3;
		}

		public override string GetDescZero()
		{
			int num = 18;
			double damage = this.GetDamage(0);
			return string.Format(LM.Get(this.descKey), AM.csa(num.ToString()), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(90f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			int count = (data as SkillActiveData).events.Count;
			double damage = this.GetDamage(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(count.ToString()), AM.csa(GameMath.GetPercentString(damage, false)) + AM.csl(" (+" + GameMath.GetPercentString(0.75, false) + ")")) + AM.GetCooldownText(90f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			int count = (data as SkillActiveData).events.Count;
			double damage = this.GetDamage(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(count.ToString()), AM.csa(GameMath.GetPercentString(damage, false))) + AM.GetCooldownText(90f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundLennyUlti(skillData, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voLennyUlti, 1f));
		}

		public double GetDamage(int level)
		{
			return 2.0 + (double)level * 0.75;
		}

		private const float COOLDOWN = 90f;

		private const int NUM_ATTACKS = 18;

		private const int ATTACK_PER_ANIM = 6;

		private const float DUR_START = 1.33333337f;

		private const float DUR_ATTACK_ANIM = 0.6f;

		private const float DUR_END = 1.13333333f;

		private const double DAMAGE_INIT = 2.0;

		private const double DAMAGE_PER_LEVEL = 0.75;
	}
}
