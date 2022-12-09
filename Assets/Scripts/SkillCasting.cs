using UnityEngine;

public class SkillCasting : MonoBehaviour
{
    [SerializeField] private int castMouseButton;

    private AttackCastInfo primary;
    
    private Player player;
    [SerializeField] private Skill skillPrefab;

    public void SetPlayer(Player player)
    {
        this.player = player;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(castMouseButton))
        {
            InstantiateAndCast();
        }
    }

    private void InstantiateAndCast()
    {
        Quaternion rotation = Quaternion.identity;
        Vector3 position = Vector3.zero;
        Skill skillObj = Instantiate(skillPrefab, position, rotation);

        skillObj.Init(player);
        skillObj.Cast();
    }
}
