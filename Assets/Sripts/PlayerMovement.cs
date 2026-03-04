using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    public Vector2 moveInput;
    private Animator animator;

    // Запомня последната посока
    public Vector2 lastDirection = Vector2.down;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveSpeed * moveInput;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        // Ако има движение → запомняме посоката
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastDirection = moveInput.normalized;
        }

        // Анимации
        animator.SetBool("isWalking", moveInput.sqrMagnitude > 0.01f);

        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);

        animator.SetFloat("LastInputX", lastDirection.x);
        animator.SetFloat("LastInputY", lastDirection.y);
    }
}