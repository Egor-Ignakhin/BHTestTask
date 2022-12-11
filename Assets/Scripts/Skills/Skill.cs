using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public abstract class Skill : MonoBehaviour
{
    protected Player Caster { get; private set; }

    public void Init(Player player)
    {
        Caster = player;
    }

    public abstract void Cast();
}
