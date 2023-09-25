using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHeroClass : AahMonoBehaviour
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

		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.heroClassIcons = new Dictionary<HeroClass, Sprite>();
			this.heroClassIcons.Add(HeroClass.ATTACKER, this.iconSprites[0]);
			this.heroClassIcons.Add(HeroClass.DEFENDER, this.iconSprites[1]);
			this.heroClassIcons.Add(HeroClass.SUPPORTER, this.iconSprites[2]);
		}

		public void SetIcon(HeroClass heroClass)
		{
			this.image.sprite = this.heroClassIcons[heroClass];
			if (heroClass == HeroClass.ATTACKER)
			{
				this.text.text = LM.Get("HERO_CLASS_ATTACKER");
			}
			else if (heroClass == HeroClass.DEFENDER)
			{
				this.text.text = LM.Get("HERO_CLASS_DEFENDER");
			}
			else
			{
				if (heroClass != HeroClass.SUPPORTER)
				{
					throw new NotImplementedException();
				}
				this.text.text = LM.Get("HERO_CLASS_SUPPORTER");
			}
		}

		public Sprite[] iconSprites;

		public Image image;

		public Text text;

		private RectTransform m_rectTransform;

		private Dictionary<HeroClass, Sprite> heroClassIcons;
	}
}
