using System;
using SaveLoad;
using Simulation;

public class Delegates
{
	public class PlayfabSaveEventArgs
	{
		public void OnSuccess()
		{
			this.sim.lastSaveTime = this.saveData.saveTime;
		}

		public Simulator sim;

		public SaveData saveData;
	}
}
