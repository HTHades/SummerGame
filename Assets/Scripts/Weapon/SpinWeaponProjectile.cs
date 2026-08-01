using UnityEngine;

public class SpinWeaponProjectile : MonoBehaviour
{
    private SpinWeapon weapon;

    void Start()
    {
        weapon = GameObject.Find("SpinWeapon").GetComponent<SpinWeapon>();
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (90 * Time.deltaTime* weapon.Stats[weapon.weaponLevel].Speed));
    }

    private void OnTriggerEnter2D(Collider2D collider){
      IDamageable damageable = collider.gameObject.GetComponentInParent<IDamageable>();
        if(damageable != null){
            damageable.TakeDamage(weapon.Stats[weapon.weaponLevel].Damage);
        }
    }
    
}
