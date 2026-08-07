using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LevelUpButton : MonoBehaviour
{
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text weaponDescription;
    [SerializeField] private Image weaponIcon;
    private Weapon assignedWeapon;
    [SerializeField] private LevelUpController levelUpController;
    public void ActivateButton( Weapon weapon)
    {
        if( weapon.gameObject.activeSelf == true)
        {
            weaponName.text = weapon.name;
            weaponDescription.text = weapon.CurrentDescription;
        }
        else
        {
            weaponName.text = "NEW" +" " + weapon.name;
            weaponDescription.text = weapon.BasicDescription;
        }
        weaponIcon.sprite = weapon.WeaponImage;
        assignedWeapon = weapon;
    }
    public void SelectUpgrade() // chỉ báo người chơi chọn gì
    {
        if (assignedWeapon == null)
        {
            return;
        }
        levelUpController.SelectWeapon(assignedWeapon);
    }
}
