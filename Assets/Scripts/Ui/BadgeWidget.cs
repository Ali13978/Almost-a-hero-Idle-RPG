using System;
using stats;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class BadgeWidget : MonoBehaviour
	{
		public Badge Badge { get; private set; }

		public void Init(Badge badge, Action<BadgeWidget> onClick, bool badgeEarned, bool notificationEnabled)
		{
			this.Badge = badge;
			this.icon.sprite = UiManager.GetBadgeIcon(badge.Id);
			this.onClick = onClick;
			this.gameButton.onClick = new GameButton.VoidFunc(this.OnClick);
			this.gameButton.Register();
			this.UpdateState(badgeEarned, notificationEnabled);
		}

		public void UpdateState(bool badgeEarned, bool notificationEnabled)
		{
			this.icon.SetAlpha((!badgeEarned) ? 0.5f : 1f);
			this.notification.enabled = notificationEnabled;
		}

		public void DisableNotifaction()
		{
			this.notification.enabled = false;
		}

		private void OnClick()
		{
			this.onClick(this);
		}

		public RectTransform rectTransform;

		public RectTransform scalerPivot;

		[SerializeField]
		private GameButton gameButton;

		[SerializeField]
		private Image icon;

		[SerializeField]
		private Image notification;

		private Action<BadgeWidget> onClick;
	}
}
