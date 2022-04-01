using System;
using UnityEngine;

namespace YandexGamesSdk
{
    public class YandexRewardedAd : IYandexRewardedAdListener
    {
        private string _placementId;
        
        public event Action<string> onRewarded;
        public event Action<string>  onRewardedShown;
        public event Action<string>  onRewardedClosed;
        public event Action<string,string>  onRewardedFailed;

        public YandexRewardedAd(string placementId)
        {
            _placementId = placementId;
            YandexAdsHandler.AddRewarded(placementId, this);
        }

        public void Show()
        {
            YandexAdsHandler.ShowRewardAds(_placementId);
        }

        public void OnRewardedOpen(string placementId)
        {
            onRewardedShown?.Invoke(placementId);
        }

        public void OnRewarded(string placementId)
        {
            onRewarded?.Invoke(placementId);
        }

        public void OnRewardedClose(string placementId)
        {
            onRewardedClosed?.Invoke(placementId);
        }

        public void OnRewardedError(string placementId, string error)
        {
            onRewardedFailed?.Invoke(placementId,error);
        }
    }
    
    public class YandexInterstitialAd : IYandexInterstitialAdListener
    {
        private string _placementId;
        public event Action onInterstitialShown;
        public event Action onInterstitialClosed;
        public event Action onInterstitialFailed;

        public YandexInterstitialAd(string id)
        {
            _placementId = id;
            YandexAdsHandler.AddInterstitial(id, this);
        }

        public void Show()
        {
            YandexAdsHandler.ShowInterstitial(_placementId);
        }

        public void OnInterstitialShown(string placementId)
        {
            onInterstitialShown?.Invoke();
        }

        public void OnInterstitialClosed(string placementId)
        {
            onInterstitialClosed?.Invoke();
        }

        public void OnInterstitialFailed(string placementId, string error)
        {
            onInterstitialFailed?.Invoke();
        }
    }
}


