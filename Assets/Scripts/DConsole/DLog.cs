using System;
using UnityEngine;
using UnityEngine.UI;

namespace DConsole
{
	public class DLog : MonoBehaviour
	{
		public RectTransform rectTranform
		{
			get
			{
				RectTransform result;
				if ((result = this.m_rectTranform) == null)
				{
					result = (this.m_rectTranform = base.GetComponent<RectTransform>());
				}
				return result;
			}
		}

		public void SetTypeInfo()
		{
			this.typeBg.color = DLog.infoColor;
			this.typeText.text = "I";
		}

		public void SetTypeWarning()
		{
			this.typeBg.color = DLog.warningColor;
			this.typeText.text = "W";
		}

		public void SetTypeError()
		{
			this.typeBg.color = DLog.errorColor;
			this.typeText.text = "E";
		}

		public void SetTypeEx()
		{
			this.typeBg.color = DLog.errorColor;
			this.typeText.text = "EX";
		}

		public void SetDark()
		{
			this.bg.color = DLog.darkColor;
		}

		public void SetDarkEr()
		{
			this.bg.color = DLog.darkerColor;
		}

		public void SetLight()
		{
			this.bg.color = DLog.lightColor;
		}

		public void SetLog(DLogConsole.Log log)
		{
			this.log = log;
			this.logText.text = log.condition;
			switch (log.type)
			{
			case LogType.Error:
			case LogType.Assert:
				this.SetTypeError();
				break;
			case LogType.Warning:
				this.SetTypeWarning();
				break;
			case LogType.Log:
				this.SetTypeInfo();
				break;
			case LogType.Exception:
				this.SetTypeEx();
				break;
			}
		}

		private RectTransform m_rectTranform;

		public Button button;

		public Image bg;

		public Image typeBg;

		public Text logText;

		public Text typeText;

		private static Color darkColor = new Color32(55, 55, 55, byte.MaxValue);

		private static Color darkerColor = new Color32(25, 25, 25, byte.MaxValue);

		private static Color lightColor = new Color32(65, 65, 65, byte.MaxValue);

		public static Color disabledColor = new Color(0.8f, 0.8f, 0.8f, 1f);

		public static Color infoColor = new Color(0.2f, 0.9f, 0.2f, 1f);

		public static Color warningColor = new Color32(244, 170, 66, byte.MaxValue);

		public static Color errorColor = new Color32(byte.MaxValue, 61, 61, byte.MaxValue);

		public DLogConsole.Log log;
	}
}
