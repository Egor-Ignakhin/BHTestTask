using UnityEngine;

public class GameStatsManager : MonoBehaviour
{
    [SerializeField] private FinishScreen finishScreen;

    private void Awake()
    {
        GameStats.Updated += OnStatisticsUpdated;
    }

    private void OnStatisticsUpdated()
    {
        var winner = GameStats.GetWinnerByDamageDone();
        var canStopGame = winner.DamageDone >= 3;

        if (canStopGame) StopGame();
    }

    private void StopGame()
    {
        finishScreen.Activate();
    }

    private void OnDestroy()
    {
        GameStats.Updated -= OnStatisticsUpdated;
    }
}
