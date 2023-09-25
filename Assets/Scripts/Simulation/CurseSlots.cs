using System;
using System.Collections.Generic;

namespace Simulation
{
	public class CurseSlots
	{
		public CurseSlots(int baseCount)
		{
			this.baseCount = baseCount;
		}

		public int slotCount
		{
			get
			{
				return this.baseCount + this.unlockedCount;
			}
		}

		public void Load(World worldRift)
		{
			this.unlockedCount = 0;
			foreach (SlotUnlockCondition slotUnlockCondition in this.lockedSlots)
			{
				if (slotUnlockCondition.HasSatisfied(worldRift))
				{
					this.unlockedCount++;
				}
			}
		}

		public bool HasUnlock()
		{
			return this.slotCount < this.GetMaxSlotCount();
		}

		public int GetMaxSlotCount()
		{
			return this.baseCount + this.lockedSlots.Count;
		}

		public SlotUnlockCondition GetSlot(int index)
		{
			return this.lockedSlots[index - this.baseCount];
		}

		private int baseCount;

		private int unlockedCount;

		public List<SlotUnlockCondition> lockedSlots;
	}
}
