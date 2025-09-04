using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerConfig playerConfig;

    [SerializeField]
    private Rigidbody2D rigidbody;

    [SerializeField]
    private Transform weaponTransformParent;

    private PlayerMover playerMover;
    private PlayerInputListener playerInputListener;
    private PlayerWeaponController playerWeaponController;

    private void Awake()
    {
        playerMover = new();
        playerInputListener = new();
        playerWeaponController = new();

        playerInputListener.Initialize(playerConfig.MoveInput, playerConfig.ShootInput, playerConfig.ChangeWeaponInput);
        playerMover.Initialize(rigidbody, playerConfig.PlayerMoveSpeed, playerConfig.PlayerJumpForce);
        playerWeaponController.Initialize(playerConfig.WeaponsConfigs, weaponTransformParent);
    }

    private void OnDestroy()
    {
        playerInputListener.Deinitialize();
    }

    private void OnEnable()
    {
        playerInputListener.ShootButtonPressed += OnShot;
        playerInputListener.ChangeWeaponButtonPressed += OnWeaponChanged;

    }

    private void OnDisable()
    {
        playerInputListener.ShootButtonPressed -= OnShot;
        playerInputListener.ChangeWeaponButtonPressed -= OnWeaponChanged;
    }

    private void FixedUpdate()
    {
        var moveDiraction = playerInputListener.GetMoveInputValue;

        playerMover.Move(moveDiraction);
        playerWeaponController.LookAtCursor();
    }

    private void OnShot()
    {
        playerWeaponController.Shot();
    }

    private void OnWeaponChanged()
    {
        playerWeaponController.ChangeWeapon();
    }
}