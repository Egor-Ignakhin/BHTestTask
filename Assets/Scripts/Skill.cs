using UnityEngine;

//[RequireComponent(typeof(NetworkObject), typeof(Net_Skill))]
public abstract class Skill : MonoBehaviour
{
    protected Player Caster { get; private set; }

    public void Init(Player player)
    {
        Caster = player;
    }
    public abstract void Cast();
}
