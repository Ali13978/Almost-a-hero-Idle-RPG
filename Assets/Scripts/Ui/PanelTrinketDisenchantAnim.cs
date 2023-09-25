using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
	public class PanelTrinketDisenchantAnim : MonoBehaviour
	{
		public void DoDisenchant(TrinketUi trinketUi, Vector3 invPos)
		{
			base.gameObject.SetActive(true);
			this.background.SetAlpha(0.8f);
			trinketUi.transform.SetParent(this.background.transform);
			trinketUi.transform.localPosition = Vector3.zero;
			trinketUi.transform.localScale = Vector3.one * 1.3f;
			CanvasGroup canvasGroup = trinketUi.gameObject.AddComponent<CanvasGroup>();
			canvasGroup.alpha = 0f;
			trinketUi.imageBody.transform.SetParent(trinketUi.imageBg.transform);
			trinketUi.icons[0].transform.SetParent(trinketUi.imageBg.transform);
			trinketUi.icons[1].transform.SetParent(trinketUi.imageBg.transform);
			Trinket simTrinket = trinketUi.simTrinket;
			List<int> list = new List<int>();
			int? num = null;
			int? num2 = null;
			int num3 = 2;
			foreach (TrinketEffect trinketEffect in simTrinket.effects)
			{
				if (trinketEffect.GetGroup() == TrinketEffectGroup.COMMON)
				{
					int num4 = trinketEffect.GetSprites().Length;
					for (int i = 0; i < num4; i++)
					{
						list.Add(num3);
						num3++;
					}
				}
				else if (trinketEffect.GetGroup() == TrinketEffectGroup.SECONDARY)
				{
					num = new int?(num3);
					num3++;
				}
				else if (trinketEffect.GetGroup() == TrinketEffectGroup.SPECIAL)
				{
					num2 = new int?(num3);
					num3++;
				}
			}
			this.sequence = DOTween.Sequence();
			float num5 = 0f;
			float num6 = 0.05f;
			List<Image> list2 = new List<Image>();
			this.sequence.Append(canvasGroup.DOFade(1f, 0.3f));
			this.sequence.Append(trinketUi.transform.DOShakePosition(0.6f, new Vector3(10f, 10f, 0f), 20, 90f, false, false));
			this.sequence.Join(trinketUi.transform.DOShakeRotation(0.5f, new Vector3(0f, 0f, 15f), 20, 90f, false));
			this.sequence.Append(trinketUi.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.2f, 10, 1f).OnComplete(delegate
			{
				SoundEventUiSimple item = new SoundEventUiSimple(SoundArchieve.inst.uiFillAeonDustBarComplete, 1f);
				UiManager.sounds.Add(item);
			}));
			num5 += 0.9f;
			for (int j = 0; j < list.Count; j++)
			{
				Image image = trinketUi.icons[list[j]];
				float x = 0f;
				if (list.Count == 2)
				{
					if (j == 0)
					{
						x = -40f - UnityEngine.Random.value * 15f;
					}
					else if (j == 1)
					{
						x = 40f + UnityEngine.Random.value * 15f;
					}
				}
				else if (j == 1)
				{
					x = -40f - UnityEngine.Random.value * 15f;
				}
				else if (j == 2)
				{
					x = 40f + UnityEngine.Random.value * 15f;
				}
				this.sequence.Insert(num5, image.rectTransform.DOAnchorPos(new Vector2(x, -45f - UnityEngine.Random.value * 5f), 0.2f, false).SetEase(Ease.OutQuart).SetRelative<Tweener>());
				this.sequence.Insert(num5, image.rectTransform.DORotate(new Vector3(0f, 0f, 90f - UnityEngine.Random.value * 180f), 0.2f, RotateMode.Fast).SetEase(Ease.OutQuart));
				num5 += num6;
				list2.Add(image);
			}
			if (num != null)
			{
				Image image2 = trinketUi.icons[num.Value];
				this.sequence.Insert(num5, image2.rectTransform.DOAnchorPosY(45f + UnityEngine.Random.value * 5f, 0.2f, false).SetEase(Ease.OutQuart).SetRelative<Tweener>());
				this.sequence.Insert(num5, image2.rectTransform.DORotate(new Vector3(0f, 0f, 90f - UnityEngine.Random.value * 180f), 0.2f, RotateMode.Fast).SetEase(Ease.OutQuart));
				num5 += num6;
				list2.Add(image2);
			}
			if (num2 != null)
			{
				Image image3 = trinketUi.icons[num2.Value];
				this.sequence.Insert(num5, image3.transform.DOScale(1.5f, 0.1f));
				num5 += num6;
				list2.Add(image3);
			}
			this.sequence.Insert(num5, trinketUi.imageBg.transform.DOScale(0f, 0.2f));
			this.sequence.AppendInterval(0.3f);
			this.sequence.Append(this.background.DOFade(0f, 0.1f));
			num5 = this.sequence.Duration(true);
			for (int k = 0; k < list2.Count; k++)
			{
				Image image4 = list2[k];
				this.sequence.Insert(num5, image4.rectTransform.DOMove(invPos, 0.2f, false));
				num5 += num6;
			}
			this.sequence.AppendCallback(delegate
			{
				this.gameObject.SetActive(false);
			});
			this.sequence.OnComplete(delegate
			{
				UnityEngine.Object.Destroy(trinketUi.gameObject);
			});
			this.sequence.Play<Sequence>();
		}

		private void Awake()
		{
			this.backgroundButton.onClick.AddListener(new UnityAction(this.OnClick));
		}

		private void OnClick()
		{
			this.sequence.timeScale = 3f;
		}

		public Image background;

		public Button backgroundButton;

		private Sequence sequence;
	}
}
