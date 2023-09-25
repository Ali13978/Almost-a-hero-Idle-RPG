using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OnscreenLog : MonoBehaviour
{
	public void Init()
	{
		Application.logMessageReceived += this.Application_logMessageReceived;
		this.toggleLogs.onClick.AddListener(new UnityAction(this.Button_ToggleLogs));
		this.toggleErrors.onClick.AddListener(new UnityAction(this.Button_ToggleErrors));
		this.toggleWarnings.onClick.AddListener(new UnityAction(this.Button_ToggleWarnings));
		this.clearLogs.onClick.AddListener(new UnityAction(this.Button_ClearLogs));
		this.logs = new List<LogElement>();
		this.toggleLogsText.text = "Logs(" + this.logCount + ")";
		this.toggleWarningsText.text = "Warnings(" + this.warningCount + ")";
		this.toggleErrorsText.text = "Errors(" + this.errorCount + ")";
	}

	private void Button_ClearLogs()
	{
		foreach (LogElement logElement in this.logs)
		{
			UnityEngine.Object.Destroy(logElement.gameObject);
		}
		this.logs.Clear();
		this.logCount = 0;
		this.warningCount = 0;
		this.errorCount = 0;
		this.toggleLogsText.text = "Logs(" + this.logCount + ")";
		this.toggleWarningsText.text = "Warnings(" + this.warningCount + ")";
		this.toggleErrorsText.text = "Errors(" + this.errorCount + ")";
	}

	private void Button_ToggleWarnings()
	{
		this.warningsShowing = !this.warningsShowing;
		foreach (LogElement logElement in this.logs)
		{
			if (logElement.logType == LogType.Warning)
			{
				logElement.gameObject.SetActive(this.warningsShowing);
			}
		}
		this.toggleWarningsImage.color = ((!this.warningsShowing) ? Color.grey : Color.white);
	}

	private void Button_ToggleErrors()
	{
		this.errorsShowing = !this.errorsShowing;
		foreach (LogElement logElement in this.logs)
		{
			if (logElement.logType == LogType.Error || logElement.logType == LogType.Exception || logElement.logType == LogType.Assert)
			{
				logElement.gameObject.SetActive(this.errorsShowing);
			}
		}
		this.toggleErrorsImage.color = ((!this.errorsShowing) ? Color.grey : Color.white);
	}

	private void Button_ToggleLogs()
	{
		this.logsShowing = !this.logsShowing;
		foreach (LogElement logElement in this.logs)
		{
			if (logElement.logType == LogType.Log)
			{
				logElement.gameObject.SetActive(this.logsShowing);
			}
		}
		this.toggleLogsImage.color = ((!this.logsShowing) ? Color.grey : Color.white);
	}

	private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
	{
		LogElement newObj = UnityEngine.Object.Instantiate<LogElement>(this.logElementPrefab, this.scrollContentRTransform);
		newObj.condition = condition;
		newObj.stackTrace = stackTrace;
		newObj.logType = type;
		newObj.button.onClick.AddListener(delegate()
		{
			this.Button_OnLogClick(newObj);
		});
		switch (type)
		{
		case LogType.Error:
			newObj.logTypeImage.color = OnscreenLog.errorColor;
			newObj.logTypetext.text = "E";
			if (!this.errorsShowing)
			{
				newObj.gameObject.SetActive(false);
			}
			this.errorCount++;
			break;
		case LogType.Assert:
			newObj.logTypeImage.color = OnscreenLog.errorColor;
			newObj.logTypetext.text = "A";
			if (!this.errorsShowing)
			{
				newObj.gameObject.SetActive(false);
			}
			this.errorCount++;
			break;
		case LogType.Warning:
			newObj.logTypeImage.color = OnscreenLog.warningColor;
			newObj.logTypetext.text = "W";
			if (!this.warningsShowing)
			{
				newObj.gameObject.SetActive(false);
			}
			this.warningCount++;
			break;
		case LogType.Log:
			newObj.logTypeImage.color = OnscreenLog.logColor;
			newObj.logTypetext.text = "L";
			if (!this.logsShowing)
			{
				newObj.gameObject.SetActive(false);
			}
			this.logCount++;
			break;
		case LogType.Exception:
			newObj.logTypeImage.color = OnscreenLog.errorColor;
			newObj.logTypetext.text = "EX";
			if (!this.errorsShowing)
			{
				newObj.gameObject.SetActive(false);
			}
			this.errorCount++;
			break;
		}
		newObj.logText.text = condition;
		this.logs.Add(newObj);
		this.toggleLogsText.text = "Logs(" + this.logCount + ")";
		this.toggleWarningsText.text = "Warnings(" + this.warningCount + ")";
		this.toggleErrorsText.text = "Errors(" + this.errorCount + ")";
	}

	private void Button_OnLogClick(LogElement newObj)
	{
		newObj.isExpanded = !newObj.isExpanded;
		Vector2 sizeDelta = newObj.rectTransform.sizeDelta;
		if (newObj.isExpanded)
		{
			sizeDelta.y = this.openedHeight;
			newObj.rectTransform.sizeDelta = sizeDelta;
			newObj.logText.text = newObj.condition + "\n" + newObj.stackTrace;
		}
		else
		{
			sizeDelta.y = this.closedHeight;
			newObj.rectTransform.sizeDelta = sizeDelta;
			newObj.logText.text = newObj.condition;
		}
	}

	public RectTransform scrollContentRTransform;

	public LogElement logElementPrefab;

	private List<LogElement> logs;

	private static Color errorColor = new Color(1f, 0f, 0f, 1f);

	private static Color warningColor = new Color(1f, 1f, 0f, 1f);

	private static Color logColor = new Color(0f, 1f, 0f, 1f);

	public Button clearLogs;

	public Button toggleLogs;

	public Button toggleWarnings;

	public Button toggleErrors;

	public Image toggleLogsImage;

	public Image toggleWarningsImage;

	public Image toggleErrorsImage;

	public Text toggleLogsText;

	public Text toggleWarningsText;

	public Text toggleErrorsText;

	private bool logsShowing = true;

	private bool errorsShowing = true;

	private bool warningsShowing = true;

	public float closedHeight = 75f;

	public float openedHeight = 400f;

	private int logCount;

	private int warningCount;

	private int errorCount;
}
