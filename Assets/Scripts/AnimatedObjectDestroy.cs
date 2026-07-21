using UnityEngine;

public class AnimatedObjectDestroy : MonoBehaviour
{
    [SerializeField] private Animator animator; // Reference to the Animator component
    void Start()
    {
        Object.Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
