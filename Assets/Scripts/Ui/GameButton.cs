using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class GameButton : AahMonoBehaviour
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

		private void OnDisable()
		{
			this.isDown = false;
			this.longPressTimer = 0f;
		}

		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			Image image = (!(this.graphicTarget == null)) ? this.graphicTarget : this.raycastTarget;
			this.originalColor = image.color;
			if (this.raycastTarget == null)
			{
				UnityEngine.Debug.LogError(base.gameObject.name + " GameButton doesn't have a raycast target. Add an Image component on it or set raycastTarget");
			}
			GameButtonHandler gameButtonHandler = this.raycastTarget.gameObject.GetComponent<GameButtonHandler>();
			if (gameButtonHandler == null)
			{
				gameButtonHandler = this.raycastTarget.gameObject.AddComponent<GameButtonHandler>();
			}
			gameButtonHandler.gameButton = this;
			if (this.text != null)
			{
				this.outline = this.text.GetComponent<Outline>();
				this.shadow = this.text.GetComponent<Shadow>();
				this.textDefaultPos = this.text.rectTransform.anchoredPosition;
			}
			if (this.icon != null)
			{
				this.iconDefaultPos = this.icon.rectTransform.anchoredPosition;
				this.iconColor = this.icon.color;
			}
			this.firstScale = base.transform.localScale;
			this.animScaleFirst = base.transform.localScale;
			this.animScaleGoal = Vector3.zero;
			this.animTimer = this.animPeriod;
			if (this.spriteDown != null || this.spriteNotInteractable != null)
			{
				this.spriteUp = image.sprite;
			}
			if (this.spriteIconNotInteractable != null)
			{
				this.spriteIconInteractable = this.icon.sprite;
			}
			this.updateTime = Time.realtimeSinceStartup;
		}

		public override void AahUpdate(float dt)
		{
			Image image = (!(this.graphicTarget == null)) ? this.graphicTarget : this.raycastTarget;
			if (UiManager.resetButtonStates)
			{
				this.OnDisable();
			}
			this.isJustPressed = (this.isDown && !this.wasDown);
			if (this.onLongPress != null)
			{
				if (this.isDown && this.interactable)
				{
					if (this.longPressTimer > 0.35f)
					{
						this.wasLongPress = true;
						this.isLongPress = true;
					}
					if (this.longPressTimer > 0.6f && this.longPressRepeater > this.GetLongPressRepeaterDuration())
					{
						this.onLongPress();
						this.longPressRepeater = 0f;
					}
					this.longPressRepeater += dt;
					this.longPressTimer += dt;
				}
				else
				{
					this.longPressTimer = 0f;
					this.longPressRepeater = 0f;
				}
			}
			this.wasDown = this.isDown;
			if (this.deltaScaleOnDown != 0f)
			{
				if (Time.realtimeSinceStartup - this.updateTime > dt * 10f)
				{
					base.transform.localScale = this.firstScale;
					this.animTimer = this.animPeriod;
					this.animScaleFirst = base.transform.localScale;
					this.animScaleGoal = Vector3.zero;
				}
				else if (this.animTimer <= this.animPeriod - dt)
				{
					this.animTimer += dt;
					base.transform.localScale = this.animScaleFirst + this.animScaleGoal * Easing.SineEaseIn(this.animTimer, 0f, 1f, this.animPeriod);
				}
				else if (!this.interactable)
				{
					base.transform.localScale = this.firstScale;
				}
				else if (!this.lessStrictScaling)
				{
					base.transform.localScale = this.animScaleFirst + this.animScaleGoal;
				}
				this.updateTime = Time.realtimeSinceStartup;
			}
			if (this.allowColorChangeDown || this.allowColorChangeInteractable)
			{
				if ((!this.interactable || this.fakeDisabled) && this.allowColorChangeInteractable)
				{
					image.color = ((!this.overrideDefaultNotInteractableColor) ? (this.originalColor * GameButton.NotInteractableColorMultiplier) : this.notInteractableColor);
				}
				else if (this.isDown && this.allowColorChangeDown)
				{
					image.color = this.originalColor * GameButton.PressedColorMultiplier;
				}
				else
				{
					image.color = this.originalColor;
				}
			}
			if (this.text != null && this.allowColorChangeText)
			{
				if (this.interactable)
				{
					this.text.color = this.colorTextInteractable;
				}
				else
				{
					this.text.color = this.colorTextNotInteractable;
				}
			}
			if (!this.dontTouchSprite && this.spriteUp != null)
			{
				if (!this.interactable || this.fakeDisabled)
				{
					image.sprite = ((!(this.spriteNotInteractable != null)) ? this.spriteUp : this.spriteNotInteractable);
				}
				else if (this.isDown)
				{
					image.sprite = ((!(this.spriteDown != null)) ? this.spriteUp : this.spriteDown);
				}
				else
				{
					image.sprite = this.spriteUp;
				}
			}
			if (this.icon != null)
			{
				if (this.interactable && !this.fakeDisabled)
				{
					this.icon.enabled = !this.disableIconWhenInteractable;
					if (this.spriteIconNotInteractable != null)
					{
						this.icon.sprite = this.spriteIconInteractable;
					}
				}
				else
				{
					this.icon.enabled = !this.disableIconWhenNotInteractable;
					if (this.spriteIconNotInteractable != null)
					{
						this.icon.sprite = this.spriteIconNotInteractable;
					}
				}
			}
			if (this.allowTextSizeChange)
			{
				if (this.interactable)
				{
					this.text.fontSize = this.textSizeInteractable;
				}
				else
				{
					this.text.fontSize = this.textSizeNotInteractable;
				}
			}
			if (this.allowTextTextChange)
			{
				if (this.interactable)
				{
					this.text.text = this.textTextInteractable;
				}
				else
				{
					this.text.text = this.textTextNotInteractable;
				}
			}
			if (this.disableTextWhenNotInteractable)
			{
				this.text.enabled = this.interactable;
			}
			if (this.allowColorChangeTextEffectInteractable)
			{
				if (this.interactable && !this.fakeDisabled)
				{
					if (this.outline != null)
					{
						this.outline.effectColor = this.colorTextEffectInteractable;
					}
					else if (this.shadow != null)
					{
						this.shadow.effectColor = this.colorTextEffectInteractable;
					}
				}
				else if (this.outline != null)
				{
					this.outline.effectColor = this.colorTextEffectNotInteractable;
				}
				else if (this.shadow != null)
				{
					this.shadow.effectColor = this.colorTextEffectNotInteractable;
				}
			}
			if (this.text != null)
			{
				if (this.allowTextPosChangeWhenNotInteractable && this.deltaTextPosY != 0f && this.text.transform.parent == base.transform)
				{
					if (this.interactable && !this.fakeDisabled)
					{
						this.text.rectTransform.anchoredPosition = this.textDefaultPos;
					}
					else
					{
						this.text.rectTransform.anchoredPosition = this.textDefaultPos + new Vector2(0f, this.deltaTextPosY);
					}
				}
				if (this.interactable && this.allowTextPosChange && this.deltaTextPosYOnDown != 0f && this.text.transform.parent == base.transform)
				{
					if (!this.isDown)
					{
						this.text.rectTransform.anchoredPosition = this.textDefaultPos;
					}
					else
					{
						this.text.rectTransform.anchoredPosition = this.textDefaultPos + new Vector2(0f, this.deltaTextPosYOnDown);
					}
				}
			}
			if (this.icon != null)
			{
				if (this.allowTextPosChangeWhenNotInteractable && this.deltaTextPosY != 0f && this.icon.transform.parent == base.transform)
				{
					if (this.interactable && !this.fakeDisabled)
					{
						this.icon.rectTransform.anchoredPosition = this.iconDefaultPos;
					}
					else
					{
						this.icon.rectTransform.anchoredPosition = ((!this.disableIconWhenInteractable) ? (this.iconDefaultPos + new Vector2(0f, this.deltaTextPosY)) : this.iconDefaultPos);
					}
				}
				if (this.deltaTextPosYOnDown != 0f && this.icon.transform.parent == base.transform)
				{
					if (!this.isDown && this.interactable && !this.fakeDisabled)
					{
						this.icon.rectTransform.anchoredPosition = this.iconDefaultPos;
					}
					else
					{
						this.icon.rectTransform.anchoredPosition = this.iconDefaultPos + new Vector2(0f, this.deltaTextPosYOnDown);
					}
				}
				if (this.allowIconColorChangeWhenNotInteractable)
				{
					this.icon.color = ((!this.interactable) ? this.iconColorNotInteractable : this.iconColor);
				}
			}
			if (!this.isLongPress)
			{
				this.wasLongPress = false;
			}
			this.isLongPress = false;
		}

		private float GetLongPressRepeaterDuration()
		{
			if (this.longPressTimer < 3f)
			{
				return 0.1f;
			}
			return 0.05f;
		}

		public void OnPointerDown()
		{
			if (UiManager.secondsSinceLastButtonClick < 0.1f)
			{
				return;
			}
			if (this.interactable)
			{
				this.isDown = true;
				if (this.onDown != null)
				{
					UiManager.secondsSinceLastButtonClick = 0f;
					this.onDown();
				}
				this.AnimPullIn();
			}
			else
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
		}

		public void OnPointerUp()
		{
			this.isDown = false;
			if (this.onUp != null && this.interactable)
			{
				this.onUp();
			}
			if (this.wasLongPress)
			{
				this.AnimPullOut();
			}
		}

		public void OnPointerEnter()
		{
			if (this.interactable)
			{
				this.isHovering = true;
				if (this.onEnter != null)
				{
					this.onEnter();
				}
				if (this.isDown)
				{
					this.AnimPullIn();
				}
			}
		}

		public void OnSelect()
		{
			if (this.onSelectionChange != null)
			{
				this.onSelectionChange(true);
			}
		}

		public void OnDeselect()
		{
			if (this.onSelectionChange != null)
			{
				this.onSelectionChange(false);
			}
		}

		public void OnPointerExit()
		{
			if (this.interactable)
			{
				if (this.wasLongPress)
				{
					this.isDown = false;
				}
				this.isHovering = false;
				if (this.onExit != null)
				{
					this.onExit();
				}
				this.AnimPullOut();
			}
		}

		public void OnPointerClick()
		{
			if ((this.interactable || this.throwClickEventWhenNoInteractable) && !this.wasLongPress && UiManager.secondsSinceLastButtonClick >= 0.1f)
			{
				if (this.onClick != null)
				{
					UiManager.secondsSinceLastButtonClick = 0f;
					this.onClick();
				}
				this.AnimPullOut();
			}
		}

		private void AnimPullIn()
		{
			if (this.deltaScaleOnDown != 0f)
			{
				this.animTimer = 0f;
				this.animScaleFirst = base.transform.localScale;
				this.animScaleGoal = this.firstScale + this.deltaScaleOnDown * 0.5f * Vector3.one - this.animScaleFirst;
			}
		}

		private void AnimPullOut()
		{
			if (this.deltaScaleOnDown != 0f)
			{
				this.animTimer = 0f;
				this.animScaleFirst = base.transform.localScale;
				this.animScaleGoal = this.firstScale - this.animScaleFirst;
			}
		}

		private int LongPressCount(float t)
		{
			int num = 0;
			t -= this.longPressPeriod;
			if (t > 0f)
			{
				num++;
				t -= this.longPressRapidPeriod;
				while (t > 0f)
				{
					num++;
					if (num > 30)
					{
						t -= this.longPressRapidPeriod * 0.25f;
					}
					else if (num > 15)
					{
						t -= this.longPressRapidPeriod * 0.5f;
					}
					else
					{
						t -= this.longPressRapidPeriod;
					}
				}
			}
			return num;
		}

		public GameButton.VoidFunc onDown;

		public GameButton.VoidFunc onUp;

		public GameButton.VoidFunc onClick;

		public GameButton.VoidFunc onEnter;

		public GameButton.VoidFunc onExit;

		public GameButton.VoidFunc onLongPress;

		public GameButton.BoolFunc onSelectionChange;

		public Image raycastTarget;

		public Image graphicTarget;

		public Text text;

		public Image icon;

		public bool interactable = true;

		public bool throwClickEventWhenNoInteractable;

		public bool fakeDisabled;

		public bool isDown;

		public bool isJustPressed;

		public bool wasLongPress;

		public bool isLongPress;

		public bool isHovering;

		public float deltaScaleOnDown = -0.1f;

		public bool lessStrictScaling;

		public bool allowColorChangeDown = true;

		public bool allowColorChangeInteractable = true;

		public bool overrideDefaultNotInteractableColor;

		public Color notInteractableColor;

		public bool allowColorChangeText;

		public Color colorTextInteractable;

		public Color colorTextNotInteractable;

		public bool allowColorChangeTextEffectInteractable;

		public Color colorTextEffectInteractable;

		public Color colorTextEffectNotInteractable;

		public bool allowTextSizeChange;

		public int textSizeInteractable;

		public int textSizeNotInteractable;

		public bool allowTextTextChange;

		public string textTextInteractable;

		public string textTextNotInteractable;

		public bool disableTextWhenNotInteractable;

		public bool allowTextPosChange;

		public bool allowTextPosChangeWhenNotInteractable;

		public float deltaTextPosY;

		private bool wasDown;

		public bool dontTouchSprite;

		public Sprite spriteDown;

		public Sprite spriteNotInteractable;

		public bool disableIconWhenInteractable;

		public bool disableIconWhenNotInteractable;

		public Sprite spriteIconNotInteractable;

		public float deltaTextPosYOnDown;

		public bool allowIconColorChangeWhenNotInteractable;

		public Color iconColorNotInteractable;

		private Color iconColor;

		private Sprite spriteUp;

		private Sprite spriteIconInteractable;

		private float animTimer;

		private float animPeriod = 0.05f;

		private Vector3 animScaleFirst;

		private Vector3 animScaleGoal;

		private Vector3 firstScale;

		private Vector2 textDefaultPos;

		private Vector2 iconDefaultPos;

		private float updateTime;

		private Outline outline;

		private Shadow shadow;

		private float longPressTimer;

		private float longPressRepeater;

		private float longPressPeriod = 0.6f;

		private float longPressRapidPeriod = 0.1f;

		private Color originalColor;

		private static readonly Color NotInteractableColorMultiplier = new Color(0.6f, 0.6f, 0.6f, 1f);

		private static readonly Color PressedColorMultiplier = new Color(0.7f, 0.7f, 0.7f, 1f);

		private RectTransform m_rectTransform;

		public delegate void VoidFunc();

		public delegate void BoolFunc(bool state);
	}
}
