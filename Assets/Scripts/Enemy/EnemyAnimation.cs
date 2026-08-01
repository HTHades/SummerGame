using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private bool useMoveX = true;
    [SerializeField] private bool useMoveY = true;

    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetMovement( Vector2 direction)
    {
        if( useMoveX)
        {
            animator.SetFloat("MoveX", direction.x);
        }
        if( useMoveY)
        {
            animator.SetFloat("MoveY", direction.y);
        }
    }
}
