using Mirror;
using TMPro;
using UnityEngine;

public class PlayerDamageDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro mText;
    [SerializeField] private Player player;

    private void Start()
    {
        player.OnDamageChanged += OnPlayerDamageChanged;
        OnPlayerDamageChanged(0);
    }

    private void OnPlayerDamageChanged(int newValue)
    {
        mText.SetText("X : " + newValue);
    }

    private void OnDestroy()
    {
        player.OnDamageChanged -= OnPlayerDamageChanged;
    }
}
