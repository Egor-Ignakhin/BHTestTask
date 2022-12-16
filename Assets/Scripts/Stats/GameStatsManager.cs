using Mirror;
using UnityEngine;

public class GameStatsManager : MonoBehaviour
{
    [SerializeField] private FinishScreen finishScreen;
    [SerializeField] private float delayBetweenSessions = 5f;

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
        finishScreen.RpcActivate(GameStats.GetWinnerByDamageDone().Name, delayBetweenSessions);
        
        StartCoroutine(Util.Delay(delayBetweenSessions, ReloadGame));
    }

    private void ReloadGame()
    {
        GameStats.ClearStats();
    }
    
    private void OnDestroy()
    {
        GameStats.Updated -= OnStatisticsUpdated;
    }
}
