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
        sound.pitch = Random.Range(0.7f, 1.3f);
        sound.Stop();
        sound.Play();
    }

}
