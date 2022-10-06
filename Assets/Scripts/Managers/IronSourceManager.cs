using TMPro;
using UnityEngine;

public class IronSourceManager : MonoBehaviour
{
    private const string APP_KEY = "85460dcd";
    private TextMeshProUGUI _rewardedButtonFailText;

    private bool _isInterstitialReady;
    private bool _isRewardedReady;

    public bool IsRewardedReady => _isRewardedReady;

    private void Awake()
    {
        _rewardedButtonFailText = FindObjectOfType<UIRewardedButton>(true).ButtonText;
        SubscribeEvents();
        IronSource.Agent.validateIntegration();
        IronSource.Agent.init(APP_KEY, IronSourceAdUnits.BANNER, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.REWARDED_VIDEO);
    }

    private void SubscribeEvents()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent += InitializeCallback;

        IronSourceEvents.onBannerAdLoadFailedEvent += BannerLoadFailed;

        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialReady;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialClosed;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialOpened;

        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedReady;
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedOpened;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedClosed;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedEvent;
        IronSourceEvents.onRewardedVideoAdLoadFailedEvent += RewardedLoadFail;
    }

    private void InitializeCallback()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM);
        IronSource.Agent.loadInterstitial();
        IronSource.Agent.loadRewardedVideo();
    }

    private void BannerLoadFailed(IronSourceError error) => Debug.LogError(error);

    #region Interstitial
    private void InterstitialReady()
    {
        _isInterstitialReady = true;
    }
    private void InterstitialOpened()
    {
        IronSource.Agent.hideBanner();
    }

    private void InterstitialClosed()
    {
        _isInterstitialReady = false;
        IronSource.Agent.displayBanner();
        IronSource.Agent.loadInterstitial();
    }

    public void ShowInterstitial()
    {
        if (_isInterstitialReady == false) return;

        IronSource.Agent.showInterstitial();
    }
    #endregion

    #region Rewarded
    private void RewardedReady(bool status)
    {
        _isRewardedReady = status;
    }

    private void RewardedOpened()
    {
        IronSource.Agent.hideBanner();
    }

    private void RewardedClosed()
    {
        _isRewardedReady = false;
        IronSource.Agent.displayBanner();
        IronSource.Agent.loadRewardedVideo();
    }

    private void RewardedLoadFail(IronSourceError error)
    {
        _rewardedButtonFailText.text = "Fail";
    }

    private void RewardedEvent(IronSourcePlacement ironSourcePlacement)
    {
        Debug.Log($"Get bonus: {ironSourcePlacement.getRewardName()}");
    }

    public void ShowRewarded()
    {
        if (_isRewardedReady == false) return;

        IronSource.Agent.showRewardedVideo();
    }
    #endregion
}
