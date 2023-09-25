using System;
using Simulation;

namespace stats
{
	public abstract class Badge
	{
		public bool NotificationDismissed
		{
			get
			{
				return this.notificationDismissed;
			}
		}

		public abstract BadgeId Id { get; }

		public abstract string Description { get; }

		public bool HasBeenEarnedByPlayer(Simulator simulator)
		{
			if (this.earned)
			{
				return true;
			}
			this.earned = this.updateEarnedState(simulator);
			return this.earned;
		}

		public bool IsNotificationEnabled(Simulator simulator)
		{
			return !this.notificationDismissed && !this.HasBeenEarnedByPlayer(simulator) && this.CanBeObtained(simulator);
		}

		public void LoadState(bool earned, bool notificationDismissed)
		{
			this.earned = earned;
			this.notificationDismissed = notificationDismissed;
		}

		public void DimissNotification()
		{
			this.notificationDismissed = true;
		}

		public abstract bool CanBeObtained(Simulator simulator);

		protected abstract bool updateEarnedState(Simulator simulator);

		private bool earned;

		private bool notificationDismissed;
	}
}
