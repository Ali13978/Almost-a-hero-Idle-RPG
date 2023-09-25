using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class VersionNotesPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void AahUpdate(float dt)
		{
			if (PatchNote.patchNotes == null)
			{
				this.textConnectionWarning.gameObject.SetActive(true);
				this.scrollRect.gameObject.SetActive(false);
			}
			else
			{
				this.textConnectionWarning.gameObject.SetActive(false);
				this.scrollRect.gameObject.SetActive(true);
			}
		}

		public void InitStrings()
		{
			this.textHeader.text = LM.Get("UI_WHAT_IS_NEW").ToUpper(LM.culture);
			this.buttonOkay.text.text = LM.Get("UI_OKAY");
			this.textConnectionWarning.text = LM.Get("UI_SHOP_CHEST_0_WAIT");
			this.textTranslationDisclaimer.text = LM.Get("TRANSLATION_DISCLAIMER");
			foreach (VersionInfoLabel versionInfoLabel in this.versionInfoLabels)
			{
				versionInfoLabel.InitStrings();
			}
		}

		public override void Init()
		{
			this.versionInfoLabels = new List<VersionInfoLabel>();
			PatchNote.onPatchNoteInitialized += this.InitializeNotes;
			this.buttonOkay.onClick = (this.buttonClose.onClick = delegate()
			{
				this.uiManager.state = UiState.HUB_OPTIONS;
			});
		}

		public void InitializeNotes()
		{
			PatchNote[] patchNotes = PatchNote.patchNotes;
			Utility.FillUiElementList<VersionInfoLabel>(this.versionInfoLabelPrefab, this.scrollRect.content, patchNotes.Length, this.versionInfoLabels);
			for (int i = 0; i < patchNotes.Length; i++)
			{
				VersionInfoLabel versionInfoLabel = this.versionInfoLabels[i];
				PatchNote patchNote = patchNotes[i];
				versionInfoLabel.textHeader.text = patchNote.header;
				versionInfoLabel.textLabel.text = patchNote.body;
				float textPreferredHeight = versionInfoLabel.textLabel.GetTextPreferredHeight();
				versionInfoLabel.textLabel.rectTransform.SetSizeDeltaY(textPreferredHeight);
				int num = 100;
				bool flag = false;
				if (patchNote.HasUri() && !flag)
				{
					num += 170;
					versionInfoLabel.button.gameObject.SetActive(true);
					versionInfoLabel.button.onClick = new GameButton.VoidFunc(patchNote.OpenUri);
					versionInfoLabel.button.text.text = LM.Get("UI_PATCH_NOTES").ToUpper();
					if (i == patchNotes.Length - 1)
					{
						versionInfoLabel.SetButtonOlderPatch();
					}
					else
					{
						versionInfoLabel.SetButtonNormalPatch();
					}
				}
				else
				{
					versionInfoLabel.button.gameObject.SetActive(false);
				}
				versionInfoLabel.rectTransform.SetSizeDeltaY(textPreferredHeight + (float)num);
				versionInfoLabel.rectTransform.SetLeftDelta(10f);
				versionInfoLabel.rectTransform.SetRightDelta(10f);
			}
			foreach (VersionInfoLabel versionInfoLabel2 in this.versionInfoLabels)
			{
				this.uiManager.HandleInstantiate(versionInfoLabel2.button.gameObject);
			}
			float num2 = 10f;
			for (int j = 0; j < this.versionInfoLabels.Count; j++)
			{
				VersionInfoLabel versionInfoLabel3 = this.versionInfoLabels[j];
				versionInfoLabel3.rectTransform.SetAnchorPosY(-num2);
				num2 += versionInfoLabel3.rectTransform.sizeDelta.y + 10f;
			}
			this.textTranslationDisclaimer.rectTransform.SetAnchorPosY(-num2);
			num2 += this.textTranslationDisclaimer.rectTransform.sizeDelta.y + 10f;
			this.scrollRect.content.SetSizeDeltaY(num2);
			float y = Mathf.Clamp(440f + num2, 440f, this.maxHeight);
			this.popupRect.SetSizeDeltaY(y);
		}

		[SerializeField]
		private float maxHeight = 500f;

		[SerializeField]
		private Text textHeader;

		[SerializeField]
		private VersionInfoLabel versionInfoLabelPrefab;

		[SerializeField]
		private ScrollRect scrollRect;

		[SerializeField]
		private GameButton buttonClose;

		[SerializeField]
		private GameButton buttonOkay;

		public RectTransform popupRect;

		[SerializeField]
		private UiManager uiManager;

		[SerializeField]
		private Text textConnectionWarning;

		[SerializeField]
		private Text textTranslationDisclaimer;

		private List<VersionInfoLabel> versionInfoLabels;
	}
}
