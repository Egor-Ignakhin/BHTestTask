using System;
using UnityEngine;

public class JerkSkill : Skill
{
    [SerializeField] private float castDistance = 4f;
    [SerializeField] private float castPower = 5f;
    [SerializeField] private float hitDuration = 3;

    public override void Cast()
    {
        var casterMovement = Caster.PlayerMovement;
        var startPoint = casterMovement.transform.position;

        Physics.Raycast( startPoint, casterMovement.transform.forward, out var hit, castDistance);
        StartCoroutine(Util.WaitWhile(() =>
        {
            var additionalPosition = casterMovement.transform.forward * (Time.deltaTime * castPower);
            casterMovement.Move(additionalPosition);

            return Vector3.Distance(startPoint, casterMovement.transform.position) < castDistance;
        }, () =>
        {
            var hitTransform = hit.transform;
            if (hitTransform && hitTransform.TryGetComponent(out Player player))
                if (!player.isInvulnerable)
                {
                    player.Hit(hitDuration);
                    GameStats.IncreaseDamageDone(Caster.PlayerId);
                }
        }));
    }
}
