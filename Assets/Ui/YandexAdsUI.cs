
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class YandexAdsUI : MonoBehaviour
{
    [SerializeField] private Button interstitialButton;
    [SerializeField] private Button rewarded10Button;
    [SerializeField] private Button rewarded100Button;
    [SerializeField] private TextMeshProUGUI rewardedLabel;

    public UnityAction InterstitialButtonListener
    {
        set => interstitialButton.UpdateListener(value);
    }
    
    public UnityAction Rewarded10ButtonListener
    {
        set => rewarded10Button.UpdateListener(value);
    }
    
    public UnityAction Rewarded100ButtonListener
    {
        set => rewarded100Button.UpdateListener(value);
    }

    public string Message
    {
        set => rewardedLabel.SetText(value);
    }
}
