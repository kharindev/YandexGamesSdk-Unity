
using TMPro;
using UnityEngine;

public class PurchaseUI : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private PurchasePanel prefab;
    [SerializeField] private TextMeshProUGUI goldLabel;
    public Transform Content => content;
    public PurchasePanel Panel => prefab;
    
    
    public int Gold
    {
        set => goldLabel.SetText("Gold: "+value);
    }
}
