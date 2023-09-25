using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DynamicLoading;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        DynamicLoadManager.Init("AaHBundles");
        //DynamicLoadManager.assetBundles = new Dictionary<string, AssetBundleWrapper>();
        //DynamicLoadManager.requests = new List<AsyncRequest>();
        //Debug.Log("Request " + DynamicLoadManager.requests.Count);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
