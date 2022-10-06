using UnityEngine;
using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.Analytics;
using Managers;

public class FirebaseManager : MonoBehaviour
{
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public async UniTask Initialize()
    {
        await CrashlyticsInitialize();
        await AnalyticsInitialize();
    }
    
    private async UniTask AnalyticsInitialize()
    {
        await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            var app = FirebaseApp.DefaultInstance;
        });
        Debug.Log($"Analytics Initialize completed");
    }

    private async UniTask CrashlyticsInitialize()
    {
        await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
            }
            else
            {
                Debug.LogError(String.Format("Could not resolve all Firebase dependencies: {0}",dependencyStatus));
            }
        });   
        Debug.Log($"Crashlytics Initialize completed");
    }
    
    public async UniTask RemoteConfigItemsCostMultiplier()
    {
        var fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
        await fetchTask.ContinueWithOnMainThread(GetBuildingsCountValue);
    }
    
    private async UniTask GetBuildingsCountValue(Task obj)
    {
        await Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
        var value = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("items_cost_multiplier");
        _gameManager.ItemsCostMultiplier = (int)value.LongValue;
        Debug.Log($"ItemsCostMultiplier config: {_gameManager.ItemsCostMultiplier}");
    }
    
    public void LevelUpEvent(int level)
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelUp, FirebaseAnalytics.ParameterLevel, level);
    }
}