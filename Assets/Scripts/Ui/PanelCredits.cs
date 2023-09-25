using System;
using UnityEngine.UI;

namespace Ui
{
	public class PanelCredits : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.textHeader.text = LM.Get("UI_BACK");
			this.textBeeSquare.text = "Bee Square";
			this.textSoundMusic.text = LM.Get("UI_CREDITS_SOUND_MUSIC");
			this.textAdditionalArt.text = LM.Get("UI_CREDITS_ADDITIONAL_ART");
			this.textWriting.text = LM.Get("UI_CREDITS_WRITING");
			this.textCast.text = LM.Get("UI_CREDITS_CAST");
		}

		public GameButton buttonBack;

		public Text textHeader;

		public Text textBeeSquare;

		public Text textBeeSquareContent;

		public Text textSoundMusic;

		public Text textSoundMusicContent;

		public Text textAdditionalArt;

		public Text textAdditionalArtContent;

		public Text textWriting;

		public Text textWritingContent;

		public Text textCast;

		public Text textCastContent0;

		public Text textCastContent1;
	}
}
