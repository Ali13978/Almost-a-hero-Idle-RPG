using System;
using DG.Tweening;
using Simulation;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonArtifact : AahMonoBehaviour
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

		public void SetButton(ButtonArtifact.State state, int level, int rarity)
		{
			if (state != ButtonArtifact.State.EMPTY)
			{
				if (state != ButtonArtifact.State.FULL)
				{
					if (state != ButtonArtifact.State.NONEXISTANT)
					{
					}
				}
				else
				{
					this.image.sprite = this.spriteBgFull;
					this.qpBg.gameObject.SetActive(true);
					this.artifactStone.gameObject.SetActive(true);
					this.textLevel.gameObject.SetActive(true);
					this.artifactStone.PlaySpineAnim(rarity);
					this.text.text = "UI_HEROES_LV".Loc();
					this.textLevel.text = level.ToString();
					this.qpBg.sprite = ((!this.maxed) ? UiData.inst.artifactQpBackground : UiData.inst.artifactMaxedQpBackground);
					this.SetEvolveState();
					this.textLevel.color = ((!this.maxed) ? UiData.inst.artifactQpAmountColor : UiData.inst.artifactMaxedQpAmountColor);
					this.text.color = ((!this.maxed) ? UiData.inst.artifactQpLabelColor : UiData.inst.artifactMaxedQpLabelColor);
				}
			}
			else
			{
				this.image.sprite = this.spriteBgEmpty;
				this.qpBg.gameObject.SetActive(false);
				this.artifactStone.gameObject.SetActive(false);
				this.textLevel.gameObject.SetActive(false);
			}
		}

		public void SetDetails()
		{
			ButtonArtifact.State state = this.state;
			if (state != ButtonArtifact.State.EMPTY)
			{
				if (state != ButtonArtifact.State.FULL)
				{
					if (state != ButtonArtifact.State.NONEXISTANT)
					{
					}
				}
				else
				{
					this.image.sprite = this.spriteBgFull;
					this.qpBg.gameObject.SetActive(true);
					this.artifactStone.gameObject.SetActive(true);
					this.textLevel.gameObject.SetActive(true);
					this.artifactStone.PlaySpineAnim(this.visualLevel);
					this.text.text = this.qualityPointString;
					this.qpBg.sprite = ((!this.maxed) ? UiData.inst.artifactQpBackground : UiData.inst.artifactMaxedQpBackground);
					this.SetEvolveState();
					this.text.text = "UI_HEROES_LV".Loc();
					this.textLevel.text = this.levelReal.ToString();
					this.textLevel.color = ((!this.maxed) ? UiData.inst.artifactQpAmountColor : UiData.inst.artifactMaxedQpAmountColor);
					this.text.color = ((!this.maxed) ? UiData.inst.artifactQpLabelColor : UiData.inst.artifactMaxedQpLabelColor);
				}
			}
			else
			{
				this.image.sprite = this.spriteBgEmpty;
				this.qpBg.gameObject.SetActive(false);
				this.artifactStone.gameObject.SetActive(false);
				this.textLevel.gameObject.SetActive(false);
			}
		}

		private void Awake()
		{
			if (this.qpBgGlow == null)
			{
				return;
			}
			this.qpBgGlow.enabled = false;
		}

		private void SetEvolveState()
		{
			if (this.qpBgGlow == null)
			{
				return;
			}
			if (this.canEvolve)
			{
				this.qpBgGlow.sprite = ((!this.maxed) ? UiData.inst.artifactQpBackgroundGlow : UiData.inst.artifactMaxedQpBackgroundGlow);
				if (!this.qpBgGlow.enabled || this.resetEvolveAnim)
				{
					this.resetEvolveAnim = false;
					DOTween.Kill(this.qpBgGlow, true);
					this.qpBgGlow.enabled = true;
					this.qpBgGlow.SetAlpha(0f);
					this.qpBgGlow.DOFade(1f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
				}
			}
			else
			{
				this.resetEvolveAnim = false;
				this.qpBgGlow.enabled = false;
				DOTween.Kill(this.qpBgGlow, true);
			}
		}

		public GameButton gameButton;

		public Image image;

		public ArtifactStone artifactStone;

		public Text text;

		[FormerlySerializedAs("textQP")]
		public Text textLevel;

		public Sprite spriteBgFull;

		public Sprite spriteBgEmpty;

		public Image qpBg;

		public Image qpBgGlow;

		public Image imagePin;

		public SkeletonGraphic appearParticlesSpine;

		[NonSerialized]
		public ButtonArtifact.State state;

		[NonSerialized]
		public bool isSelected;

		[NonSerialized]
		public int visualLevel;

		[NonSerialized]
		public int levelReal;

		[NonSerialized]
		public bool maxed;

		[NonSerialized]
		public bool canEvolve;

		[NonSerialized]
		public bool resetEvolveAnim;

		[NonSerialized]
		public ArtifactEffectCategory type;

		[NonSerialized]
		public string qualityPointString;

		private RectTransform m_rectTransform;

		public enum State
		{
			EMPTY,
			FULL,
			NONEXISTANT
		}
	}
}
