using System;

namespace Simulation
{
	public class Skill
	{
		public Skill(SkillActiveData data, Unit by)
		{
			this.data = data;
			this.by = by;
			this.isEnding = false;
			this.isTogglingOff = false;
			this.time = 0f;
			this.atEvent = 0;
			this.atAnimEvent = 0;
			this.atAnimIndex = 0;
			this.curAnimDur = data.dur;
			if (data.animEvents != null && data.animEvents.Count > 0)
			{
				this.curAnimDur = data.animEvents[0].time;
			}
			this.timeAtCurAnim = 0f;
		}

		public void Update(float dt)
		{
			this.time += dt;
			if (!this.isEnding && this.data.liveCondition != null && this.data.liveCondition.IsNotOkay(this.by))
			{
				this.isEnding = true;
			}
			while (this.atEvent < this.data.events.Count && this.data.events[this.atEvent].time <= this.time)
			{
				this.data.events[this.atEvent++].Apply(this.by);
			}
			this.timeAtCurAnim += dt;
			if (this.data.animEvents != null)
			{
				int num = this.atAnimEvent;
				while (num < this.data.animEvents.Count && this.data.animEvents[num].time <= this.time)
				{
					num++;
				}
				if (num != this.atAnimEvent)
				{
					this.atAnimEvent = num;
					this.atAnimIndex = this.data.animEvents[this.atAnimEvent - 1].animIndex;
					float num2 = this.data.animEvents[this.atAnimEvent - 1].time;
					float num3 = (this.atAnimEvent >= this.data.animEvents.Count) ? this.data.dur : this.data.animEvents[this.atAnimEvent].time;
					this.curAnimDur = num3 - num2;
					this.timeAtCurAnim = this.time - num2;
				}
			}
		}

		public void ToggleOff()
		{
			if (this.atAnimEvent >= this.data.animEvents.Count)
			{
				return;
			}
			float num = this.data.animEvents[this.atAnimEvent].time;
			float num2 = this.time - num;
			this.atEvent = this.data.events.Count - 1;
			this.atAnimEvent = this.data.animEvents.Count - 1;
			this.time = this.data.animEvents[this.atAnimEvent].time + num2;
			this.toggleOffTimeDelta = num2;
			this.isTogglingOff = true;
		}

		public void Cancel()
		{
			this.data.dataBase.RemoveSkillBuffs(this.by);
			for (int i = 0; i < this.atEvent; i++)
			{
				this.data.events[i].Cancel(this.by, this.time - this.data.events[i].time);
			}
		}

		public bool HasEnded()
		{
			return this.time > this.data.dur || this.isEnding;
		}

		public bool IsTogglingOff()
		{
			return this.isTogglingOff;
		}

		public SkillActiveDataBase GetDataBase()
		{
			return this.data.dataBase;
		}

		public int GetAnimIndex()
		{
			return this.atAnimIndex;
		}

		public float GetCurAnimTimeRatio()
		{
			return this.timeAtCurAnim / this.curAnimDur;
		}

		public float GetCurAnimDur()
		{
			return this.curAnimDur;
		}

		public float GetTime()
		{
			return this.time;
		}

		public float GetDur()
		{
			return this.data.dur;
		}

		public void PlaySound(World world)
		{
			this.data.PlaySound(world, this.by);
		}

		public void StopSound(World world)
		{
			this.data.StopSound(world, this.by);
		}

		public SkillActiveData data;

		public Unit by;

		private bool isEnding;

		private bool isTogglingOff;

		private float time;

		private int atEvent;

		private int atAnimEvent;

		private int atAnimIndex;

		private float curAnimDur;

		private float timeAtCurAnim;

		public float toggleOffTimeDelta;
	}
}
