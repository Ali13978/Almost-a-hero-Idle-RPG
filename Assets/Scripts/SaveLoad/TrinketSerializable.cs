using System;
using System.Collections.Generic;

namespace SaveLoad
{
	[Serializable]
	public class TrinketSerializable
	{
		public List<int> effects;

		public List<int> levels;

		public int bodyColorIndex;

		public int bodySpriteIndex;

		public int req;
	}
}
