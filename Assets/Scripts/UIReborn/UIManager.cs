using System;
using Simulation;
using UnityEngine;

namespace UIReborn
{
	public class UIManager : MonoBehaviour
	{
		public void Init()
		{
			this.navigationStack = new NavigationStack(this.windowHub);
		}

		public void Step(Simulator sim)
		{
		}

		public NavigationStack navigationStack;

		public WindowHub windowHub;
	}
}
