using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelNextUnlockStage : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.rectTransform = base.GetComponent<RectTransform>();
			this.firstPos = this.rectTransform.anchoredPosition;
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.textNextUnlock.text = LM.Get("UI_NEXT_UNLOCK_STAGE");
		}

		public override void AahUpdate(float dt)
		{
			if (this.animTimer > 0f)
			{
				this.animTimer += dt;
				if (this.animTimer >= 5f)
				{
					this.animTimer = 0f;
					base.gameObject.SetActive(false);
				}
				else if (this.animTimer < 0.5f)
				{
					this.Place(this.animTimer / 0.5f);
				}
				else if (this.animTimer < 4.5f)
				{
					this.Place(1f);
				}
				else
				{
					this.Place((5f - this.animTimer) / 0.5f);
				}
			}
		}

		public void SetAllComplete()
		{
			this.textNextUnlock.rectTransform.SetAnchorPosX(0f);
			this.imageLock.enabled = false;
			this.textNextUnlock.resizeTextForBestFit = true;
			this.textNextUnlock.color = this.colorAllDone;
			this.textNextUnlock.rectTransform.SetLeftDelta(5f);
			this.textNextUnlock.rectTransform.SetRightDelta(5f);
			this.textNextUnlock.text = LM.Get("ALL_QUESTS_COMPLETE");
		}

		public void StartAnim(int nextUnlockStage)
		{
			this.nextUnlockStage = nextUnlockStage;
			this.imageLock.enabled = true;
			this.textNextUnlock.rectTransform.SetLeftDelta(33f);
			this.textNextUnlock.rectTransform.SetRightDelta(5f);
			this.textNextUnlock.rectTransform.SetAnchorPosX(14f);
			this.textNextUnlock.resizeTextForBestFit = true;
			this.textNextUnlock.resizeTextMaxSize = 30;
			this.textNextUnlock.color = Color.white;
			this.textNextUnlock.fontSize = 30;
			this.textNextUnlock.text = nextUnlockStage.ToString();
		}

		public void StopAnim()
		{
			this.animTimer = 0f;
			this.Place(0f);
		}

		private void Place(float animRatio)
		{
			this.rectTransform.anchoredPosition = this.firstPos + Vector2.up * Easing.CubicEaseInOut(animRatio, 100f, -100f, 1f);
		}

		public Text textNextUnlock;

		public int nextUnlockStage = -1;

		public Color colorAllDone = Color.white;

		private float animTimer;

		private const float animMovePeriod = 0.5f;

		private const float animPeriod = 5f;

		private const float animDeltaY = 100f;

		private Vector2 firstPos;

		private RectTransform rectTransform;

		public Image imageLock;
	}
}
