using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class StageBar : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.nodeTexts = new List<Text>();
			foreach (Image image in this.nodes)
			{
				this.nodeTexts.Add(image.GetComponentInChildren<Text>());
			}
			this.SetBar(0);
			this.nodeJump = 341f;
		}

		public override void AahUpdate(float dt)
		{
			if (this.animTimer > 0f)
			{
				this.animTimer += dt;
				if (this.animTimer > 1f)
				{
					this.animTimer = 0f;
					this.stageNoSaved = this.stageNoTransitioningTo;
					this.SetBar(0, this.stageNoTransitioningTo, 0f);
				}
				else
				{
					this.SetBar(10, this.stageNoTransitioningTo - 1, this.animTimer / 1f);
				}
			}
		}

		public void SetBar(int totWave)
		{
			int totWave2 = totWave - 1;
			int waveNo = ChallengeStandard.GetWaveNo(totWave2);
			int stageNo = ChallengeStandard.GetStageNo(totWave2);
			if (stageNo == this.stageNoSaved)
			{
				this.SetBar(waveNo, stageNo, 0f);
			}
			else if (this.animTimer == 0f)
			{
				this.animTimer = 0.01f;
				this.stageNoTransitioningTo = stageNo;
			}
		}

		private void SetBar(int waveNo, int stageNo, float animRatio = 0f)
		{
			int num = (this.nextUnlock != null) ? this.nextUnlock.GetReqInt() : -1;
			int i = 0;
			int count = this.nodes.Count;
			while (i < count)
			{
				this.SetNode(this.nodes[i], (stageNo + i) * 11, i <= 0, stageNo + i == num);
				i++;
			}
			int j = 0;
			int count2 = this.nodeTexts.Count;
			while (j < count2)
			{
				this.nodeTexts[j].text = (stageNo + j).ToString();
				j++;
			}
			int num2 = 0;
			if (animRatio > 1f)
			{
				animRatio = 1f;
			}
			float num3 = Easing.CubicEaseInOut(animRatio, 0f, -this.nodeJump, 1f);
			int k = 0;
			int count3 = this.nodes.Count;
			while (k < count3)
			{
				this.nodes[k].rectTransform.anchoredPosition = new Vector2(num3, this.nodes[0].rectTransform.anchoredPosition.y);
				num3 += this.nodes[k].rectTransform.sizeDelta.x + this.nodeLineJump;
				int l = num2;
				int num4 = Mathf.Min(this.lines.Count, 10 * (k + 1));
				while (l < num4)
				{
					Sprite sprite = (l >= waveNo) ? this.spriteLine : this.spriteLineActive;
					if (this.lines[l].sprite != sprite)
					{
						this.lines[l].sprite = sprite;
					}
					this.lines[l].rectTransform.anchoredPosition = new Vector2(num3, this.lines[l].rectTransform.anchoredPosition.y);
					num3 += this.lineJump;
					num2++;
					l++;
				}
				num3 += this.nodeLineJump - this.lineJump;
				k++;
			}
		}

		public void SetNode(Image node, int totWave, bool isActive, bool isUnlockStage)
		{
			StageBar.NodeType nodeType = StageBar.NodeType.NORMAL;
			bool flag = ChallengeStandard.IsBossEpicWave(totWave);
			if (flag)
			{
				nodeType = StageBar.NodeType.EPIC_BOSS;
			}
			else if (isUnlockStage)
			{
				nodeType = StageBar.NodeType.UNLOCK;
			}
			Sprite sprite;
			switch (nodeType)
			{
			case StageBar.NodeType.NORMAL:
				sprite = ((!isActive) ? this.spriteNodeNormal : this.spriteNodeNormalActive);
				break;
			case StageBar.NodeType.BOSS:
				sprite = ((!isActive) ? this.spriteNodeBoss : this.spriteNodeBossActive);
				break;
			case StageBar.NodeType.EPIC_BOSS:
				sprite = ((!isActive) ? this.spriteNodeEpicBoss : this.spriteNodeEpicBossActive);
				break;
			case StageBar.NodeType.UNLOCK:
				sprite = ((!isActive) ? this.spriteNodeUnlock : this.spriteNodeUnlockActive);
				break;
			default:
				throw new NotImplementedException();
			}
			if (sprite != node.sprite)
			{
				node.sprite = sprite;
				node.SetNativeSize();
			}
		}

		public Sprite spriteLine;

		public Sprite spriteLineActive;

		public Sprite spriteNodeNormal;

		public Sprite spriteNodeBoss;

		public Sprite spriteNodeEpicBoss;

		public Sprite spriteNodeUnlock;

		public Sprite spriteNodeNormalActive;

		public Sprite spriteNodeBossActive;

		public Sprite spriteNodeEpicBossActive;

		public Sprite spriteNodeUnlockActive;

		public List<Image> nodes;

		private List<Text> nodeTexts;

		public List<Image> lines;

		public float lineJump;

		public float nodeLineJump;

		public float animTimer;

		public const float animPeriod = 1f;

		public int stageNoSaved;

		private int stageNoTransitioningTo;

		public float nodeJump;

		public Unlock nextUnlock;

		public Simulator sim;

		public enum NodeType
		{
			NORMAL,
			BOSS,
			EPIC_BOSS,
			UNLOCK
		}
	}
}
