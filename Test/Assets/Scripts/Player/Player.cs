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

    public bool isDead { get; private set; }

    private int currentHealth;

    public void Iniilize()
    {
        playerMover = new();
        playerInputListener = new();
        playerWeaponController = new();

        playerInputListener.Initialize(playerConfig.MoveInput, playerConfig.ShootInput, playerConfig.ChangeWeaponInput);
        playerMover.Initialize(rigidbody, playerConfig.PlayerMoveSpeed);
        playerWeaponController.Initialize(playerConfig.WeaponsConfigs, weaponTransformParent);

        currentHealth = playerConfig.PlayerHealth;

        playerInputListener.ShootButtonPressed += OnShot;
        playerInputListener.ChangeWeaponButtonPressed += OnWeaponChanged;

    }

    public void Deinitialize()
    {
        playerInputListener.Deinitialize();

        playerInputListener.ShootButtonPressed -= OnShot;
        playerInputListener.ChangeWeaponButtonPressed -= OnWeaponChanged;
    }

    private void FixedUpdate()
    {
        var moveDiraction = playerInputListener.GetMoveInputValue;

        playerMover.Move(moveDiraction);
        playerWeaponController.LookAtCursor();
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;
        }
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