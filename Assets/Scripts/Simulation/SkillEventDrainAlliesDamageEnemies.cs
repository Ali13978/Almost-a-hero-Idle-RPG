using System;
using UnityEngine;

namespace Simulation
{
	public class SkillEventDrainAlliesDamageEnemies : SkillEvent
	{
		public override void Apply(Unit by)
		{
			double num = 0.0;
			World world = by.world;
			foreach (UnitHealthy unitHealthy in by.GetAllies())
			{
				if (unitHealthy != by)
				{
					if (unitHealthy.IsAlive())
					{
						double healthRatio = unitHealthy.GetHealthRatio();
						double shieldRatio = unitHealthy.GetShieldRatio();
						world.DamageMain(by, unitHealthy, new Damage(unitHealthy.GetHealthMax() * this.allyDamage, false, false, false, false)
						{
							isExact = true,
							type = this.damageType
						});
						num += GameMath.GetMaxDouble(0.0, healthRatio - unitHealthy.GetHealthRatio());
						num += GameMath.GetMaxDouble(0.0, shieldRatio - unitHealthy.GetShieldRatio());
					}
				}
			}
			num *= 100.0;
			SoundDelayed sound = new SoundDelayed(0.3f, SoundArchieve.inst.warlockUltiAttacks[this.attackIndex % 2], 1f);
			this.AddSoundEvent(world, by.GetId(), false, sound);
			double num2 = by.GetDamage() * num * this.damagePerRatio;
			double num3 = by.GetDamage() * this.regardlessPerDamage;
			foreach (UnitHealthy unitHealthy2 in by.GetOpponents())
			{
				if (unitHealthy2.IsAlive())
				{
					Damage damage = new Damage(num2 + num3, false, false, false, false);
					damage.isExact = true;
					damage.type = this.damageType;
					if (GameMath.GetProbabilityOutcome(by.GetCritChance(), GameMath.RandType.NoSeed))
					{
						damage.amount *= by.GetCritFactor();
						damage.isCrit = true;
					}
					Projectile projectile = new Projectile(by, Projectile.Type.WARLOCK_ATTACK, Projectile.TargetType.SINGLE_ENEMY, unitHealthy2, 0.3f, new ProjectilePathDemonicSwarm
					{
						heightAddMax = 0.2f
					});
					projectile.startPointOffset = this.projectileStartPointOffset;
					Hero hero = by as Hero;
					if (hero != null && hero.GetId() == "WARLOCK")
					{
						int numHeroSkins = world.currentSim.GetNumHeroSkins(hero.GetId());
						int num4 = hero.GetEquippedSkinIndex();
						if (this.attackIndex == 1)
						{
							num4 += numHeroSkins;
						}
						projectile.projectileAlternativeIndex = num4;
					}
					else
					{
						projectile.projectileAlternativeIndex = this.attackIndex;
					}
					projectile.damage = damage;
					projectile.InitPath();
					world.AddProjectile(projectile);
				}
			}
			Damage damage2 = new Damage(num * this.damagePerRatio + this.regardlessPerDamage, false, false, false, false);
			damage2.showAsPer = true;
			GlobalPastDamage globalPastDamage = new GlobalPastDamage(by as UnitHealthy, damage2);
			world.AddPastDamage(globalPastDamage);
			globalPastDamage.highlight = !damage2.doNotHighlight;
			globalPastDamage.totTime = 1.35f;
		}

		private void AddSoundEvent(World world, string by, bool isVoice, Sound sound)
		{
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, by, isVoice, 0f, sound);
			world.AddSoundEvent(e);
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
		}

		public double allyDamage;

		public double damagePerRatio;

		public double regardlessPerDamage;

		public DamageType damageType;

		public bool isPureDamage = true;

		public int attackIndex;

		public Vector3 projectileStartPointOffset;
	}
}
