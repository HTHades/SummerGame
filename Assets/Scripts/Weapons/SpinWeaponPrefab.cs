using UnityEngine;

public class SpinWeaponPrefab : MonoBehaviour
{
    [SerializeField] private SpinWeaponProjectile projectile;
    [SerializeField] private float resizeSpeed;
    [SerializeField] private float baseRotationSpeed;
    private Vector3 TargetSize;
    private float duration;
    private float orbitSpeed;
    private bool initialized;
    private bool isDespawning;
    public void initialize( float damage, float duration, float range, float speed, float rotationOffset)
    {
        this.duration = duration;
        orbitSpeed = baseRotationSpeed * speed;
        transform.localScale = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0f, 0f, rotationOffset);
        TargetSize = Vector3.one;
        projectile.transform.localPosition = Vector3.up * range;
        projectile.Initialize(damage, orbitSpeed);
        initialized = true;

    }
    
    void Update()
    {
        if( !initialized)
        {
           return;
        }
        RotateOrbit();
        Resize();
        UpdateDuration();
    }
    private void RotateOrbit()
    {
        transform.Rotate(0f, 0f, orbitSpeed * Time.deltaTime);
    }
    private void Resize()
    {
        transform.localScale = Vector3.MoveTowards( transform.localScale, TargetSize, resizeSpeed * Time.deltaTime);
        if( isDespawning && transform.localScale == Vector3.zero)
        {
            Destroy(gameObject);
        }
    }
    private void UpdateDuration()
    {
        duration -= Time.deltaTime;
        if( duration > 0)
        {
            return;
        }
        isDespawning = true;
        TargetSize = Vector3.zero;
    }
   
}
