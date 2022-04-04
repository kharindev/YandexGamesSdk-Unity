
using System.Collections;
using UnityEngine;

public class YandexLeaderboardExample : MonoBehaviour
{
    [SerializeField] private LeaderboardUI _leaderboardUi;
    public YandexLeaderboard _leaderboard = new YandexLeaderboard("mainLeaderboard");

    private void Start()
    {
        _leaderboardUi.RefreshButtonListener = () =>
        {
            _leaderboardUi.Content.Clear();
            _leaderboard.LoadScore();
            
        };
        
        _leaderboardUi.PointsButtonListener = () =>
        {
            var points = Random.Range(0, 100);
            _leaderboard.SetScore(points);
            _leaderboardUi.Content.Clear();
            _leaderboard.LoadScore();
        };
    }

    private void OnEnable()
    {
        _leaderboard.OnScoreLoaded += OnScoreLoaded;
    }
    
    private void OnDisable()
    {
        _leaderboard.OnScoreLoaded -= OnScoreLoaded;
    }

    private void OnScoreLoaded(LeaderboardObject leaderboard)
    {
        foreach (var entry in leaderboard.entries)
        {
            var panel = Instantiate(_leaderboardUi.ItemPrefab, _leaderboardUi.Content, false);
            StartCoroutine(LoadIMG(entry.player.avatarUrlLarge, panel));
            panel.UserName = entry.player.publicName.ToUpper();
            panel.Rank = entry.rank;
            panel.Score = entry.score;
        }
    }

    private IEnumerator LoadIMG(string url, LeaderboardItemUI itemUi)
    {
        var www = new WWW(url);
        yield return www;
        itemUi.Icon = www.texture;
    }
}
