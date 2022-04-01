using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private LeaderboardItemUI itemPrefab;
    [SerializeField] private Button pointsButton;
    [SerializeField] private Button refreshButton;
    [SerializeField] private Transform content;

    public UnityAction PointsButtonListener
    {
        set => pointsButton.UpdateListener(value);
    }
    
    public UnityAction RefreshButtonListener
    {
        set => refreshButton.UpdateListener(value);
    }

    public LeaderboardItemUI ItemPrefab => itemPrefab;
    public Transform Content => content;
    
}
