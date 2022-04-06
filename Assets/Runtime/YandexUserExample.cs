using System.Collections;
using UnityEngine;
using YandexGamesSdk;

public class YandexUserExample : MonoBehaviour
{
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
            Debug.Log("SAVE CLICK");
            var message = "";
            var saveKey = _dataUi.InputKeyValue;
            var saveValue = _dataUi.InputTextValue;
            if (saveKey.Length == 0)
            {
                message += "введите ключь ";
            }
            else if(saveValue.Length == 0)
            {
                message += "введите значение";
            }
            else
            {
                _yandexUser.Save(saveKey, saveValue); 
            }

            _dataUi.Message = message;
        };
        
        _dataUi.LoadButtonListener = () =>
        {
            var saveKey = _dataUi.InputKeyValue;
            if (saveKey.Length == 0)
            {
                _dataUi.Message = "введите ключь ";
            }
            else
            {
                _yandexUser.LoadData(saveKey); 
            }
            
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
    private IEnumerator LoadIMG(string url, AvatarUI avatarUi)
    {
        var www = new WWW(url);
        yield return www;
        avatarUi.Icon = www.texture;
    }
    
    private void OnDeviceInfoReceived(DeviceInfo deviceInfo)
    {
        _avatarUi.Device = deviceInfo.type;
    }

    
    private void OnDataLoaded(string key, string data)
    {
        _dataUi.Message = "Loaded " + key +" : "+data;
        _dataUi.Data = data;
    }
    
    private void OnDataSaved()
    {
        _dataUi.Message = "Saved " + _dataUi.InputKeyValue +":"+_dataUi.InputTextValue;
    }
    
    private void OnReviewSent()
    {
        _avatarUi.ReviewButtonText = "Sent";
    }
    
    private void OnReviewError(string error)
    {
        _avatarUi.ReviewButtonText = error;
    }
    
  
    

}
