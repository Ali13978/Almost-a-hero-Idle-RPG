using System;

namespace DG.Tweening.Plugins.Options
{
	public struct ColorOptions : IPlugOptions
	{
		public void Reset()
		{
			this.alphaOnly = false;
		}

		public bool alphaOnly;
	}
}
