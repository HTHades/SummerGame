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
        if( weapon.gameObject.activeSelf == true)
        {
            weaponName.text = weapon.name;
            WeaponDescription.text = weapon.Stats[weapon.weaponLevel].WeaponDescription;
        }
        else
        {
            weaponName.text = "NEW" +" " + weapon.name;
            WeaponDescription.text = weapon.basicDescription;
        }
        WeaponIcon.sprite = weapon.weaponImage;
        assignedWeapon = weapon;
    }
    public void SelectUpgrade()
    {
        if(assignedWeapon.gameObject.activeSelf == true)
        {
            assignedWeapon.LevelUpWeapon();
        }
        else
        {
            PlayerController.Instance.ActiveWeapon(assignedWeapon);
        }
        
        AudioController.Instance.PlaySound(AudioController.Instance.SelectUpgrade);
        UIController.Instance.LevelUpPanelClose();
    }
}
