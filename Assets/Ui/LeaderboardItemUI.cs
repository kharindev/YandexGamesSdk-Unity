using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItemUI : MonoBehaviour
{
    [SerializeField] private RawImage iconImage;
    [SerializeField] private TextMeshProUGUI userNameLabel;
    [SerializeField] private TextMeshProUGUI rankLabel;
    [SerializeField] private TextMeshProUGUI scoreLabel;
    
    public Texture Icon
    {
        set => iconImage.texture = value;
    }
    
    public int Rank
    {
        set => rankLabel.SetText("Rank:"+value);
    }
    
    public int Score
    {
        set => scoreLabel.SetText("Score:"+value);
    }
    
    public string UserName
    {
        set => userNameLabel.SetText("Name:"+value);
    }
}
