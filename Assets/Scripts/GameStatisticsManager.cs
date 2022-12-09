using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatisticsManager : MonoBehaviour
{
   [SerializeField] private FinishScreen finishScreen;

   private void Awake()
   {
      GameStatistics.Updated += OnStatisticsUpdated;
   }

   private void OnStatisticsUpdated()
   {
      var winner = CalcualteWinner();
      var canStopGame = winner.DamageDone >= 3;

      if (canStopGame)
      {
         StopGame();
      }
   }

   private PlayerStats CalcualteWinner()
   {
      throw new Exception();
   }

   private void StopGame()
   {
      finishScreen.Activate();
   }

   private void OnDestroy()
   {
      GameStatistics.Updated -= OnStatisticsUpdated;
   }
}
