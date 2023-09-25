using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonSelectTrinket : MonoBehaviour
	{
		public RectTransform rectTransform
		{
			get
			{
				RectTransform result;
				if ((result = this.m_rectTransform) == null)
				{
					result = (this.m_rectTransform = base.GetComponent<RectTransform>());
				}
				return result;
			}
		}

		public TrinketUi trinketUi
		{
			get
			{
				if (this._trinketUi == null)
				{
					this._trinketUi = UnityEngine.Object.Instantiate<GameObject>(UiData.inst.prefabTrinket, this.trinketParent).GetComponent<TrinketUi>();
					this._trinketUi.Register();
					this._trinketUi.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
					this._trinketUi.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				return this._trinketUi;
			}
			set
			{
				this._trinketUi = value;
			}
		}

		private RectTransform m_rectTransform;

		public CanvasGroup canvasGroup;

		public GameButton gameButton;

		public Transform trinketParent;

		public Image imageHero;

		private TrinketUi _trinketUi;

		public GameObject heroParent;

		public Image notification;

		public GameObject pinParent;

		public Sprite maxedSprite;

		public Image multipleSelectionImage;

		[NonSerialized]
		public bool isAnimatingPop;
	}
}
