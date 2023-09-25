using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelArtifactsReroll : AahMonoBehaviour
	{
		public PanelArtifactsReroll.State state
		{
			get
			{
				return this._state;
			}
			set
			{
				switch (value)
				{
				case PanelArtifactsReroll.State.Begin:
					this.imageBg.color = new Color(this.imageBg.color.r, this.imageBg.color.g, this.imageBg.color.b, 0f);
					this.panelRerolledArtifactOld.SetAlpha(0f);
					this.panelRerolledArtifactNew.SetAlpha(0f);
					this.panelRerolledArtifactOld.SetStoneBoxAlpha(0f);
					this.panelRerolledArtifactNew.SetStoneBoxAlpha(0f);
					this.panelRerolledArtifactOld.artifactStone.SetAlpha(1f);
					this.panelRerolledArtifactNew.artifactStone.SetAlpha(0f);
					this.panelRerolledArtifactOld.artifactStone.transform.localScale = Vector3.one * 1.2f;
					this.panelRerolledArtifactOld.buttonSelect.interactable = false;
					this.panelRerolledArtifactNew.buttonSelect.interactable = false;
					this.panelRerolledArtifactOld.imageStoneHolder.rectTransform.anchoredPosition = this.stoneBoxOldBeginPos;
					this.panelRerolledArtifactNew.imageStoneHolder.rectTransform.anchoredPosition = this.stoneBoxNewBeginPos;
					this.buttonReroll.transform.localScale = new Vector3(0f, 0f, 1f);
					this.period = 0.3f;
					break;
				case PanelArtifactsReroll.State.BeginAgain:
					this.imageBg.color = new Color(this.imageBg.color.r, this.imageBg.color.g, this.imageBg.color.b, 1f);
					this.panelRerolledArtifactOld.SetAlpha(0f);
					this.panelRerolledArtifactNew.SetAlpha(0f);
					this.panelRerolledArtifactOld.SetStoneBoxAlpha(0f);
					this.panelRerolledArtifactNew.SetStoneBoxAlpha(0f);
					this.panelRerolledArtifactOld.artifactStone.SetAlpha(1f);
					this.panelRerolledArtifactNew.artifactStone.SetAlpha(0f);
					this.panelRerolledArtifactOld.artifactStone.transform.localScale = Vector3.one * 1.2f;
					this.panelRerolledArtifactOld.buttonSelect.interactable = false;
					this.panelRerolledArtifactNew.buttonSelect.interactable = false;
					this.panelRerolledArtifactOld.imageStoneHolder.rectTransform.anchoredPosition = this.stoneBoxOldBeginPos;
					this.panelRerolledArtifactNew.imageStoneHolder.rectTransform.anchoredPosition = this.stoneBoxNewBeginPos;
					this.buttonReroll.transform.localScale = new Vector3(0f, 0f, 1f);
					this.period = 0.3f;
					break;
				case PanelArtifactsReroll.State.PreviousArtifactSlide:
					this.period = 0.6f;
					break;
				case PanelArtifactsReroll.State.NewArtifactAppear:
					this.panelRerolledArtifactNew.artifactStone.rectTransform.position = this.artifactAppearPos;
					this.artifactStoneNewBeginPos = this.panelRerolledArtifactNew.artifactStone.rectTransform.anchoredPosition;
					this.period = 0.6f;
					break;
				case PanelArtifactsReroll.State.NewArtifactSlide:
					this.period = 0.3f;
					break;
				case PanelArtifactsReroll.State.RerollAgainAppear:
					this.period = 0.2f;
					break;
				case PanelArtifactsReroll.State.Choose:
					this.panelRerolledArtifactOld.buttonSelect.interactable = true;
					this.panelRerolledArtifactNew.buttonSelect.interactable = true;
					break;
				case PanelArtifactsReroll.State.ChosenArtifactSlide:
					this.panelRerolledArtifactOld.buttonSelect.interactable = false;
					this.panelRerolledArtifactNew.buttonSelect.interactable = false;
					if (this.isNewChosen)
					{
						this.panelRerolledArtifactNew.imageStoneHolder.rectTransform.position = this.artifactAppearPos;
						this.stoneBoxChosenMiddlePos = this.panelRerolledArtifactNew.imageStoneHolder.rectTransform.anchoredPosition;
						this.panelRerolledArtifactNew.imageStoneHolder.rectTransform.anchoredPosition = this.stoneBoxNewBeginPos;
					}
					else
					{
						this.panelRerolledArtifactOld.imageStoneHolder.rectTransform.position = this.artifactAppearPos;
						this.stoneBoxChosenMiddlePos = this.panelRerolledArtifactOld.imageStoneHolder.rectTransform.anchoredPosition;
						this.panelRerolledArtifactOld.imageStoneHolder.rectTransform.anchoredPosition = this.stoneBoxOldBeginPos;
					}
					this.period = 0.5f;
					break;
				case PanelArtifactsReroll.State.FadeOut:
					if (this.isNewChosen)
					{
						this.panelRerolledArtifactNew.imageStoneHolder.rectTransform.position = this.boxArtifactsScreen.position;
						this.stoneBoxChosenSitPos = this.panelRerolledArtifactNew.imageStoneHolder.rectTransform.anchoredPosition;
						this.panelRerolledArtifactNew.imageStoneHolder.rectTransform.anchoredPosition = this.stoneBoxChosenMiddlePos;
					}
					else
					{
						this.panelRerolledArtifactOld.imageStoneHolder.rectTransform.position = this.boxArtifactsScreen.position;
						this.stoneBoxChosenSitPos = this.panelRerolledArtifactOld.imageStoneHolder.rectTransform.anchoredPosition;
						this.panelRerolledArtifactOld.imageStoneHolder.rectTransform.anchoredPosition = this.stoneBoxChosenMiddlePos;
					}
					this.boxArtifactsScreen.gameObject.SetActive(false);
					this.period = 0.5f;
					break;
				}
				this._state = value;
				this.timer = 0f;
				this.animating = true;
			}
		}

		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.gameButton.onDown = delegate()
			{
				this.pressed = true;
			};
			this.artifactStoneOldGoalPos = this.panelRerolledArtifactOld.artifactStone.rectTransform.anchoredPosition;
			this.artifactStoneNewGoalPos = this.panelRerolledArtifactNew.artifactStone.rectTransform.anchoredPosition;
			this.stoneBoxOldBeginPos = this.panelRerolledArtifactOld.imageStoneHolder.rectTransform.anchoredPosition;
			this.stoneBoxNewBeginPos = this.panelRerolledArtifactNew.imageStoneHolder.rectTransform.anchoredPosition;
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.panelRerolledArtifactOld.InitStrings();
			this.panelRerolledArtifactNew.InitStrings();
			this.panelRerolledArtifactOld.textTitle.text = LM.Get("UI_ARTIFACTS_OLD");
			this.panelRerolledArtifactNew.textTitle.text = LM.Get("UI_ARTIFACTS_NEW");
			this.buttonReroll.textDown.text = LM.Get("UI_ARTIFACTS_REROLL_AGAIN");
		}

		public override void AahUpdate(float dt)
		{
			switch (this.state)
			{
			case PanelArtifactsReroll.State.Begin:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer < this.period && !this.pressed)
					{
						float a = Easing.Linear(this.timer, 0f, 1f, this.period);
						this.imageBg.color = new Color(this.imageBg.color.r, this.imageBg.color.g, this.imageBg.color.b, a);
						if (this.stoneArtifactsScreen != null)
						{
							RectTransform rectTransform = this.panelRerolledArtifactOld.artifactStone.rectTransform;
							rectTransform.position = this.stoneArtifactsScreen.position;
						}
					}
					else
					{
						this.imageBg.color = new Color(this.imageBg.color.r, this.imageBg.color.g, this.imageBg.color.b, 1f);
						this.artifactStoneOldBeginPos = this.panelRerolledArtifactOld.artifactStone.rectTransform.anchoredPosition;
						this.animating = false;
					}
				}
				else
				{
					this.state = PanelArtifactsReroll.State.PreviousArtifactSlide;
				}
				break;
			case PanelArtifactsReroll.State.BeginAgain:
				this.imageBg.color = new Color(this.imageBg.color.r, this.imageBg.color.g, this.imageBg.color.b, 1f);
				this.artifactStoneOldBeginPos = this.panelRerolledArtifactOld.artifactStone.rectTransform.anchoredPosition + new Vector2(0f, this.panelRerolledArtifactNew.GetComponent<RectTransform>().anchoredPosition.y - this.panelRerolledArtifactOld.GetComponent<RectTransform>().anchoredPosition.y);
				this.animating = false;
				this.state = PanelArtifactsReroll.State.PreviousArtifactSlide;
				break;
			case PanelArtifactsReroll.State.PreviousArtifactSlide:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer < this.period && !this.pressed)
					{
						float num;
						float num2;
						if (this.timer > this.period * 0.5f)
						{
							num = 1f;
							num2 = 1f;
						}
						else
						{
							num = Easing.Linear(this.timer, 0f, 1f, this.period * 0.5f);
							num2 = Easing.SineEaseOut(this.timer, 0f, 1f, this.period * 0.5f);
						}
						RectTransform rectTransform2 = this.panelRerolledArtifactOld.artifactStone.rectTransform;
						rectTransform2.anchoredPosition = this.artifactStoneOldBeginPos + (this.artifactStoneOldGoalPos - this.artifactStoneOldBeginPos) * num2;
						rectTransform2.localScale = Vector3.one * (1.2f - 0.2f * num2);
						this.panelRerolledArtifactOld.SetAlpha(num);
						this.panelRerolledArtifactOld.SetStoneBoxAlpha(num);
						this.RotateStoneSlightly(rectTransform2, num, true);
					}
					else
					{
						this.panelRerolledArtifactOld.SetAlpha(1f);
						this.panelRerolledArtifactOld.SetStoneBoxAlpha(1f);
						RectTransform rectTransform3 = this.panelRerolledArtifactOld.artifactStone.rectTransform;
						rectTransform3.anchoredPosition = this.artifactStoneOldGoalPos;
						rectTransform3.localScale = Vector3.one;
						this.RotateStoneSlightly(rectTransform3, 1f, true);
						this.animating = false;
					}
				}
				else
				{
					this.state = PanelArtifactsReroll.State.NewArtifactAppear;
				}
				break;
			case PanelArtifactsReroll.State.NewArtifactAppear:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer < this.period && !this.pressed)
					{
						float d = Easing.BackEaseOut(this.timer, 0f, 1f, this.period);
						RectTransform rectTransform4 = this.panelRerolledArtifactNew.artifactStone.rectTransform;
						this.panelRerolledArtifactNew.artifactStone.SetAlpha(1f);
						rectTransform4.localScale = Vector3.one * 2f * d;
					}
					else
					{
						this.panelRerolledArtifactNew.artifactStone.SetAlpha(1f);
						this.panelRerolledArtifactNew.artifactStone.rectTransform.localScale = Vector3.one * 2f;
						this.animating = false;
					}
				}
				else
				{
					this.state = PanelArtifactsReroll.State.NewArtifactSlide;
				}
				break;
			case PanelArtifactsReroll.State.NewArtifactSlide:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer < this.period && !this.pressed)
					{
						float num3 = Easing.Linear(this.timer, 0f, 1f, this.period);
						float num4 = Easing.SineEaseOut(this.timer, 0f, 1f, this.period);
						RectTransform rectTransform5 = this.panelRerolledArtifactNew.artifactStone.rectTransform;
						rectTransform5.anchoredPosition = this.artifactStoneNewBeginPos + (this.artifactStoneNewGoalPos - this.artifactStoneNewBeginPos) * num3;
						this.panelRerolledArtifactNew.artifactStone.rectTransform.localScale = Vector3.one * (2f - num4);
						this.panelRerolledArtifactNew.SetAlpha(num3);
						this.panelRerolledArtifactNew.SetStoneBoxAlpha(num3);
						this.RotateStoneSlightly(rectTransform5, num3, false);
					}
					else
					{
						this.panelRerolledArtifactNew.SetAlpha(1f);
						this.panelRerolledArtifactNew.SetStoneBoxAlpha(1f);
						RectTransform rectTransform6 = this.panelRerolledArtifactNew.artifactStone.rectTransform;
						rectTransform6.anchoredPosition = this.artifactStoneOldGoalPos;
						rectTransform6.localScale = Vector3.one;
						this.RotateStoneSlightly(rectTransform6, 1f, true);
						this.animating = false;
					}
				}
				else
				{
					this.state = PanelArtifactsReroll.State.Choose;
				}
				break;
			case PanelArtifactsReroll.State.RerollAgainAppear:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer < this.period && !this.pressed)
					{
						float num5 = Easing.BackEaseOut(this.timer, 0f, 1f, this.period);
						this.buttonReroll.transform.localScale = new Vector3(num5, num5, 1f);
					}
					else
					{
						this.buttonReroll.transform.localScale = new Vector3(1f, 1f, 1f);
						this.animating = false;
					}
				}
				else
				{
					this.state = PanelArtifactsReroll.State.Choose;
				}
				break;
			case PanelArtifactsReroll.State.ChosenArtifactSlide:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer < this.period && !this.pressed)
					{
						float num6 = Easing.Linear(this.timer, 0f, 1f, this.period);
						float num7 = Easing.SineEaseOut(this.timer, 0f, 1f, this.period);
						RectTransform rectTransform7 = this.panelRerolledArtifactOld.imageStoneHolder.rectTransform;
						ArtifactStone artifactStone = this.panelRerolledArtifactNew.artifactStone;
						PanelNewArtifact panelNewArtifact = this.panelRerolledArtifactNew;
						if (this.isNewChosen)
						{
							rectTransform7 = this.panelRerolledArtifactNew.imageStoneHolder.rectTransform;
							artifactStone = this.panelRerolledArtifactOld.artifactStone;
							panelNewArtifact = this.panelRerolledArtifactOld;
							rectTransform7.anchoredPosition = this.stoneBoxNewBeginPos + (this.stoneBoxChosenMiddlePos - this.stoneBoxNewBeginPos) * num7;
						}
						else
						{
							rectTransform7.anchoredPosition = this.stoneBoxOldBeginPos + (this.stoneBoxChosenMiddlePos - this.stoneBoxOldBeginPos) * num7;
						}
						rectTransform7.localScale = Vector3.one * (1f + num7);
						this.panelRerolledArtifactNew.SetAlpha(1f - num6);
						this.panelRerolledArtifactOld.SetAlpha(1f - num6);
						panelNewArtifact.SetStoneBoxAlpha(1f - num6);
						artifactStone.SetAlpha(1f - num6);
						if (this.buttonReroll.transform.localScale.x > 0f)
						{
							if (num6 > 0.2f)
							{
								this.buttonReroll.transform.localScale = new Vector3(0f, 0f, 1f);
							}
							else
							{
								float num8 = Easing.BackEaseOut(0.2f - num6, 0f, 1f, 0.2f);
								this.buttonReroll.transform.localScale = new Vector3(num8, num8, 1f);
							}
						}
					}
					else
					{
						RectTransform rectTransform8 = this.panelRerolledArtifactOld.imageStoneHolder.rectTransform;
						ArtifactStone artifactStone2 = this.panelRerolledArtifactNew.artifactStone;
						PanelNewArtifact panelNewArtifact2 = this.panelRerolledArtifactNew;
						if (this.isNewChosen)
						{
							rectTransform8 = this.panelRerolledArtifactNew.imageStoneHolder.rectTransform;
							artifactStone2 = this.panelRerolledArtifactOld.artifactStone;
							panelNewArtifact2 = this.panelRerolledArtifactOld;
						}
						rectTransform8.localScale = Vector3.one * 2f;
						this.panelRerolledArtifactNew.SetAlpha(0f);
						this.panelRerolledArtifactOld.SetAlpha(0f);
						panelNewArtifact2.SetStoneBoxAlpha(0f);
						artifactStone2.SetAlpha(0f);
						rectTransform8.anchoredPosition = this.stoneBoxChosenMiddlePos;
						this.buttonReroll.transform.localScale = new Vector3(0f, 0f, 1f);
						this.animating = false;
					}
				}
				else
				{
					this.state = PanelArtifactsReroll.State.FadeOut;
				}
				break;
			case PanelArtifactsReroll.State.FadeOut:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer < this.period && !this.pressed)
					{
						float num9 = Easing.Linear(this.timer, 0f, 1f, this.period);
						float num10 = Easing.SineEaseOut(this.timer, 0f, 1f, this.period);
						RectTransform rectTransform9 = this.panelRerolledArtifactOld.imageStoneHolder.rectTransform;
						if (this.isNewChosen)
						{
							rectTransform9 = this.panelRerolledArtifactNew.imageStoneHolder.rectTransform;
						}
						rectTransform9.anchoredPosition = this.stoneBoxChosenMiddlePos + (this.stoneBoxChosenSitPos - this.stoneBoxChosenMiddlePos) * num10;
						rectTransform9.localScale = Vector3.one * (2f - 1f * num10);
						this.imageBg.color = new Color(this.imageBg.color.r, this.imageBg.color.g, this.imageBg.color.b, 1f - num9);
					}
					else
					{
						RectTransform rectTransform10 = this.panelRerolledArtifactOld.imageStoneHolder.rectTransform;
						if (this.isNewChosen)
						{
							rectTransform10 = this.panelRerolledArtifactNew.imageStoneHolder.rectTransform;
						}
						rectTransform10.anchoredPosition = this.stoneBoxChosenSitPos;
						rectTransform10.localScale = Vector3.one;
						this.imageBg.color = new Color(this.imageBg.color.r, this.imageBg.color.g, this.imageBg.color.b, 0f);
						this.animating = false;
					}
				}
				else
				{
					this.state = PanelArtifactsReroll.State.End;
				}
				break;
			}
			if (this.pressed)
			{
				this.pressed = false;
			}
		}

		private void RotateStoneSlightly(RectTransform stoneRect, float animRatio, bool isRight = true)
		{
			if (animRatio == 0f || animRatio == 1f)
			{
				stoneRect.eulerAngles = Vector3.zero;
			}
			else if (animRatio < 0.5f)
			{
				float d = Easing.SineEaseOut(animRatio, 0f, 1f, 0.5f);
				stoneRect.eulerAngles = Vector3.forward * 15f * ((!isRight) ? -1f : 1f) * d;
			}
			else
			{
				float d2 = Easing.SineEaseOut(1f - animRatio, 0f, 1f, 0.5f);
				stoneRect.eulerAngles = Vector3.forward * 15f * ((!isRight) ? -1f : 1f) * d2;
			}
		}

		public void Choose(bool isNew)
		{
			this.isNewChosen = isNew;
			this.state = PanelArtifactsReroll.State.ChosenArtifactSlide;
		}

		private PanelArtifactsReroll.State _state;

		public Image imageBg;

		public GameButton gameButton;

		public PanelNewArtifact panelRerolledArtifactOld;

		public PanelNewArtifact panelRerolledArtifactNew;

		public ButtonUpgradeAnim buttonReroll;

		private bool pressed;

		public bool updateDetails;

		public RectTransform stoneArtifactsScreen;

		public RectTransform boxArtifactsScreen;

		private float timer;

		private float period;

		private bool animating;

		private Vector2 artifactStoneOldBeginPos;

		private Vector2 artifactStoneOldGoalPos;

		private Vector2 artifactStoneNewBeginPos;

		private Vector2 artifactStoneNewGoalPos;

		private Vector2 stoneBoxChosenMiddlePos;

		private Vector2 stoneBoxChosenSitPos;

		private Vector2 stoneBoxNewBeginPos;

		private Vector2 stoneBoxOldBeginPos;

		private Vector3 artifactAppearPos = new Vector3(0f, -0.3f, 0f);

		private bool isNewChosen;

		public Simulator sim;

		public Artifact newArtifact;

		public enum State
		{
			Begin,
			BeginAgain,
			PreviousArtifactSlide,
			NewArtifactAppear,
			NewArtifactSlide,
			RerollAgainAppear,
			Choose,
			ChosenArtifactSlide,
			FadeOut,
			End
		}
	}
}
