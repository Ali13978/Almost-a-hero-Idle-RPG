using System;
using System.Collections.Generic;
using PlayFab.EventsModels;
using UnityEngine;

namespace PlayFab.Public
{
	public class ScreenTimeTracker : IScreenTimeTracker
	{
		public void ClientSessionStart(string entityId, string entityType, string playFabUserId)
		{
			this.gameSessionID = Guid.NewGuid();
			this.entityKey.Id = entityId;
			this.entityKey.Type = entityType;
			EventContents eventContents = new EventContents();
			eventContents.Name = "client_session_start";
			eventContents.EventNamespace = "com.playfab.events.sessions";
			eventContents.Entity = this.entityKey;
			eventContents.OriginalTimestamp = new DateTime?(DateTime.UtcNow);
			Dictionary<string, object> payload = new Dictionary<string, object>
			{
				{
					"UserID",
					playFabUserId
				},
				{
					"DeviceType",
					SystemInfo.deviceType
				},
				{
					"DeviceModel",
					SystemInfo.deviceModel
				},
				{
					"OS",
					SystemInfo.operatingSystem
				},
				{
					"ClientSessionID",
					this.gameSessionID
				}
			};
			eventContents.Payload = payload;
			this.eventsRequests.Enqueue(eventContents);
			this.OnApplicationFocus(true);
		}

		public void OnApplicationFocus(bool isFocused)
		{
			EventContents eventContents = new EventContents();
			DateTime utcNow = DateTime.UtcNow;
			eventContents.Name = "client_focus_change";
			eventContents.EventNamespace = "com.playfab.events.sessions";
			eventContents.Entity = this.entityKey;
			double num = 0.0;
			if (this.initialFocus)
			{
				this.focusId = Guid.NewGuid();
			}
			if (isFocused)
			{
				this.focusOnDateTime = utcNow;
				this.focusId = Guid.NewGuid();
				if (!this.initialFocus)
				{
					num = (utcNow - this.focusOffDateTime).TotalSeconds;
					if (num < 0.0)
					{
						num = 0.0;
					}
				}
			}
			else
			{
				num = (utcNow - this.focusOnDateTime).TotalSeconds;
				if (num < 0.0)
				{
					num = 0.0;
				}
				this.focusOffDateTime = utcNow;
			}
			Dictionary<string, object> payload = new Dictionary<string, object>
			{
				{
					"FocusID",
					this.focusId
				},
				{
					"FocusState",
					isFocused
				},
				{
					"FocusStateDuration",
					num
				},
				{
					"EventTimestamp",
					utcNow
				},
				{
					"ClientSessionID",
					this.gameSessionID
				}
			};
			eventContents.OriginalTimestamp = new DateTime?(utcNow);
			eventContents.Payload = payload;
			this.eventsRequests.Enqueue(eventContents);
			this.initialFocus = false;
			if (!isFocused)
			{
				this.Send();
			}
		}

		public void Send()
		{
			if (PlayFabClientAPI.IsClientLoggedIn() && !this.isSending)
			{
				this.isSending = true;
				WriteEventsRequest writeEventsRequest = new WriteEventsRequest();
				writeEventsRequest.Events = new List<EventContents>();
				while (this.eventsRequests.Count > 0 && writeEventsRequest.Events.Count < 10)
				{
					EventContents item = this.eventsRequests.Dequeue();
					writeEventsRequest.Events.Add(item);
				}
				if (writeEventsRequest.Events.Count > 0)
				{
					PlayFabEventsAPI.WriteEvents(writeEventsRequest, new Action<WriteEventsResponse>(this.EventSentSuccessfulCallback), new Action<PlayFabError>(this.EventSentErrorCallback), null, null);
				}
				this.isSending = false;
			}
		}

		private void EventSentSuccessfulCallback(WriteEventsResponse response)
		{
		}

		private void EventSentErrorCallback(PlayFabError response)
		{
			UnityEngine.Debug.LogWarning("Failed to send session data. Error: " + response.GenerateErrorReport());
		}

		public void OnEnable()
		{
		}

		public void OnDisable()
		{
		}

		public void OnDestroy()
		{
		}

		public void OnApplicationQuit()
		{
			this.Send();
		}

		private Guid focusId;

		private Guid gameSessionID;

		private bool initialFocus = true;

		private bool isSending;

		private DateTime focusOffDateTime = DateTime.UtcNow;

		private DateTime focusOnDateTime = DateTime.UtcNow;

		private Queue<EventContents> eventsRequests = new Queue<EventContents>();

		private EntityKey entityKey = new EntityKey();

		private const string eventNamespace = "com.playfab.events.sessions";

		private const int maxBatchSizeInEvents = 10;
	}
}
