using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    public Transform attackPoint;     // празен обект пред играча
    public float attackRange = 0.5f;  // радиус на удара
    public int damage = 1;
    public float attackCooldown = 0.5f;

    private float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // бутон за атака
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void Attack()
    {
        // намира всички обекти в радиуса
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D obj in hitObjects)
        {
            SimpleDestructible destructible = obj.GetComponent<SimpleDestructible>();

            if (destructible != null)
            {
                destructible.TakeDamage(damage);
            }
        }
    }

    // показва радиуса в Scene view
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}