using System;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class TrinketUi : AahMonoBehaviour
	{
		public override void Register()
		{
		}

		public override void Init()
		{
			this.InitVisual(false);
		}

		public void InitVisual(bool animateIcons = false)
		{
			if (this.randomTrinket)
			{
				this.simTrinket = Trinket.GetRandom(1);
			}
			if (!this.inited)
			{
				this.inited = true;
			}
			else if (this.icons != null)
			{
			}
			if (this.simTrinket != null)
			{
				int bodySpriteIndex = this.simTrinket.bodySpriteIndex;
				Vector2[] array;
				Vector2 anchoredPosition;
				Vector2 anchoredPosition2;
				Vector2[] array2;
				if (bodySpriteIndex == 0)
				{
					array = new Vector2[]
					{
						new Vector2(-29f, 33f),
						new Vector2(3f, -24f)
					};
					anchoredPosition = new Vector2(0f, 60f);
					anchoredPosition2 = new Vector2(0f, 8f);
					array2 = new Vector2[]
					{
						new Vector2(0f, -56f),
						new Vector2(-55f, -22f),
						default(Vector2)
					};
				}
				else if (bodySpriteIndex == 1)
				{
					array = new Vector2[]
					{
						new Vector2(-29f, 33f),
						new Vector2(3f, -24f)
					};
					anchoredPosition = new Vector2(0f, 57f);
					anchoredPosition2 = new Vector2(0f, 8f);
					array2 = new Vector2[]
					{
						new Vector2(0f, -54f),
						new Vector2(-38f, -29f),
						default(Vector2)
					};
				}
				else if (bodySpriteIndex == 2)
				{
					array = new Vector2[]
					{
						new Vector2(-28f, 22f),
						new Vector2(23f, -16f)
					};
					anchoredPosition = new Vector2(0f, 59f);
					anchoredPosition2 = new Vector2(0f, 1f);
					array2 = new Vector2[]
					{
						new Vector2(0f, -55f),
						new Vector2(-49f, -31f),
						default(Vector2)
					};
				}
				else if (bodySpriteIndex == 3)
				{
					array = new Vector2[]
					{
						new Vector2(-17f, 20f),
						new Vector2(20f, -24f)
					};
					anchoredPosition = new Vector2(0f, 58f);
					anchoredPosition2 = new Vector2(0f, -4f);
					array2 = new Vector2[]
					{
						new Vector2(0f, -54f),
						new Vector2(-53f, -33f),
						default(Vector2)
					};
				}
				else if (bodySpriteIndex == 4)
				{
					array = new Vector2[]
					{
						new Vector2(-18f, 31f),
						new Vector2(18f, -24f)
					};
					anchoredPosition = new Vector2(0f, 61f);
					anchoredPosition2 = new Vector2(0f, 8f);
					array2 = new Vector2[]
					{
						new Vector2(0f, -55f),
						new Vector2(-52f, -42f),
						default(Vector2)
					};
				}
				else
				{
					if (bodySpriteIndex != 5)
					{
						throw new NotImplementedException();
					}
					array = new Vector2[]
					{
						new Vector2(-18f, 23f),
						new Vector2(3f, -21f)
					};
					anchoredPosition = new Vector2(0f, 53f);
					anchoredPosition2 = new Vector2(0f, 3f);
					array2 = new Vector2[]
					{
						new Vector2(0f, -52f),
						new Vector2(-39f, -36f),
						default(Vector2)
					};
				}
				array2[2].x = -array2[1].x;
				array2[2].y = array2[1].y;
				this.imageBg.sprite = UiData.inst.spriteTrinketBgs[bodySpriteIndex];
				this.imageBg.SetNativeSize();
				this.imageBody.sprite = UiData.inst.spriteTrinketBodies[bodySpriteIndex];
				this.imageBody.color = UiManager.colorTrinketLevels[this.simTrinket.bodyColorIndex];
				this.imageBody.SetNativeSize();
				int num = 2;
				this.numEffects = this.simTrinket.effects.Count;
				if (animateIcons)
				{
					if (DOTween.IsTweening(231283, true))
					{
						DOTween.Complete(231283, false);
					}
					base.transform.DOScale(0.8f, 0.2f).SetEase(EaseFactory.Kick(Ease.InSine, Ease.InSine)).SetId(231283);
				}
				int num2 = 0;
				int i = 0;
				int num3 = this.numEffects;
				while (i < num3)
				{
					num2 += this.simTrinket.effects[i].GetSprites().Length;
					i++;
				}
				for (int j = 0; j < 7; j++)
				{
					if (!(this.icons[j] == null))
					{
						this.icons[j].gameObject.SetActive(j < num + num2);
					}
				}
				for (int k = 0; k < num; k++)
				{
					if (this.icons[k] == null)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabIcon, base.transform);
						this.icons[k] = gameObject.GetComponent<Image>();
					}
					this.icons[k].sprite = UiData.inst.spriteTrinketShines[bodySpriteIndex * 2 + k];
					this.icons[k].SetNativeSize();
					this.icons[k].transform.localScale = new Vector3(1f, 1f, 1f);
					this.icons[k].raycastTarget = false;
					this.icons[k].rectTransform.anchoredPosition = array[k];
				}
				int num4 = num;
				int l = 0;
				int num5 = this.numEffects;
				while (l < num5)
				{
					TrinketEffectGroup group = this.simTrinket.effects[l].GetGroup();
					if (group != TrinketEffectGroup.SPECIAL)
					{
						if (group != TrinketEffectGroup.SECONDARY)
						{
							if (group == TrinketEffectGroup.COMMON)
							{
								Sprite[] sprites = this.simTrinket.effects[l].GetSprites();
								int num6 = sprites.Length;
								for (int m = 0; m < num6; m++)
								{
									if (this.icons[num4 + m] == null)
									{
										GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.prefabIcon, base.transform);
										this.icons[num4 + m] = gameObject2.GetComponent<Image>();
									}
									this.icons[num4 + m].sprite = sprites[m];
									this.icons[num4 + m].SetNativeSize();
									if (animateIcons)
									{
										this.icons[num4 + m].transform.localScale = new Vector3(0f, 0f, 0f);
										this.icons[num4 + m].transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack).SetDelay(0.1f + (float)m * 0.1f);
									}
									else
									{
										this.icons[num4 + m].transform.localScale = new Vector3(1f, 1f, 1f);
									}
								}
								if (num6 == 1)
								{
									this.icons[num4].rectTransform.anchoredPosition = array2[0];
								}
								else if (num6 == 2)
								{
									this.icons[num4].rectTransform.anchoredPosition = array2[1];
									this.icons[num4 + 1].rectTransform.anchoredPosition = array2[2];
								}
								else
								{
									if (num6 != 3)
									{
										throw new NotImplementedException();
									}
									this.icons[num4].rectTransform.anchoredPosition = array2[0];
									this.icons[num4 + 1].rectTransform.anchoredPosition = array2[1];
									this.icons[num4 + 2].rectTransform.anchoredPosition = array2[2];
								}
								num4 += num6;
							}
						}
						else
						{
							if (this.icons[num4] == null)
							{
								GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.prefabIcon, base.transform);
								this.icons[num4] = gameObject3.GetComponent<Image>();
							}
							this.icons[num4].sprite = this.simTrinket.effects[l].GetSprites()[0];
							this.icons[num4].SetNativeSize();
							if (animateIcons)
							{
								this.icons[num4].transform.localScale = new Vector3(0f, 0f, 0f);
								this.icons[num4].transform.DOScale(1f, 0.3f).SetDelay(0.1f).SetEase(Ease.OutBack);
							}
							else
							{
								this.icons[num4].transform.localScale = new Vector3(1f, 1f, 1f);
							}
							this.icons[num4].rectTransform.anchoredPosition = anchoredPosition;
							num4++;
						}
					}
					else
					{
						if (this.icons[num4] == null)
						{
							GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.prefabIcon, base.transform);
							this.icons[num4] = gameObject4.GetComponent<Image>();
						}
						this.icons[num4].sprite = this.simTrinket.effects[l].GetSprites()[0];
						this.icons[num4].SetNativeSize();
						if (animateIcons)
						{
							this.icons[num4].transform.localScale = new Vector3(0f, 0f, 0f);
							this.icons[num4].transform.DOScale(1f, 0.3f).SetDelay(0.1f).SetEase(Ease.OutBack);
						}
						else
						{
							this.icons[num4].transform.localScale = new Vector3(1f, 1f, 1f);
						}
						this.icons[num4].rectTransform.anchoredPosition = anchoredPosition2;
						num4++;
					}
					l++;
				}
			}
		}

		public void Init(Trinket simTrinket)
		{
			if (this.simTrinket == simTrinket)
			{
				return;
			}
			this.simTrinket = simTrinket;
			this.InitVisual(false);
		}

		public void InitAnimated(Trinket simTrinket)
		{
			if (this.simTrinket == simTrinket)
			{
				return;
			}
			this.simTrinket = simTrinket;
			this.InitVisual(true);
		}

		public Trinket simTrinket;

		public Image imageBg;

		public Image imageBody;

		public GameObject prefabIcon;

		private bool inited;

		public Image[] icons = new Image[7];

		private int numEffects;

		public bool randomTrinket;

		public Image movingGlow;
	}
}
