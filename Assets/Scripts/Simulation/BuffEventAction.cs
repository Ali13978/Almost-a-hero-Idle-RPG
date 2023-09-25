using System;

namespace Simulation
{
	public class BuffEventAction : BuffEvent
	{
		public BuffEventAction(float time, int id)
		{
			this.time = time;
			this.id = id;
		}

		public override void Apply(Unit by, World world)
		{
			by.OnActionBuffEventTriggered(this, by);
		}

		public static int ID_BABU_ULTI_FINISH;

		public int id;
	}
}
