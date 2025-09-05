using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player Config")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField]
    public float PlayerMoveSpeed { get; private set; }

    [field: SerializeField]
    public int PlayerHealth { get; private set; }

    [field: SerializeField]
    public InputActionReference ChangeWeaponInput { get; private set; }

    [field: SerializeField]
    public InputActionReference ShootInput { get; private set; }

    [field: SerializeField]
    public InputActionReference MoveInput { get; private set; }

    [field: SerializeField]
    public List<WeaponConfig> WeaponsConfigs { get; private set; }
}