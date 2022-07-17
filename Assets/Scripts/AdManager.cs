using GoogleMobileAds.Api;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AdManager : MonoBehaviour
{
    public Text uiMessage;

    private RewardBasedVideoAd rewardBasedVideo;
    private BannerView bannerView;
    void Start()
    {
        #if UNITY_ANDROID
            string appId = "ca-app-pub-1080459824922223~1775746097";
        #elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
        #else
            string appId = "unexpected_platform";
        #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        this.RequestBanner();
    }

    public void RequestBanner()
    {
        string bannerID = "ca-app-pub-1080459824922223/3720109501";
        this.bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
        this.bannerView.OnAdOpening += BannerView_OnAdOpening;
        this.bannerView.OnAdFailedToLoad += BannerView_OnAdFailedToLoad;
    }

    private void BannerView_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        uiMessage.text += $"\n Banner Failed to Load >> {e.Message}";
    }

    private void BannerView_OnAdOpening(object sender, EventArgs e)
    {
        uiMessage.text += "\n Banner Opening";
    }

    public void RequestRewardBasedVideo()
    {
        uiMessage.text += "\n Ad Requested";
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-1080459824922223/3827194368";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
        this.rewardBasedVideo.OnAdLoaded += RewardBasedVideo_OnAdLoaded;
        this.rewardBasedVideo.OnAdCompleted += RewardBasedVideo_OnAdCompleted;
        this.rewardBasedVideo.OnAdFailedToLoad += RewardBasedVideo_OnAdFailedToLoad;
        this.rewardBasedVideo.OnAdOpening += RewardBasedVideo_OnAdOpening;
        this.rewardBasedVideo.OnAdClosed += RewardBasedVideo_OnAdClosed;
        this.rewardBasedVideo.OnAdStarted += RewardBasedVideo_OnAdStarted;
        this.rewardBasedVideo.OnAdRewarded += RewardBasedVideo_OnAdRewarded;
    }

    private void RewardBasedVideo_OnAdRewarded(object sender, Reward e)
    {
        Transform emote = GameObject.FindGameObjectWithTag("Emote").transform;
        emote.DOScale(0, 0.25f);
        FindObjectOfType<UIManager>().HideGameOver();
        FindObjectOfType<GameplayManager>().isGameStart = true;
        GameObject.FindGameObjectWithTag("ButtonAd").GetComponent<Button>().interactable = false;
        uiMessage.text += "\n Ad Rewarded";
    }

    private void RewardBasedVideo_OnAdStarted(object sender, EventArgs e)
    {
        uiMessage.text += "\n Ad Started";
    }

    private void RewardBasedVideo_OnAdClosed(object sender, EventArgs e)
    {
        uiMessage.text += "\n Ad Closed";
    }

    private void RewardBasedVideo_OnAdOpening(object sender, EventArgs e)
    {
        uiMessage.text += "\n Ad Opening";
    }

    private void RewardBasedVideo_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        uiMessage.text += $"\n Ad Failed to Load >> {e.Message}";
    }

    private void RewardBasedVideo_OnAdCompleted(object sender, EventArgs e)
    {
        uiMessage.text += "\n Ad Completed";
    }

    private void RewardBasedVideo_OnAdLoaded(object sender, EventArgs e)
    {
        this.rewardBasedVideo.Show();
        uiMessage.text += "\n Ad Loaded";
    }
}
