using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController
{
    private List<WeaponConfig> weaponConfigs;
    private Transform weaponTransformParent;

    private List<Weapon> weapons;

    private Weapon currentWeapon;

    public void Initialize(List<WeaponConfig> weaponConfigs, Transform weaponTransformParent)
    {
        this.weaponConfigs = weaponConfigs;
        this.weaponTransformParent = weaponTransformParent;

        CreateWeapons();
    }

    public void Shot()
    {
        currentWeapon.Shot();
    }

    public void ChangeWeapon()
    {
        currentWeapon.gameObject.SetActive(false);

        var currentWeaponIndex = weapons.FindIndex(0, weapons.Count, weapon => weapon == currentWeapon);

        currentWeaponIndex++;

        if (currentWeaponIndex >= weapons.Count)
        {
            currentWeapon = weapons[0];
        }
        else
        {
            currentWeapon = weapons[currentWeaponIndex];
        }

        currentWeapon.gameObject.SetActive(true);
    }

    public void LookAtCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angleRadians = Mathf.Atan2(mousePosition.y - weaponTransformParent.position.y, mousePosition.x - weaponTransformParent.position.x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        weaponTransformParent.rotation = Quaternion.Euler(0f, 0f, angleDegrees);
    }

    private void CreateWeapons()
    {
        weapons = new List<Weapon>(weaponConfigs.Count);

        foreach (var weaponConfig in weaponConfigs)
        {
            var weapon = GameObject.Instantiate(weaponConfig.weaponPrefab);

            weapon.transform.SetParent(weaponTransformParent, false);
            weapon.transform.position = Vector3.zero;

            weapon.gameObject.SetActive(false);

            weapon.Initialize();

            weapons.Add(weapon);
        }

        currentWeapon = weapons[0];

        currentWeapon.gameObject.SetActive(true);
    }
}