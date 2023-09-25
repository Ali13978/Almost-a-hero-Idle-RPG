using System;
using UnityEngine.EventSystems;

namespace Ui
{
	public class GameButtonHandler : AahMonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IEventSystemHandler
	{
		public override void Register()
		{
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			this.gameButton.OnPointerDown();
			EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			this.gameButton.OnPointerUp();
		}

		public void OnDisable()
		{
			this.gameButton.OnPointerUp();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			this.gameButton.OnPointerClick();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			this.gameButton.OnPointerEnter();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			this.gameButton.OnPointerExit();
		}

		public void OnSelect(BaseEventData eventData)
		{
			this.gameButton.OnSelect();
		}

		public void OnDeselect(BaseEventData eventData)
		{
			this.gameButton.OnDeselect();
		}

		public GameButton gameButton;
	}
}
