using System;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelRing : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.timer = 0.6f;
			this.SetViewDiscrete();
		}

		public override void AahUpdate(float dt)
		{
			if (this.totem is TotemLightning && this.timer < 0.6f)
			{
				this.timer += dt;
				float num = Easing.SineEaseOut(0.6f - this.timer, 0f, 1f, 0.6f);
				this.onCount = (this.shineCount = Mathf.CeilToInt((float)this.totalCount * num));
				if (this.onCount <= this.onCountDuringAnim)
				{
					this.timer = 0.6f;
					this.shineCount = 0;
				}
			}
		}

		public void SetViewDiscrete(int totalCountSim, int onCountSim)
		{
			onCountSim = Mathf.Min(totalCountSim - 1, onCountSim);
			if (this.timer >= 0.6f)
			{
				if (onCountSim < totalCountSim - 1)
				{
					this.shineCount = 0;
					this.shineBackCount = 1;
					if (this.totem is TotemLightning)
					{
						this.textPercentage.color = this.colorTextLightning;
						this.outlinePercentage.enabled = false;
					}
				}
				else
				{
					this.shineCount = onCountSim;
					this.shineBackCount = 0;
					if (this.totem is TotemLightning)
					{
						this.textPercentage.color = Color.white;
						this.outlinePercentage.enabled = true;
						this.outlinePercentage.effectColor = this.colorOutlineLightning;
					}
				}
				if (this.onCount <= onCountSim || totalCountSim == 0)
				{
					this.onCount = onCountSim;
				}
				else
				{
					this.timer = 0f;
					this.onCountDuringAnim = 0;
				}
			}
			else
			{
				this.onCountDuringAnim = onCountSim;
			}
			this.totalCount = totalCountSim - 1;
			this.SetViewDiscrete();
		}

		private void SetViewDiscrete()
		{
			if (this.totalCount <= 0)
			{
				this.barsLayout.childCount = 0;
				this.bgBarsLayout.childCount = 0;
			}
			else
			{
				this.barsLayout.childCount = this.totalCount;
				this.bgBarsLayout.spacing = this.barsLayout.children[0].rectTransform.sizeDelta.x * this.barsLayout.scaleX;
				this.bgBarsLayout.childCount = this.totalCount + 1;
				int i = 0;
				int count = this.bgBarsLayout.children.Count;
				while (i < count)
				{
					this.bgBarsLayout.children[i].enabled = (i != 0 && i != count - 1);
					i++;
				}
				int j = 0;
				int count2 = this.barsLayout.children.Count;
				while (j < count2)
				{
					this.barsLayout.children[j].GetComponent<RingBar>().SetOnOff(j < this.onCount, j < this.shineCount || j >= this.onCount - this.shineBackCount);
					j++;
				}
				if (this.totem is TotemLightning)
				{
					this.textPercentage.text = GameMath.GetPercentString(1f * (float)this.onCount / (float)this.totalCount, true);
				}
			}
		}

		public void SetRingSprites(Sprite ringNormal, Sprite ringShine)
		{
			this.spriteRingNormal = ringNormal;
			this.spriteRingShine = ringShine;
		}

		public void SetBarContinuousSprites()
		{
			if (this.totem is TotemFire)
			{
				this.imageFillContinuous.sprite = this.spriteBarFire;
				this.imageFillContinuousOverheat.sprite = this.spriteBarFireShine;
				this.imageContinuousHead.sprite = this.spriteBarHeadFire;
			}
			else if (this.totem is TotemIce)
			{
				this.imageFillContinuous.sprite = this.spriteBarIce;
				this.imageFillContinuousOverheat.sprite = this.spriteBarIceShine;
				this.imageContinuousHead.sprite = this.spriteBarHeadIce;
			}
			else
			{
				if (!(this.totem is TotemEarth))
				{
					throw new NotImplementedException();
				}
				this.imageFillContinuous.sprite = this.spriteBarEarth;
				this.imageFillContinuousOverheat.sprite = this.spriteBarEarthShine;
				this.imageContinuousHead.sprite = this.spriteBarHeadEarth;
			}
		}

		public void SetViewContinuous(float fillRatio, bool overheated)
		{
			fillRatio = Mathf.Min(1f, fillRatio);
			Image image = this.imageFillContinuous;
			float fillAmount = fillRatio;
			this.imageFillContinuousOverheat.fillAmount = fillAmount;
			image.fillAmount = fillAmount;
			this.imageFillContinuous.enabled = !overheated;
			this.imageFillContinuousOverheat.enabled = overheated;
			bool flag = this.totem is TotemFire;
			bool flag2 = this.totem is TotemIce;
			bool flag3 = this.totem is TotemEarth;
			if (flag || flag2 || flag3)
			{
				this.textPercentage.text = GameMath.GetPercentString(fillRatio, true);
				if (overheated)
				{
					this.textPercentage.color = Color.white;
					this.outlinePercentage.enabled = true;
					if (flag2)
					{
						this.outlinePercentage.effectColor = this.colorOutlineIce;
					}
					else if (flag)
					{
						this.outlinePercentage.effectColor = this.colorOutlineFire;
					}
					else if (flag3)
					{
						this.outlinePercentage.effectColor = this.colorOutlineEarth;
					}
				}
				else
				{
					if (flag2)
					{
						this.textPercentage.color = this.colorTextIce;
					}
					else if (flag)
					{
						this.textPercentage.color = this.colorTextFire;
					}
					else if (flag3)
					{
						this.textPercentage.color = this.colorTextEarth;
					}
					this.outlinePercentage.enabled = false;
				}
				if (fillRatio > 0f && !overheated)
				{
					if (fillRatio > 0.05f)
					{
						this.imageContinuousHead.color = Color.white;
					}
					else
					{
						this.imageContinuousHead.color = new Color(1f, 1f, 1f, fillRatio / 0.05f);
					}
					this.imageContinuousHead.rectTransform.anchoredPosition = new Vector2(this.imageFillContinuous.rectTransform.sizeDelta.x * fillRatio, 0f);
					this.imageContinuousHead.enabled = true;
				}
				else
				{
					this.imageContinuousHead.enabled = false;
				}
			}
			else
			{
				this.imageContinuousHead.enabled = false;
			}
			this.fillRatioLast = fillRatio;
		}

		public void MakePanelVisible(bool visible)
		{
			if (visible)
			{
				this.imageRing.enabled = true;
				this.imageBar.enabled = true;
				if (this.shineCount > 0 || this.imageFillContinuousOverheat.enabled)
				{
					this.imageRing.sprite = this.spriteRingShine;
				}
				else
				{
					this.imageRing.sprite = this.spriteRingNormal;
				}
				this.imagePercentage.gameObject.SetActive(true);
			}
			else
			{
				this.imageRing.enabled = false;
				this.imageBar.enabled = false;
				this.imagePercentage.gameObject.SetActive(false);
			}
		}

		public void TiltRing(float extraTilt = 1f)
		{
			if (this.tiltAnim != null && this.tiltAnim.isPlaying)
			{
				this.tiltAnim.Complete(true);
				this.tiltAnim.Goto(0f, false);
			}
			else
			{
				this.tiltAnim = DOTween.Sequence();
				this.tiltAnim.Append(this.imageRing.rectTransform.DOPunchRotation(new Vector3(0f, 0f, 8f * extraTilt), 0.2f, 25, 1f)).Join(this.imageRing.rectTransform.DOPunchScale(new Vector3(0.05f, 0.05f, 1f) * extraTilt, 0.2f, 10, 1f));
				this.tiltAnim.Play<Sequence>();
			}
		}

		public Totem totem;

		private int onCount;

		private int totalCount;

		private int shineCount;

		private int shineBackCount;

		private int onCountDuringAnim;

		public HorizontalLayout bgBarsLayout;

		public HorizontalLayout barsLayout;

		public Image imageRing;

		public Image imageBar;

		public Image imagePercentage;

		public Image imageFillContinuous;

		public Image imageFillContinuousOverheat;

		public Image imageContinuousHead;

		public Text textPercentage;

		public NicerOutline outlinePercentage;

		public RingSpine ringSpine;

		[NonSerialized]
		public Sprite spriteRingNormal;

		[NonSerialized]
		public Sprite spriteRingShine;

		private float timer;

		private const float period = 0.6f;

		public Color colorTextLightning;

		public Color colorOutlineLightning;

		public Color colorTextFire;

		public Color colorOutlineFire;

		public Color colorTextIce;

		public Color colorOutlineIce;

		public Color colorTextEarth;

		public Color colorOutlineEarth;

		public Sprite spriteBarFire;

		public Sprite spriteBarIce;

		public Sprite spriteBarEarth;

		public Sprite spriteBarFireShine;

		public Sprite spriteBarIceShine;

		public Sprite spriteBarEarthShine;

		public Sprite spriteBarHeadFire;

		public Sprite spriteBarHeadIce;

		public Sprite spriteBarHeadEarth;

		private Sequence tiltAnim;

		public CanvasGroup canvasGroup;

		[HideInInspector]
		public float fillRatioLast;
	}
}
