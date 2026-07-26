using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LevelUpButton : MonoBehaviour
{
    public TMP_Text weaponName;
    public TMP_Text WeaponDescription;
    public Image WeaponIcon;
    private Weapon assignedWeapon;
    public void ActivateButton( Weapon weapon)
    {
        weaponName.text = weapon.name;
        WeaponDescription.text = weapon.Stats[weapon.weaponLevel].WeaponDescription;
        WeaponIcon.sprite = weapon.weaponImage;
        assignedWeapon = weapon;
    }
    public void SelectUpgrade()
    {
        assignedWeapon.LevelUp();
        AudioController.Instance.PlaySound(AudioController.Instance.SelectUpgrade);
        UIController.Instance.LevelUpPanelClose();
    }
}
