
using UnityEngine;
using YandexGamesSdk;

public class YandexAdsExample : MonoBehaviour
{
    [SerializeField] private YandexAdsUI _yandexAdsUi;
    private YandexInterstitialAd _yandexInterstitialAd = new YandexInterstitialAd("mainInterstitial");
    private YandexRewardedAd _yandexRewardedAd10 = new YandexRewardedAd("_yandexRewardedAd10");
    private YandexRewardedAd _yandexRewardedAd100 = new YandexRewardedAd("_yandexRewardedAd100");

    private void Start()
    {
        _yandexAdsUi.InterstitialButtonListener = () =>
        {
            _yandexInterstitialAd.Show();
        };
        
        _yandexAdsUi.Rewarded10ButtonListener = () =>
        {
            _yandexRewardedAd10.Show();
        };
        
        _yandexAdsUi.Rewarded100ButtonListener = () =>
        {
            _yandexRewardedAd100.Show();
        };
    }
    
    private void OnEnable()
    {
        _yandexRewardedAd10.onRewarded += OnRewarded10;
        _yandexRewardedAd100.onRewarded += OnRewarded100;
    }
    
    private void OnDisable()
    {
        _yandexRewardedAd10.onRewarded -= OnRewarded10;
        _yandexRewardedAd100.onRewarded -= OnRewarded100;
    }

    private void OnRewarded10(string placementId)
    {
        _yandexAdsUi.Message = "Rewarded "+placementId;
    }
    
    private void OnRewarded100(string placementId)
    {
        _yandexAdsUi.Message = "Rewarded "+placementId;
    }
}
