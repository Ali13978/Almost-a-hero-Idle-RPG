using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DG.Tweening;
using Simulation;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelCharmPackOpening : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public void InitStrings()
		{
			this.charmOptionCard.charmCard.newLabel.text = LM.Get("UI_NEW");
		}

		public override void Init()
		{
			this.charmDupplicatesToOpen = new List<CharmDuplicate>();
			this.oldDupplicateCounts = new List<int>();
			this.neededCounts = new List<int>();
			this.oldLevels = new List<int>();
			this.packSpineSkeleton.Initialize(true);
			this.packParticleSpineSkeleton.Initialize(true);
			this.animCard = this.packSpineSkeleton.SkeletonDataAsset.GetSkeletonData(false).FindAnimation("cards");
			this.animCardLast = this.packSpineSkeleton.SkeletonDataAsset.GetSkeletonData(false).FindAnimation("last_cards");
			this.animOpen = this.packSpineSkeleton.SkeletonDataAsset.GetSkeletonData(false).FindAnimation("opening");
			this.animOpenSpecial = this.packSpineSkeleton.SkeletonDataAsset.GetSkeletonData(false).FindAnimation("opening_special");
			this.animShake = this.packSpineSkeleton.SkeletonDataAsset.GetSkeletonData(false).FindAnimation("shake");
			this.animEmptyIdle = this.packSpineSkeleton.SkeletonDataAsset.GetSkeletonData(false).FindAnimation("idle_empty");
			this.charmOptionCard.charmCard.backgroundFrame.enabled = false;
		}

		public void SetHidden()
		{
			this.charmOptionCard.gameObject.SetActive(false);
			this.charmOptionCard.rectTransform.SetSizeDeltaX(170f);
			this.charmOptionCard.rectTransform.anchoredPosition = new Vector2(-238f, 274f);
			this.charmOptionCard.charmCardInfo.gameObject.SetActive(false);
			this.charmOptionCard.charmCard.levelProgresBarParent.SetAnchorPosY(50f);
			this.charmOptionCard.charmCard.copyCounterParent.rectTransform.SetSizeDeltaX(52f);
			this.charmOptionCard.charmCard.copyCounterParent.gameObject.SetActive(false);
			this.charmOptionCard.charmCard.levelProgresBarParent.gameObject.SetActive(false);
			this.charmOptionCard.charmCard.charmIcon.gameObject.SetActive(false);
			this.charmOptionCard.charmCard.levelParent.gameObject.SetActive(false);
			this.packParticleSpineSkeleton.gameObject.SetActive(false);
			this.duplicateCounterText.gameObject.SetActive(false);
			this.duplicateCounterText.SetAlpha(0f);
			this.targetDupplicateCount = this.GetTargetCount();
			this.remainingDupplicateToCount = this.charmDupplicateToOpen.count;
			this.consumedDupplicateToCount = 0;
			this.charmOptionCard.charmCard.copyCounterText.text = "x" + this.charmDupplicateToOpen.count;
			this.isCardBackActive = true;
			this.phase = 0;
		}

		public override void AahUpdate(float dt)
		{
			if (!this.isBigPack)
			{
				if (this.state != PanelCharmPackOpening.State.IDLE && this.state != PanelCharmPackOpening.State.DONE)
				{
					if (this.tweenSpeed > 1f)
					{
						this.tweenSpeed -= dt;
					}
					else
					{
						this.tweenSpeed = 1f;
					}
				}
				else
				{
					if (this.state == PanelCharmPackOpening.State.IDLE)
					{
						if (this.currentIdleTime == 0f)
						{
							UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiCharmPackShake, 1f));
						}
						this.currentIdleTime += dt;
						if (this.currentIdleTime > 4f)
						{
							this.currentIdleTime %= 4f;
							UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiCharmPackShake, 1f));
						}
					}
					this.tweenSpeed = 1f;
				}
			}
			if (this.currentSeq != null && this.currentSeq.IsPlaying())
			{
				this.currentSeq.timeScale = this.tweenSpeed;
			}
			this.packSpineSkeleton.timeScale = this.tweenSpeed;
		}

		public void SetCardDeckCounting()
		{
			this.isCardBackActive = true;
		}

		public void DoPackOpen(CharmType charmType)
		{
			this.state = PanelCharmPackOpening.State.OPENING;
			this.charmOptionCard.selectionOutline.gameObject.SetActive(false);
			switch (charmType)
			{
			case CharmType.Attack:
				this.PlayOpeningAnimation(PanelCharmPackOpening.SkinNameRed);
				this.packParticleSpineSkeleton.Skeleton.SetSkin(PanelCharmPackOpening.SkinNameRed);
				break;
			case CharmType.Defense:
				this.PlayOpeningAnimation(PanelCharmPackOpening.SkinNameBlue);
				this.packParticleSpineSkeleton.Skeleton.SetSkin(PanelCharmPackOpening.SkinNameBlue);
				break;
			case CharmType.Utility:
				this.PlayOpeningAnimation(PanelCharmPackOpening.SkinNameGreen);
				this.packParticleSpineSkeleton.Skeleton.SetSkin(PanelCharmPackOpening.SkinNameRed);
				break;
			}
			this.currentSeq = DOTween.Sequence();
			this.currentSeq.Append(DOTween.TimerTween(this.animOpen.duration).OnComplete(delegate
			{
				this.currentIdleTime = 0f;
				this.state = PanelCharmPackOpening.State.IDLE;
			}));
			this.currentSeq.Play<Sequence>();
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiCharmPackOpening, 1f));
		}

		public void DoStep()
		{
			switch (this.state)
			{
			case PanelCharmPackOpening.State.OPENING:
			case PanelCharmPackOpening.State.COUNTING:
			case PanelCharmPackOpening.State.SHOWING:
				this.tweenSpeed = 3f;
				break;
			case PanelCharmPackOpening.State.IDLE:
				if (this.phase < 3)
				{
					this.state = PanelCharmPackOpening.State.COUNTING;
					this.currentSeq = DOTween.Sequence();
					int endValue = this.consumedDupplicateToCount + this.ConsumeDuplicate();
					this.duplicateCounterText.gameObject.SetActive(true);
					this.duplicateCounterText.text = "x" + this.consumedDupplicateToCount;
					if (this.phase == 3)
					{
						this.PlayLastDistributeAnimation();
					}
					else
					{
						this.PlayDistributeAnimation();
					}
					this.currentSeq.Append(this.duplicateCounterText.DOFade(1f, 0.4f)).AppendCallback(delegate
					{
						this.charmOptionCard.gameObject.SetActive(true);
					}).Append(DOTween.To(() => this.consumedDupplicateToCount, delegate(int x)
					{
						this.consumedDupplicateToCount = x;
					}, endValue, 0.3f).OnUpdate(new TweenCallback(this.SetCounterText))).Append(this.duplicateCounterText.transform.DOPunchScale(Vector3.one * 0.3f, 0.4f, 3, 1f)).OnComplete(delegate
					{
						if (this.phase == 3)
						{
							this.state = PanelCharmPackOpening.State.IDLE;
							this.DoStep();
						}
						else
						{
							this.state = PanelCharmPackOpening.State.IDLE;
						}
						this.currentIdleTime = 0f;
					});
					this.currentSeq.Play<Sequence>();
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiCharmPackDrop, 1f));
				}
				else
				{
					this.state = PanelCharmPackOpening.State.SHOWING;
					this.currentSeq = DOTween.Sequence();
					this.currentSeq.AppendCallback(new TweenCallback(this.PlayRevealCardSound)).Append(this.charmOptionCard.charmCard.transform.DORotate(new Vector3(0f, 90f, 0f), 0.2f, DG.Tweening.RotateMode.Fast)).Join(this.duplicateCounterText.DOFade(0f, 0.2f)).AppendCallback(delegate
					{
						this.charmOptionCard.charmCard.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
						this.charmOptionCard.charmCard.charmIcon.gameObject.SetActive(true);
						this.isCardBackActive = false;
					}).Append(this.charmOptionCard.charmCard.transform.DORotate(new Vector3(0f, 0f, 0f), 0.2f, DG.Tweening.RotateMode.Fast)).AppendCallback(delegate
					{
						this.charmOptionCard.charmCardInfo.gameObject.SetActive(true);
						this.charmOptionCard.charmCard.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
					}).Append(this.charmOptionCard.rectTransform.DOSizeDelta(new Vector2(720f, 200f), 0.4f, false).SetEase(Ease.InOutQuad)).Join(this.charmOptionCard.rectTransform.DOAnchorPosX(-360f, 0.4f, false)).AppendCallback(delegate
					{
						this.charmOptionCard.charmCard.levelProgresBarParent.gameObject.SetActive(true);
					}).Append(this.charmOptionCard.charmCard.levelProgresBarParent.DOAnchorPosY(-9.8f, 0.2f, false).SetEase(Ease.OutQuint)).AppendCallback(delegate
					{
						this.charmOptionCard.charmCard.copyCounterParent.gameObject.SetActive(true);
					}).Append(this.charmOptionCard.charmCard.copyCounterParent.rectTransform.DOSizeDelta(new Vector2(220f, 65f), 0.2f, false).SetEase(Ease.OutBack)).AppendInterval(0.1f).Append(DOTween.To(() => this.oldDupplicateCount, delegate(int x)
					{
						this.oldDupplicateCount = x;
					}, this.targetDupplicateCount, 0.4f).SetEase(Ease.Linear)).OnUpdate(new TweenCallback(this.SetSliderAndText)).OnComplete(delegate
					{
						this.state = PanelCharmPackOpening.State.DONE;
					});
					this.currentSeq.Play<Sequence>();
				}
				break;
			}
		}

		private void SetCounterText()
		{
			this.duplicateCounterText.text = "x" + this.consumedDupplicateToCount;
		}

		private int ConsumeDuplicate()
		{
			int num = this.charmDupplicateToOpen.counts[this.phase];
			this.phase++;
			this.remainingDupplicateToCount -= num;
			return num;
		}

		public void DoShowCard(CharmType charmType)
		{
			switch (charmType)
			{
			case CharmType.Attack:
				this.PlayOpeningAnimation(PanelCharmPackOpening.SkinNameRed);
				this.packParticleSpineSkeleton.Skeleton.SetSkin(PanelCharmPackOpening.SkinNameRed);
				break;
			case CharmType.Defense:
				this.PlayOpeningAnimation(PanelCharmPackOpening.SkinNameBlue);
				this.packParticleSpineSkeleton.Skeleton.SetSkin(PanelCharmPackOpening.SkinNameBlue);
				break;
			case CharmType.Utility:
				this.PlayOpeningAnimation(PanelCharmPackOpening.SkinNameGreen);
				this.packParticleSpineSkeleton.Skeleton.SetSkin(PanelCharmPackOpening.SkinNameRed);
				break;
			}
			this.isAnimating = true;
			DOTween.TimerTween(this.animOpen.duration * 0.75f).OnComplete(delegate
			{
				this.packParticleSpineSkeleton.gameObject.SetActive(true);
			});
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(this.animCard.duration + this.animOpen.duration).AppendCallback(delegate
			{
				this.charmOptionCard.gameObject.SetActive(true);
			}).Append(this.charmOptionCard.charmCard.transform.DORotate(new Vector3(0f, 90f, 0f), 0.2f, DG.Tweening.RotateMode.Fast)).AppendCallback(delegate
			{
				this.charmOptionCard.charmCard.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
				this.charmOptionCard.charmCard.charmIcon.gameObject.SetActive(true);
				this.isCardBackActive = false;
			}).Append(this.charmOptionCard.charmCard.transform.DORotate(new Vector3(0f, 0f, 0f), 0.2f, DG.Tweening.RotateMode.Fast)).AppendCallback(delegate
			{
				this.charmOptionCard.charmCardInfo.gameObject.SetActive(true);
			}).Append(this.charmOptionCard.rectTransform.DOSizeDelta(new Vector2(650f, 200f), 0.4f, false).SetEase(Ease.InOutQuad)).AppendCallback(delegate
			{
				this.charmOptionCard.charmCard.levelProgresBarParent.gameObject.SetActive(true);
			}).Append(this.charmOptionCard.charmCard.levelProgresBarParent.DOAnchorPosY(-9.8f, 0.2f, false).SetEase(Ease.OutQuint)).AppendCallback(delegate
			{
				this.charmOptionCard.charmCard.copyCounterParent.gameObject.SetActive(true);
			}).Append(this.charmOptionCard.charmCard.copyCounterParent.rectTransform.DOSizeDelta(new Vector2(220f, 65f), 0.2f, false).SetEase(Ease.OutBack)).AppendInterval(0.1f).Append(DOTween.To(() => this.oldDupplicateCount, delegate(int x)
			{
				this.oldDupplicateCount = x;
			}, this.targetDupplicateCount, 0.4f).SetEase(Ease.Linear)).OnUpdate(new TweenCallback(this.SetSliderAndText)).OnComplete(delegate
			{
				this.isAnimating = false;
			});
			sequence.Play<Sequence>();
		}

		private void PlayRevealCardSound()
		{
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackItem[2], 1f));
		}

		private int GetTargetCount()
		{
			return ((this.oldLevel != -1) ? this.charmDupplicateToOpen.count : (this.charmDupplicateToOpen.count - 1)) + this.oldDupplicateCount;
		}

		private int GetRemainingCount()
		{
			return this.targetDupplicateCount - this.oldDupplicateCount;
		}

		private void SetSliderAndText()
		{
			this.charmOptionCard.charmCard.levelProgresBar.SetScale(GameMath.Clamp((float)this.oldDupplicateCount / (float)this.neededCount, 0f, 1f));
			this.charmOptionCard.charmCard.levelProgresBarText.text = this.oldDupplicateCount + "/" + this.neededCount;
		}

		private void PlayOpeningAnimation(string skinName)
		{
			this.packSpineSkeleton.Skeleton.SetSkin(skinName);
			this.packSpineSkeleton.AnimationState.SetAnimation(0, this.animOpen, false);
			this.packSpineSkeleton.AnimationState.AddAnimation(0, this.animShake, true, 0f);
		}

		private void PlayDistributeAnimation()
		{
			this.packSpineSkeleton.AnimationState.SetAnimation(0, this.animCard, false);
			this.packSpineSkeleton.AnimationState.AddAnimation(0, this.animShake, true, 0f);
		}

		private void PlayLastDistributeAnimation()
		{
			this.packSpineSkeleton.AnimationState.SetAnimation(0, this.animCardLast, false);
			this.packSpineSkeleton.AnimationState.AddAnimation(0, this.animEmptyIdle, true, 0f);
		}

		public void SetMultipleHidden()
		{
			this.duplicateCounterText.gameObject.SetActive(false);
			this.packParticleSpineSkeleton.gameObject.SetActive(false);
			this.charmOptionCard.gameObject.SetActive(false);
		}

		[InspectButton(visibityMode = InspectButtonAttribute.VisibityMode.PlayModeOnly)]
		public void DoMultiPackOpen()
		{
			this.state = PanelCharmPackOpening.State.OPENING;
			Main.coroutineObject.StartCoroutine(this.DoMultiPackOpenCo());
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiCharmPackOpening, 1f));
		}

		public void DisposeMultiPack()
		{
			foreach (CharmCard charmCard in this.multipleCards)
			{
				charmCard.gameObject.SetActive(false);
			}
		}

		private Vector2 GetPackCardPosition(int index)
		{
			int num = index % 3;
			int num2 = index / 3;
			return new Vector2(-196f + (float)num * 196f, 350f - (float)num2 * 245f);
		}

		private IEnumerator DoMultiPackOpenCo()
		{
			this.tweenSpeed = 1f;
			this.packSpineSkeleton.Skeleton.SetSkin(PanelCharmPackOpening.SkinNameSpecial);
			this.packSpineSkeleton.AnimationState.SetAnimation(0, this.animOpenSpecial, false);
			this.packSpineSkeleton.Update(0.1f);
			this.packSpineSkeleton.timeScale = this.tweenSpeed;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			if (this.multipleCards.Count != 9)
			{
				yield return this.LoadCharmCards(9);
			}
			stopwatch.Stop();
			float interval = 1.895f - (float)stopwatch.Elapsed.TotalSeconds;
			this.currentSeq = DOTween.Sequence();
			this.currentSeq.AppendInterval(interval);
			this.currentSeq.AppendCallback(new TweenCallback(this.CardDistributionAnim));
			this.currentSeq.timeScale = this.tweenSpeed;
			this.currentSeq.Play<Sequence>();
			yield break;
		}

		private void CardDistributionAnim()
		{
			Sequence sequence = DOTween.Sequence();
			for (int i = 0; i < this.multipleCards.Count; i++)
			{
				CharmCard c = this.multipleCards[this.multipleCards.Count - i - 1];
				c.gameObject.SetActive(false);
				c.copyCounterParent.gameObject.SetActive(false);
				c.copyCounterParent.rectTransform.SetSizeDeltaX(0f);
				c.copyCounterText.rectTransform.localScale = Vector3.zero;
				c.rectTransform.anchoredPosition = new Vector2(0f, -450f) + UnityEngine.Random.insideUnitCircle * 80f;
				c.button.interactable = false;
				this.flipStates[i] = false;
				c.SetCardBack(UiData.inst.spriteCharmBack);
				Vector2 packCardPosition = this.GetPackCardPosition(i);
				Vector2 vector = packCardPosition - c.rectTransform.anchoredPosition;
				float z = -Mathf.Atan2(vector.x, vector.y) * 57.29578f;
				float num = 0.15f;
				float num2 = num / 2f;
				float num3 = 0.4f;
				sequence.InsertCallback(0f, delegate
				{
					c.gameObject.SetActive(true);
				});
				sequence.Insert(0f, c.rectTransform.DOAnchorPos(new Vector2(0f, -450f) + UnityEngine.Random.insideUnitCircle * 180f, num3, false).SetEase(Ease.OutCubic));
				sequence.Insert((float)i * 0.03f + num3, c.rectTransform.DOAnchorPos(this.GetPackCardPosition(i), num, false).SetEase(Ease.OutCubic));
				sequence.Insert((float)i * 0.03f + num3, c.rectTransform.DORotate(new Vector3(0f, 0f, z), num2, DG.Tweening.RotateMode.Fast).SetEase(Ease.OutCubic));
				sequence.Insert((float)i * 0.03f + num2 + num3, c.rectTransform.DORotate(new Vector3(0f, 0f, 0f), num2, DG.Tweening.RotateMode.Fast).SetEase(Ease.OutCubic));
				sequence.AppendCallback(delegate
				{
					c.button.interactable = true;
				});
			}
			sequence.InsertCallback(0.4f, delegate
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiCharmPackDrop, 1f));
			});
			sequence.Play<Sequence>();
		}

		private IEnumerator LoadCharmCards(int count)
		{
			this.flipStates = new List<bool>();
			for (int i = 0; i < count; i++)
			{
				GameObject parent = new GameObject("CardCanvas" + i);
				Canvas canvas = parent.AddComponent<Canvas>();
				parent.AddComponent<GraphicRaycaster>();
				RectTransform parentTransform = parent.transform as RectTransform;
				parentTransform.SetParent(this.multipleCharmPackParent);
				parentTransform.anchorMax = Vector2.one;
				parentTransform.anchorMin = Vector2.zero;
				parentTransform.offsetMax = Vector2.zero;
				parentTransform.offsetMin = Vector2.zero;
				parentTransform.localScale = Vector3.one;
				CharmCard c = UnityEngine.Object.Instantiate<CharmCard>(this.charmOptionCard.charmCard, parentTransform);
				c.rectTransform.rotation = Quaternion.identity;
				c.rectTransform.localScale = Vector3.one;
				c.rectTransform.anchorMax = Vector2.one * 0.5f;
				c.rectTransform.anchorMin = Vector2.one * 0.5f;
				c.rectTransform.anchoredPosition = new Vector2(0f, -450f) + UnityEngine.Random.insideUnitCircle * 80f;
				c.gameObject.SetActive(false);
				c.InitButton();
				CharmCard charmCard = c;
				charmCard.onClicked = (Action<int>)Delegate.Combine(charmCard.onClicked, new Action<int>(this.Button_OnCharmClickedToFlip));
				c.SetCardBack(UiData.inst.spriteCharmBack);
				c.charmId = i;
				c.copyCounterParent.rectTransform.SetSizeDeltaX(0f);
				c.copyCounterText.rectTransform.SetAnchorPosX(0f);
				c.copyCounterText.rectTransform.localScale = Vector3.zero;
				c.copyCounterParent.rectTransform.SetAsLastSibling();
				c.copyCounterParent.rectTransform.pivot = new Vector2(0f, 0.5f);
				c.copyCounterParent.rectTransform.SetAnchorPosX(-98f);
				c.copyCounterParent.rectTransform.SetAnchorPosY(-93f);
				this.flipStates.Add(false);
				this.multipleCards.Add(c);
				yield return null;
				canvas.overrideSorting = true;
				canvas.sortingOrder = 1 + i;
			}
			yield break;
		}

		private void Button_OnCharmClickedToFlip(int index)
		{
			bool flag = this.flipStates[index];
			if (flag)
			{
				if (this.flipStates.All((bool x) => x))
				{
					if (this.uiManager.panelShop.isHubMode)
					{
						this.uiManager.state = UiState.HUB_SHOP;
					}
					else
					{
						this.uiManager.state = UiState.SHOP;
					}
					this.DisposeMultiPack();
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuBack, 1f));
				}
			}
			else
			{
				CharmCard c = this.multipleCards[index];
				CharmDuplicate duplicate = this.charmDupplicatesToOpen[index];
				this.flipStates[index] = true;
				Sequence sequence = DOTween.Sequence();
				sequence.Append(c.rectTransform.DORotate(new Vector3(0f, 90f, 0f), 0.2f, DG.Tweening.RotateMode.Fast).SetEase(Ease.InQuart));
				sequence.AppendCallback(delegate
				{
					c.rectTransform.localRotation = Quaternion.Euler(0f, -90f, 0f);
					c.levelParent.gameObject.SetActive(true);
					c.charmIcon.gameObject.SetActive(true);
					c.copyCounterParent.gameObject.SetActive(true);
					c.copyCounterText.text = "x0";
					this.uiManager.UpdateSoloEnchantmentCard(c, duplicate.charmData, true);
					if (duplicate.charmData.isNew)
					{
						c.newLabelParent.gameObject.SetActive(true);
						c.levelParent.gameObject.SetActive(false);
					}
					else
					{
						c.newLabelParent.gameObject.SetActive(false);
						c.levelParent.gameObject.SetActive(true);
					}
				});
				sequence.Append(c.rectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.4f, DG.Tweening.RotateMode.Fast).SetEase(Ease.OutElastic));
				sequence.Insert(0.25f, c.copyCounterParent.rectTransform.DOSizeDeltaX(120f, 0.2f, false));
				sequence.Insert(0.25f, c.copyCounterText.rectTransform.DOScale(1f, 0.2f));
				int counter = 0;
				sequence.Insert(0.4f, DOTween.To(() => counter, delegate(int x)
				{
					counter = x;
				}, duplicate.count, 0.4f).OnUpdate(delegate
				{
					c.copyCounterText.text = "x" + counter;
				}));
				sequence.OnComplete(delegate
				{
					if (this.flipStates.All((bool x) => x))
					{
						this.state = PanelCharmPackOpening.State.DONE;
					}
				});
				sequence.Play<Sequence>();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiLootpackItem[2], 1f));
			}
		}

		private Spine.Animation animCard;

		private Spine.Animation animCardLast;

		private Spine.Animation animOpen;

		private Spine.Animation animOpenSpecial;

		private Spine.Animation animShake;

		private Spine.Animation animEmptyIdle;

		private static string SkinNameRed = "gold";

		private static string SkinNameGreen = "green";

		private static string SkinNameBlue = "blue";

		private static string SkinNameSpecial = "special";

		public CharmDuplicate charmDupplicateToOpen;

		public int oldDupplicateCount;

		public int neededCount;

		public int oldLevel;

		public CharmOptionCard charmOptionCard;

		public Text duplicateCounterText;

		public List<CharmCard> multipleCards;

		public List<CharmDuplicate> charmDupplicatesToOpen;

		public List<bool> flipStates;

		public List<int> oldDupplicateCounts;

		public List<int> neededCounts;

		public List<int> oldLevels;

		public SkeletonGraphic packSpineSkeleton;

		public SkeletonGraphic packParticleSpineSkeleton;

		private int targetDupplicateCount;

		public RectTransform multipleCharmPackParent;

		public GameButton skipButton;

		public PanelCharmPackOpening.State state;

		public bool isCardBackActive;

		public bool isAnimating;

		private int remainingDupplicateToCount;

		private int consumedDupplicateToCount;

		private int phase;

		private Sequence currentSeq;

		private float tweenSpeed;

		private float currentIdleTime;

		public UiManager uiManager;

		public bool isBigPack;

		public enum State
		{
			OPENING,
			IDLE,
			COUNTING,
			SHOWING,
			DONE
		}
	}
}
