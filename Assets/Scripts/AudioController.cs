using UnityEngine;

public enum SoundType
{
    Pause,
    Unpause,
    EnemyDeath,
    SelectUpgrade,
    ArenaWeaponSpawn,
    ArenaWeaponDespawn,
    SpinWeaponSpawn,
    ShootWeaponSpawn,
    ShootWeaponDespawn,
    GameOver
}
public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    [SerializeField] private AudioSource Pause;
    [SerializeField] private AudioSource Unpause;
    [SerializeField] private AudioSource EnemyDie;
    [SerializeField] private AudioSource SelectUpgrade;
    [SerializeField] private AudioSource ArenaWeaponSpawn;
    [SerializeField] private AudioSource ArenaWeaponDespawn;
    [SerializeField] private AudioSource SpinWeaponSpawn;
    //public AudioSource SpinWeaponDespawn;
    [SerializeField] private AudioSource ShootWeaponSpawn;
    [SerializeField] private AudioSource ShootWeaponDespawn;
    [SerializeField] private AudioSource GameOver;

    void Awake()
    {
        if( Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }
    public void PlaySound(SoundType soundType)
    {
        AudioSource sound = null;

        switch (soundType)
        {
            case SoundType.Pause:
                sound = Pause;
                break;

            case SoundType.Unpause:
                sound = Unpause;
                break;

            case SoundType.EnemyDeath:
                sound = EnemyDie;
                break;

            case SoundType.SelectUpgrade:
                sound = SelectUpgrade;
                break;

            case SoundType.ArenaWeaponSpawn:
                sound = ArenaWeaponSpawn;
                break;

            case SoundType.ArenaWeaponDespawn:
                sound = ArenaWeaponDespawn;
                break;

            case SoundType.SpinWeaponSpawn:
                sound = SpinWeaponSpawn;
                break;

            case SoundType.ShootWeaponSpawn:
                sound = ShootWeaponSpawn;
                break;

            case SoundType.ShootWeaponDespawn:
                sound = ShootWeaponDespawn;
                break;

            case SoundType.GameOver:
                sound = GameOver;
                break;
        }

        if (sound == null)
        {
            return;
        }

        if (soundType == SoundType.EnemyDeath)
        {
            sound.pitch = Random.Range(0.5f, 1.5f);
        }
        else
        {
            sound.pitch = 1f;
        }
    sound.Stop();
    sound.Play();
}   

}
