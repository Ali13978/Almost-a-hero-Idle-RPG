using System;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class CharmEffectWidget : MonoBehaviour, IMatchRemovable
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

		public bool willDispose { get; set; }

		public void SetFill(float value)
		{
			this.fillIcon.fillAmount = value;
		}

		public void SetIcon(Sprite sprite)
		{
			this.baseIcon.sprite = sprite;
			this.fillIcon.sprite = sprite;
		}

		public void SetRectSize(Vector2 size)
		{
			this.baseIcon.rectTransform.sizeDelta = size;
			this.fillIcon.rectTransform.sizeDelta = size;
		}

		public void EnableEmitter()
		{
			ParticleSystem.EmissionModule emission = this.ps.emission;
			if (emission.enabled)
			{
				return;
			}
			emission.enabled = true;
		}

		public void DisableEmitter()
		{
			ParticleSystem.EmissionModule emission = this.ps.emission;
			if (!emission.enabled)
			{
				return;
			}
			emission.enabled = false;
		}

		public void SimulateParticle(float time)
		{
			this.ps.Simulate(time, false);
		}

		[InspectButton(visibityMode = InspectButtonAttribute.VisibityMode.PlayModeOnly)]
		public void DoCurseInAnimation(Vector2 targetPosition, TweenCallback onComplete, CurseEffectData curse)
		{
			Sequence sequence = DOTween.Sequence();
			this.DisableEmitter();
			this.isAnimating = true;
			this.glowImage.color = ((!(curse is CurseEffectGhostlyHeroes)) ? CharmEffectWidget.curseBuffColor : CharmEffectWidget.curseBuffColorGhostlyHeroes);
			this.glowImage.gameObject.SetActive(true);
			this.glowImage.SetAlpha(0f);
			this.rectTransform.SetScale(0.75f);
			this.rectTransform.anchoredPosition = new Vector2(150f, 200f);
			DOTween.defaultEaseType = Ease.InCirc;
			sequence.Append(this.rectTransform.DOAnchorPosX(-372.5f, 0.3f, false).SetEase(Ease.OutBack)).Join(this.rectTransform.DOScale(1.5f, 0.3f)).Append(this.glowImage.DOFade(1f, 0.3f).SetEase(Ease.OutCubic)).Join(this.glowImage.rectTransform.DOScale(1.4f, 0.2f).SetEase(Ease.OutCubic)).Append(this.glowImage.rectTransform.DOScale(1f, 0.1f).SetEase(Ease.OutCubic)).Append(this.rectTransform.DOAnchorPos(targetPosition, 0.3f, false)).Join(this.rectTransform.DOScale(0.75f, 0.3f)).Append(this.rectTransform.DOShakeAnchorPos(0.3f, 5f, 30, 90f, false, true)).AppendCallback(delegate
			{
				this.isAnimating = false;
			}).AppendCallback(delegate
			{
				this.doSelfPosCheck = true;
			}).AppendCallback(onComplete);
			sequence.Play<Sequence>();
			DOTween.defaultEaseType = Ease.Linear;
		}

		[InspectButton(visibityMode = InspectButtonAttribute.VisibityMode.PlayModeOnly)]
		public void DoCurseOutAnimation(Action onComplete, bool challengeActive)
		{
			this.EnableEmitter();
			Sequence sequence = DOTween.Sequence();
			DOTween.defaultEaseType = Ease.InCirc;
			sequence.AppendInterval(0.4f).Append(this.fillIcon.rectTransform.DOScale(0f, 0.2f).SetEase(Ease.InBack)).Join(this.baseIcon.rectTransform.DOScale(0f, 0.2f).SetEase(Ease.InBack)).AppendCallback(delegate
			{
				this.isAnimating = false;
			}).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(this.gameObject);
				onComplete();
			});
			sequence.Play<Sequence>();
			DOTween.defaultEaseType = Ease.Linear;
			if (challengeActive)
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.curseDispel, 1f));
			}
		}

		public void DisableAllRaycast()
		{
			this.glowImage.raycastTarget = false;
			this.baseIcon.raycastTarget = false;
			this.fillIcon.raycastTarget = false;
			this.textLevel.raycastTarget = false;
		}

		[InspectButton]
		public void DoCurseLevelUpAnimation(int lvl, bool isFromZero, CurseBuff curse)
		{
			if (this.levelChangeAnim != null && this.levelChangeAnim.IsPlaying())
			{
				this.levelChangeAnim.Complete(true);
			}
			this.isAnimating = true;
			this.textLevel.text = lvl.ToString();
			this.levelChangeAnim = DOTween.Sequence();
			this.levelChangeAnim.Append(this.baseIcon.transform.DOScale(1.3f, 0.8f).SetEase(EaseFactory.Kick(Ease.InSine, Ease.InBack)));
			this.levelChangeAnim.Join(this.fillIcon.transform.DOScale(1.3f, 0.8f).SetEase(EaseFactory.Kick(Ease.InSine, Ease.InBack)));
			if (isFromZero)
			{
				this.levelChangeAnim.Join(this.baseIcon.DOColor((!(curse is CurseBuffGhostlyHeroes)) ? CharmEffectWidget.curseBuffColorB : CharmEffectWidget.curseBuffColorGhostlyHeroesB, 0.8f));
			}
			this.levelChangeAnim.OnComplete(delegate
			{
				this.isAnimating = false;
			}).Play<Sequence>();
		}

		[InspectButton]
		public void DoCurseLevelDownAnimation(int lvl, bool grayout)
		{
			if (this.levelChangeAnim != null && this.levelChangeAnim.IsPlaying())
			{
				this.levelChangeAnim.Complete(true);
			}
			this.isAnimating = true;
			this.SetFill(0f);
			this.levelChangeAnim = DOTween.Sequence();
			this.levelChangeAnim.Append(this.baseIcon.transform.DOShakePosition(0.4f, new Vector3(5f, 5f, 0f), 20, 90f, false, false));
			if (grayout)
			{
				this.levelChangeAnim.Join(this.baseIcon.DOColor(new Color(1f, 1f, 1f, 0.3f), 0.8f));
			}
			this.levelChangeAnim.InsertCallback(0.2f, delegate
			{
				this.textLevel.text = lvl.ToString();
			});
			this.levelChangeAnim.OnComplete(delegate
			{
				this.isAnimating = false;
			}).Play<Sequence>();
		}

		private RectTransform m_rectTransform;

		public ParticleSystem ps;

		public Image baseIcon;

		public Image fillIcon;

		public Image glowImage;

		public Text textLevel;

		public int lastLevel;

		public bool needInitialization = true;

		public bool isAnimating;

		public int index;

		public static Color charmBuffColor = new Color(0f, 1f, 0.835f);

		public static Color curseBuffColor = Utility.HexColor("EC232B");

		public static Color curseBuffColorB = Utility.HexColor("EC232B80");

		public static Color curseBuffColorGhostlyHeroes = Utility.HexColor("8c4fc1");

		public static Color curseBuffColorGhostlyHeroesB = Utility.HexColor("8c4fc180");

		public bool doSelfPosCheck;

		private Sequence levelChangeAnim;
	}
}
