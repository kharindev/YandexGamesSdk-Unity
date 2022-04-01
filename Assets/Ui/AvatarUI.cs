
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AvatarUI : MonoBehaviour
{
    [SerializeField] private RawImage avatarIcon;
    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private TextMeshProUGUI deviceLabel;
    [SerializeField] private Button reviewButton;
    [SerializeField] private TextMeshProUGUI reviewButtonLabel;
    [SerializeField] private TextMeshProUGUI authorizedLabel;

    public string Name
    {
        set => nameLabel.SetText(value);
    }
    
    public string Device
    {
        set => deviceLabel.SetText(value);
    }
    
    public bool IsAuthorized
    {
        set => authorizedLabel.SetText(value ? "authorized" : "not authorized");
    }
    
    public Texture Icon
    {
        set => avatarIcon.texture = value;
    }
    
    public UnityAction ReviewButtonListener
    {
        set => reviewButton.UpdateListener(value);
    }
    
    public string ReviewButtonText
    {
        set => reviewButtonLabel.SetText(value);
    }
}
