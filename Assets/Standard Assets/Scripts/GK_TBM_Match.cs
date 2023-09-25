using System;
using System.Collections.Generic;
using System.Text;

public class GK_TBM_Match
{
	public void SetData(string val)
	{
		byte[] data = Convert.FromBase64String(val);
		this.Data = data;
	}

	public string UTF8StringData
	{
		get
		{
			if (this.Data != null)
			{
				return Encoding.UTF8.GetString(this.Data);
			}
			return string.Empty;
		}
	}

	public GK_TBM_Participant GetParticipantByPlayerId(string playerId)
	{
		foreach (GK_TBM_Participant gk_TBM_Participant in this.Participants)
		{
			if (gk_TBM_Participant.Player == null)
			{
				if (playerId.Length == 0)
				{
					return gk_TBM_Participant;
				}
			}
			else if (playerId.Equals(gk_TBM_Participant.Player.Id))
			{
				return gk_TBM_Participant;
			}
		}
		return null;
	}

	public GK_TBM_Participant LocalParticipant
	{
		get
		{
			foreach (GK_TBM_Participant gk_TBM_Participant in this.Participants)
			{
				if (gk_TBM_Participant.Player != null && gk_TBM_Participant.PlayerId.Equals(GameCenterManager.Player.Id))
				{
					return gk_TBM_Participant;
				}
			}
			return null;
		}
	}

	public string Id;

	public string Message;

	public GK_TBM_Participant CurrentParticipant;

	public DateTime CreationTimestamp;

	public byte[] Data;

	public GK_TurnBasedMatchStatus Status;

	public List<GK_TBM_Participant> Participants;
}
