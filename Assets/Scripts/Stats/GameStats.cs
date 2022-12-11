using System;
using System.Collections.Generic;
using System.Linq;

public static class GameStats
{
    public static event Action Updated;

    private static readonly Dictionary<int, PlayerStats> playerStats = new();

    public static PlayerStats AddPlayer(int id)
    {
        var stats = new PlayerStats("Player -" + id);
        playerStats.Add(id, stats);

        return stats;
    }

    public static void IncreaseDamageDone(int playerId)
    {
        playerStats[playerId].DamageDone++;
        Updated?.Invoke();
    }

    public static void ClearStats()
    {
        foreach (var playerStat in playerStats) playerStat.Value.DamageDone = 0;
    }

    public static PlayerStats GetWinnerByDamageDone()
    {
        return playerStats.OrderBy(s => s.Value.DamageDone).Last().Value;
    }
}
