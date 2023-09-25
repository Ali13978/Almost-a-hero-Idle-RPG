using System;
using DG.Tweening;
using DynamicLoading;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelRatePopup : AahMonoBehaviour
	{
		public void OnEnable()
		{
			this.artSprite.LoadObjectAsync(new Action<GameObject>(this.OnArtLoaded));
		}

		public void OnDisable()
		{
			if (this.artInstance != null)
			{
				UnityEngine.Object.Destroy(this.artInstance);
				this.artInstance = null;
			}
			this.artSprite.Unload();
		}

		private void OnArtLoaded(GameObject obj)
		{
			if (this.artInstance != null)
			{
				UnityEngine.Object.Destroy(this.artInstance);
			}
			this.artInstance = UnityEngine.Object.Instantiate<GameObject>(obj, this.graphicParent);
			this.artInstance.transform.SetScale(0f);
			this.artInstance.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
		}

		public void InitStrings()
		{
			this.textHeader.text = LM.Get("UI_RANK");
			this.textBody.text = LM.Get("UI_RANK_DESC");
			this.buttonRate.text.text = LM.Get("UI_YES");
			this.buttonAskLater.text.text = LM.Get("UI_RANK_ASK_LATER");
			this.buttonDontAskAgain.text.text = LM.Get("UI_RANK_DONT_ASK_AGAIN");
		}

		public Text textHeader;

		public Text textBody;

		public GameButton buttonDontAskAgain;

		public GameButton buttonAskLater;

		public GameButton buttonRate;

		public RectTransform graphicParent;

		public BPrefab artSprite;

		private GameObject artInstance;

		[NonSerialized]
		public UiState previousState;
	}
}
