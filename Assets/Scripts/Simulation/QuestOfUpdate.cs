using System;
using System.Collections.Generic;

namespace Simulation
{
	public abstract class QuestOfUpdate
	{
		public void StartQuest(DateTime time)
		{
			this.startTime = time;
		}

		public bool IsExpired(DateTime time)
		{
			this.isExpired = (time.Ticks > this.endDate.Ticks);
			return this.isExpired;
		}

		public void Update(DateTime time)
		{
			this.isExpired = (time.Ticks > this.endDate.Ticks);
		}

		public bool IsExpired()
		{
			return this.isExpired;
		}

		public double GetRemainingDuration(DateTime time)
		{
			return (this.endDate - time).TotalSeconds;
		}

		public virtual bool IsCompleted()
		{
			return this.progress >= this.goal;
		}

		public float GetProgress()
		{
			return (float)GameMath.Clamp(this.progress / this.goal, 0.0, 1.0);
		}

		public virtual void OnPrestige()
		{
		}

		public static List<QuestOfUpdate> GetAllQuests()
		{
			return new List<QuestOfUpdate>
			{
				new QuestOfUpdateAnniversary01()
			};
		}

		public virtual bool IsAvailable(Simulator sim, DateTime currentTime, bool isTimeReady)
		{
			return isTimeReady && currentTime.Ticks > this.startDate.Ticks && currentTime.Ticks < this.endDate.Ticks;
		}

		public static string GetQuestName(int id)
		{
			if (id == QuestOfUpdateIds.Anniversary01)
			{
				return LM.Get("QUEST_OF_UPDATE_ANNIVERSARY_01_NAME");
			}
			throw new Exception("Quest not found id: " + id.ToString());
		}

		public static string GetQuestDesc(int id)
		{
			if (id == QuestOfUpdateIds.Anniversary01)
			{
				return LM.Get("QUEST_OF_UPDATE_ANNIVERSARY_01_DESC");
			}
			throw new Exception("Quest not found id: " + id.ToString());
		}

		public static QuestOfUpdate GetQuestOfUpdate()
		{
			return null;
		}

		public DateTime startDate;

		public DateTime endDate;

		public bool isExpired;

		public DateTime startTime = DateTime.MinValue;

		public int id;

		public UnlockRewardCurrency reward;

		public double goal;

		public double progress;
	}
}
