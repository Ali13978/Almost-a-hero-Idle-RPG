using System;

namespace UIReborn
{
	public abstract class UIWidget : UIObject
	{
		public abstract void Step();

		private UIManager manager;
	}
}
