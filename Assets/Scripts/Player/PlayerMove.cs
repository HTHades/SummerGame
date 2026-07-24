using UnityEngine;
using UnityEngine.InputSystem; // thư viên để dùng inputAction

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private InputAction moveAction;
    private Vector2 movement;
    private Animator animator;
    [SerializeField] private float speed = 2f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        movement = moveAction.ReadValue<Vector2>();
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
    }

    private void FixedUpdate()
    {
       
        rb2d.linearVelocity = new Vector3 (movement.x * speed, movement.y * speed, 0);
       // FlipSprite();
    }
    // private void FlipSprite()
    // {
    //     if( movement.x > 0 )
    //     {
    //         transform.localScale = new Vector3(1,1,1); 
    //     }
    //     if( movement.x <0 )
    //     {
    //         transform.localScale = new Vector3(-1,1,1);
    //     }
    // }
}