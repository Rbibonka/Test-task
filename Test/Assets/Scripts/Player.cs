using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerConfig playerConfig;

    [SerializeField]
    private Rigidbody2D rigidbody;

    private PlayerMover playerMover;
    private PlayerInputListener playerInputListener;

    private void Awake()
    {
        playerMover = new();
        playerInputListener = new();

        playerInputListener.Initialize(playerConfig.MoveInput, playerConfig.JumpInput);
        playerMover.Initialize(rigidbody, playerConfig.PlayerMoveSpeed, playerConfig.PlayerJumpForce);
    }

    private void OnDestroy()
    {
        playerInputListener.Deinitialize();
    }

    private void OnEnable()
    {
        playerInputListener.JumpedButtonPressed += OnJumped;
    }

    private void OnDisable()
    {
        playerInputListener.JumpedButtonPressed -= OnJumped;
    }

    private void FixedUpdate()
    {
        var moveDiraction = playerInputListener.GetMoveInputValue;

        playerMover.Move(moveDiraction);
    }

    private void OnJumped()
    {
        playerMover.Jump();
    }
}