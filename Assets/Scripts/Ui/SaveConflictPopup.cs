using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class SaveConflictPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.cloudSaveState.buttonSelect.onClick = new GameButton.VoidFunc(this.Button_OnClickCloudSave);
			this.localSaveState.buttonSelect.onClick = new GameButton.VoidFunc(this.Button_OnClickLocalSave);
			this.warningPopupNo.onClick = delegate()
			{
				this.warningPopup.gameObject.SetActive(false);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			};
		}

		public void InitStrings()
		{
			this.popupHeader.text = LM.Get("SAVE_CONFLICT_POPUP_HEADER");
			this.popupDesc.text = LM.Get("SAVE_CONFLICT_POPUP_DESC");
			this.cloudSaveState.buttonSelect.text.text = LM.Get("SAVE_CONFLICT_POPUP_CLOUD");
			this.localSaveState.buttonSelect.text.text = LM.Get("SAVE_CONFLICT_POPUP_LOCAL");
			this.warningPopupYes.text.text = LM.Get("UI_YES");
			this.warningPopupNo.text.text = LM.Get("UI_NO");
		}

		private void Button_OnClickLocalSave()
		{
			this.warningPopupHeader.text = LM.Get("TRINKET_DESTROY_DESC1");
			this.warningPopupDesc.text = string.Format(LM.Get("SAVE_CONFLICT_POPUP_WARNING"), LM.Get("SAVE_CONFLICT_POPUP_LOCAL"), LM.Get("SAVE_CONFLICT_POPUP_CLOUD"));
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
			this.warningPopup.gameObject.SetActive(true);
			this.warningPopupYes.onClick = delegate()
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
				this.onSelectionMade(false);
			};
		}

		private void Button_OnClickCloudSave()
		{
			this.warningPopupHeader.text = LM.Get("TRINKET_DESTROY_DESC1");
			this.warningPopupDesc.text = string.Format(LM.Get("SAVE_CONFLICT_POPUP_WARNING"), LM.Get("SAVE_CONFLICT_POPUP_CLOUD"), LM.Get("SAVE_CONFLICT_POPUP_LOCAL"));
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
			this.warningPopup.gameObject.SetActive(true);
			this.warningPopupYes.onClick = delegate()
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
				this.onSelectionMade(true);
			};
		}

		public Action<bool> onSelectionMade;

		public Text popupHeader;

		public Text popupDesc;

		public SaveStatePanel cloudSaveState;

		public SaveStatePanel localSaveState;

		public RectTransform warningPopup;

		public Text warningPopupHeader;

		public Text warningPopupDesc;

		public GameButton warningPopupYes;

		public GameButton warningPopupNo;
	}
}
