using System;

namespace SaveLoad
{
	[Serializable]
	public class QuestOfUpdateSerializable
	{
		public int type;

		public double progress;

		public long startTime;

		public bool isExpired;
	}
}
