using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseOutOfControl : SkillActiveDataBase
	{
		public SkillDataBaseOutOfControl()
		{
			this.nameKey = "SKILL_NAME_OUT_OF_CONTROL";
			this.descKey = "SKILL_DESC_OUT_OF_CONTROL";
			this.requiredHeroLevel = 1;
			this.maxLevel = 2;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			double num = 0.0;
			foreach (SkillEnhancer skillEnhancer in enhancers)
			{
				if (skillEnhancer.enhancerBase is SkillEnhancerBaseTricks)
				{
					num += AM.LinearEquationDouble(SkillEnhancerBaseTricks.LEVEL_BONUS, (double)skillEnhancer.level, SkillEnhancerBaseTricks.INITIAL_BONUS);
				}
			}
			int numAttacks = this.GetNumAttacks(level);
			data.dur = 1.33333337f + (float)numAttacks * 0.333333343f + 0.6666667f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 60f;
			data.events = new List<SkillEvent>();
			for (int i = 0; i < numAttacks; i++)
			{
				SkillEventProjectile skillEventProjectile = new SkillEventProjectile();
				data.events.Add(skillEventProjectile);
				skillEventProjectile.damageType = DamageType.SKILL;
				skillEventProjectile.time = 1.33333337f + 0.333333343f * (float)i;
				skillEventProjectile.canCrit = true;
				skillEventProjectile.damageInDps = 2.0 * (1.0 + num);
				skillEventProjectile.projectileType = Projectile.Type.DEREK_MAGIC_BALL;
				skillEventProjectile.durFly = 0.3f;
				skillEventProjectile.path = new ProjectilePathWiggly();
			}
			data.animEvents = new List<SkillAnimEvent>();
			for (int j = 0; j < numAttacks; j++)
			{
				SkillAnimEvent skillAnimEvent = new SkillAnimEvent();
				data.animEvents.Add(skillAnimEvent);
				skillAnimEvent.animIndex = 1;
				skillAnimEvent.time = data.events[j].time;
			}
			SkillAnimEvent skillAnimEvent2 = new SkillAnimEvent();
			data.animEvents.Add(skillAnimEvent2);
			skillAnimEvent2.animIndex = 2;
			skillAnimEvent2.time = data.events[numAttacks - 1].time + 0.333333343f;
		}

		public override string GetDescZero()
		{
			double number = 2.0;
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetNumAttacks(0).ToString()), AM.csa(GameMath.GetPercentString(number, false))) + AM.GetCooldownText(60f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			double damageInDps = ((data as SkillActiveData).events[0] as SkillEventProjectile).damageInDps;
			int count = (data as SkillActiveData).events.Count;
			return string.Format(LM.Get(this.descKey), AM.csa(count.ToString()) + AM.csl(" (+" + 2.ToString() + ")"), AM.csa(GameMath.GetPercentString(damageInDps, false))) + AM.GetCooldownText(60f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			double damageInDps = ((data as SkillActiveData).events[0] as SkillEventProjectile).damageInDps;
			int count = (data as SkillActiveData).events.Count;
			return string.Format(LM.Get(this.descKey), AM.csa(count.ToString()), AM.csa(GameMath.GetPercentString(damageInDps, false))) + AM.GetCooldownText(60f, -1f);
		}

		public int GetNumAttacks(int level)
		{
			return 5 + level * 2;
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundDerekSkillAuto(0, skillData, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voDerekSkillAuto1, 1f));
		}

		private const float COOLDOWN = 60f;

		private const float DUR_START = 1.33333337f;

		private const float DUR_ATTACK = 0.333333343f;

		private const float DUR_END = 0.6666667f;

		private const int LEVEL_NUM_ATTACKS = 2;

		private const int NUM_ATTACKS = 5;

		private const double INITIAL_DAMAGE = 2.0;
	}
}
