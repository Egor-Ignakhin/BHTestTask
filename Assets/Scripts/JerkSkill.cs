using UnityEngine;

public class JerkSkill : Skill
{
    [SerializeField] private float castDistance = 4f;
    [SerializeField] private float castPower = 5f;
    [SerializeField] private float hitDuration = 3;

    private void HitPlayer(Player player)
    {
        player.Hit();
    }

    public override void Cast()
    {
        var casterMovement = Caster.PlayerMovement;
        var startPoint = casterMovement.transform.position;
        casterMovement.OnCollision += CasterMovementOnCollision;
        StartCoroutine(Util.WaitWhile(() =>
            {
                var additionalPosition = casterMovement.transform.forward * (Time.deltaTime * castPower);
                casterMovement.Move(additionalPosition);
                return Vector3.Distance(startPoint, casterMovement.transform.position) < castDistance;
            },
            () => casterMovement.OnCollision -= CasterMovementOnCollision));
    }

    private void CasterMovementOnCollision(Collider collider)
    {
        
    }
}
