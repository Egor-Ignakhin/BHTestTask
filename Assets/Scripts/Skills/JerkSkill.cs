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

        StartCoroutine(Util.WaitWhile(() =>
        {
            var additionalPosition = casterMovement.transform.forward * (Time.deltaTime * castPower);
            casterMovement.Move(additionalPosition);

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, 1f))
            {
                var objectHit = hit.transform;
                if (objectHit.TryGetComponent(out Player player))
                {
                    if (!player.isInvulnerable)
                    {
                        player.Hit(hitDuration);
                        GameStats.IncreaseDamageDone(Caster.PlayerId);
                    }
                }
            }

            return Vector3.Distance(startPoint, casterMovement.transform.position) < castDistance;
        }));
    }
}
