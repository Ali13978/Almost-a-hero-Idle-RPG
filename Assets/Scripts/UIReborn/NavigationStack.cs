using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIReborn
{
	public class NavigationStack
	{
		public NavigationStack(UIWindowBase rootWindow)
		{
			this.root = rootWindow;
			this.navigationStack = new List<UIWindowBase>();
			this.navigationStack.Add(this.root);
		}

		public void OpenWindow(UIWindowBase window)
		{
			if (this.navigationStack.Count > 0)
			{
				UIWindowBase lastItem = this.navigationStack.GetLastItem<UIWindowBase>();
				lastItem.DisableLogic();
				lastItem.OnFocusChange(false);
			}
			window.EnableObject();
			window.EnableLogic();
			window.OnOpen();
			this.navigationStack.Add(window);
		}

		public void ReplaceWindow(UIWindowBase window)
		{
			if (this.navigationStack.Count > 0)
			{
				UIWindowBase lastItem = this.navigationStack.GetLastItem<UIWindowBase>();
				lastItem.DisableObject();
				lastItem.DisableLogic();
				lastItem.OnClose();
			}
			window.EnableObject();
			window.EnableLogic();
			window.OnOpen();
		}

		public void MoveBack()
		{
			if (this.navigationStack.Count <= 1)
			{
				UnityEngine.Debug.Log("Stack reached to the root, returning...");
				return;
			}
			UIWindowBase uiwindowBase = this.navigationStack.PopLastItem<UIWindowBase>();
			UIWindowBase lastItem = this.navigationStack.GetLastItem<UIWindowBase>();
			uiwindowBase.DisableObject();
			uiwindowBase.DisableLogic();
			uiwindowBase.OnClose();
			lastItem.EnableObject();
			lastItem.EnableLogic();
			lastItem.OnOpen();
		}

		public void OpenStack(params UIWindowBase[] windows)
		{
		}

		public void OpenFullStack(params UIWindowBase[] windows)
		{
		}

		public readonly UIWindowBase root;

		public List<UIWindowBase> navigationStack;
	}
}
