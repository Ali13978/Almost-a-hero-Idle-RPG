using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Zlib;
using PlayFab.SharedModels;
using UnityEngine;
using UnityEngine.Networking;

namespace PlayFab.Internal
{
	public class PlayFabUnityHttp : IPlayFabTransportPlugin, ITransportPlugin, IPlayFabPlugin
	{
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
			this._isInitialized = true;
		}

		public void Update()
		{
		}

		public void OnDestroy()
		{
		}

		public void SimpleGetCall(string fullUrl, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(PlayFabUnityHttp.SimpleCallCoroutine("get", fullUrl, null, successCallback, errorCallback));
		}

		public void SimplePutCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(PlayFabUnityHttp.SimpleCallCoroutine("put", fullUrl, payload, successCallback, errorCallback));
		}

		public void SimplePostCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(PlayFabUnityHttp.SimpleCallCoroutine("post", fullUrl, payload, successCallback, errorCallback));
		}

		private static IEnumerator SimpleCallCoroutine(string method, string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			if (payload == null)
			{
				using (UnityWebRequest www = UnityWebRequest.Get(fullUrl))
				{
					yield return www.SendWebRequest();
					if (!string.IsNullOrEmpty(www.error))
					{
						errorCallback(www.error);
					}
					else
					{
						successCallback(www.downloadHandler.data);
					}
				}
			}
			else
			{
				UnityWebRequest request;
				if (method == "put")
				{
					request = UnityWebRequest.Put(fullUrl, payload);
				}
				else
				{
					request = new UnityWebRequest(fullUrl, "POST");
					request.uploadHandler = new UploadHandlerRaw(payload);
					request.downloadHandler = new DownloadHandlerBuffer();
					request.SetRequestHeader("Content-Type", "application/json");
				}
				request.chunkedTransfer = false;
				yield return request.SendWebRequest();
				if (request.isNetworkError || request.isHttpError)
				{
					errorCallback(request.error);
				}
				else
				{
					successCallback(request.downloadHandler.data);
				}
			}
			yield break;
		}

		public void MakeApiCall(object reqContainerObj)
		{
			CallRequestContainer callRequestContainer = (CallRequestContainer)reqContainerObj;
			callRequestContainer.RequestHeaders["Content-Type"] = "application/json";
			if (PlayFabSettings.CompressApiData)
			{
				callRequestContainer.RequestHeaders["Content-Encoding"] = "GZIP";
				callRequestContainer.RequestHeaders["Accept-Encoding"] = "GZIP";
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestCompression))
					{
						gzipStream.Write(callRequestContainer.Payload, 0, callRequestContainer.Payload.Length);
					}
					callRequestContainer.Payload = memoryStream.ToArray();
				}
			}
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(this.Post(callRequestContainer));
		}

		private IEnumerator Post(CallRequestContainer reqContainer)
		{
			UnityWebRequest www = new UnityWebRequest(reqContainer.FullUrl)
			{
				uploadHandler = new UploadHandlerRaw(reqContainer.Payload),
				downloadHandler = new DownloadHandlerBuffer(),
				method = "POST"
			};
			foreach (KeyValuePair<string, string> keyValuePair in reqContainer.RequestHeaders)
			{
				if (!string.IsNullOrEmpty(keyValuePair.Key) && !string.IsNullOrEmpty(keyValuePair.Value))
				{
					www.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
				}
				else
				{
					UnityEngine.Debug.LogWarning("Null header: " + keyValuePair.Key + " = " + keyValuePair.Value);
				}
			}
			yield return www.SendWebRequest();
			if (!string.IsNullOrEmpty(www.error))
			{
				this.OnError(www.error, reqContainer);
			}
			else
			{
				try
				{
					byte[] data = www.downloadHandler.data;
					bool flag = data != null && data[0] == 31 && data[1] == 139;
					string response = "Unexpected error: cannot decompress GZIP stream.";
					if (!flag && data != null)
					{
						response = Encoding.UTF8.GetString(data, 0, data.Length);
					}
					if (flag)
					{
						MemoryStream stream = new MemoryStream(data);
						using (GZipStream gzipStream = new GZipStream(stream, CompressionMode.Decompress, false))
						{
							byte[] array = new byte[4096];
							using (MemoryStream memoryStream = new MemoryStream())
							{
								int count;
								while ((count = gzipStream.Read(array, 0, array.Length)) > 0)
								{
									memoryStream.Write(array, 0, count);
								}
								memoryStream.Seek(0L, SeekOrigin.Begin);
								StreamReader streamReader = new StreamReader(memoryStream);
								string response2 = streamReader.ReadToEnd();
								this.OnResponse(response2, reqContainer);
							}
						}
					}
					else
					{
						this.OnResponse(response, reqContainer);
					}
				}
				catch (Exception arg)
				{
					this.OnError("Unhandled error in PlayFabUnityHttp: " + arg, reqContainer);
				}
			}
			www.Dispose();
			yield break;
		}

		public int GetPendingMessages()
		{
			return this._pendingWwwMessages;
		}

		public void OnResponse(string response, CallRequestContainer reqContainer)
		{
			try
			{
				ISerializerPlugin plugin = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty);
				HttpResponseObject httpResponseObject = plugin.DeserializeObject<HttpResponseObject>(response);
				if (httpResponseObject.code == 200)
				{
					reqContainer.JsonResponse = plugin.SerializeObject(httpResponseObject.data);
					reqContainer.DeserializeResultJson();
					reqContainer.ApiResult.Request = reqContainer.ApiRequest;
					reqContainer.ApiResult.CustomData = reqContainer.CustomData;
					SingletonMonoBehaviour<PlayFabHttp>.instance.OnPlayFabApiResult(reqContainer.ApiResult);
					PlayFabDeviceUtil.OnPlayFabLogin(reqContainer.ApiResult);
					try
					{
						PlayFabHttp.SendEvent(reqContainer.ApiEndpoint, reqContainer.ApiRequest, reqContainer.ApiResult, ApiProcessingEventType.Post);
					}
					catch (Exception exception)
					{
						UnityEngine.Debug.LogException(exception);
					}
					try
					{
						reqContainer.InvokeSuccessCallback();
					}
					catch (Exception exception2)
					{
						UnityEngine.Debug.LogException(exception2);
					}
				}
				else if (reqContainer.ErrorCallback != null)
				{
					reqContainer.Error = PlayFabHttp.GeneratePlayFabError(reqContainer.ApiEndpoint, response, reqContainer.CustomData);
					PlayFabHttp.SendErrorEvent(reqContainer.ApiRequest, reqContainer.Error);
					reqContainer.ErrorCallback(reqContainer.Error);
				}
			}
			catch (Exception exception3)
			{
				UnityEngine.Debug.LogException(exception3);
			}
		}

		public void OnError(string error, CallRequestContainer reqContainer)
		{
			reqContainer.JsonResponse = error;
			if (reqContainer.ErrorCallback != null)
			{
				reqContainer.Error = PlayFabHttp.GeneratePlayFabError(reqContainer.ApiEndpoint, reqContainer.JsonResponse, reqContainer.CustomData);
				PlayFabHttp.SendErrorEvent(reqContainer.ApiRequest, reqContainer.Error);
				reqContainer.ErrorCallback(reqContainer.Error);
			}
		}

		private bool _isInitialized;

		private readonly int _pendingWwwMessages;
	}
}
