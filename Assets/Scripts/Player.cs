using System;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SkillCasting skillCasting;
    public event Action<int> OnDamageChanged;

    public PlayerMovement PlayerMovement => playerMovement;
    public SkillCasting SkillCasting => skillCasting;

    [SerializeField] private float rotationSpeed = 0.5f;
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private KeyCode runKeycode = KeyCode.LeftShift;

    private float x;
    private float y;

    private PlayerState currentState;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        SkillCasting.SetPlayer(this);
    }
    
    private void Update()
    {
        if (isOwned)
        {
            GetInput(out var additionalPosition,
                out var additionalCameraEulers,
                out var additionalTransformEulers);

            PlayerMovement.Move(additionalPosition);
            PlayerMovement.Rotate(additionalCameraEulers, additionalTransformEulers);
        }
    }

    private void GetInput(out Vector3 additionalPosition, out Vector3 additionalCameraEulers,
        out Vector3 additionalTransformEulers)
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");
        var camRotateValue = new Vector3(y, 0, 0);
        var playerRotateValue = new Vector3(0, x * -1, 0);

        additionalCameraEulers = -(camRotateValue + camRotateValue * rotationSpeed);
        additionalTransformEulers = -(playerRotateValue + playerRotateValue * rotationSpeed);

        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);

        additionalPosition = moveDirection * (Time.deltaTime * (Input.GetKey(runKeycode) ? runSpeed : movementSpeed));
    }


    public void Hit()
    {
        SetState(new InvulnerablePlayerState());
    }

    private void SetState(PlayerState playerState)
    {
        currentState = playerState;
    }
}
