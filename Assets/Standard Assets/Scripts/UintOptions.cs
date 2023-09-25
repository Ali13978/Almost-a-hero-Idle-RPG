using System;

namespace DG.Tweening.Plugins.Options
{
	public struct UintOptions : IPlugOptions
	{
		public void Reset()
		{
			this.isNegativeChangeValue = false;
		}

		public bool isNegativeChangeValue;
	}
}
