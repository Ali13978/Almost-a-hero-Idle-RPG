using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class NotificationBadge : AahMonoBehaviour
	{
		public int numNotifications
		{
			get
			{
				return this._numNotifications;
			}
			set
			{
				bool flag = this._numNotifications > 0;
				bool flag2 = this._numNotifications != value;
				this._numNotifications = value;
				bool flag3 = this._numNotifications > 0;
				if (this.text == null)
				{
					this.text = base.GetComponentInChildren<Text>();
				}
				if (this.exclamationOnly)
				{
					this.text.text = "!";
				}
				else
				{
					this.text.text = this._numNotifications.ToString();
				}
				if (flag3 && !flag)
				{
					this.BeginAnim(1f, false);
				}
				if (flag3 && flag2)
				{
					this.BeginAnim(1f, true);
				}
				else if (!flag3 && flag)
				{
					this.BeginAnim(0f, false);
				}
			}
		}

		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.text = base.GetComponentInChildren<Text>();
			this.numNotifications = 0;
			base.transform.localScale = Vector3.zero;
			this.animTimer = 0.7f;
			this.animPeriod = 0.7f;
		}

		public void ResetScale()
		{
			base.transform.localScale = Vector3.zero;
			this.scaleGoal = 0f;
			this.animTimer = 0.7f;
			this.animPeriod = 0.7f;
		}

		public override void AahUpdate(float dt)
		{
			if (this.animTimer <= this.animPeriod + dt)
			{
				this.animTimer += dt;
				if (this.scaleFirst == this.scaleGoal)
				{
					float t = this.animTimer / (this.animPeriod / 2f);
					if (this.animTimer > this.animPeriod / 2f)
					{
						t = (this.animPeriod - this.animTimer) / (this.animPeriod / 2f);
					}
					base.transform.localScale = this.maxScale * Easing.SineEaseOut(t, this.scaleGoal, 0.2f, 1f);
				}
				else if (this.scaleFirst > this.scaleGoal)
				{
					float a = Easing.BackEaseIn(this.animTimer, this.scaleFirst, this.scaleGoal - this.scaleFirst, this.animPeriod);
					base.transform.localScale = this.maxScale * GameMath.GetMaxFloat(a, this.scaleGoal);
				}
				else
				{
					base.transform.localScale = this.maxScale * Easing.BounceEaseOut(this.animTimer, this.scaleFirst, this.scaleGoal - this.scaleFirst, this.animPeriod);
				}
			}
			else
			{
				base.transform.localScale = this.maxScale * this.scaleGoal;
			}
		}

		private void BeginAnim(float scaleGoal, bool isJump)
		{
			this.scaleFirst = base.transform.localScale.x;
			this.scaleGoal = scaleGoal;
			if (isJump)
			{
				if (this.animTimer >= this.animPeriod)
				{
					this.animTimer = 0f;
					this.animPeriod = 0.35f;
				}
			}
			else
			{
				this.animTimer = 0f;
				this.animPeriod = 0.7f;
				if (scaleGoal < this.scaleFirst)
				{
					this.animPeriod *= 0.5f;
				}
			}
		}

		public bool exclamationOnly;

		public Vector3 maxScale = Vector3.one;

		private int _numNotifications;

		private Text text;

		private float scaleFirst;

		private float scaleGoal;

		private float animTimer;

		private float animPeriod;

		private const float AnimPeriod = 0.7f;
	}
}
