using System;

namespace YandexGamesSdk
{
    public class YandexUser : IYandexUserListener
    {
        public event Action<UserData> onAuthenticateSuccess;
        public event Action<string, string> onDataReceived;
        public event Action onDataSaved;
        public event Action<DeviceInfo> onDeviceInfoReceived;
        public event Action onReviewSent;
        public event Action<string> onReviewError;

        public YandexUser()
        {
            YandexUserHandler.SetListener(this);
        }

        public void OnAuthenticateSuccess(UserData userData)
        {
            onAuthenticateSuccess?.Invoke(userData);
        }

        public void OnDataReceived(string key, string json)
        {
            onDataReceived?.Invoke(key,json);
        }

        public void OnDataSaved()
        {
            onDataSaved?.Invoke();
        }

        public void OnDeviceInfoReceived(DeviceInfo deviceInfo)
        {
            onDeviceInfoReceived?.Invoke(deviceInfo);
        }

        public void ReviewSent()
        {
            onReviewSent?.Invoke();
        }

        public void ReviewError(string error)
        {
            onReviewError?.Invoke(error);
        }

        public void Authenticate()
        {
            YandexUserHandler.Authenticate();
        }

        public void LoadData(string key)
        {
            YandexGames.LoadData(key);
        }

        public void GetDeviceInfo()
        {
            YandexGames.GetDeviceInfo();
        }
        

        public void RequestReview()
        {
            YandexGames.RequestReview();
        }

        public void Save(string key, string json)
        {
            YandexGames.SaveData(key, json);
        }
    }
}
