using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseReversedExcalibur : SkillActiveDataBase
	{
		public SkillDataBaseReversedExcalibur()
		{
			this.nameKey = "SKILL_NAME_REVERSED_EXCALIBUR";
			this.descKey = "SKILL_DESC_REVERSED_EXCALIBUR";
			this.requiredHeroLevel = 1;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 2.5f;
			data.durInvulnurability = 0f;
			data.cooldownMax = 90f;
			data.events = new List<SkillEvent>();
			SkillEventProjectile skillEventProjectile = new SkillEventProjectile();
			data.events.Add(skillEventProjectile);
			skillEventProjectile.time = data.dur * 0.66f;
			skillEventProjectile.canCrit = true;
			skillEventProjectile.projectileType = Projectile.Type.REVERSED_EXCALIBUR_MUD;
			skillEventProjectile.targetType = Projectile.TargetType.ALL_ENEMIES;
			skillEventProjectile.damageType = DamageType.SKILL;
			skillEventProjectile.durFly = 0.32f;
			skillEventProjectile.path = new ProjectilePathLinear();
			skillEventProjectile.damageInDps = AM.LinearEquationDouble((double)level, 2.0, 4.0);
			skillEventProjectile.visualEffect = new VisualEffect(VisualEffect.Type.REVERSED_EXCALIBUR_MUD, 0.5f);
			BuffDataStun buffDataStun = new BuffDataStun();
			buffDataStun.id = 168;
			buffDataStun.visuals |= 512;
			skillEventProjectile.buffs = new List<BuffData>
			{
				buffDataStun
			};
			buffDataStun.dur = this.GetStunDuration(level);
		}

		private float GetStunDuration(int level)
		{
			return 2f + (float)level * 1f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetStunDuration(0)))) + AM.GetCooldownText(90f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(2.0, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetStunDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(1f) + ")")) + AM.GetCooldownText(90f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetStunDuration(data.level)))) + AM.GetCooldownText(90f, -1f);
		}

		public double GetDamageBonus(int level)
		{
			return 4.0 + 2.0 * (double)level;
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.horatioReverseExcaliburTakeGround, 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(0.5f, SoundArchieve.inst.horatioReverseExcaliburInsertSword, 1f));
			base.AddSoundEvent(world, by, new SoundDelayed(1.8f, SoundArchieve.inst.horatioReverseExcaliburThrowRock, 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voHoratioSkillAuto1, 1f));
		}

		private const float COOLDOWN = 90f;

		private const double INITIAL_DAMAGE_BONUS = 4.0;

		private const double LEVEL_DAMAGE_BONUS = 2.0;

		private const float INITIAL_STUN_DURATION = 2f;

		private const float LEVEL_STUN_DURATION = 1f;
	}
}
