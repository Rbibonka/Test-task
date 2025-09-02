using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player Config")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField]
    public float PlayerMoveSpeed { get; private set; }

    [field: SerializeField]
    public float PlayerJumpForce { get; private set; }

    [field: SerializeField]
    public InputActionReference MoveInput { get; private set; }

    [field: SerializeField]
    public InputActionReference JumpInput { get; private set; }
}