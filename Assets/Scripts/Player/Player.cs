using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public int PlayerId { get; set; } = -1;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SkillCasting skillCasting;
    [SerializeField] private TextMeshPro damageText;

    public PlayerMovement PlayerMovement => playerMovement;
    public SkillCasting SkillCasting => skillCasting;

    [SerializeField] private float rotationSpeed = 0.5f;
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private KeyCode runKeycode = KeyCode.LeftShift;

    private float x;
    private float y;

    [SyncVar(hook = nameof(UpdatePlayerMaterial))]
    public bool isInvulnerable; //TODO: realise strategy pattern
    [SerializeField] private Material invulnerableMat;
    [SerializeField] private Material defMat;

    #region Static

    public static int LastPlayerId { get; set; } = -1;

    private static readonly List<Player> allPlayers = new();

    #endregion

    private void Awake()
    {
        SkillCasting.SetPlayer(this);
    }

    private void Update()
    {
        if (isOwned)
        {
            //TODO: move input to input handler
            GetInput(out var additionalPosition,
                out var additionalCameraEulers,
                out var additionalTransformEulers);

            PlayerMovement.Move(additionalPosition);
            PlayerMovement.Rotate(additionalTransformEulers);
            PlayerMovement.RotateCamera(additionalCameraEulers);

            skillCasting.OnUpdate();
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


    public void Hit(float hitDuration)
    {
        isInvulnerable = true;

        StartCoroutine(Util.Delay(hitDuration, () =>
        {
            isInvulnerable = false;
        }));
    }

    private void UpdatePlayerMaterial(bool oldValue, bool newValue)
    {
        GetComponent<MeshRenderer>().sharedMaterial = isInvulnerable? invulnerableMat : defMat; //TODO: make config manager to ease select data
    }

    public void Initialize()
    {
        PlayerId = LastPlayerId + 1;
        LastPlayerId++;

        var stats = GameStats.AddPlayer(PlayerId);
        stats.Changed += () => { UpdateTextOnClients(stats.Name, stats.DamageDone); };

        if (!allPlayers.Contains(this))
            allPlayers.Add(this);
    }

    [ClientRpc]
    private void UpdateTextOnClients(string playerName, int damageDone)
    {
        damageText.SetText($"{playerName}\nX : " + damageDone);
    }

    public static void ClearIds()
    {
        LastPlayerId = 0;
    }
}
