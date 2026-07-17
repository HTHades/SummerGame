using UnityEngine;
using UnityEngine.Timeline;

public class WhipWeapon : MonoBehaviour
{
    private float timeDelay = 4f;
    private float timer;
    void Update()
    {
        timer -= Time.deltaTime;
        if( timer < 0)
        {
            Attack();
        }
    }
    private void Attack()
    {
        Debug.Log("người chơi đánh nè");
        timer = timeDelay;
    }
}
