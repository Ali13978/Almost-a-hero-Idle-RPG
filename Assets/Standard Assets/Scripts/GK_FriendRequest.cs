using System;
using System.Collections.Generic;
using SA.Common.Util;

public class GK_FriendRequest
{
	public GK_FriendRequest()
	{
		this._Id = IdFactory.NextId;
	}

	public void addRecipientsWithEmailAddresses(params string[] emailAddresses)
	{
		foreach (string item in emailAddresses)
		{
			if (!this._Emails.Contains(item))
			{
				this._Emails.Add(item);
			}
		}
	}

	public void addRecipientPlayers(params GK_Player[] players)
	{
		foreach (GK_Player gk_Player in players)
		{
			if (!this._PlayersIds.Contains(gk_Player.Id))
			{
				this._PlayersIds.Add(gk_Player.Id);
			}
		}
	}

	public void Send()
	{
		GameCenterManager.SendFriendRequest(this, this._Emails, this._PlayersIds);
	}

	public int Id
	{
		get
		{
			return this._Id;
		}
	}

	private int _Id;

	private List<string> _PlayersIds = new List<string>();

	private List<string> _Emails = new List<string>();
}
