using System;
using System.Collections.Generic;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class PTPGameController : MonoBehaviour
{
	private void Awake()
	{
		PTPGameController.instance = this;
		GameCenterManager.OnAuthFinished += this.OnAuthFinished;
		GameCenterManager.Init();
		this.b = base.gameObject.AddComponent<ConnectionButton>();
		this.b.enabled = false;
		this.d = base.gameObject.AddComponent<DisconnectButton>();
		this.d.enabled = false;
		this.m = base.gameObject.GetComponent<ClickManagerExample>();
		this.m.enabled = false;
		GameCenter_RTM.ActionPlayerStateChanged += this.HandleActionPlayerStateChanged;
		GameCenter_RTM.ActionMatchStarted += this.HandleActionMatchStarted;
	}

	public void createRedSphere(Vector3 pos)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pref);
		gameObject.transform.position = pos;
		gameObject.GetComponent<Renderer>().enabled = true;
		gameObject.GetComponent<Renderer>().material = new Material(gameObject.GetComponent<Renderer>().material);
		gameObject.GetComponent<Renderer>().material.color = Color.red;
		this.spheres.Add(gameObject);
	}

	public void createGreenSphere(Vector3 pos)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pref);
		gameObject.transform.position = pos;
		gameObject.GetComponent<Renderer>().enabled = true;
		gameObject.GetComponent<Renderer>().material = new Material(gameObject.GetComponent<Renderer>().material);
		gameObject.GetComponent<Renderer>().material.color = Color.green;
		this.spheres.Add(gameObject);
	}

	private void OnAuthFinished(Result res)
	{
		if (res.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Player Authed ", "ID: " + GameCenterManager.Player.Id + "\nName: " + GameCenterManager.Player.DisplayName);
			this.cleanUpScene();
		}
	}

	private void HandleActionPlayerStateChanged(GK_Player player, GK_PlayerConnectionState state, GK_RTM_Match macth)
	{
		if (state == GK_PlayerConnectionState.Disconnected)
		{
			IOSNativePopUpManager.showMessage("Disconnect", "Game finished");
			Singleton<GameCenter_RTM>.Instance.Disconnect();
			this.cleanUpScene();
		}
		else
		{
			this.CheckMatchState(macth);
		}
	}

	private void HandleActionMatchStarted(GK_RTM_MatchStartedResult result)
	{
		if (result.IsSucceeded)
		{
			this.CheckMatchState(result.Match);
		}
		else
		{
			IOSNativePopUpManager.showMessage("Match Start Error", result.Error.Message);
		}
	}

	private void CheckMatchState(GK_RTM_Match macth)
	{
		IOSNativePopUpManager.dismissCurrentAlert();
		if (macth != null)
		{
			if (macth.ExpectedPlayerCount == 0)
			{
				IOSNativePopUpManager.showMessage("Match Started", "let's play now\n   Macth.ExpectedPlayerCount): " + macth.ExpectedPlayerCount);
				this.m.enabled = true;
				this.b.enabled = false;
				this.d.enabled = true;
				ISN_Logger.Log("Sending HelloPackage ", LogType.Log);
				HelloPackage helloPackage = new HelloPackage();
				helloPackage.send();
			}
			else
			{
				IOSNativePopUpManager.showMessage("Match Created", "Macth.ExpectedPlayerCount): " + macth.ExpectedPlayerCount);
			}
		}
	}

	private void cleanUpScene()
	{
		this.b.enabled = true;
		this.m.enabled = false;
		this.d.enabled = false;
		foreach (GameObject obj in this.spheres)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.spheres.Clear();
	}

	public GameObject pref;

	private DisconnectButton d;

	private ConnectionButton b;

	private ClickManagerExample m;

	public static PTPGameController instance;

	private List<GameObject> spheres = new List<GameObject>();
}
