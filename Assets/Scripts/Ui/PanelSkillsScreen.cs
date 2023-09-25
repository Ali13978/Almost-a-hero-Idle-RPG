using System;
using System.Collections.Generic;
using Simulation;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelSkillsScreen : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.buttonsBranches = new List<List<ButtonSkillUpgrade>>
			{
				this.buttonsBranch1,
				this.buttonsBranch2
			};
			this.pathsBranches = new List<List<SkillScreenPath>>
			{
				this.pathsBranch1,
				this.pathsBranch2
			};
			this.buttonSelectedIllusion.gameButton.onClick = delegate()
			{
				this.ResetSelectedSkill();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOff, 1f));
			};
			this.buttonPanelSkillInfoBg.onClick = delegate()
			{
				this.ResetSelectedSkill();
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOff, 1f));
			};
			if (!this.updradesAvailable)
			{
				this.buttonUlti.gameButton.onClick = delegate()
				{
					this.SelectSkill(-1, 0);
					UiManager.AddUiSound(SoundArchieve.inst.uiToggleOn);
				};
				for (int i = 0; i < 2; i++)
				{
					int bic = i;
					for (int j = this.buttonsBranches[i].Count - 1; j >= 0; j--)
					{
						int ic = j;
						ButtonSkillUpgrade buttonSkillUpgrade = this.buttonsBranches[i][j];
						buttonSkillUpgrade.gameButton.onClick = delegate()
						{
							this.SelectSkill(bic, ic);
							UiManager.AddUiSound(SoundArchieve.inst.uiToggleOn);
						};
					}
				}
			}
			for (int k = 0; k < this.buttonsBranch1.Count; k++)
			{
				Vector3 localPosition = this.buttonsBranch1[k].transform.localPosition;
				localPosition.x = -localPosition.x;
				localPosition.y = -localPosition.y;
				this.buttonsBranch2[k].transform.localPosition = localPosition;
			}
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.buttonUlti.textUltimate.text = LM.Get("UI_SKILLS_ULTIMATE");
			if (this.updradesAvailable)
			{
				this.textSkillPointsAvailable.text = LM.Get("UI_SKILLS_POINTS");
				this.buttonUpgrade.text.text = LM.Get("UI_UPGRADE");
				this.textOneTapUpgrade.text = LM.Get("UI_SKILLS_ONE_TAP_UPGRADE");
			}
			if (this.buttonTab != null)
			{
				this.buttonTab.text.text = LM.Get("UI_TAB_SKILLS");
			}
		}

		public void ResetSelectedSkill()
		{
			if (this.updradesAvailable)
			{
				this.buttonUpgrade.interactable = false;
			}
			this.selectedBranchIndex = -2;
			this.selectedSkillIndex = 0;
			this.panelSkillInfo.gameObject.SetActive(false);
			this.buttonPanelSkillInfoBg.gameObject.SetActive(false);
		}

		public void SelectSkill(int branchIndex, int skillIndex = 0)
		{
			this.selectedBranchIndex = branchIndex;
			this.selectedSkillIndex = skillIndex;
			this.panelSkillInfo.gameObject.SetActive(true);
			this.buttonPanelSkillInfoBg.gameObject.SetActive(true);
		}

		public void OnClickUpgradeSkill()
		{
			if (this.spineSkillUpgrade.AnimationState != null)
			{
				this.spineSkillUpgrade.transform.localScale = new Vector3(1f, 1f, 1f);
				this.spineSkillUpgrade.AnimationState.SetAnimation(0, this.spineAnim, false);
				this.spineSkillUpgrade.rectTransform.position = this.spinePos + Vector3.up * this.spinePosYOffset;
			}
		}

		public void UpdatePanel(Hero hero, UiManager uiManager, Simulator sim)
		{
			SkillTree skillTree = hero.GetSkillTree();
			SkillTreeProgress skillTreeProgress = hero.GetSkillTreeProgress();
			int numUnspentSkillPoints = hero.GetNumUnspentSkillPoints();
			if (UiManager.stateJustChanged)
			{
				List<List<bool>> list = new List<List<bool>>
				{
					new List<bool>(),
					new List<bool>()
				};
				List<List<bool>> list2 = new List<List<bool>>
				{
					new List<bool>(),
					new List<bool>()
				};
				for (int i = 0; i < 2; i++)
				{
					int j = 0;
					int num = skillTree.branches[i].Length;
					while (j < num)
					{
						list[i].Add(hero.CanSeeSkillInSkillScreen(i, j));
						list2[i].Add(j <= hero.GetData().GetDataBase().skillBranchesEverUnlocked[i]);
						j++;
					}
				}
				int level = hero.GetLevel();
				Sprite spriteSkillIconSkillScreen = uiManager.GetSpriteSkillIconSkillScreen(skillTree.ulti.GetType());
				this.SetUltiBadge(hero.GetData().GetDataBase());
				int skillEnhancerGearLevel = sim.GetSkillEnhancerGearLevel(skillTree.ulti);
				this.buttonUlti.SetDetails(spriteSkillIconSkillScreen, ButtonSkillUpgrade.Kind.Ultimate, LM.Get(skillTree.ulti.nameKey), string.Empty, skillTreeProgress.ulti, skillEnhancerGearLevel, skillTree.ulti.maxLevel, 0, 0, true, true);
				for (int k = 0; k < 2; k++)
				{
					for (int l = skillTree.branches[k].Length - 1; l >= 0; l--)
					{
						Sprite spriteSkillIconSkillScreen2 = uiManager.GetSpriteSkillIconSkillScreen(skillTree.branches[k][l].GetType());
						string sName = LM.Get(skillTree.branches[k][l].nameKey);
						int num2 = skillTreeProgress.branches[k][l];
						int skillEnhancerGearLevel2 = sim.GetSkillEnhancerGearLevel(skillTree.branches[k][l]);
						int sMaxLevel = skillTree.branches[k][l].maxLevel;
						int hLevelReq = skillTree.branches[k][l].requiredHeroLevel - sim.GetActiveWorld().universalBonus.heroLevelRequiredForSkillDecrease - sim.GetActiveWorld().activeChallenge.totalGainedUpgrades.heroLevelRequiredForSkillDecrease;
						SkillTreeNode skillTreeNode = skillTree.branches[k][l];
						bool flag = !skillTreeNode.IsActive();
						ButtonSkillUpgrade.Kind sKind = (!flag) ? ButtonSkillUpgrade.Kind.AutoActive : ButtonSkillUpgrade.Kind.Passive;
						this.buttonsBranches[k][l].SetDetails(spriteSkillIconSkillScreen2, sKind, sName, string.Empty, num2, skillEnhancerGearLevel2, sMaxLevel, hLevelReq, level, list[k][l], list2[k][l]);
						this.pathsBranches[k][l].SetOnOff(num2 > -1);
					}
				}
			}
			this.UpdateDetails(numUnspentSkillPoints);
			if (this.selectedBranchIndex > -2)
			{
				if (this.selectedBranchIndex == -1)
				{
					this.textSkillInfo.text = hero.GetSkillDescUlti();
					if (hero.HasSkillUltiReachedMaxLevel())
					{
						this.buttonUpgrade.gameObject.SetActive(false);
						this.maxLevel.SetActive(true);
					}
					else
					{
						this.buttonUpgrade.gameObject.SetActive(true);
						this.buttonUpgrade.interactable = hero.CanUpgradeSkillUlti();
						this.maxLevel.SetActive(false);
					}
				}
				else
				{
					this.textSkillInfo.text = hero.GetSkillDesc(this.selectedBranchIndex, this.selectedSkillIndex);
					if (hero.HasSkillReachedMaxLevel(this.selectedBranchIndex, this.selectedSkillIndex))
					{
						this.buttonUpgrade.gameObject.SetActive(false);
						this.maxLevel.SetActive(true);
					}
					else
					{
						this.buttonUpgrade.gameObject.SetActive(true);
						bool interactable = hero.CanUpgradeSkill(this.selectedBranchIndex, this.selectedSkillIndex);
						this.buttonUpgrade.interactable = interactable;
						this.maxLevel.SetActive(false);
					}
				}
			}
			this.buttonOneTapUpgrade.isOn = sim.skillOneTapUpgrade;
		}

		public void UpdatePanel(HeroDataBase heroData, UiManager uiManager, Simulator sim)
		{
			SkillTree skillTree = heroData.skillTree;
			if (UiManager.stateJustChanged)
			{
				List<List<bool>> list = new List<List<bool>>
				{
					new List<bool>(),
					new List<bool>()
				};
				List<List<bool>> list2 = new List<List<bool>>
				{
					new List<bool>(),
					new List<bool>()
				};
				for (int i = 0; i < 2; i++)
				{
					int j = 0;
					int num = skillTree.branches[i].Length;
					while (j < num)
					{
						list[i].Add(true);
						list2[i].Add(j <= heroData.skillBranchesEverUnlocked[i]);
						j++;
					}
				}
				Sprite spriteSkillIconSkillScreen = uiManager.GetSpriteSkillIconSkillScreen(skillTree.ulti.GetType());
				this.SetUltiBadge(heroData);
				this.buttonUlti.SetDetails(spriteSkillIconSkillScreen, ButtonSkillUpgrade.Kind.Ultimate, 0, LM.Get(skillTree.ulti.nameKey), string.Empty, true);
				for (int k = 0; k < 2; k++)
				{
					for (int l = skillTree.branches[k].Length - 1; l >= 0; l--)
					{
						Sprite spriteSkillIconSkillScreen2 = uiManager.GetSpriteSkillIconSkillScreen(skillTree.branches[k][l].GetType());
						string sName = LM.Get(skillTree.branches[k][l].nameKey);
						int requiredHeroLevel = skillTree.branches[k][l].requiredHeroLevel;
						SkillTreeNode skillTreeNode = skillTree.branches[k][l];
						bool flag = !skillTreeNode.IsActive();
						ButtonSkillUpgrade.Kind sKind = (!flag) ? ButtonSkillUpgrade.Kind.AutoActive : ButtonSkillUpgrade.Kind.Passive;
						this.buttonsBranches[k][l].SetDetails(spriteSkillIconSkillScreen2, sKind, requiredHeroLevel, sName, string.Empty, list2[k][l]);
					}
				}
			}
			this.UpdateDetails(0);
			if (this.selectedBranchIndex > -2)
			{
				if (this.selectedBranchIndex == -1)
				{
					this.textSkillInfo.text = heroData.skillTree.ulti.GetDescZero();
				}
				else
				{
					this.textSkillInfo.text = heroData.skillTree.branches[this.selectedBranchIndex][this.selectedSkillIndex].GetDescZero();
				}
				this.textSkillInfo.text = this.textSkillInfo.text.Replace("</color>", string.Empty);
				for (int num2 = this.textSkillInfo.text.IndexOf('<'); num2 != -1; num2 = this.textSkillInfo.text.IndexOf('<'))
				{
					this.textSkillInfo.text = this.textSkillInfo.text.Remove(num2, this.textSkillInfo.text.IndexOf('>') - num2 + 1);
				}
			}
		}

		public void UpdateDetails(int unspentSkillPoints)
		{
			if (this.textUnspentSkillPoints != null)
			{
				this.textUnspentSkillPoints.text = unspentSkillPoints.ToString();
			}
			ButtonSkillUpgrade buttonSkillUpgrade = null;
			if (this.selectedBranchIndex == -1)
			{
				buttonSkillUpgrade = this.buttonUlti;
			}
			else if (this.selectedBranchIndex > -1)
			{
				buttonSkillUpgrade = this.buttonsBranches[this.selectedBranchIndex][this.selectedSkillIndex];
			}
			if (this.selectedBranchIndex > -2)
			{
				this.textSkillName.text = buttonSkillUpgrade.skillName;
				this.textSkillInfo.text = buttonSkillUpgrade.skillInfo;
			}
			if (buttonSkillUpgrade != null)
			{
				this.spinePos = buttonSkillUpgrade.GetComponent<RectTransform>().position;
			}
			this.buttonSelectedIllusion.gameObject.SetActive(this.buttonUlti != buttonSkillUpgrade);
			for (int i = 0; i < 2; i++)
			{
				for (int j = this.buttonsBranches[i].Count - 1; j >= 0; j--)
				{
					if (this.buttonsBranches[i][j] == buttonSkillUpgrade)
					{
						this.buttonSelectedIllusion.transform.position = this.buttonsBranches[i][j].transform.position;
						this.buttonSelectedIllusion.StealInfo(this.buttonsBranches[i][j]);
					}
				}
			}
		}

		private void SetUltiBadge(HeroDataBase heroData)
		{
			int num;
			switch (heroData.ultiCatagory)
			{
			case HeroDataBase.UltiCatagory.GREEN:
				num = 0;
				break;
			case HeroDataBase.UltiCatagory.BLUE:
				num = 1;
				break;
			case HeroDataBase.UltiCatagory.ORANGE:
				num = 2;
				break;
			case HeroDataBase.UltiCatagory.RED:
				num = 3;
				break;
			default:
				throw new Exception(heroData.ultiCatagory.ToString());
			}
			this.ultiBadge.sprite = UiData.inst.ultiBadges[num];
		}

		public bool updradesAvailable;

		public ButtonSkillUpgrade buttonUlti;

		public Image ultiBadge;

		[SerializeField]
		private List<ButtonSkillUpgrade> buttonsBranch1;

		[SerializeField]
		private List<ButtonSkillUpgrade> buttonsBranch2;

		[SerializeField]
		private List<SkillScreenPath> pathsBranch1;

		[SerializeField]
		private List<SkillScreenPath> pathsBranch2;

		public List<List<ButtonSkillUpgrade>> buttonsBranches;

		public List<List<SkillScreenPath>> pathsBranches;

		public GameButton buttonUpgrade;

		public ButtonSkillUpgrade buttonSelectedIllusion;

		public Text textSkillName;

		public Text textSkillInfo;

		public RectTransform panelSkillInfo;

		public GameButton buttonPanelSkillInfoBg;

		public Text textUnspentSkillPoints;

		public Text textSkillPointsAvailable;

		public SkeletonGraphic spineSkillUpgrade;

		[SpineAnimation("", "", true, false)]
		private string spineAnim = "upgrade";

		private Vector3 spinePos;

		[SerializeField]
		private float spinePosYOffset;

		public GameButton buttonTab;

		public Image imagePanelSkillInfoBg;

		public GameObject maxLevel;

		public ButtonOnOff buttonOneTapUpgrade;

		public Text textOneTapUpgrade;

		public GameButton buttonHeroNext;

		[NonSerialized]
		public int selectedBranchIndex;

		[NonSerialized]
		public int selectedSkillIndex;
	}
}
