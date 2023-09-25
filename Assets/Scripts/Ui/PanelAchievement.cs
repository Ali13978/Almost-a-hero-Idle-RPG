using System;
using System.Collections.Generic;
using Simulation;
using Static;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelAchievement : AahMonoBehaviour
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

		public void InitStrings()
		{
			this.buttonCollect.text.text = LM.Get("UI_COLLECT");
		}

		public void SetState(Simulator sim, PanelAchievements pa)
		{
			int num = 0;
			int count = this.achievementIds.Count;
			this.achievementIndex = this.achievementIds.Count - 1;
			string text = this.achievementIds[this.achievementIndex];
			string id = this.achievementIds[this.achievementIndex];
			string text2 = this.achievementIds[this.achievementIndex];
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.achievementIds.Count; i++)
			{
				flag = PlayerStats.achievements[this.panelIndex][this.achievementIds[i]];
				flag2 = Simulator.achievementCollecteds[this.achievementIds[i]];
				if (!flag || !flag2)
				{
					if (num > 0)
					{
						id = this.achievementIds[GameMath.GetMinInt(this.achievementIds.Count - 1, i)];
					}
					else
					{
						id = this.achievementIds[GameMath.GetMaxInt(0, i - 1)];
					}
					text = this.achievementIds[i];
					this.achievementIndex = i;
					break;
				}
				num++;
			}
			this.textTitle.text = UiManager.GetAchievementTitle(id);
			this.textRewardAmount.text = "+" + GameMath.GetDoubleString(Simulator.achievementRewards[text]);
			this.imageIcon.sprite = this.spritesIcon[this.achievementIndex];
			this.stars.SetNumberOfStars(num, count);
			if (num == 0)
			{
				this.imageIcon.color = new Color(1f, 1f, 1f, 0.5f);
			}
			else
			{
				this.imageIcon.color = new Color(1f, 1f, 1f, 1f);
			}
			this.iconBackColorImage.sprite = pa.achievementBackgrounds[num];
			if (flag && flag2)
			{
				this.textTitle.color = this.colorTextTitleUnlocked;
				this.textDesc.gameObject.SetActive(true);
				this.textDesc.color = this.colorTextDescUnlocked;
				this.textDesc.text = UiManager.GetAchievementDesc(text);
				this.imageBg.sprite = this.spriteBgLocked;
				this.buttonCollect.gameObject.SetActive(!flag2);
				this.imageCheck.gameObject.SetActive(flag2);
				this.rewardBg.gameObject.SetActive(!flag2);
				this.buttonCollect.interactable = true;
				this.barProgress.gameObject.SetActive(false);
			}
			else
			{
				this.textTitle.color = this.colorTextTitleLocked;
				this.textDesc.color = this.colorTextDescLocked;
				this.imageBg.sprite = this.spriteBgUnlocked;
				this.imageCheck.gameObject.SetActive(false);
				this.barProgress.gameObject.SetActive(true);
				this.rewardBg.gameObject.SetActive(true);
				this.barProgress.SetScale(GameMath.GetMinFloat(1f, PlayerStats.GetAchievementProgress(text, sim)));
				this.textDesc.gameObject.SetActive(false);
				this.textDescBar.text = UiManager.GetAchievementDesc(text);
				this.buttonCollect.gameObject.SetActive(true);
				this.buttonCollect.interactable = flag;
			}
		}

		public int panelIndex;

		public int achievementIndex;

		public List<string> achievementIds;

		public List<Sprite> spritesIcon;

		public Image imageBg;

		public Image imageIcon;

		public Image iconBackColorImage;

		public Text textTitle;

		public Text textDesc;

		public Text textDescBar;

		public Text textRewardAmount;

		public Image rewardBg;

		public GameButton buttonCollect;

		public Image imageCheck;

		public Sprite spriteBgUnlocked;

		public Sprite spriteBgLocked;

		public Color colorTextTitleUnlocked;

		public Color colorTextTitleLocked;

		public Color colorTextDescUnlocked;

		public Color colorTextDescLocked;

		public EvolveStars stars;

		public Scaler barProgress;

		private RectTransform m_rectTransform;
	}
}
