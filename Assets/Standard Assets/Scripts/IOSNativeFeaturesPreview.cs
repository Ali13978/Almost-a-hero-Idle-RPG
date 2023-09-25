using System;
using UnityEngine;

public class IOSNativeFeaturesPreview : BaseIOSFeaturePreview
{
	private void Awake()
	{
		if (IOSNativeFeaturesPreview.back == null)
		{
			IOSNativeFeaturesPreview.back = IOSNativePreviewBackButton.Create();
		}
	}

	private void OnGUI()
	{
		base.UpdateToStartPos();
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Game Center Examples", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Basic Features"))
		{
			base.LoadLevel("GameCenterGeneral");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Friends Load Example"))
		{
			base.LoadLevel("FriendsLoadExample");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Custom Leaderboard GUI"))
		{
			base.LoadLevel("CustomLeaderboardGUIExample");
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YLableStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Main Features", this.style);
		this.StartX = this.XStartPos;
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Billing"))
		{
			base.LoadLevel("BillingExample");
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "iCloud"))
		{
			base.LoadLevel("iCloudExampleScene");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Social Posting"))
		{
			base.LoadLevel("SocialPostingExample");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Local And Push Notifications"))
		{
			base.LoadLevel("NotificationExample");
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Replay Kit"))
		{
			base.LoadLevel("ReplayKitExampleScene");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Cloud Kit"))
		{
			base.LoadLevel("CloudKitExampleScene");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Game Saves"))
		{
			base.LoadLevel("GameSavesExample");
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YLableStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Networking", this.style);
		this.StartX = this.XStartPos;
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "TBM Multiplayer Example"))
		{
			base.LoadLevel("TMB_Multiplayer_Example");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "RTM Multiplayer Example"))
		{
			base.LoadLevel("RTM_Multiplayer_Example");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "P2P Game Example"))
		{
			base.LoadLevel("Peer-To-PeerGameExample");
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YLableStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Additional Features Features", this.style);
		this.StartX = this.XStartPos;
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Native Popups and Events"))
		{
			base.LoadLevel("PopUpsAndAppEventsHandler");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Media Player API"))
		{
			base.LoadLevel("MediaExample");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "IOS Native Actions"))
		{
			base.LoadLevel("NativeIOSActionsExample");
		}
	}

	public float x;

	public static IOSNativePreviewBackButton back;
}
