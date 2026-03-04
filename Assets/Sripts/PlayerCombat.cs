using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Настройки на атаката")]
    public Transform attackPoint;      // Обектът, от който тръгва удара
    public float attackRange = 0.5f;   // Радиус на удара
    public float attackOffset = 0.7f;  // Разстояние пред играча
    public LayerMask hitLayers;        // Слой "Interactable" (Enemy и Trash)

    [Header("Статистика")]
    public int damage = 1;

    private PlayerMovement moveScript;

    void Start()
    {
        moveScript = GetComponent<PlayerMovement>();

        // Автоматично намиране на AttackPoint, ако не е зададен в Inspector-а
        if (attackPoint == null)
        {
            attackPoint = transform.Find("AttackPoint");
            
            if (attackPoint == null)
            {
                Debug.LogError("Грешка: Не намерих обект с име 'AttackPoint' като дете на Player!");
            }
        }
    }

    void Update()
    {
        // Обновяваме позицията на AttackPoint спрямо посоката на гледане
        UpdateAttackPointPosition();

        // Атака при натискане на Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void UpdateAttackPointPosition()
    {
        if (attackPoint != null && moveScript != null)
        {
            // Поставяме точката на атака пред играча спрямо lastDirection
            attackPoint.localPosition = moveScript.lastDirection * attackOffset;
        }
    }

    void Attack()
    {
        // Проверка за обекти в обсега (OverlapCircle)
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitLayers);

        foreach (Collider2D hit in hitObjects)
        {
            // Логика за Врагове
            if (hit.CompareTag("Enemy"))
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    Debug.Log("Удари враг!");
                }
            }

            // Логика за Боклук
            if (hit.CompareTag("Trash"))
            {
                Destroy(hit.gameObject);
                Debug.Log("Изчисти боклук!");
            }
        }
    }

    // Визуализация в редактора (червен кръг)
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}