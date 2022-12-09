using System;
using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public event Action<Collider> OnCollision;
    
    private Transform cameraTr;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (isOwned)
        {
            cameraTr = Camera.main.transform;
            cameraTr.SetParent(transform);
            cameraTr.localPosition = new Vector3(0, 1, 0);
        }
    }
    
    public void Move(Vector3 additionalPosition)
    {
        transform.position += additionalPosition;
    }

    public void Rotate(Vector3 additionalCameraEulers, Vector3 additionalTransformEulers)
    {
        cameraTr.localEulerAngles += additionalCameraEulers;
        transform.localEulerAngles += additionalTransformEulers;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.transform.gameObject.name);
    }
}
