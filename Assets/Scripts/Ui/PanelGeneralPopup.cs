using System;
using DG.Tweening;
using DynamicLoading;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelGeneralPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.posButtonYes = this.buttonYes.GetComponent<RectTransform>().anchoredPosition;
			this.popupFirstPos = this.rectPopup.anchoredPosition;
			this.chestGraphicInfo = new PanelGeneralPopup.SpineGraphicInfo
			{
				dataAsset = this.chestSkeletonDataAsset,
				skinName = "common",
				animName = "idle",
				animLooped = false,
				scale = 0.5f
			};
		}

		public void ShowRateArt()
		{
			this.rateUsArt.LoadObjectAsync(new Action<GameObject>(this.OnLoadRateArt));
		}

		public void UnloadRateArt()
		{
			if (this.rateUsContentInstance != null)
			{
				UnityEngine.Object.Destroy(this.rateUsContentInstance.gameObject);
				this.rateUsContentInstance = null;
			}
			this.rateUsArt.Unload();
			this.textBodyUp.rectTransform.SetAnchorPosY(85f);
		}

		private void OnLoadRateArt(GameObject obj)
		{
			if (this.rateUsContentInstance != null)
			{
				UnityEngine.Object.Destroy(this.rateUsContentInstance);
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(obj, this.rectPopup);
			this.rateUsContentInstance = gameObject.GetComponent<RateUsGraphicContent>();
			this.rateUsContentInstance.transform.SetScale(0f);
			this.rateUsContentInstance.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
			this.rateUsContentInstance.rectTransform.anchoredPosition = new Vector2(-36f, 70f);
			this.rateUsContentInstance.imageStars.gameObject.SetActive(false);
			this.textBodyUp.rectTransform.SetAnchorPosY(-160f);
		}

		public void SetDetails(PanelGeneralPopup.State _state, string sHeader, string sBodyUp, bool twoButtons, GameButton.VoidFunc onYes, string sYes, GameButton.VoidFunc onNo = null, string sNo = "", float popupY = 0f, float popupHeight = 160f, PanelGeneralPopup.SpineGraphicInfo graphicInfo = null, string sBodyDown = null)
		{
			this.state = _state;
			this.textHeader.text = sHeader;
			this.textBar.text = sHeader;
			this.textBodyUp.text = sBodyUp;
			this.buttonYes.onClick = onYes;
			this.buttonYes.text.text = sYes;
			this.buttonYesCurrency.gameObject.SetActive(false);
			this.buttonYes.gameObject.SetActive(false);
			this.buttonNo.gameObject.SetActive(false);
			if (twoButtons)
			{
				this.buttonNo.gameObject.SetActive(true);
				if (this.hasConsuming)
				{
					this.buttonYesCurrency.gameObject.SetActive(true);
					this.buttonYesCurrency.gameButton.onClick = onYes;
					this.buttonYesCurrency.textDown.text = sYes;
					this.buttonYesCurrency.GetComponent<RectTransform>().anchoredPosition = this.posButtonYes;
					this.buttonNo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-this.posButtonYes.x, this.posButtonYes.y);
					this.buttonNo.onClick = onNo;
					this.buttonNo.text.text = sNo;
				}
				else
				{
					this.buttonYes.gameObject.SetActive(true);
					this.buttonYes.GetComponent<RectTransform>().anchoredPosition = this.posButtonYes;
					this.buttonNo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-this.posButtonYes.x, this.posButtonYes.y);
					this.buttonNo.onClick = onNo;
					this.buttonNo.text.text = sNo;
				}
			}
			else
			{
				this.buttonYes.gameObject.SetActive(true);
				this.buttonYes.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, this.posButtonYes.y);
			}
			bool flag = graphicInfo != null;
			this.textBodyUp.rectTransform.SetAnchorPosY((!flag && sBodyDown == null) ? -8f : 85f);
			if (sBodyDown == null)
			{
				this.textBodyDown.enabled = false;
			}
			else
			{
				this.textBodyDown.enabled = true;
				this.textBodyDown.text = sBodyDown;
			}
			this.skeletonGraphicBody.enabled = flag;
			if (flag)
			{
				graphicInfo.Apply(this.skeletonGraphicBody);
				this.skeletonGraphicBody.rectTransform.SetScale(0f);
				this.skeletonGraphicBody.rectTransform.DOScale(graphicInfo.scale, 0.2f).SetEase(Ease.OutBack).SetDelay(0.2f);
			}
			this.rectPopup.anchoredPosition = this.popupFirstPos + new Vector2(0f, popupY);
			this.rectPopup.SetSizeDeltaY(popupHeight);
			this.timer = 0f;
			this.hasConsuming = false;
		}

		public void SetViewContinuous()
		{
			float num = Mathf.Clamp(this.timer / 3f, 0f, 1f);
			this.imageBarFill.fillAmount = num;
			if (num > 0f)
			{
				this.imageBarHead.enabled = true;
				if (num > 0.05f)
				{
					this.imageBarHead.color = Color.white;
				}
				else
				{
					this.imageBarHead.color = new Color(1f, 1f, 1f, num / 0.05f);
				}
				this.imageBarHead.rectTransform.anchoredPosition = new Vector2(this.imageBarFill.rectTransform.sizeDelta.x * num, 0f);
			}
			else
			{
				this.imageBarHead.enabled = false;
			}
		}

		public PanelGeneralPopup.State state;

		public Text textHeader;

		public Text textBodyUp;

		public Text textBodyDown;

		public SkeletonGraphic skeletonGraphicBody;

		public GameButton buttonNo;

		public GameButton buttonYes;

		public ButtonUpgradeAnim buttonYesCurrency;

		private Vector2 posButtonYes;

		public RectTransform rectPopup;

		private Vector2 popupFirstPos;

		public Text textBar;

		public Image imageBarFill;

		public Image imageBarHead;

		public float timer;

		public bool hasConsuming;

		public const float period = 3f;

		[SerializeField]
		private SkeletonDataAsset chestSkeletonDataAsset;

		public PanelGeneralPopup.SpineGraphicInfo chestGraphicInfo;

		private RateUsGraphicContent rateUsContentInstance;

		public BPrefab rateUsArt;

		private const float NoGraphicTextPos = -8f;

		private const float TextWithGraphicPos = 85f;

		public enum State
		{
			NONE,
			OPTIONS,
			HARD_RESET,
			MODE,
			SHOP,
			SERVER_REWARD_HUB,
			SERVER_REWARD,
			SELECT_TRINKET,
			DATABASE_TRINKET,
			HUB_DAILY_SKIP,
			LOSE_RIFT_HINT,
			TRINKET_INFO_POPUP,
			CHRISTMAS_SHOP,
			DAILY_QUEST
		}

		public class SpineGraphicInfo
		{
			public void Apply(SkeletonGraphic skeletonGraphic)
			{
				skeletonGraphic.skeletonDataAsset = this.dataAsset;
				skeletonGraphic.initialSkinName = this.skinName;
				skeletonGraphic.startingAnimation = this.animName;
				skeletonGraphic.startingLoop = this.animLooped;
				skeletonGraphic.Initialize(true);
			}

			public SkeletonDataAsset dataAsset;

			public string skinName;

			public string animName;

			public bool animLooped;

			public float scale;
		}
	}
}
