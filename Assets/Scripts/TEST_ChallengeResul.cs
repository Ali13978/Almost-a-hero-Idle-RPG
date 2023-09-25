using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TEST_ChallengeResul : MonoBehaviour
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

	private void ResetToStart()
	{
		this.rectTransform.localScale = Vector3.zero;
		this.rectTransform.SetSizeDeltaY(this.startHeight);
		this.paperRollRect.SetSizeDeltaX(this.paperRollStartWidth);
	}

	private void ShowAnimation()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(this.rectTransform.DOScale(1f, 0.2f).SetEase(Ease.OutQuint)).Insert(0.1f, this.paperRollRect.DOSizeDelta(new Vector2(this.paperRollEndWidth, this.paperRollRect.sizeDelta.y), 0.5f, false).SetEase(Ease.OutElastic, 1.7f, 0.4f)).Insert(0.1f, this.headerText.rectTransform.DOScale(1f, 0.5f).SetEase(Ease.OutElastic, 1.7f, 0.4f)).Insert(0.15f, this.rectTransform.DOSizeDelta(new Vector2(this.rectTransform.sizeDelta.x, this.endHeight), 0.3f, false).SetEase(Ease.OutBack));
		sequence.Play<Sequence>();
	}

	private void HideAnimation()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(this.rectTransform.DOSizeDelta(new Vector2(this.rectTransform.sizeDelta.x, this.startHeight), 0.2f, false)).Append(this.paperRollRect.DOSizeDelta(new Vector2(this.paperRollStartWidth, this.paperRollRect.sizeDelta.y), 0.2f, false)).Join(this.headerText.rectTransform.DOScale(0f, 0.2f)).Insert(0.3f, this.rectTransform.DOScale(0f, 0.2f));
		sequence.Play<Sequence>();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.show)
		{
			this.show = false;
			this.ShowAnimation();
		}
		if (this.hide)
		{
			this.hide = false;
			this.HideAnimation();
		}
	}

	private RectTransform m_rectTransform;

	public RectTransform paperRollRect;

	public Text headerText;

	public float startHeight = 60f;

	public float endHeight = 760f;

	public float paperRollStartWidth = 150f;

	public float paperRollEndWidth = 422f;

	public bool hide;

	public bool show;
}
