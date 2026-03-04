using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRange = 0.8f;
    public float attackOffset = 0.8f;
    public int damage = 1;

    public LayerMask hitLayers;

    private PlayerMovement movementScript;

    void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Взимаме последната посока от movement script-а
        Vector2 direction = movementScript.lastDirection.normalized;

        // Изчисляваме позицията пред играча
        Vector2 attackPosition =
            (Vector2)transform.position + direction * attackOffset;

        // Проверяваме какво има в тази зона
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPosition,
            attackRange,
            hitLayers
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<Enemy>().TakeDamage(damage);
            }

            if (hit.CompareTag("Trash"))
            {
                Destroy(hit.gameObject);
            }
        }
    }

    // Само за визуализация в Scene view
    void OnDrawGizmosSelected()
    {
        if (movementScript == null)
            movementScript = GetComponent<PlayerMovement>();

        if (movementScript == null) return;

        Gizmos.color = Color.red;

        Vector2 attackPosition =
            (Vector2)transform.position +
            movementScript.lastDirection.normalized * attackOffset;

        Gizmos.DrawWireSphere(attackPosition, attackRange);
    }
}