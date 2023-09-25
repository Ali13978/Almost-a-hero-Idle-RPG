using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNode : MonoBehaviour
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

	public void SetColorTheme(StageNode.NodeState nodeState)
	{
		switch (nodeState)
		{
		case StageNode.NodeState.Inactive:
			this.SetThemeCollected();
			break;
		case StageNode.NodeState.Active:
			this.SetThemeFresh();
			break;
		case StageNode.NodeState.Boss:
			this.SetThemeBoss();
			break;
		}
	}

	public void SetThemeFresh()
	{
		this.hat.color = this.themeFresh.colorBg1;
		this.background1.color = this.themeFresh.colorBg1;
		this.background2.color = this.themeFresh.colorBg2;
	}

	public void SetThemeCollected()
	{
		this.hat.color = this.themeCollected.colorBg1;
		this.background1.color = this.themeCollected.colorBg1;
		this.background2.color = this.themeCollected.colorBg2;
	}

	public void SetThemeBoss()
	{
		this.hat.color = this.themeBoss.colorBg1;
		this.background1.color = this.themeBoss.colorBg1;
		this.background2.color = this.themeBoss.colorBg2;
	}

	public void EnableHat(StageNode.HatType hatType)
	{
		if (hatType != StageNode.HatType.Crown)
		{
			if (hatType == StageNode.HatType.LockOn)
			{
				this.hat.sprite = this.spriteLockOn;
			}
		}
		else
		{
			this.hat.sprite = this.spriteCrown;
		}
		this.hat.SetNativeSize();
		this.hat.gameObject.SetActive(true);
	}

	public void DisableHat()
	{
		this.hat.gameObject.SetActive(false);
	}

	private RectTransform m_rectTransform;

	public Image background1;

	public Image background2;

	public Image hat;

	public Text text;

	public Sprite spriteCrown;

	public Sprite spriteLockOn;

	public StageNode.ColorTheme themeFresh;

	public StageNode.ColorTheme themeCollected;

	public StageNode.ColorTheme themeBoss;

	[Serializable]
	public struct ColorTheme
	{
		public bool Equals(object obj)
		{
			if (!(obj is StageNode.ColorTheme))
			{
				return false;
			}
			StageNode.ColorTheme colorTheme = (StageNode.ColorTheme)obj;
			return this.colorBg1.Equals(colorTheme.colorBg1) && this.colorBg2.Equals(colorTheme.colorBg2);
		}

		public int GetHashCode()
		{
			int num = 854157433;
			num = num * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.colorBg1);
			return num * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.colorBg2);
		}

		public static bool operator ==(StageNode.ColorTheme lhs, StageNode.ColorTheme rhs)
		{
			return lhs.colorBg1 == rhs.colorBg1 && lhs.colorBg2 == rhs.colorBg2;
		}

		public static bool operator !=(StageNode.ColorTheme lhs, StageNode.ColorTheme rhs)
		{
			return lhs.colorBg1 != rhs.colorBg1 || lhs.colorBg2 != rhs.colorBg2;
		}

		public Color colorBg1;

		public Color colorBg2;
	}

	public enum HatType
	{
		Crown,
		LockOn
	}

	public enum NodeState
	{
		Inactive,
		Active,
		Boss
	}
}
