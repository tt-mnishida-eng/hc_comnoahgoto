using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UnityRemoteSettingManager : SingletonMonoBehaviour<UnityRemoteSettingManager>
{

    private UnityRemoteSettingService _remoteSettingService;
    public UnityRemoteSettingService RemoteSettingService
    {
        get
        {
            if (!IsCompleted)
            {
                Debug.LogWarning("RemoteSettings is not completed.");
            }
            return _remoteSettingService;
        }
    }
    private bool _isCompleted = false;
    public bool IsCompleted => _isCompleted;

    protected override void Initialize()
    {
        //Set up callback
        RemoteSettings.Completed += OnRemoteSettingCompleted;
    }

    private void OnRemoteSettingCompleted(bool wasUpdatedFromServer, bool settingsChanged, int serverResponse)
    {
        if (wasUpdatedFromServer)
        {
            Debug.LogWarning("RemoteSettings is downloaded. Http response code is : " + serverResponse);
        }
        _isCompleted = true;
        _remoteSettingService = new UnityRemoteSettingService();
    }
}
