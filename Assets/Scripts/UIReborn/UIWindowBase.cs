using System;

namespace UIReborn
{
	public abstract class UIWindowBase : UIWidget
	{
		public virtual void Init()
		{
		}

		public virtual void OnOpen()
		{
		}

		public virtual void OnClose()
		{
		}

		public virtual void OnFocusChange(bool isFocus)
		{
		}

		public override void Step()
		{
		}

		public void DisableLogic()
		{
			this.isLogicEnabled = false;
		}

		public void EnableLogic()
		{
			this.isLogicEnabled = true;
		}

		public void DisableObject()
		{
			base.gameObject.SetActive(false);
		}

		public void EnableObject()
		{
			base.gameObject.SetActive(true);
		}

		[NonSerialized]
		public bool isLogicEnabled;
	}
}
