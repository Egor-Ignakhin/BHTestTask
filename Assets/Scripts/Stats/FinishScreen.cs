using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class FinishScreen : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI winnerNameText;

    [SerializeField] private float delaySec = 5f;

    public void Activate()
    {
        background.enabled = true;
        winnerNameText.SetText(GameStats.GetWinnerByDamageDone().Name);

        StartCoroutine(Util.Delay(delaySec, ReloadGame));
    }

    private void ReloadGame()
    {
        Player.ClearIds();
        GameStats.ClearStats();
        NetworkManager.singleton.ServerChangeScene("Game");
    }
}
