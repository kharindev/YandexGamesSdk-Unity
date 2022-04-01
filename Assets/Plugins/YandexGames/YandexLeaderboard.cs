using System;


public class YandexLeaderboard : IYandexLeaderboard
    {
        private string _id;
        public event Action<LeaderboardObject> OnScoreLoaded; 

        public YandexLeaderboard(string id)
        {
            _id = id;
            YandexLeaderboardHandler.AddLeaderboard(id, this);
        }

        public void SetScore(int score)
        {
            YandexGames.SetLeaderboardScore(_id, score);
        }

        public void LoadScore()
        {
            YandexGames.RequestLeaderboardEntries(_id);
        }


        public void LeaderboardEntriesReceived(LeaderboardObject @object)
        {
            OnScoreLoaded?.Invoke(@object);
        }
    }

    public interface IYandexLeaderboard
    {
        void LeaderboardEntriesReceived(LeaderboardObject @object);
    }


