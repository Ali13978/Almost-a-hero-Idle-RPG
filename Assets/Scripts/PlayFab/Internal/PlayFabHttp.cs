using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using PlayFab.AuthenticationModels;
using PlayFab.ClientModels;
using PlayFab.Json;
using PlayFab.Public;
using PlayFab.SharedModels;
using UnityEngine;

namespace PlayFab.Internal
{
	public class PlayFabHttp : SingletonMonoBehaviour<PlayFabHttp>
	{
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event PlayFabHttp.ApiProcessingEvent<ApiProcessingEventArgs> ApiProcessingEventHandler;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event PlayFabHttp.ApiProcessErrorEvent ApiProcessingErrorEventHandler;

		public static int GetPendingMessages()
		{
			ITransportPlugin plugin = PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, string.Empty);
			return (!plugin.IsInitialized) ? 0 : plugin.GetPendingMessages();
		}

		[Obsolete("This method is deprecated, please use PlayFab.PluginManager.SetPlugin(..) instead.", false)]
		public static void SetHttp<THttpObject>(THttpObject httpObj) where THttpObject : ITransportPlugin
		{
			PluginManager.SetPlugin(httpObj, PluginContract.PlayFab_Transport, string.Empty);
		}

		[Obsolete("This method is deprecated, please use PlayFab.IPlayFabTransportPlugin.AuthKey property instead.", false)]
		public static void SetAuthKey(string authKey)
		{
			PluginManager.GetPlugin<IPlayFabTransportPlugin>(PluginContract.PlayFab_Transport, string.Empty).AuthKey = authKey;
		}

		public static void InitializeHttp()
		{
			if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
			{
				throw new PlayFabException(PlayFabExceptionCode.TitleNotSet, "You must set PlayFabSettings.TitleId before making API Calls.");
			}
			ITransportPlugin plugin = PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, string.Empty);
			if (plugin.IsInitialized)
			{
				return;
			}
			Application.runInBackground = true;
			plugin.Initialize();
			SingletonMonoBehaviour<PlayFabHttp>.CreateInstance();
		}

		public static void InitializeLogger(IPlayFabLogger setLogger = null)
		{
			if (PlayFabHttp._logger != null)
			{
				throw new InvalidOperationException("Once initialized, the logger cannot be reset.");
			}
			if (setLogger == null)
			{
				setLogger = new PlayFabLogger();
			}
			PlayFabHttp._logger = setLogger;
		}

		public static void InitializeScreenTimeTracker(string entityId, string entityType, string playFabUserId)
		{
			PlayFabHttp.screenTimeTracker.ClientSessionStart(entityId, entityType, playFabUserId);
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(PlayFabHttp.SendScreenTimeEvents(5f));
		}

		private static IEnumerator SendScreenTimeEvents(float secondsBetweenBatches)
		{
			WaitForSeconds delay = new WaitForSeconds(secondsBetweenBatches);
			while (!PlayFabSettings.DisableFocusTimeCollection)
			{
				PlayFabHttp.screenTimeTracker.Send();
				yield return delay;
			}
			yield break;
		}

		public static void SimpleGetCall(string fullUrl, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			PlayFabHttp.InitializeHttp();
			PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, string.Empty).SimpleGetCall(fullUrl, successCallback, errorCallback);
		}

		public static void SimplePutCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			PlayFabHttp.InitializeHttp();
			PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, string.Empty).SimplePutCall(fullUrl, payload, successCallback, errorCallback);
		}

		public static void SimplePostCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			PlayFabHttp.InitializeHttp();
			PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, string.Empty).SimplePostCall(fullUrl, payload, successCallback, errorCallback);
		}

		protected internal static void MakeApiCall<TResult>(string apiEndpoint, PlayFabRequestCommon request, AuthType authType, Action<TResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null, bool allowQueueing = false) where TResult : PlayFabResultCommon
		{
			PlayFabHttp.InitializeHttp();
			PlayFabHttp.SendEvent(apiEndpoint, request, null, ApiProcessingEventType.Pre);
			ISerializerPlugin serializer = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty);
			CallRequestContainer reqContainer = new CallRequestContainer
			{
				ApiEndpoint = apiEndpoint,
				FullUrl = PlayFabSettings.GetFullUrl(apiEndpoint, PlayFabSettings.RequestGetParams),
				CustomData = customData,
				Payload = Encoding.UTF8.GetBytes(serializer.SerializeObject(request)),
				ApiRequest = request,
				ErrorCallback = errorCallback,
				RequestHeaders = (extraHeaders ?? new Dictionary<string, string>())
			};
			foreach (KeyValuePair<string, string> keyValuePair in PlayFabHttp.GlobalHeaderInjection)
			{
				if (!reqContainer.RequestHeaders.ContainsKey(keyValuePair.Key))
				{
					reqContainer.RequestHeaders[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			IPlayFabTransportPlugin plugin = PluginManager.GetPlugin<IPlayFabTransportPlugin>(PluginContract.PlayFab_Transport, string.Empty);
			reqContainer.RequestHeaders["X-ReportErrorAsSuccess"] = "true";
			reqContainer.RequestHeaders["X-PlayFabSDK"] = "UnitySDK-2.58.181218";
			if (authType != AuthType.LoginSession)
			{
				if (authType == AuthType.EntityToken)
				{
					reqContainer.RequestHeaders["X-EntityToken"] = plugin.EntityToken;
				}
			}
			else
			{
				reqContainer.RequestHeaders["X-Authorization"] = plugin.AuthKey;
			}
			reqContainer.DeserializeResultJson = delegate()
			{
				reqContainer.ApiResult = serializer.DeserializeObject<TResult>(reqContainer.JsonResponse);
			};
			reqContainer.InvokeSuccessCallback = delegate()
			{
				if (resultCallback != null)
				{
					resultCallback((TResult)((object)reqContainer.ApiResult));
				}
			};
			if (allowQueueing && PlayFabHttp._apiCallQueue != null)
			{
				for (int i = PlayFabHttp._apiCallQueue.Count - 1; i >= 0; i--)
				{
					if (PlayFabHttp._apiCallQueue[i].ApiEndpoint == apiEndpoint)
					{
						PlayFabHttp._apiCallQueue.RemoveAt(i);
					}
				}
				PlayFabHttp._apiCallQueue.Add(reqContainer);
			}
			else
			{
				plugin.MakeApiCall(reqContainer);
			}
		}

		internal void OnPlayFabApiResult(PlayFabResultCommon result)
		{
			GetEntityTokenResponse getEntityTokenResponse = result as GetEntityTokenResponse;
			if (getEntityTokenResponse != null)
			{
				IPlayFabTransportPlugin plugin = PluginManager.GetPlugin<IPlayFabTransportPlugin>(PluginContract.PlayFab_Transport, string.Empty);
				plugin.EntityToken = getEntityTokenResponse.EntityToken;
			}
			LoginResult loginResult = result as LoginResult;
			RegisterPlayFabUserResult registerPlayFabUserResult = result as RegisterPlayFabUserResult;
			if (loginResult != null)
			{
				IPlayFabTransportPlugin plugin2 = PluginManager.GetPlugin<IPlayFabTransportPlugin>(PluginContract.PlayFab_Transport, string.Empty);
				plugin2.AuthKey = loginResult.SessionTicket;
				if (loginResult.EntityToken != null)
				{
					plugin2.EntityToken = loginResult.EntityToken.EntityToken;
				}
			}
			else if (registerPlayFabUserResult != null)
			{
				IPlayFabTransportPlugin plugin3 = PluginManager.GetPlugin<IPlayFabTransportPlugin>(PluginContract.PlayFab_Transport, string.Empty);
				plugin3.AuthKey = registerPlayFabUserResult.SessionTicket;
				if (registerPlayFabUserResult.EntityToken != null)
				{
					plugin3.EntityToken = registerPlayFabUserResult.EntityToken.EntityToken;
				}
			}
		}

		private void OnEnable()
		{
			if (PlayFabHttp._logger != null)
			{
				PlayFabHttp._logger.OnEnable();
			}
			if (PlayFabHttp.screenTimeTracker != null && !PlayFabSettings.DisableFocusTimeCollection)
			{
				PlayFabHttp.screenTimeTracker.OnEnable();
			}
		}

		private void OnDisable()
		{
			if (PlayFabHttp._logger != null)
			{
				PlayFabHttp._logger.OnDisable();
			}
			if (PlayFabHttp.screenTimeTracker != null && !PlayFabSettings.DisableFocusTimeCollection)
			{
				PlayFabHttp.screenTimeTracker.OnDisable();
			}
		}

		private void OnDestroy()
		{
			ITransportPlugin plugin = PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, string.Empty);
			if (plugin.IsInitialized)
			{
				plugin.OnDestroy();
			}
			if (PlayFabHttp._logger != null)
			{
				PlayFabHttp._logger.OnDestroy();
			}
			if (PlayFabHttp.screenTimeTracker != null && !PlayFabSettings.DisableFocusTimeCollection)
			{
				PlayFabHttp.screenTimeTracker.OnDestroy();
			}
		}

		public void OnApplicationFocus(bool isFocused)
		{
			if (PlayFabHttp.screenTimeTracker != null && !PlayFabSettings.DisableFocusTimeCollection)
			{
				PlayFabHttp.screenTimeTracker.OnApplicationFocus(isFocused);
			}
		}

		public void OnApplicationQuit()
		{
			if (PlayFabHttp.screenTimeTracker != null && !PlayFabSettings.DisableFocusTimeCollection)
			{
				PlayFabHttp.screenTimeTracker.OnApplicationQuit();
			}
		}

		private void Update()
		{
			ITransportPlugin plugin = PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, string.Empty);
			if (plugin.IsInitialized)
			{
				if (PlayFabHttp._apiCallQueue != null)
				{
					foreach (CallRequestContainer reqContainer in PlayFabHttp._apiCallQueue)
					{
						plugin.MakeApiCall(reqContainer);
					}
					PlayFabHttp._apiCallQueue = null;
				}
				plugin.Update();
			}
		}

		public static bool IsClientLoggedIn()
		{
			IPlayFabTransportPlugin plugin = PluginManager.GetPlugin<IPlayFabTransportPlugin>(PluginContract.PlayFab_Transport, string.Empty);
			return plugin.IsInitialized && !string.IsNullOrEmpty(plugin.AuthKey);
		}

		public static void ForgetAllCredentials()
		{
			IPlayFabTransportPlugin plugin = PluginManager.GetPlugin<IPlayFabTransportPlugin>(PluginContract.PlayFab_Transport, string.Empty);
			if (plugin.IsInitialized)
			{
				plugin.AuthKey = null;
				plugin.EntityToken = null;
			}
		}

		protected internal static PlayFabError GeneratePlayFabError(string apiEndpoint, string json, object customData)
		{
			JsonObject jsonObject = null;
			Dictionary<string, List<string>> errorDetails = null;
			ISerializerPlugin plugin = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty);
			try
			{
				jsonObject = plugin.DeserializeObject<JsonObject>(json);
			}
			catch (Exception)
			{
			}
			try
			{
				if (jsonObject != null && jsonObject.ContainsKey("errorDetails"))
				{
					errorDetails = plugin.DeserializeObject<Dictionary<string, List<string>>>(jsonObject["errorDetails"].ToString());
				}
			}
			catch (Exception)
			{
			}
			return new PlayFabError
			{
				ApiEndpoint = apiEndpoint,
				HttpCode = ((jsonObject == null || !jsonObject.ContainsKey("code")) ? 400 : Convert.ToInt32(jsonObject["code"])),
				HttpStatus = ((jsonObject == null || !jsonObject.ContainsKey("status")) ? "BadRequest" : ((string)jsonObject["status"])),
				Error = (PlayFabErrorCode)((jsonObject == null || !jsonObject.ContainsKey("errorCode")) ? 1123 : Convert.ToInt32(jsonObject["errorCode"])),
				ErrorMessage = ((jsonObject == null || !jsonObject.ContainsKey("errorMessage")) ? json : ((string)jsonObject["errorMessage"])),
				ErrorDetails = errorDetails,
				CustomData = customData
			};
		}

		protected internal static void SendErrorEvent(PlayFabRequestCommon request, PlayFabError error)
		{
			if (PlayFabHttp.ApiProcessingErrorEventHandler == null)
			{
				return;
			}
			try
			{
				PlayFabHttp.ApiProcessingErrorEventHandler(request, error);
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}

		protected internal static void SendEvent(string apiEndpoint, PlayFabRequestCommon request, PlayFabResultCommon result, ApiProcessingEventType eventType)
		{
			if (PlayFabHttp.ApiProcessingEventHandler == null)
			{
				return;
			}
			try
			{
				PlayFabHttp.ApiProcessingEventHandler(new ApiProcessingEventArgs
				{
					ApiEndpoint = apiEndpoint,
					EventType = eventType,
					Request = request,
					Result = result
				});
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}

		protected internal static void ClearAllEvents()
		{
			PlayFabHttp.ApiProcessingEventHandler = null;
			PlayFabHttp.ApiProcessingErrorEventHandler = null;
		}

		private static List<CallRequestContainer> _apiCallQueue = new List<CallRequestContainer>();

		public static readonly Dictionary<string, string> GlobalHeaderInjection = new Dictionary<string, string>();

		private static IPlayFabLogger _logger;

		private static IScreenTimeTracker screenTimeTracker = new ScreenTimeTracker();

		private const float delayBetweenBatches = 5f;

		public delegate void ApiProcessingEvent<in TEventArgs>(TEventArgs e);

		public delegate void ApiProcessErrorEvent(PlayFabRequestCommon request, PlayFabError error);
	}
}
