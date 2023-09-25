using System;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonGameMode : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.unlockAnimationEventSet = false;
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.panelGlobalBonus.InitStrings();
		}

		public void Reset()
		{
			this.startAnimTimer = -1f;
			this.unlockAnimationCalled = false;
			this.spineAnimLock.AnimationState.SetAnimation(0, "idle", true);
			this.state = ButtonGameMode.State.LOCKED;
		}

		public override void AahUpdate(float dt)
		{
			if (!this.unlockAnimationEventSet && this.spineAnimLock.AnimationState != null)
			{
				this.unlockAnimationEventSet = true;
				this.spineAnimLock.AnimationState.Complete += this.AnimationState_Complete;
				this.spineAnimLock.AnimationState.GetCurrent(0).MixTime = this.spineAnimLock.AnimationState.GetCurrent(0).AnimationEnd * GameMath.GetRandomFloat(0f, 1f, GameMath.RandType.NoSeed);
			}
			if (this.startAnimTimer > 0f)
			{
				this.startAnimTimer -= dt;
				if (this.startAnimTimer <= 0f)
				{
					if (this.spineAnimLock.AnimationState != null)
					{
						this.spineAnimLock.AnimationState.SetAnimation(0, "open", false);
						this.unlockAnimationInSession = true;
						this.unlockAnimationCalled = true;
					}
					else
					{
						this.startAnimTimer = dt * 0.5f;
					}
				}
				this.state = ButtonGameMode.State.LOCKED;
			}
			ButtonGameMode.State state = this.state;
			if (state != ButtonGameMode.State.NONE)
			{
				if (state != ButtonGameMode.State.SELECTED)
				{
					if (state == ButtonGameMode.State.LOCKED)
					{
						this.textGold.gameObject.SetActive(false);
						this.image.color = this.colorLocked;
						this.lockedObject.SetActive(true);
						if (!this.unlockAnimationInSession && this.unlockAnimationCalled && this.spineAnimLock.AnimationState != null)
						{
							this.unlockAnimationCalled = false;
							this.spineAnimLock.AnimationState.SetAnimation(0, "idle", true);
							this.textLock.color = new Color(this.textLock.color.r, this.textLock.color.g, this.textLock.color.b, 1f);
						}
						this.barUnlocks.gameObject.SetActive(false);
					}
				}
				else
				{
					this.textGold.gameObject.SetActive(true);
					this.textGold.text = this.goldString;
					this.image.color = this.colorSelected;
					this.HandleUnlockAnim(dt);
					if (!this.dontAutoEnableBar)
					{
						this.barUnlocks.gameObject.SetActive(true);
					}
				}
			}
			else
			{
				this.textGold.gameObject.SetActive(true);
				this.textGold.text = this.goldString;
				this.image.color = this.colorUnlocked;
				this.HandleUnlockAnim(dt);
				if (!this.dontAutoEnableBar)
				{
					this.barUnlocks.gameObject.SetActive(true);
				}
			}
		}

		private void AnimationState_Complete(TrackEntry trackEntry)
		{
			this.unlockAnimationInSession = false;
			this.unlockAnimationPlaying = false;
		}

		public void StartUnlockAnim(float startAnimDelay)
		{
			this.unlockAnimationPlaying = true;
			this.textLock.color = new Color(this.textLock.color.r, this.textLock.color.g, this.textLock.color.b, 0f);
			this.startAnimTimer = startAnimDelay;
		}

		private void HandleUnlockAnim(float dt)
		{
			this.lockedObject.SetActive(this.unlockAnimationInSession || this.forceLockState);
			if (this.forceLockState)
			{
				this.textLock.color = new Color(this.textLock.color.r, this.textLock.color.g, this.textLock.color.b, 0f);
			}
		}

		public bool IsInfoEnabled()
		{
			return this.panelGlobalBonus.gameObject.activeSelf;
		}

		public void Enableinfo()
		{
			this.panelGlobalBonus.gameObject.SetActive(true);
		}

		public void Disableinfo()
		{
			this.panelGlobalBonus.gameObject.SetActive(false);
		}

		public ButtonGameMode.State state;

		public string goldString;

		public Text text;

		public Image image;

		[SerializeField]
		private Image imageOverlay;

		[SerializeField]
		private GameObject iconGold;

		[SerializeField]
		private Text textGold;

		public GameButton gameButton;

		public GameButton buttonInfo;

		public Color colorSelected;

		public Color colorUnlocked;

		public Color colorLocked;

		public GameObject lockedObject;

		public SkeletonGraphic spineAnimLock;

		public Scaler barUnlocks;

		public Text textBarUnlocks;

		public Text textLock;

		public bool dontAutoEnableBar;

		public PanelGlobalBonus panelGlobalBonus;

		public bool forceLockState;

		public bool unlockAnimationPlaying;

		private bool unlockAnimationInSession;

		private bool unlockAnimationCalled;

		private bool unlockAnimationEventSet;

		private float startAnimTimer;

		public enum State
		{
			NONE,
			SELECTED,
			LOCKED
		}
	}
}
