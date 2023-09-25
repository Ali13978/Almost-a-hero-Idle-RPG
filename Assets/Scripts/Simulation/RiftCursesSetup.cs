using System;

namespace Simulation
{
	public class RiftCursesSetup
	{
		public RiftCursesSetup GetCopy(int riftNo)
		{
			return new RiftCursesSetup
			{
				progressPerMinute = this.progressPerMinute,
				progressPerWave = this.progressPerWave,
				originalRiftNo = riftNo
			};
		}

		public float progressPerWave;

		public float progressPerMinute;

		public int originalRiftNo;
	}
}
