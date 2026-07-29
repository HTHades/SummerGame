using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public AudioSource Pause;
    public AudioSource Unpause;
    public AudioSource EnemyDie;
    public AudioSource SelectUpgrade;
    public AudioSource ArenaWeaponSpawn;
    public AudioSource ArenaWeaponDespawn;
    public AudioSource SpinWeaponSpawn;
    //public AudioSource SpinWeaponDespawn;
    public AudioSource ShootWeaponSpawn;
    public AudioSource ShootWeaponDespawn;
    public AudioSource GameOver;

    void Awake()
    {
        if( Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void PlaySound( AudioSource sound)
    {
        // dùng stop() trước tránh lỗi ngắt âm
        sound.Stop();
        sound.Play();
    }
    // trick using pitch
    public void PlayEnemyDieSounnd( AudioSource sound)
    {
        sound.pitch = Random.Range(0.5f, 1.5f);
        sound.Stop();
        sound.Play();
    }

}
