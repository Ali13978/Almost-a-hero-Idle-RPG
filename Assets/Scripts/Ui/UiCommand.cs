using System;
using Simulation;

namespace Ui
{
	public abstract class UiCommand
	{
		public abstract void Apply(Simulator sim);

		public virtual void Apply(Main main)
		{
		}
	}
}
