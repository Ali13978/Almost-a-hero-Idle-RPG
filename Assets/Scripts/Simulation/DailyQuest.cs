using System;
using System.Collections.Generic;

namespace Simulation
{
	public abstract class DailyQuest
	{
		public double GetRewardAmount(Simulator simulator)
		{
			return this.reward.GetAmount() * DailyQuest.GetRewardAmountModifier(simulator);
		}

		public void GiveReward(DropPosition dropPosition, Simulator simulator)
		{
			this.reward.dropPosition = dropPosition;
			this.reward.Give(simulator, simulator.GetActiveWorld(), DailyQuest.GetRewardAmountModifier(simulator));
		}

		public void GiveRewardSilently(Simulator simulator)
		{
			this.reward.GiveSilently(simulator);
		}

		private static double GetRewardAmountModifier(Simulator simulator)
		{
			return GameMath.PowDouble(1.15, (double)GameMath.GetMaxInt(simulator.riftDiscoveryIndex - 1, 0));
		}

		public virtual void OnCatchDragon(int count)
		{
		}

		public virtual void OnKilledEnemy(int count)
		{
		}

		public virtual void OnPassStage(int count)
		{
		}

		public virtual void OnUltiSkill(int count)
		{
		}

		public virtual void OnHiltDodge()
		{
		}

		public virtual void OnBellylarfAnger(int count)
		{
		}

		public virtual void OnVexxCool(int count)
		{
		}

		public virtual void OnLennyKillStunned(int count)
		{
		}

		public virtual void OnWendleCast()
		{
		}

		public virtual void OnVStealBoss()
		{
		}

		public virtual void OnBoomerBoom()
		{
		}

		public virtual void OnSamShield(int count)
		{
		}

		public virtual void OnLiaMiss(int count)
		{
		}

		public virtual void OnJimAllyDeath()
		{
		}

		public virtual void OnWendleHeal()
		{
		}

		public virtual void OnTamKillBlinded(int count)
		{
		}

		public virtual void OnVTreasureChestKill(int count)
		{
		}

		public virtual void OnLennyHealAlly(int count)
		{
		}

		public virtual void OnHiltCriticalHit(int count)
		{
		}

		public virtual void OnTamHitMarkedTargets(int count)
		{
		}

		public virtual void OnStunnedEnemy(int count)
		{
		}

		public virtual void OnLiaStealHealth(int count)
		{
		}

		public virtual void OnCollectCandy(int count)
		{
		}

		public virtual void OnWarlockBlindEnemy(int count)
		{
		}

		public virtual void OnWarlockRedirectDamage(int count)
		{
		}

		public virtual void OnGoblinMiss(int count)
		{
		}

		public virtual void OnGoblinSummonDragon(int count)
		{
		}

		public virtual void OnGoblinKillTreasure(int count)
		{
		}

		public virtual void OnBabuHealAlly(int count)
		{
		}

		public virtual void OnBabuGetHit(int count)
		{
		}

		public virtual void OnRonLandedCritHit()
		{
		}

		public virtual void OnRonImpulsiveSkillTriggered()
		{
		}

		public abstract bool IsAvailable(Simulator sim);

		public static List<DailyQuest> GetAllAvailable(Simulator sim)
		{
			List<DailyQuest> list = new List<DailyQuest>
			{
				new DailyQuestKillEnemiesEasy(),
				new DailyQuestPassStagesEasy(),
				new DailyQuestUltiSkillEasy(),
				new DailyQuestCatchDragonEasy(),
				new DailyQuestKillEnemiesHard(),
				new DailyQuestPassStagesHard(),
				new DailyQuestUltiSkillHard(),
				new DailyQuestCatchDragonHard(),
				new DailyQuestHiltDodgeAttacks(),
				new DailyQuestBellylarfStayAngry(),
				new DailyQuestVexxCoolWeapon(),
				new DailyQuestWendleCastSpell(),
				new DailyQuestVStealFromBoss(),
				new DailyQuestBoomerBoom(),
				new DailyQuestSamShield(),
				new DailyQuestLennyKillStunned(),
				new DailyQuestLiaMiss(),
				new DailyQuestJimAllyDeath(),
				new DailyQuestTamKillBlinded(),
				new DailyQuestWendleHeal(),
				new DailyQuestVTreasureChestKill(),
				new DailyQuestLennyHealAlly(),
				new DailyQuestHiltCriticalHit(),
				new DailyQuestTamHitMarkedTargets(),
				new DailyQuestStunEnemies(),
				new DailyQuestWarlockEnemyBlind(),
				new DailyQuestWarlockRedirectDamage(),
				new DailyQuestGoblinSummonDragons(),
				new DailyQuestGoblinKillTreasure(),
				new DailyQuestHealAlliesBabu(),
				new DailyQuestBodyBlockWithBabu(),
				new DailyQuestUseRonLandCritHit(),
				new DailyQuestTriggerRonImpulsiveSkill()
			};
			List<DailyQuest> list2 = new List<DailyQuest>();
			bool flag = true && sim.dailyQuestsAppearedCount % 2 == 0;
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if ((!flag || list[i].goal >= 100) && list[i].IsAvailable(sim) && DailyQuest.IsDifferentQuestThanPreviousOne(SaveLoadManager.ConvertDailyQuest(list[i]), sim.lastDailyQuest))
				{
					list2.Add(list[i]);
				}
				i++;
			}
			return list2;
		}

		private static bool IsDifferentQuestThanPreviousOne(int questId, int previousQuestId)
		{
			return questId != previousQuestId && (questId != 1 || previousQuestId != 4) && (questId != 4 || previousQuestId != 1) && (questId != 2 || previousQuestId != 5) && (questId != 5 || previousQuestId != 2) && (questId != 3 || previousQuestId != 6) && (questId != 6 || previousQuestId != 3) && (questId != 7 || previousQuestId != 8) && (questId != 8 || previousQuestId != 7);
		}

		public abstract float GetChanceWeight(Simulator sim);

		public abstract string GetQuestString();

		public static double GetTimeBetweenQuests()
		{
			return 28800.0;
		}

		private const int FORCED_QUESTS_FREQUENCY = 2;

		private const int FORCED_QUESTS_MIN_AMOUNT = 100;

		private const float TIME_BETWEEN_DAILY_QUESTS = 28800f;

		protected UnlockRewardCurrency reward;

		public int progress;

		public int goal;
	}
}
