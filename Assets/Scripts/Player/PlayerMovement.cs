using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
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

    [Command(requiresAuthority = false)]
    public void Move(Vector3 additionalPosition)
    {
        transform.position += additionalPosition;
    }


    [Command(requiresAuthority = false)]
    public void Rotate(Vector3 additionalTransformEulers)
    {
        transform.localEulerAngles += additionalTransformEulers;
    }

    public void RotateCamera(Vector3 additionalCameraEulers)
    {
        cameraTr.localEulerAngles += additionalCameraEulers;
    }
}