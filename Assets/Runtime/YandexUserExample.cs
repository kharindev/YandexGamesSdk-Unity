using System.Collections;
using UnityEngine;
using YandexGamesSdk;

public class YandexUserExample : MonoBehaviour
{
    public string saveKey = "default";
    [SerializeField] private AvatarUI _avatarUi;
    [SerializeField] private DataUI _dataUi;
    private YandexUser _yandexUser = new YandexUser();
    
    private void OnEnable()
    {
        _yandexUser.onAuthenticateSuccess += OnAuthenticateSuccess;
        _yandexUser.onDataReceived += OnDataLoaded;
        _yandexUser.onDataSaved += OnDataSaved;
        _yandexUser.onDeviceInfoReceived += OnDeviceInfoReceived;
        _yandexUser.onReviewSent += OnReviewSent;
        _yandexUser.onReviewError += OnReviewError;
    }
    
    private void OnDisable()
    {
        _yandexUser.onAuthenticateSuccess -= OnAuthenticateSuccess;
        _yandexUser.onDataReceived -= OnDataLoaded;
        _yandexUser.onDataSaved -= OnDataSaved;
        _yandexUser.onDeviceInfoReceived -= OnDeviceInfoReceived;
        _yandexUser.onReviewSent -= OnReviewSent;
        _yandexUser.onReviewError -= OnReviewError;
    }

    private void Start()
    {
        _yandexUser.Authenticate();
        _dataUi.SaveButtonListener = () =>
        {
            var saveString = _dataUi.InputValue;
            Debug.Log("save string "+saveString);
            _yandexUser.Save(saveKey, saveString);
        };
        
        _dataUi.LoadButtonListener = () =>
        {
            _yandexUser.LoadData(saveKey);
        };

        _avatarUi.ReviewButtonListener = () =>
        {
            _yandexUser.RequestReview();
        };
    }

    private void OnAuthenticateSuccess(UserData userData)
    {
        _yandexUser.GetDeviceInfo();
        _avatarUi.Name = userData.name.ToUpper();
        _avatarUi.IsAuthorized = userData.isAuthorized;
        StartCoroutine(LoadIMG(userData.avatarUrlLarge, _avatarUi));
    }
    
    private void OnDeviceInfoReceived(DeviceInfo deviceInfo)
    {
        _avatarUi.Device = deviceInfo.type;
    }

    
    private void OnDataLoaded(string key, string data)
    {
        _dataUi.Message = "Loaded " + key;
        _dataUi.Data = data;
    }
    
    private void OnDataSaved()
    {
        _dataUi.Message = "Saved " + saveKey;
    }
    
    private void OnReviewSent()
    {
        _avatarUi.ReviewButtonText = "Sent";
    }
    
    private void OnReviewError(string error)
    {
        _avatarUi.ReviewButtonText = error;
    }
    
    private IEnumerator LoadIMG(string url, AvatarUI avatarUi)
    {
        var www = new WWW(url);
        yield return www;
        avatarUi.Icon = www.texture;
    }
    

}
