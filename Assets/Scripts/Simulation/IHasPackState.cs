using System;

namespace Simulation
{
	public interface IHasPackState
	{
		bool isAppeared { get; set; }

		bool isPurchased { get; set; }
	}
}
