using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui
{
	public class CharmOptionCard : Selectable, IPointerClickHandler, IEventSystemHandler
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

		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.onClick == null || !base.interactable)
			{
				return;
			}
			this.onClick(this.index);
		}

		private RectTransform m_rectTransform;

		public CharmCard charmCard;

		public CharmCardInfo charmCardInfo;

		public Image selectionOutline;

		public Action<int> onClick;

		public int index;
	}
}
