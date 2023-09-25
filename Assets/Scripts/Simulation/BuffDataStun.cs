using System;

namespace Simulation
{
	public class BuffDataStun : BuffData
	{
		public BuffDataStun()
		{
			this.isStackable = true;
			this.visuals |= 512;
		}
	}
}
