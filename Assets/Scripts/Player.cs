using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Transform publicFace;
    
    [SerializeField] private float camSpeed = 0.5f;
    [SerializeField] private float movementSpeed = 1;

    private float x;
    private float y;
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

    private void Update()
    {
        if (isOwned)
        {
            x = Input.GetAxis("Mouse X");
            y = Input.GetAxis("Mouse Y");
            Vector3 rotateValue = new Vector3(y, x * -1, 0);

            cameraTr.localEulerAngles -= rotateValue + rotateValue * camSpeed;

            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * movementSpeed;

        }
    }
}
