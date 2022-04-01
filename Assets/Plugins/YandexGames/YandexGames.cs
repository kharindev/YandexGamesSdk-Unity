using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
public class YandexGames : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void ShowRewardedAd(string placementId);

    [DllImport("__Internal")]
    public static extern void ShowInterstitialAd(string placementId);
   
    [DllImport("__Internal")]
    public static extern string LoadData(string key);

    [DllImport("__Internal")]
    public static extern void SaveData(string key, string data);
    
    [DllImport("__Internal")]
    public static extern void Authenticate();
    
    [DllImport("__Internal")]
    public static extern void GetDeviceInfo();
    
    [DllImport("__Internal")]
    public static extern void RequestReview();
    
    [DllImport("__Internal")]
    public static extern void RequestLeaderboardEntries(string name);
    
    [DllImport("__Internal")]
    public static extern void SetLeaderboardScore(string leaderboardId, int score);

    [DllImport("__Internal")]
    public static extern void ConsoleLog(string error);

    public void RewardedOpen(string placementId)
    {  
        Dispatcher.RunOnMainThread(()=>{
            YandexAdsHandler.SendRewardedOpen(placementId);
        });
    }
    
    public void Rewarded(string placementId)
    {  
        Dispatcher.RunOnMainThread(()=>{
            YandexAdsHandler.SendRewarded(placementId);
        });
    }
    
    public void RewardedClose(string placementId)
    {  
        Dispatcher.RunOnMainThread(()=>{
            YandexAdsHandler.SendRewardedClose(placementId);
        });
    }
    
    public void RewardedError(string errorObject)
    {  
        Dispatcher.RunOnMainThread(()=>{
            var error = JsonUtility.FromJson<RewardedError>(errorObject);
            YandexAdsHandler.SendRewardedError(error.id, error.error);
        });
    }
    
    public void InterstitialShown(string placementId)
    {  
        Dispatcher.RunOnMainThread(()=>{
            YandexAdsHandler.SendInterstitialShown(placementId);
        });
    }
    
    public void InterstitialClosed(string placementId)
    {  
        Dispatcher.RunOnMainThread(()=>{
            YandexAdsHandler.SendInterstitialClosed(placementId);
        });
    }
    
    public void InterstitialFailed(string errorObject)
    {  
        Dispatcher.RunOnMainThread(()=>{
            var error = JsonUtility.FromJson<RewardedError>(errorObject);
            YandexAdsHandler.SendInterstitialFailed(error.id, error.error);
        });
    }

    public void AuthenticateSuccess(string userName)
    {  
        Dispatcher.RunOnMainThread(()=>{
            YandexUserHandler.OnAuthenticateSuccess(userName);
        });
    }
    
    public void DataGetting(string json)
    {  
        Dispatcher.RunOnMainThread(()=>
        {
            var data = JsonUtility.FromJson<LoadedData>(json);
            YandexUserHandler.OnDataReceived(data.key, data.data);
        });
    }

    public void DataSavedSuccess()
    {   
        Dispatcher.RunOnMainThread(()=>{ 
            YandexUserHandler.OnDataSaved(); 
        });
    }

    public void DeviceInfoReceived(string json)
    {  
        Dispatcher.RunOnMainThread(()=>{
            var deviceInfo = JsonUtility.FromJson<DeviceInfo>(json);
            YandexUserHandler.OnDeviceInfoReceived(deviceInfo);
        });
    }
    
    public void ReviewSent()
    {  
        Dispatcher.RunOnMainThread(()=>{
            YandexUserHandler.ReviewSent();
        });
    }
    
    public void ReviewError(string error)
    {
        Dispatcher.RunOnMainThread(() =>
        {
            YandexUserHandler.ReviewError(error);
        });
    }

    public void LeaderBoardPlayerRatingReceived(string json)
    {
        Dispatcher.RunOnMainThread(()=>{
            var response = JsonUtility.FromJson<LeaderboardObject>(json);
            YandexLeaderboardHandler.Received(response);
        });
    }
}

public class YandexLeaderboardHandler
{
    private static Dictionary<string, IYandexLeaderboard> _leaderboards = new Dictionary<string, IYandexLeaderboard>();

    public static void AddLeaderboard(string id, IYandexLeaderboard leaderboard)
    {
        if(!_leaderboards.ContainsKey(id))
            _leaderboards.Add(id, leaderboard);
    }
    
    public static void RemoveLeaderboard(string id)
    {
        if (_leaderboards.ContainsKey(id)) 
            _leaderboards.Remove(id);
    }

    public static void Received(LeaderboardObject @object)
    {
        var leaderboardId = @object.leaderboard.name;
        if (_leaderboards.ContainsKey(leaderboardId))
        {
            _leaderboards[leaderboardId]?.LeaderboardEntriesReceived(@object);
        }
    }
}
public class YandexUserHandler
{
    private static IYandexUserListener _listener;
    public static void SetListener(IYandexUserListener listener)
    {
        _listener = listener;
    }
    
    public static void RemoveListener()
    {
        _listener = null;
    }

    public static void OnAuthenticateSuccess(string userData)
    {
        var data = JsonUtility.FromJson<UserData>(userData);
        _listener.OnAuthenticateSuccess(data);
    }
    
    public static void OnDataReceived(string key, string data)
    {
        _listener.OnDataReceived(key, data);
    }
    
    public static void OnDataSaved()
    {
        _listener.OnDataSaved();
    }

    public static void Authenticate()
    {
        YandexGames.Authenticate();
    }

    public static void OnDeviceInfoReceived(DeviceInfo deviceInfo)
    {
        _listener.OnDeviceInfoReceived(deviceInfo);
    }

    public static void ReviewSent()
    {
        _listener.ReviewSent();
    }

    public static void ReviewError(string error)
    {
        _listener.ReviewError(error);
    }
}

public class YandexAdsHandler
{
    private static bool _noAds = false;
    private static readonly Dictionary<string, IYandexRewardedAdListener> _rewardedAds = new Dictionary<string, IYandexRewardedAdListener>();
    private static readonly Dictionary<string, IYandexInterstitialAdListener> _interstitialAds = new Dictionary<string, IYandexInterstitialAdListener>();
    private static bool _rewardedIsActive = false;
    public static void AddRewarded(string id, IYandexRewardedAdListener listener)
    {
        if(!_rewardedAds.ContainsKey(id))
            _rewardedAds.Add(id, listener);
    }
    
    public static void RemoveRewarded(string key)
    {
        if(_rewardedAds.ContainsKey(key))
            _rewardedAds.Remove(key);
    }
    
    public static void AddInterstitial(string id, IYandexInterstitialAdListener listener)
    {
        if(!_interstitialAds.ContainsKey(id))
            _interstitialAds.Add(id, listener);
    }
    
    public static void RemoveInterstitial(string key)
    {
        if(_interstitialAds.ContainsKey(key))
            _interstitialAds.Remove(key);
    }
    public static void ShowRewardAds(string placementId)
    {
        if(_noAds || _rewardedIsActive) return;
        _rewardedIsActive = true;
        YandexGames.ShowRewardedAd(placementId);
    }

    public static void ShowInterstitial(string placementId)
    {
        if(_noAds) return;
        YandexGames.ShowInterstitialAd(placementId);
    }
    
    public static void SendRewardedOpen(string placementId)
    {
        if (!_rewardedAds.ContainsKey(placementId)) return;
        _rewardedAds[placementId]?.OnRewardedOpen(placementId);
    }

    public static void SendRewarded(string placementId)
    {
        if (!_rewardedAds.ContainsKey(placementId)) return;
        _rewardedAds[placementId]?.OnRewarded(placementId);
    }
    
    public static void SendRewardedClose(string placementId)
    {
        if (!_rewardedAds.ContainsKey(placementId)) return;
        _rewardedIsActive = false;
        _rewardedAds[placementId]?.OnRewardedClose(placementId);
    }
    public static void SendRewardedError(string placementId, string error)
    {
        if (!_rewardedAds.ContainsKey(placementId)) return;
        _rewardedIsActive = false;
        _rewardedAds[placementId]?.OnRewardedError(placementId, error);
    }

    public static void SendInterstitialShown(string placementId)
    {
        if (!_interstitialAds.ContainsKey(placementId)) return;
        _interstitialAds[placementId]?.OnInterstitialShown(placementId);
    }
    
    public static void SendInterstitialClosed(string placementId)
    {
        if (!_interstitialAds.ContainsKey(placementId)) return;
        _interstitialAds[placementId]?.OnInterstitialClosed(placementId);
    }
    
    public static void SendInterstitialFailed(string placementId, string error)
    {
        if (!_interstitialAds.ContainsKey(placementId)) return;
        _interstitialAds[placementId]?.OnInterstitialFailed(placementId,error);
    }

    public static void SetNoAds(bool result)
    {
        _noAds = result;
    }
}


public interface IYandexUserListener
{
    void OnAuthenticateSuccess(UserData userData);
    void OnDataReceived(string key, string json);
    void OnDataSaved();
    void OnDeviceInfoReceived(DeviceInfo deviceInfo);
    void ReviewSent();
    void ReviewError(string error);
}

public interface IYandexRewardedAdListener
{
    void OnRewardedOpen(string placementId);
    void OnRewarded(string placementId);
    void OnRewardedClose(string placementId);
    void OnRewardedError(string placementId, string error);
}

public interface IYandexInterstitialAdListener
{
    void OnInterstitialShown(string placementId);
    void OnInterstitialClosed(string placementId);
    void OnInterstitialFailed(string placementId, string error);
}




