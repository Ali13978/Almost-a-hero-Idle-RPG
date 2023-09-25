using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using PlayFab.SharedModels;
using UnityEngine;

namespace PlayFab.Internal
{
	public class PlayFabWebRequest : IPlayFabTransportPlugin, ITransportPlugin, IPlayFabPlugin
	{
		public static void SkipCertificateValidation()
		{
			RemoteCertificateValidationCallback serverCertificateValidationCallback = new RemoteCertificateValidationCallback(PlayFabWebRequest.AcceptAllCertifications);
			ServicePointManager.ServerCertificateValidationCallback = serverCertificateValidationCallback;
			PlayFabWebRequest.certValidationSet = true;
		}

		public static RemoteCertificateValidationCallback CustomCertValidationHook
		{
			set
			{
				ServicePointManager.ServerCertificateValidationCallback = value;
				PlayFabWebRequest.certValidationSet = true;
			}
		}

		public string AuthKey { get; set; }

		public string EntityToken { get; set; }

		public bool IsInitialized
		{
			get
			{
				return this._isInitialized;
			}
		}

		public void Initialize()
		{
			this.SetupCertificates();
			PlayFabWebRequest._isApplicationPlaying = true;
			PlayFabWebRequest._unityVersion = Application.unityVersion;
			this._isInitialized = true;
		}

		public void OnDestroy()
		{
			PlayFabWebRequest._isApplicationPlaying = false;
			object resultQueueTransferThread = PlayFabWebRequest.ResultQueueTransferThread;
			lock (resultQueueTransferThread)
			{
				PlayFabWebRequest.ResultQueueTransferThread.Clear();
			}
			object activeRequests = PlayFabWebRequest.ActiveRequests;
			lock (activeRequests)
			{
				PlayFabWebRequest.ActiveRequests.Clear();
			}
			object threadLock = PlayFabWebRequest._ThreadLock;
			lock (threadLock)
			{
				PlayFabWebRequest._requestQueueThread = null;
			}
		}

		private void SetupCertificates()
		{
			ServicePointManager.DefaultConnectionLimit = 10;
			ServicePointManager.Expect100Continue = false;
			if (!PlayFabWebRequest.certValidationSet)
			{
				UnityEngine.Debug.LogWarning("PlayFab API calls will likely fail because you have not set up a HttpWebRequest certificate validation mechanism");
				UnityEngine.Debug.LogWarning("Please set a validation callback into PlayFab.Internal.PlayFabWebRequest.CustomCertValidationHook, or set PlayFab.Internal.PlayFabWebRequest.SkipCertificateValidation()");
			}
		}

		private static bool AcceptAllCertifications(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		public void SimpleGetCall(string fullUrl, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			Thread thread = new Thread(delegate()
			{
				this.SimpleHttpsWorker("GET", fullUrl, null, successCallback, errorCallback);
			});
			thread.Start();
		}

		public void SimplePutCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			Thread thread = new Thread(delegate()
			{
				this.SimpleHttpsWorker("PUT", fullUrl, payload, successCallback, errorCallback);
			});
			thread.Start();
		}

		public void SimplePostCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			Thread thread = new Thread(delegate()
			{
				this.SimpleHttpsWorker("POST", fullUrl, payload, successCallback, errorCallback);
			});
			thread.Start();
		}

		private void SimpleHttpsWorker(string httpMethod, string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(fullUrl);
			httpWebRequest.UserAgent = "UnityEngine-Unity; Version: " + PlayFabWebRequest._unityVersion;
			httpWebRequest.Method = httpMethod;
			httpWebRequest.KeepAlive = PlayFabSettings.RequestKeepAlive;
			httpWebRequest.Timeout = PlayFabSettings.RequestTimeout;
			httpWebRequest.AllowWriteStreamBuffering = false;
			httpWebRequest.ReadWriteTimeout = PlayFabSettings.RequestTimeout;
			if (payload != null)
			{
				httpWebRequest.ContentLength = payload.LongLength;
				using (Stream requestStream = httpWebRequest.GetRequestStream())
				{
					requestStream.Write(payload, 0, payload.Length);
				}
			}
			try
			{
				WebResponse response = httpWebRequest.GetResponse();
				byte[] array = null;
				using (Stream responseStream = response.GetResponseStream())
				{
					if (responseStream != null)
					{
						array = new byte[response.ContentLength];
						responseStream.Read(array, 0, array.Length);
					}
				}
				successCallback(array);
			}
			catch (WebException ex)
			{
				try
				{
					using (Stream responseStream2 = ex.Response.GetResponseStream())
					{
						if (responseStream2 != null)
						{
							using (StreamReader streamReader = new StreamReader(responseStream2))
							{
								errorCallback(streamReader.ReadToEnd());
							}
						}
					}
				}
				catch (Exception exception)
				{
					UnityEngine.Debug.LogException(exception);
				}
			}
			catch (Exception exception2)
			{
				UnityEngine.Debug.LogException(exception2);
			}
		}

		public void MakeApiCall(object reqContainerObj)
		{
			CallRequestContainer callRequestContainer = (CallRequestContainer)reqContainerObj;
			callRequestContainer.HttpState = HttpRequestState.Idle;
			object activeRequests = PlayFabWebRequest.ActiveRequests;
			lock (activeRequests)
			{
				PlayFabWebRequest.ActiveRequests.Insert(0, callRequestContainer);
			}
			PlayFabWebRequest.ActivateThreadWorker();
		}

		private static void ActivateThreadWorker()
		{
			object threadLock = PlayFabWebRequest._ThreadLock;
			lock (threadLock)
			{
				if (PlayFabWebRequest._requestQueueThread == null)
				{
					if (PlayFabWebRequest._003C_003Ef__mg_0024cache0 == null)
					{
						PlayFabWebRequest._003C_003Ef__mg_0024cache0 = new ThreadStart(PlayFabWebRequest.WorkerThreadMainLoop);
					}
					PlayFabWebRequest._requestQueueThread = new Thread(PlayFabWebRequest._003C_003Ef__mg_0024cache0);
					PlayFabWebRequest._requestQueueThread.Start();
				}
			}
		}

		private static void WorkerThreadMainLoop()
		{
			try
			{
				object threadLock = PlayFabWebRequest._ThreadLock;
				lock (threadLock)
				{
					PlayFabWebRequest._threadKillTime = DateTime.UtcNow + PlayFabWebRequest.ThreadKillTimeout;
				}
				List<CallRequestContainer> list = new List<CallRequestContainer>();
				bool flag;
				do
				{
					object activeRequests = PlayFabWebRequest.ActiveRequests;
					lock (activeRequests)
					{
						list.AddRange(PlayFabWebRequest.ActiveRequests);
						PlayFabWebRequest.ActiveRequests.Clear();
						PlayFabWebRequest._activeCallCount = list.Count;
					}
					int count = list.Count;
					for (int i = count - 1; i >= 0; i--)
					{
						switch (list[i].HttpState)
						{
						case HttpRequestState.Sent:
							if (list[i].HttpRequest.HaveResponse)
							{
								PlayFabWebRequest.ProcessHttpResponse(list[i]);
							}
							break;
						case HttpRequestState.Received:
							PlayFabWebRequest.ProcessJsonResponse(list[i]);
							list.RemoveAt(i);
							break;
						case HttpRequestState.Idle:
							PlayFabWebRequest.Post(list[i]);
							break;
						case HttpRequestState.Error:
							list.RemoveAt(i);
							break;
						}
					}
					object threadLock2 = PlayFabWebRequest._ThreadLock;
					lock (threadLock2)
					{
						DateTime utcNow = DateTime.UtcNow;
						if (count > 0 && PlayFabWebRequest._isApplicationPlaying)
						{
							PlayFabWebRequest._threadKillTime = utcNow + PlayFabWebRequest.ThreadKillTimeout;
						}
						flag = (utcNow <= PlayFabWebRequest._threadKillTime);
						if (!flag)
						{
							PlayFabWebRequest._requestQueueThread = null;
						}
					}
					Thread.Sleep(1);
				}
				while (flag);
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
				PlayFabWebRequest._requestQueueThread = null;
			}
		}

		private static void Post(CallRequestContainer reqContainer)
		{
			try
			{
				reqContainer.HttpRequest = (HttpWebRequest)WebRequest.Create(reqContainer.FullUrl);
				reqContainer.HttpRequest.UserAgent = "UnityEngine-Unity; Version: " + PlayFabWebRequest._unityVersion;
				reqContainer.HttpRequest.SendChunked = false;
				reqContainer.HttpRequest.Proxy = null;
				foreach (KeyValuePair<string, string> keyValuePair in reqContainer.RequestHeaders)
				{
					reqContainer.HttpRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
				}
				reqContainer.HttpRequest.ContentType = "application/json";
				reqContainer.HttpRequest.Method = "POST";
				reqContainer.HttpRequest.KeepAlive = PlayFabSettings.RequestKeepAlive;
				reqContainer.HttpRequest.Timeout = PlayFabSettings.RequestTimeout;
				reqContainer.HttpRequest.AllowWriteStreamBuffering = false;
				reqContainer.HttpRequest.Proxy = null;
				reqContainer.HttpRequest.ContentLength = reqContainer.Payload.LongLength;
				reqContainer.HttpRequest.ReadWriteTimeout = PlayFabSettings.RequestTimeout;
				using (Stream requestStream = reqContainer.HttpRequest.GetRequestStream())
				{
					requestStream.Write(reqContainer.Payload, 0, reqContainer.Payload.Length);
				}
				reqContainer.HttpState = HttpRequestState.Sent;
			}
			catch (WebException ex)
			{
				reqContainer.JsonResponse = (PlayFabWebRequest.ResponseToString(ex.Response) ?? (ex.Status + ": WebException making http request to: " + reqContainer.FullUrl));
				WebException exception = new WebException(reqContainer.JsonResponse, ex);
				UnityEngine.Debug.LogException(exception);
				PlayFabWebRequest.QueueRequestError(reqContainer);
			}
			catch (Exception innerException)
			{
				reqContainer.JsonResponse = "Unhandled exception in Post : " + reqContainer.FullUrl;
				Exception exception2 = new Exception(reqContainer.JsonResponse, innerException);
				UnityEngine.Debug.LogException(exception2);
				PlayFabWebRequest.QueueRequestError(reqContainer);
			}
		}

		private static void ProcessHttpResponse(CallRequestContainer reqContainer)
		{
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)reqContainer.HttpRequest.GetResponse();
				if (httpWebResponse.StatusCode == HttpStatusCode.OK)
				{
					reqContainer.JsonResponse = PlayFabWebRequest.ResponseToString(httpWebResponse);
				}
				if (httpWebResponse.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(reqContainer.JsonResponse))
				{
					reqContainer.JsonResponse = (reqContainer.JsonResponse ?? "No response from server");
					PlayFabWebRequest.QueueRequestError(reqContainer);
				}
				else
				{
					reqContainer.HttpState = HttpRequestState.Received;
				}
			}
			catch (Exception innerException)
			{
				string text = "Unhandled exception in ProcessHttpResponse : " + reqContainer.FullUrl;
				reqContainer.JsonResponse = (reqContainer.JsonResponse ?? text);
				Exception exception = new Exception(text, innerException);
				UnityEngine.Debug.LogException(exception);
				PlayFabWebRequest.QueueRequestError(reqContainer);
			}
		}

		private static void QueueRequestError(CallRequestContainer reqContainer)
		{
			reqContainer.Error = PlayFabHttp.GeneratePlayFabError(reqContainer.ApiEndpoint, reqContainer.JsonResponse, reqContainer.CustomData);
			reqContainer.HttpState = HttpRequestState.Error;
			object resultQueueTransferThread = PlayFabWebRequest.ResultQueueTransferThread;
			lock (resultQueueTransferThread)
			{
				PlayFabWebRequest.ResultQueueTransferThread.Enqueue(delegate
				{
					PlayFabHttp.SendErrorEvent(reqContainer.ApiRequest, reqContainer.Error);
					if (reqContainer.ErrorCallback != null)
					{
						reqContainer.ErrorCallback(reqContainer.Error);
					}
				});
			}
		}

		private static void ProcessJsonResponse(CallRequestContainer reqContainer)
		{
			try
			{
				ISerializerPlugin plugin = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty);
				HttpResponseObject httpResponseObject = plugin.DeserializeObject<HttpResponseObject>(reqContainer.JsonResponse);
				if (httpResponseObject == null || httpResponseObject.code != 200)
				{
					PlayFabWebRequest.QueueRequestError(reqContainer);
				}
				else
				{
					reqContainer.JsonResponse = plugin.SerializeObject(httpResponseObject.data);
					reqContainer.DeserializeResultJson();
					reqContainer.ApiResult.Request = reqContainer.ApiRequest;
					reqContainer.ApiResult.CustomData = reqContainer.CustomData;
					SingletonMonoBehaviour<PlayFabHttp>.instance.OnPlayFabApiResult(reqContainer.ApiResult);
					object resultQueueTransferThread = PlayFabWebRequest.ResultQueueTransferThread;
					lock (resultQueueTransferThread)
					{
						PlayFabWebRequest.ResultQueueTransferThread.Enqueue(delegate
						{
							PlayFabDeviceUtil.OnPlayFabLogin(reqContainer.ApiResult);
						});
					}
					object resultQueueTransferThread2 = PlayFabWebRequest.ResultQueueTransferThread;
					lock (resultQueueTransferThread2)
					{
						PlayFabWebRequest.ResultQueueTransferThread.Enqueue(delegate
						{
							try
							{
								PlayFabHttp.SendEvent(reqContainer.ApiEndpoint, reqContainer.ApiRequest, reqContainer.ApiResult, ApiProcessingEventType.Post);
								reqContainer.InvokeSuccessCallback();
							}
							catch (Exception exception2)
							{
								UnityEngine.Debug.LogException(exception2);
							}
						});
					}
				}
			}
			catch (Exception innerException)
			{
				string text = "Unhandled exception in ProcessJsonResponse : " + reqContainer.FullUrl;
				reqContainer.JsonResponse = (reqContainer.JsonResponse ?? text);
				Exception exception = new Exception(text, innerException);
				UnityEngine.Debug.LogException(exception);
				PlayFabWebRequest.QueueRequestError(reqContainer);
			}
		}

		public void Update()
		{
			object resultQueueTransferThread = PlayFabWebRequest.ResultQueueTransferThread;
			lock (resultQueueTransferThread)
			{
				while (PlayFabWebRequest.ResultQueueTransferThread.Count > 0)
				{
					Action item = PlayFabWebRequest.ResultQueueTransferThread.Dequeue();
					PlayFabWebRequest.ResultQueueMainThread.Enqueue(item);
				}
			}
			while (PlayFabWebRequest.ResultQueueMainThread.Count > 0)
			{
				Action action = PlayFabWebRequest.ResultQueueMainThread.Dequeue();
				action();
			}
		}

		private static string ResponseToString(WebResponse webResponse)
		{
			if (webResponse == null)
			{
				return null;
			}
			string result;
			try
			{
				using (Stream responseStream = webResponse.GetResponseStream())
				{
					if (responseStream == null)
					{
						result = null;
					}
					else
					{
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							result = streamReader.ReadToEnd();
						}
					}
				}
			}
			catch (WebException ex)
			{
				try
				{
					using (Stream responseStream2 = ex.Response.GetResponseStream())
					{
						if (responseStream2 == null)
						{
							result = null;
						}
						else
						{
							using (StreamReader streamReader2 = new StreamReader(responseStream2))
							{
								result = streamReader2.ReadToEnd();
							}
						}
					}
				}
				catch (Exception exception)
				{
					UnityEngine.Debug.LogException(exception);
					result = null;
				}
			}
			catch (Exception exception2)
			{
				UnityEngine.Debug.LogException(exception2);
				result = null;
			}
			return result;
		}

		public int GetPendingMessages()
		{
			int num = 0;
			object activeRequests = PlayFabWebRequest.ActiveRequests;
			lock (activeRequests)
			{
				num += PlayFabWebRequest.ActiveRequests.Count + PlayFabWebRequest._activeCallCount;
			}
			object resultQueueTransferThread = PlayFabWebRequest.ResultQueueTransferThread;
			lock (resultQueueTransferThread)
			{
				num += PlayFabWebRequest.ResultQueueTransferThread.Count;
			}
			return num;
		}

		private static readonly Queue<Action> ResultQueueTransferThread = new Queue<Action>();

		private static readonly Queue<Action> ResultQueueMainThread = new Queue<Action>();

		private static readonly List<CallRequestContainer> ActiveRequests = new List<CallRequestContainer>();

		private static bool certValidationSet = false;

		private static Thread _requestQueueThread;

		private static readonly object _ThreadLock = new object();

		private static readonly TimeSpan ThreadKillTimeout = TimeSpan.FromSeconds(60.0);

		private static DateTime _threadKillTime = DateTime.UtcNow + PlayFabWebRequest.ThreadKillTimeout;

		private static bool _isApplicationPlaying;

		private static int _activeCallCount;

		private static string _unityVersion;

		private bool _isInitialized;

		[CompilerGenerated]
		private static ThreadStart _003C_003Ef__mg_0024cache0;
	}
}
