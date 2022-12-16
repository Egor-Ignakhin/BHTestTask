using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class FinishScreen : NetworkBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI winnerNameText;


    [ClientRpc]
    public void RpcActivate(string winnerName, float screenShowTime)
    {
        background.enabled = true;
        winnerNameText.SetText($"Winner: {winnerName}!!!");

        StartCoroutine(Util.Delay(screenShowTime, () =>
        {
            background.enabled = false;
            winnerNameText.SetText(string.Empty);
        }));
    }
}
