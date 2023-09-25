using System;

namespace DG.Tweening.Plugins.Options
{
	public struct RectOptions : IPlugOptions
	{
		public void Reset()
		{
			this.snapping = false;
		}

		public bool snapping;
	}
}
