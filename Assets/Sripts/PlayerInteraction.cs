using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRange = 1.5f;

    private Vector2 lastMoveDirection = Vector2.down;

    public void UpdateLastDirection(Vector2 moveInput)
    {
        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastMoveDirection, interactRange);

        if (hit.collider != null)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}