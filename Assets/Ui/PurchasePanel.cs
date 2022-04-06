using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PurchasePanel : MonoBehaviour
{
    [SerializeField] private RawImage icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI buttonLabel;
    [SerializeField] private Button button;
    
    
    public string Title
    {
        set => title.SetText(value);
    }
    
    public string Description
    {
        set => description.SetText(value);
    }
    
    public string ButtonText
    {
        set => buttonLabel.SetText(value);
    }
    
    public Texture Icon
    {
        set => icon.texture = value;
    }
    
    public UnityAction ButtonListener
    {
        set => button.UpdateListener(value);
    }
    
}
