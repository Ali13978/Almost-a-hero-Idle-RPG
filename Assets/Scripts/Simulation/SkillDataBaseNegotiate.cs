using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class SkillDataBaseNegotiate : SkillActiveDataBase
	{
		public SkillDataBaseNegotiate()
		{
			this.nameKey = "SKILL_NAME_NEGOTIATE";
			this.descKey = "SKILL_DESC_NEGOTIATE";
			this.requiredHeroLevel = 0;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.dur = 3.6f;
			data.durInvulnurability = 3.2f;
			data.cooldownMax = 120f;
			data.events = new List<SkillEvent>();
			SkillEventProjectile skillEventProjectile = new SkillEventProjectile();
			data.events.Add(skillEventProjectile);
			skillEventProjectile.time = 2.6f;
			skillEventProjectile.projectileType = Projectile.Type.GOBLIN_SMOKE_BOMB;
			skillEventProjectile.targetType = Projectile.TargetType.NONE;
			skillEventProjectile.durFly = 0.5f;
			skillEventProjectile.targetPosition = new Vector3(0.4f, -0.6f, 0f);
			skillEventProjectile.path = new ProjectilePathJumpy
			{
				dontJumpFirst = true,
				jumpCount = 3,
				heightAddMax = 0.3f
			};
			skillEventProjectile.damageInDps = 0.0;
			skillEventProjectile.damageInTeamDps = 0.0;
			skillEventProjectile.visualEffect = new VisualEffect(VisualEffect.Type.GOBLIN_SMOKE, 2.067f);
			BuffEventStealGold buffEventStealGold = new BuffEventStealGold();
			buffEventStealGold.time = 0f;
			buffEventStealGold.goldFactor = this.GetGold(level);
			BuffDataNegotiate buffDataNegotiate = new BuffDataNegotiate();
			buffDataNegotiate.id = 288;
			buffDataNegotiate.dur = this.GetDurEnemy(level);
			buffDataNegotiate.missChanceDebuff = new BuffDataMissChanceAdd();
			buffDataNegotiate.missChanceDebuff.id = 132;
			buffDataNegotiate.missChanceDebuff.isPermenant = false;
			buffDataNegotiate.missChanceDebuff.isStackable = false;
			buffDataNegotiate.missChanceDebuff.missChanceAdd = this.GetMissChance(level);
			buffDataNegotiate.missChanceDebuff.visuals |= 2048;
			buffDataNegotiate.missChanceDebuff.dur = 1f;
			buffDataNegotiate.events = new List<BuffEvent>();
			buffDataNegotiate.events.Add(buffEventStealGold);
			SkillEventBuffSelf skillEventBuffSelf = new SkillEventBuffSelf();
			skillEventBuffSelf.time = 3.1f;
			skillEventBuffSelf.buff = buffDataNegotiate;
			data.events.Add(skillEventBuffSelf);
		}

		public override string GetDescZero()
		{
			double gold = this.GetGold(0);
			float missChance = this.GetMissChance(0);
			float durEnemy = this.GetDurEnemy(0);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(durEnemy)), AM.csa(GameMath.GetPercentString(missChance, false)), AM.csa(GameMath.GetPercentString(gold, false))) + AM.GetCooldownText(120f, -1f);
		}

		public override string GetDesc(SkillData data)
		{
			double gold = this.GetGold(data.level);
			float missChance = this.GetMissChance(data.level);
			float durEnemy = this.GetDurEnemy(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(durEnemy)), AM.csa(GameMath.GetPercentString(missChance, false)), AM.csa(GameMath.GetPercentString(gold, false)) + AM.csl(" (+" + GameMath.GetPercentString(0.25f, false) + ")")) + AM.GetCooldownText(120f, -1f);
		}

		public override string GetDescFull(SkillData data)
		{
			float durEnemy = this.GetDurEnemy(data.level);
			float missChance = this.GetMissChance(data.level);
			double gold = this.GetGold(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(durEnemy)), AM.csa(GameMath.GetPercentString(missChance, false)), AM.csa(GameMath.GetPercentString(gold, false))) + AM.GetCooldownText(120f, -1f);
		}

		public override void PlaySound(World world, Unit by, SkillActiveData skillData)
		{
			base.AddSoundEvent(world, by, new SoundSimple(SoundArchieve.inst.goblinAutoSkills[0], 1f));
			base.AddSoundVoEvent(world, by, new SoundVariedSimple(SoundArchieve.inst.voGoblinNegotiate, 1f));
		}

		public double GetGold(int level)
		{
			return (double)(1f + 0.25f * (float)level);
		}

		public float GetDurEnemy(int level)
		{
			return 20f + 0f * (float)level;
		}

		public float GetMissChance(int level)
		{
			return 0.4f + 0f * (float)level;
		}

		private const float COOLDOWN = 120f;

		private const float GOLD_INITIAL = 1f;

		private const float GOLD_LEVEL = 0.25f;

		private const float MISS_INITIAL = 0.4f;

		private const float MISS_LEVEL = 0f;

		private const float DUR_START = 0f;

		private const float DUR_ANIM = 2.067f;

		private const float ENEMY_DURATION_INITIAL = 20f;

		private const float ENEMY_DURATION_LEVEL = 0f;
	}
}
