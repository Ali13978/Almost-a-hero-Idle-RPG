using System;
using System.Collections.Generic;

namespace Simulation
{
	public abstract class Totem : Unit
	{
		public Totem(int[] levelJumps, World world) : base(world)
		{
			this.levelJumps = levelJumps;
		}

		public static Totem CreateTotem(TotemDataBase database, int[] levelJumps, int level, int xp, World world)
		{
			Totem totem;
			if (database is TotemDataBaseEarth)
			{
				totem = new TotemEarth((TotemDataBaseEarth)database, levelJumps, level, xp, world);
			}
			else if (database is TotemDataBaseFire)
			{
				totem = new TotemFire((TotemDataBaseFire)database, levelJumps, level, xp, world);
			}
			else if (database is TotemDataBaseLightning)
			{
				totem = new TotemLightning((TotemDataBaseLightning)database, levelJumps, level, xp, world);
			}
			else
			{
				if (!(database is TotemDataBaseIce))
				{
					throw new NotImplementedException();
				}
				totem = new TotemIce((TotemDataBaseIce)database, levelJumps, level, xp, world);
			}
			int xpNeedForNextLevel = totem.GetXpNeedForNextLevel();
			if (totem.xp >= xpNeedForNextLevel)
			{
				totem.xp = xpNeedForNextLevel - 1;
			}
			return totem;
		}

		public void Refresh()
		{
			this.UpdateData();
			this.UpdateStats(0f);
		}

		public void RefreshRunes(List<Rune> wornRunes)
		{
			Dictionary<int, int> dictionary;
			Dictionary<int, float> dictionary2;
			Dictionary<int, bool> dictionary3;
			base.ClearPermenantBuffs(out dictionary, out dictionary2, out dictionary3);
			foreach (Rune rune in wornRunes)
			{
				BuffData buff = rune.buff;
				int genericCounter = (!dictionary.ContainsKey(buff.id)) ? 0 : dictionary[buff.id];
				buff.genericTimer = ((!dictionary2.ContainsKey(buff.id)) ? 0f : dictionary2[buff.id]);
				buff.genericFlag = (dictionary3.ContainsKey(buff.id) && dictionary3[buff.id]);
				this.AddBuff(buff, genericCounter, false);
			}
		}

		public abstract void UpdateData();

		public abstract bool CanAutoTapOnThisFrame(float dt);

		public string id
		{
			get
			{
				return this.GetData().id;
			}
		}

		public string name
		{
			get
			{
				return this.GetData().name;
			}
		}

		public override string GetId()
		{
			return this.id;
		}

		public void SetLevel(int level, int xp)
		{
			this.level = level;
			int totemXpNeedForNextLevel = this.world.GetTotemXpNeedForNextLevel();
			if (xp < totemXpNeedForNextLevel)
			{
				this.xp = xp;
			}
			else
			{
				this.xp = totemXpNeedForNextLevel - 1;
			}
			this.UpdateData();
		}

		public void Upgrade()
		{
			this.xp++;
			if (this.xp == this.GetXpNeedForNextLevel())
			{
				this.level++;
				this.xp = 0;
			}
			foreach (Buff buff in this.buffs)
			{
				buff.OnUpgradedSelf();
			}
			this.UpdateData();
		}

		public abstract TotemData GetData();

		public double GetUpgradeCost(bool takeFreeUpgradeIntoAccount = true)
		{
			if (takeFreeUpgradeIntoAccount && this.nextUpgradeFree)
			{
				return 0.0;
			}
			if (this.level < 0 || this.level >= TotemDataBase.LEVEL_XPS.Length - 3)
			{
				return double.PositiveInfinity;
			}
			bool flag = false;
			if (this.xp + 1 == this.world.GetRequiredXpToLevelHero(this.level))
			{
				flag = true;
			}
			double p = (double)this.GetProgress(this.level, 0) * 0.65 + (double)this.xp;
			double num = 10.0 * GameMath.PowDouble(1.6, p);
			if (flag)
			{
				num *= 5.0;
			}
			return num * this.statCache.upgradeCostFactor;
		}

		protected int GetProgress()
		{
			return this.GetProgress(this.level, this.xp);
		}

		protected int GetNextProgress()
		{
			int num = this.xp + 1;
			int num2 = this.level;
			if (num == this.GetXpNeedForNextLevel())
			{
				num = 0;
				num2++;
			}
			return this.GetProgress(num2, num);
		}

		public int GetProgress(int level, int xp)
		{
			return this.levelJumps[GameMath.GetMinInt(level, this.levelJumps.Length - 1)] + xp;
		}

		public int GetLevel()
		{
			return this.level;
		}

		public int GetXp()
		{
			return this.xp;
		}

		public void SetLevel(int newLevel)
		{
			this.level = newLevel;
		}

		public void SetXp(int newXp)
		{
			int totemXpNeedForNextLevel = this.world.GetTotemXpNeedForNextLevel();
			if (this.xp < totemXpNeedForNextLevel)
			{
				this.xp = newXp;
			}
			else
			{
				this.xp = totemXpNeedForNextLevel - 1;
			}
		}

		public int GetXpNeedForNextLevel()
		{
			return this.world.GetRequiredXpToLevelRing(this.level);
		}

		public override double GetDpsTeam()
		{
			return this.world.GetHeroTeamDps();
		}

		public override float GetHeight()
		{
			return -1f;
		}

		public override float GetScaleHealthBar()
		{
			return -1f;
		}

		public override float GetScaleBuffVisual()
		{
			return -1f;
		}

		public override void Update(float dt, int addVisuals)
		{
			throw new Exception("Don`t call me!!! Call UpdateTotem instead..");
		}

		public virtual void UpdateTotem(float dt, Taps taps, bool autoTap)
		{
			base.Update(dt, 0);
		}

		public override void DamageAll(Damage damage)
		{
			foreach (Enemy damaged in this.world.activeChallenge.enemies)
			{
				this.world.DamageMain(this, damaged, damage);
			}
		}

		public override IEnumerable<UnitHealthy> GetAllies()
		{
			foreach (Hero hero in this.world.heroes)
			{
				yield return hero;
			}
			yield break;
		}

		public override IEnumerable<UnitHealthy> GetOpponents()
		{
			foreach (Enemy enemy in this.world.activeChallenge.enemies)
			{
				yield return enemy;
			}
			yield break;
		}

		public double GetDamageAll()
		{
			return this.statCache.damage;
		}

		public double GetDamageDifUpgrade()
		{
			int progress = this.GetProgress(this.level, this.xp);
			int nextProgress = this.GetNextProgress();
			double num = this.GetData().GetDamageFromProgress(nextProgress) / this.GetData().GetDamageFromProgress(progress);
			return this.statCache.damageNonAdded * num - this.statCache.damageNonAdded;
		}

		public const int LEVEL_CAP = 90;

		protected int xp;

		protected int level;

		public int[] levelJumps;

		public UnitHealthy oldAttackTarget;

		public UnitHealthy attackTarget;

		public bool nextUpgradeFree;
	}
}
