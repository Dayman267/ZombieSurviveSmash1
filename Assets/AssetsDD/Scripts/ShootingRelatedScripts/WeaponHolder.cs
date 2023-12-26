using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : NetworkBehaviour
{
    private Image bar;
    private Image barBackground;

    //private int equippedWeapon;
    private int selectedWeapon;

    private void Start()
    {
        if (!isLocalPlayer) return;
        SelectWeapon();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        var equippedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedWeapon = 0;

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2) selectedWeapon = 1;

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3) selectedWeapon = 2;

        if (equippedWeapon != selectedWeapon) SelectWeapon();
    }

    private void SelectWeapon()
    {
        if (!isLocalPlayer) return;
        var i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}