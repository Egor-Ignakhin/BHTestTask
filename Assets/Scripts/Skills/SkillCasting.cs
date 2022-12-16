using Mirror;
using UnityEngine;

public class SkillCasting : NetworkBehaviour
{
    [SerializeField] private int castMouseButton;
    [SerializeField] private Skill skillPrefab;

    private Player player;

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void OnUpdate()
    {
        if (isLocalPlayer)
            if (Input.GetMouseButtonDown(castMouseButton))
                InstantiateAndCast();
    }

    [Command(requiresAuthority = false)]
    private void InstantiateAndCast()
    {
        var rotation = Quaternion.identity;
        var position = Vector3.zero;
        var skillObj = Instantiate(skillPrefab, position, rotation);

        skillObj.Init(player);
        skillObj.Cast();
    }
}
