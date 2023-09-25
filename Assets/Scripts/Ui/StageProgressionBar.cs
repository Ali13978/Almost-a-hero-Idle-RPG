using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class StageProgressionBar : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToUpdates();
			this.stageSkipTextPool = new Pool<Text>(this.skipStageText1, 16, int.MaxValue);
			this.lastShownText = this.stageSkipTextPool.Obtain();
		}

		public override void AahUpdate(float dt)
		{
			int num = this.targetStage - this.currentStageCounter;
			int num2 = this.targetStage - this.fromStage;
			float num3 = ((float)(num2 - num) + this.timer) / (float)num2;
			if (num3 >= 0.5f)
			{
				this.targetSpeed = 0f;
			}
			if (num > 0)
			{
				float value = this.targetSpeed - this.currentSpeed;
				float num4 = Mathf.Clamp(value, -this.accel, this.accel);
				this.currentSpeed += num4 * dt;
				this.timer += this.currentSpeed * dt;
				float f = this.timer / this.durTimer;
				int num5 = Mathf.FloorToInt(f);
				if (num5 > 0)
				{
					this.timer %= this.durTimer;
					this.currentStageCounter += num5;
					if (this.currentStageCounter > this.targetStage)
					{
						this.currentStageCounter = this.targetStage;
					}
					this.SetNodes(this.currentStageCounter, 0, this.bossState);
				}
				if (this.targetStage - this.currentStageCounter == 0)
				{
					this.timer = 0f;
					this.currentSpeed = 0f;
				}
				StageNode stageNode = this.nodes[0];
				StageNode stageNode2 = this.nodes[2];
				stageNode.transform.SetScale(Mathf.Clamp01(-100f * this.timer * this.timer + 1f));
				if (this.timer < 0.9f)
				{
					stageNode2.transform.SetScale(0f);
				}
				else
				{
					stageNode2.transform.SetScale(Mathf.Clamp01(100f * (this.timer - 0.9f) * (this.timer - 0.9f)));
				}
				this.parentRect.SetAnchorPosX(Mathf.Lerp(0f, -339f, this.timer));
			}
			else
			{
				StageNode stageNode3 = this.nodes[0];
				this.timer = 0f;
				stageNode3.transform.SetScale(1f);
			}
		}

		public void DoStageSkipAnim1(int skipCount)
		{
			if (this.isStageSkipIndicatorUp)
			{
				Text o = this.lastShownText;
				this.lastShownText.rectTransform.DOAnchorPosY(20f, 0.3f, false).SetEase(Ease.OutQuint);
				this.lastShownText.DOFade(0f, 0.3f).OnComplete(delegate
				{
					this.stageSkipTextPool.Free(o);
				});
				this.lastShownText = this.stageSkipTextPool.Obtain();
				this.lastShownText.SetAlpha(1f);
				this.lastShownText.rectTransform.SetAnchorPosY(-4.1f);
				this.lastShownText.text = "+" + skipCount.ToString();
				this.lastShownText.rectTransform.SetScale(0f);
				this.lastShownText.rectTransform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
				if (this.trayAnim != null && this.trayAnim.isPlaying && this.trayAnim.Elapsed(true) < 1f)
				{
					this.trayAnim.Goto(0.2f, true);
				}
			}
			else
			{
				this.isStageSkipIndicatorUp = true;
				this.lastShownText.text = "+" + skipCount.ToString();
				this.lastShownText.SetAlpha(1f);
				this.skipStageParent1.gameObject.SetActive(true);
				this.skipStageParent1.SetAnchorPosX(34f);
				this.trayAnim = DOTween.Sequence();
				this.trayAnim.Append(this.skipStageParent1.DOAnchorPosX(-12f, 0.1f, false).SetEase(Ease.OutQuint)).AppendInterval(1f).Append(this.skipStageParent1.DOAnchorPosX(34f, 0.1f, false).SetEase(Ease.OutQuint)).AppendCallback(delegate
				{
					this.skipStageParent1.gameObject.SetActive(false);
					this.isStageSkipIndicatorUp = false;
					this.currentSkipCount = 0;
				}).Play<Sequence>();
			}
		}

		public void DoStageSkipAnim(int skipCount)
		{
			Sequence s = DOTween.Sequence();
			this.currentSkipCount += skipCount;
			this.skipStageText.text = "+" + this.currentSkipCount.ToString();
			if (this.skipCounterJumpAnim != null && this.skipCounterJumpAnim.isPlaying)
			{
				this.skipCounterJumpAnim.Complete();
			}
			this.skipCounterJumpAnim = DOTween.Sequence().Append(this.skipStageText.rectTransform.DOAnchorPosY(4f, 0.1f, false).SetEase(Ease.InSine)).AppendCallback(delegate
			{
				this.skipStageText.rectTransform.SetAnchorPosY(-10f);
			}).AppendInterval(0.05f).Append(this.skipStageText.rectTransform.DOAnchorPosY(-4.1f, 0.1f, false).SetEase(Ease.OutBack)).Play<Sequence>();
			if (!this.isStageSkipIndicatorUp)
			{
				this.isStageSkipIndicatorUp = true;
				this.skipStageParent.gameObject.SetActive(true);
				this.skipStageParent.SetAnchorPosX(34f);
				s.Append(this.skipStageParent.DOAnchorPosX(-12f, 0.1f, false).SetEase(Ease.OutQuint)).AppendInterval(1f).Append(this.skipStageParent.DOAnchorPosX(34f, 0.1f, false).SetEase(Ease.OutQuint)).AppendCallback(delegate
				{
					this.skipStageParent.gameObject.SetActive(false);
					this.isStageSkipIndicatorUp = false;
					this.currentSkipCount = 0;
				}).Play<Sequence>();
			}
		}

		[InspectButton]
		public void SetTargetStage(int target)
		{
			int value = target - this.currentStageCounter;
			float num = this.durTimer / (float)Mathf.Clamp(value, 1, int.MaxValue);
			this.targetStage = target;
			this.fromStage = this.currentStageCounter;
			this.targetSpeed = 20f / num;
			this.accel = this.targetSpeed * 8f;
		}

		[InspectButton]
		public void AddMore(int stages = 3)
		{
			this.SetTargetStage(this.targetStage + stages);
		}

		public void SetBar(int totWave, int nextUnlockStage, StageProgressionBar.BossState bossState)
		{
			int totWave2 = totWave - 1;
			int waveNo = ChallengeStandard.GetWaveNo(totWave2);
			int stageNo = ChallengeStandard.GetStageNo(totWave2);
			if (this.lastStageNo != stageNo || !bossState.Equal(this.bossState))
			{
				if (stageNo <= this.lastStageNo)
				{
					this.currentStageCounter = stageNo;
					this.timer = 0f;
					this.targetStage = stageNo;
					this.fromStage = 0;
					this.SetNodes(stageNo, nextUnlockStage, bossState);
					this.parentRect.SetAnchorPosX(0f);
				}
				else
				{
					this.SetTargetStage(stageNo);
				}
				this.bossState = bossState;
				this.lastStageNo = stageNo;
			}
			for (int i = 0; i < 20; i++)
			{
				Image image = this.lineForegrounds[i];
				if (i <= waveNo)
				{
					if (i == waveNo)
					{
						image.color = this.colorCurrentWave;
					}
					else
					{
						image.color = this.colorPreviousWave;
					}
					if (!image.gameObject.activeSelf)
					{
						image.gameObject.gameObject.SetActive(true);
						image.transform.SetScale(2f);
						image.SetAlpha(0f);
						image.DOFade(1f, 0.1f);
						image.transform.DOScale(1f, 0.2f).SetEase(Ease.InQuart);
					}
				}
				else
				{
					image.gameObject.gameObject.SetActive(false);
				}
			}
		}

		private void SetNodes(int stageNo, int nextUnlockStage, StageProgressionBar.BossState bossState)
		{
			int i = 0;
			int count = this.nodes.Count;
			while (i < count)
			{
				int totWave = (stageNo + i) * 11;
				bool flag = i <= 0;
				bool flag2 = stageNo + i == nextUnlockStage;
				bool flag3 = ChallengeStandard.IsBossEpicWave(totWave);
				StageNode stageNode = this.nodes[i];
				stageNode.text.text = (stageNo + i).ToString();
				if (flag)
				{
					stageNode.SetColorTheme(StageNode.NodeState.Active);
				}
				else if (bossState.isBossWave && i == 1)
				{
					stageNode.SetColorTheme(StageNode.NodeState.Boss);
				}
				else
				{
					stageNode.SetColorTheme(StageNode.NodeState.Inactive);
				}
				if (flag3)
				{
					stageNode.EnableHat(StageNode.HatType.Crown);
				}
				else if (flag2)
				{
					stageNode.DisableHat();
				}
				else
				{
					stageNode.DisableHat();
				}
				i++;
			}
		}

		[InspectButton]
		public void SetPlacement()
		{
			float width = this.rectTransform.rect.width;
			int num = this.lines.Count / 2;
			float num2 = width - this.nodeWidth * 2f - this.nodeLineDist * 2f;
			float num3 = num2 / (float)(num - 1);
			this.nodes[0].rectTransform.SetAnchorPosX(0f);
			this.nodes[1].rectTransform.SetAnchorPosX(this.nodeWidth + this.nodeLineDist * 2f + num2);
			this.nodes[2].rectTransform.SetAnchorPosX(this.nodeLineDist * 2f + num2 + width);
			for (int i = 0; i < num; i++)
			{
				Image image = this.lines[i];
				Image image2 = this.lineForegrounds[i];
				image.rectTransform.SetAnchorPosX(this.nodeWidth + this.nodeLineDist + (float)i * num3);
				image2.rectTransform.SetAnchorPosX(this.nodeWidth + this.nodeLineDist + (float)i * num3);
			}
			for (int j = num; j < num * 2; j++)
			{
				Image image3 = this.lines[j];
				Image image4 = this.lineForegrounds[j];
				int num4 = j - num;
				image3.rectTransform.SetAnchorPosX(width + this.nodeLineDist + (float)num4 * num3);
				image4.rectTransform.SetAnchorPosX(width + this.nodeLineDist + (float)num4 * num3);
			}
		}

		public RectTransform rectTransform;

		public RectTransform parentRect;

		public List<StageNode> nodes;

		public List<Image> lines;

		public List<Image> lineForegrounds;

		public RectTransform skipStageParent;

		public Text skipStageText;

		public RectTransform skipStageParent1;

		public Text skipStageText1;

		public float nodeWidth;

		public float nodeLineDist;

		public Sprite spriteLinePassive;

		public Sprite spriteLineActive;

		private int lastStageNo = int.MinValue;

		private int currentStageCounter = -1;

		private int targetStage;

		private int fromStage;

		private float timer;

		private float animSpeed;

		private float tanimSpeed;

		private float durTimer = 1f;

		public float accel = 10f;

		public float targetSpeedLimit = 10f;

		private float currentSpeed;

		private float targetSpeed;

		private float targetSpeedt;

		[SerializeField]
		private Color colorCurrentWave;

		[SerializeField]
		private Color colorPreviousWave;

		public StageProgressionBar.BossState bossState;

		private bool isStageSkipIndicatorUp;

		private int currentSkipCount;

		private Sequence skipCounterJumpAnim;

		private Pool<Text> stageSkipTextPool;

		private Text lastShownText;

		private Sequence trayAnim;

		public struct BossState
		{
			public bool Equal(StageProgressionBar.BossState other)
			{
				return other.isBossWave == this.isBossWave && other.hasBoss == this.hasBoss && other.isAlive == this.isAlive;
			}

			public bool isBossWave;

			public bool hasBoss;

			public bool isAlive;
		}
	}
}
