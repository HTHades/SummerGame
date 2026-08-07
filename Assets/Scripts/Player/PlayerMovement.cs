using UnityEngine;
using UnityEngine.InputSystem; // thư viên để dùng inputAction

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Rigidbody2D rb2d;
    private InputAction moveAction;
    private Vector2 movement;
    private Vector2 lastMovement;
    private Animator animator;
    [SerializeField] private float speed = 2f;
    public Vector2 LastMovement
    {
        get
        {
            return lastMovement;
        }
    }
    public Transform PlayerTransform
    {
        get
        {
            return playerTransform;
        }
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        lastMovement = new Vector3(0,-1);
    }
    private void Update()
    {
        movement = moveAction.ReadValue<Vector2>();
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        if (movement != Vector2.zero)
        {
            lastMovement = movement;
        }
    }

    private void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector3 (movement.x * speed, movement.y * speed, 0);
    }
}