using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DConsole
{
	public class DLogConsole : MonoBehaviour
	{
		public static void Init()
		{
			DLogConsole.instance = UnityEngine.Object.FindObjectOfType<DLogConsole>();
			if (DLogConsole.instance == null)
			{
				UnityEngine.Debug.LogError("There is not any instance use prefab from assets");
			}
			else
			{
				DLogConsole.instance.Initalize();
			}
		}

		public static void Kill()
		{
			DLogConsole.instance = UnityEngine.Object.FindObjectOfType<DLogConsole>();
			if (DLogConsole.instance == null)
			{
				UnityEngine.Debug.LogError("There is not any instance use prefab from assets");
			}
			else
			{
				UnityEngine.Object.Destroy(DLogConsole.instance.gameObject);
			}
		}

		public static void Open()
		{
			if (DLogConsole.instance != null)
			{
				DLogConsole.instance.Button_OnOpenClick();
			}
		}

		private void Initalize()
		{
			bool flag = PlayerPrefs.GetInt(DLogConsole.s_isOpened, 0) == 1;
			this.isExpanded = (PlayerPrefs.GetInt(DLogConsole.s_isExpanded, 0) == 1);
			this.isInfosOn = (PlayerPrefs.GetInt(DLogConsole.s_isInfosOn, 1) == 1);
			this.isWarningsOn = (PlayerPrefs.GetInt(DLogConsole.s_isWarningsOn, 1) == 1);
			this.isErrorsOn = (PlayerPrefs.GetInt(DLogConsole.s_isErrorsOn, 1) == 1);
			if (flag)
			{
				this.Button_OnOpenClick();
			}
			else
			{
				this.Button_OnCloseClick();
			}
			this.expandToggle.onClick.AddListener(new UnityAction(this.Button_OnExpandToggled));
			this.closeButton.onClick.AddListener(new UnityAction(this.Button_OnCloseClick));
			this.clearButton.onClick.AddListener(new UnityAction(this.Button_OnClearClick));
			this.infoToggleButton.onClick.AddListener(delegate()
			{
				this.Button_ToggleLogType(0);
			});
			this.warningToggleButton.onClick.AddListener(delegate()
			{
				this.Button_ToggleLogType(1);
			});
			this.errorToggleButton.onClick.AddListener(delegate()
			{
				this.Button_ToggleLogType(2);
			});
			this.expandedHeight = 33.3f * (float)this.logCountToShow + this.compactHeight + 20f;
			this.SetExpanse();
			this.CalculateScrollSize();
			Application.logMessageReceived += this.Application_logMessageReceived;
			this.lastLogCount = -1;
			this.infoToggleButton.targetGraphic.color = ((!this.isInfosOn) ? DLog.disabledColor : DLog.infoColor);
			this.warningToggleButton.targetGraphic.color = ((!this.isWarningsOn) ? DLog.disabledColor : DLog.warningColor);
			this.errorToggleButton.targetGraphic.color = ((!this.isErrorsOn) ? DLog.disabledColor : DLog.errorColor);
			this.isInitialized = true;
			Canvas component = base.GetComponent<Canvas>();
			if (component.worldCamera == null)
			{
				component.worldCamera = UnityEngine.Object.FindObjectOfType<Camera>();
			}
		}

		private void Button_ToggleLogType(int v)
		{
			switch (v)
			{
			case 0:
				this.isInfosOn = !this.isInfosOn;
				PlayerPrefs.SetInt(DLogConsole.s_isInfosOn, (!this.isInfosOn) ? 0 : 1);
				this.infoToggleButton.targetGraphic.color = ((!this.isInfosOn) ? DLog.disabledColor : DLog.infoColor);
				break;
			case 1:
				this.isWarningsOn = !this.isWarningsOn;
				PlayerPrefs.SetInt(DLogConsole.s_isWarningsOn, (!this.isWarningsOn) ? 0 : 1);
				this.warningToggleButton.targetGraphic.color = ((!this.isWarningsOn) ? DLog.disabledColor : DLog.warningColor);
				break;
			case 2:
				this.isErrorsOn = !this.isErrorsOn;
				PlayerPrefs.SetInt(DLogConsole.s_isErrorsOn, (!this.isErrorsOn) ? 0 : 1);
				this.errorToggleButton.targetGraphic.color = ((!this.isErrorsOn) ? DLog.disabledColor : DLog.errorColor);
				break;
			default:
				throw new Exception(v.ToString());
			}
			this.filteredlogs.Clear();
			for (int i = 0; i < this.logs.Count; i++)
			{
				DLogConsole.Log log = this.logs[i];
				if (this.isInfosOn && log.type == LogType.Log)
				{
					this.filteredlogs.Add(log);
				}
				if (this.isWarningsOn && log.type == LogType.Warning)
				{
					this.filteredlogs.Add(log);
				}
				if (this.isErrorsOn && (log.type == LogType.Error || log.type == LogType.Assert || log.type == LogType.Exception))
				{
					this.filteredlogs.Add(log);
				}
			}
		}

		private void Button_OnClearClick()
		{
			this.ClearLogs();
		}

		private void Scroll_OnValueChanged(Vector2 scrl)
		{
			this.currentScroll = Mathf.Clamp01(1f - scrl.y);
			this.UpdateLogs();
		}

		private void Update()
		{
			if (!this.isInitialized)
			{
				base.gameObject.SetActive(false);
				return;
			}
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			if (this.isExpanded)
			{
				this.mainLog.gameObject.SetActive(false);
				if (this.filteredlogs.Count != this.lastLogCount)
				{
					this.lastLogCount = this.filteredlogs.Count;
					this.SpawnLogs();
					this.CalculateScrollSize();
					if (this.lastScroll >= 0.999f)
					{
						this.scrollRect.verticalNormalizedPosition = 0f;
					}
					this.UpdateLogs();
				}
				this.currentScroll = Mathf.Clamp01(1f - this.scrollRect.verticalNormalizedPosition);
				if (this.lastScroll != this.currentScroll)
				{
					this.UpdateLogs();
					this.lastScroll = this.currentScroll;
				}
				this.infoToggleText.text = string.Format("I({0})", this.numInfos);
				this.warningToggleText.text = string.Format("W({0})", this.numWarnings);
				this.errorToggleText.text = string.Format("E({0})", this.numErrors);
			}
			else if (this.filteredlogs.Count == 0)
			{
				this.mainLog.gameObject.SetActive(false);
			}
			else
			{
				this.mainLog.gameObject.SetActive(true);
				this.mainLog.SetLog(this.filteredlogs[this.filteredlogs.Count - 1]);
			}
		}

		private void Button_OnCloseClick()
		{
			this.consoleRectTransform.gameObject.SetActive(false);
			PlayerPrefs.SetInt(DLogConsole.s_isOpened, 0);
			DLogConsole.isOpen = false;
		}

		private void Button_OnOpenClick()
		{
			this.consoleRectTransform.gameObject.SetActive(true);
			PlayerPrefs.SetInt(DLogConsole.s_isOpened, 1);
			DLogConsole.isOpen = true;
		}

		private void Button_OnExpandToggled()
		{
			this.isExpanded = !this.isExpanded;
			PlayerPrefs.SetInt(DLogConsole.s_isExpanded, (!this.isExpanded) ? 0 : 1);
			this.SetExpanse();
		}

		private void SetExpanse()
		{
			this.scrollRect.gameObject.SetActive(this.isExpanded);
			this.togglesParent.SetActive(this.isExpanded);
			if (this.isExpanded)
			{
				this.consoleRectTransform.sizeDelta = new Vector2(this.consoleRectTransform.sizeDelta.x, this.expandedHeight);
			}
			else
			{
				this.consoleRectTransform.sizeDelta = new Vector2(this.consoleRectTransform.sizeDelta.x, this.compactHeight);
			}
		}

		private void OnDestroy()
		{
			Application.logMessageReceived -= this.Application_logMessageReceived;
		}

		private void CalculateScrollSize()
		{
			float scrollHeight = this.GetScrollHeight();
			this.scrollRect.content.sizeDelta = new Vector2(this.scrollRect.content.sizeDelta.x, scrollHeight);
		}

		private float GetScrollHeight()
		{
			return (float)this.filteredlogs.Count * 33.3f + 16.65f + 5f;
		}

		private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
		{
			StackTrace stackTrace2 = new StackTrace(6, true);
			DLogConsole.Log item = new DLogConsole.Log
			{
				condition = condition,
				stackTrace = stackTrace2.ToString(),
				type = type
			};
			this.logs.Add(item);
			switch (type)
			{
			case LogType.Error:
			case LogType.Assert:
			case LogType.Exception:
				this.numErrors++;
				break;
			case LogType.Warning:
				this.numWarnings++;
				break;
			case LogType.Log:
				this.numInfos++;
				break;
			}
			if (this.isInfosOn && type == LogType.Log)
			{
				this.filteredlogs.Add(item);
			}
			if (this.isWarningsOn && type == LogType.Warning)
			{
				this.filteredlogs.Add(item);
			}
			if (this.isErrorsOn && (type == LogType.Error || type == LogType.Assert || type == LogType.Exception))
			{
				this.filteredlogs.Add(item);
			}
		}

		private void ClearLogs()
		{
			this.logs.Clear();
			this.filteredlogs.Clear();
			this.SpawnLogs();
			this.scrollRect.horizontalNormalizedPosition = 1f;
			this.currentScroll = 0f;
			this.numErrors = 0;
			this.numWarnings = 0;
			this.numInfos = 0;
			this.stackTrace.gameObject.SetActive(false);
		}

		private void SpawnLogs()
		{
			int num = Mathf.Min(this.filteredlogs.Count, this.logCountToShow);
			while (this.logObjects.Count != num)
			{
				if (this.logObjects.Count < num)
				{
					DLog newG = UnityEngine.Object.Instantiate<DLog>(this.mainLog, this.scrollRect.content);
					newG.gameObject.SetActive(true);
					newG.rectTranform.offsetMin = new Vector2(5f, newG.rectTranform.offsetMin.y);
					newG.rectTranform.offsetMax = new Vector2(-5f, newG.rectTranform.offsetMax.y);
					newG.button.onClick.AddListener(delegate()
					{
						this.Button_OnSelectLog(newG.log);
					});
					this.logObjects.Add(newG);
				}
				else if (this.logObjects.Count > num)
				{
					DLog dlog = this.logObjects[this.logObjects.Count - 1];
					this.logObjects.RemoveAt(this.logObjects.Count - 1);
					UnityEngine.Object.Destroy(dlog.gameObject);
				}
			}
		}

		private void Button_OnSelectLog(DLogConsole.Log log)
		{
			this.stackTrace.gameObject.SetActive(true);
			this.stackTrace.text = ((!string.IsNullOrEmpty(log.stackTrace)) ? log.stackTrace : "No Stacktrace");
			this.selectedLog = log;
			this.UpdateLogs();
		}

		private void UpdateLogs()
		{
			float scrollHeight = this.GetScrollHeight();
			float num = this.currentScroll * Mathf.Clamp(scrollHeight - this.scrollRect.viewport.rect.height, 0f, float.MaxValue) + 16.65f + 5f;
			int num2 = Mathf.FloorToInt(this.currentScroll * (float)(this.filteredlogs.Count - this.logObjects.Count));
			for (int i = 0; i < this.logObjects.Count; i++)
			{
				DLog dlog = this.logObjects[i];
				try
				{
					dlog.SetLog(this.filteredlogs[num2 + i]);
				}
				catch (ArgumentOutOfRangeException)
				{
					throw new ArgumentOutOfRangeException(string.Concat(new object[]
					{
						num2.ToString(),
						" ",
						this.logObjects.Count,
						" ",
						this.filteredlogs.Count
					}));
				}
				if (dlog.log == this.selectedLog)
				{
					dlog.SetDarkEr();
				}
				else if (i % 2 == 0)
				{
					dlog.SetDark();
				}
				else
				{
					dlog.SetLight();
				}
				dlog.rectTranform.anchoredPosition = new Vector2(0f, -num - 33.3f * (float)i);
			}
		}

		private const float perObjectSize = 33.3f;

		public DLog mainLog;

		public GameObject togglesParent;

		public Button expandToggle;

		public Button closeButton;

		public Button clearButton;

		public Button infoToggleButton;

		public Button warningToggleButton;

		public Button errorToggleButton;

		public Text infoToggleText;

		public Text warningToggleText;

		public Text errorToggleText;

		public RectTransform consoleRectTransform;

		public float compactHeight = 50f;

		public ScrollRect scrollRect;

		public Text stackTrace;

		private List<DLog> logObjects = new List<DLog>();

		private bool isExpanded;

		private int logCountToShow = 6;

		private List<DLogConsole.Log> logs = new List<DLogConsole.Log>();

		private List<DLogConsole.Log> filteredlogs = new List<DLogConsole.Log>();

		private int lastLogCount;

		private float currentScroll;

		private float lastScroll;

		private float expandedHeight = 400f;

		private DLogConsole.Log selectedLog;

		private bool isInfosOn;

		private bool isWarningsOn;

		private bool isErrorsOn;

		private int numInfos;

		private int numWarnings;

		private int numErrors;

		private bool isInitialized;

		private static string s_isOpened = "s_isOpened";

		private static string s_isExpanded = "s_isExpanded";

		private static string s_isInfosOn = "s_isInfosOn";

		private static string s_isWarningsOn = "s_isWarningsOn";

		private static string s_isErrorsOn = "s_isErrorsOn";

		private static DLogConsole instance;

		public static bool isOpen;

		public class Log
		{
			public string condition;

			public string stackTrace;

			public LogType type;
		}
	}
}
