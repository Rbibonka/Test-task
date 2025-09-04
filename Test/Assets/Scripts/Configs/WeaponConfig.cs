using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeapon", menuName = "Player Weapon")]
public class WeaponConfig : ScriptableObject
{
    [field: SerializeField]
    public Weapon weaponPrefab { get; private set; }

    [field: SerializeField]
    public string weaponName { get; private set; }
}