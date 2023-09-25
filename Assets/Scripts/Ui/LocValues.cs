using System;
using System.Collections.Generic;

namespace Ui
{
	[Serializable]
	public class LocValues
	{
		public LocValues()
		{
			this.values = new List<string>();
		}

		public List<string> values;
	}
}
