using UnityEngine;

public class SpinWeaponPrefab : MonoBehaviour
{
    private SpinWeapon weapon;
    private float Duration;
    private Vector3 TargetSize;
    [SerializeField] private GameObject projectile;
    void Start()
    {
        weapon = GameObject.Find("SpinWeapon").GetComponent<SpinWeapon>();
        Duration = weapon.Stats[weapon.weaponLevel].duration;
        TargetSize = Vector3.one;
        transform.localScale = Vector3.zero;
        projectile.transform.localPosition = new Vector3(0f, weapon.Stats[weapon.weaponLevel].range, 0f);
        AudioController.Instance.PlaySound(AudioController.Instance.SpinWeaponSpawn);
    }
    void Update()
    {
        //rotate
         transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (90 * Time.deltaTime * weapon.Stats[weapon.weaponLevel].Speed));
         // grow
         transform.localScale = Vector3.MoveTowards(transform.localScale, TargetSize, Time.deltaTime);
         // shrink
         Duration -= Time.deltaTime;
         if( Duration <= 0)
        {
            TargetSize = Vector3.zero;
            if( transform.localScale.x == 0f)
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetRotationOffSet( float RotationOffset)
    {
        transform.rotation = Quaternion.Euler(0f, 0f,RotationOffset);
    }
}
