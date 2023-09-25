using System;
using System.Collections;
using System.IO;
using System.Text;
using Ionic.Zlib;
using PlayFab.SharedModels;
using UnityEngine;
using UnityEngine.Networking;

namespace PlayFab.Internal
{
	public class PlayFabWww : IPlayFabTransportPlugin, ITransportPlugin, IPlayFabPlugin
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
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(PlayFabWww.SimpleCallCoroutine("get", fullUrl, null, successCallback, errorCallback));
		}

		public void SimplePutCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(PlayFabWww.SimpleCallCoroutine("put", fullUrl, payload, successCallback, errorCallback));
		}

		public void SimplePostCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(PlayFabWww.SimpleCallCoroutine("post", fullUrl, payload, successCallback, errorCallback));
		}

		private static IEnumerator SimpleCallCoroutine(string method, string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			if (payload == null)
			{
				WWW www = new WWW(fullUrl);
				yield return www;
				if (!string.IsNullOrEmpty(www.error))
				{
					errorCallback(www.error);
				}
				else
				{
					successCallback(www.bytes);
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
					string @string = Encoding.UTF8.GetString(payload, 0, payload.Length);
					request = UnityWebRequest.PostWwwForm(fullUrl, @string);
				}
				request.chunkedTransfer = false;
				request.SendWebRequest();
				while (request.uploadProgress < 1f && request.downloadProgress < 1f)
				{
					yield return 1;
				}
				if (!string.IsNullOrEmpty(request.error))
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
			CallRequestContainer reqContainer = (CallRequestContainer)reqContainerObj;
			reqContainer.RequestHeaders["Content-Type"] = "application/json";
			if (PlayFabSettings.CompressApiData)
			{
				reqContainer.RequestHeaders["Content-Encoding"] = "GZIP";
				reqContainer.RequestHeaders["Accept-Encoding"] = "GZIP";
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestCompression))
					{
						gzipStream.Write(reqContainer.Payload, 0, reqContainer.Payload.Length);
					}
					reqContainer.Payload = memoryStream.ToArray();
				}
			}
			WWW www = new WWW(reqContainer.FullUrl, reqContainer.Payload, reqContainer.RequestHeaders);
			Action<string> wwwSuccessCallback = delegate(string response)
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
			};
			Action<string> wwwErrorCallback = delegate(string errorCb)
			{
				reqContainer.JsonResponse = errorCb;
				if (reqContainer.ErrorCallback != null)
				{
					reqContainer.Error = PlayFabHttp.GeneratePlayFabError(reqContainer.ApiEndpoint, reqContainer.JsonResponse, reqContainer.CustomData);
					PlayFabHttp.SendErrorEvent(reqContainer.ApiRequest, reqContainer.Error);
					reqContainer.ErrorCallback(reqContainer.Error);
				}
			};
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(this.PostPlayFabApiCall(www, wwwSuccessCallback, wwwErrorCallback));
		}

		private IEnumerator PostPlayFabApiCall(WWW www, Action<string> wwwSuccessCallback, Action<string> wwwErrorCallback)
		{
			yield return www;
			if (!string.IsNullOrEmpty(www.error))
			{
				wwwErrorCallback(www.error);
			}
			else
			{
				try
				{
					byte[] bytes = www.bytes;
					bool flag = bytes != null && bytes[0] == 31 && bytes[1] == 139;
					string obj = "Unexpected error: cannot decompress GZIP stream.";
					if (!flag && bytes != null)
					{
						obj = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
					}
					if (flag)
					{
						MemoryStream stream = new MemoryStream(bytes);
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
								string obj2 = streamReader.ReadToEnd();
								wwwSuccessCallback(obj2);
							}
						}
					}
					else
					{
						wwwSuccessCallback(obj);
					}
				}
				catch (Exception arg)
				{
					wwwErrorCallback("Unhandled error in PlayFabWWW: " + arg);
				}
			}
			www.Dispose();
			yield break;
		}

		public int GetPendingMessages()
		{
			return this._pendingWwwMessages;
		}

		private bool _isInitialized;

		private int _pendingWwwMessages;
	}
}
