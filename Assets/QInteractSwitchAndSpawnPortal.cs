using UnityEngine;

public class QInteractSwitchAndSpawnPortal : MonoBehaviour
{
    [Header("Interaction")]
    public KeyCode interactKey = KeyCode.Q;

    [Header("Sprite swap")]
    public SpriteRenderer targetRenderer;   // ако е празно, ще вземе SpriteRenderer от този обект
    public Sprite newSprite;

    [Header("Portal spawn")]
    public GameObject portalPrefab;         // ако ползваш Instantiate
    public Transform portalSpawnPoint;      // къде да се появи порталът (в далечината)
    public GameObject existingPortal;       // ако порталът вече е в сцената (изключен)

    [Header("One-time use")]
    public bool onlyOnce = true;

    bool playerInRange;
    bool used;

    void Awake()
    {
        if (!targetRenderer) targetRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!playerInRange) return;
        if (onlyOnce && used) return;

        if (Input.GetKeyDown(interactKey))
        {
            // 1) Смени изображението
            if (targetRenderer && newSprite)
                targetRenderer.sprite = newSprite;

            // 2) Появи портал
            if (existingPortal != null)
            {
                existingPortal.SetActive(true);
            }
            else if (portalPrefab != null && portalSpawnPoint != null)
            {
                Instantiate(portalPrefab, portalSpawnPoint.position, portalSpawnPoint.rotation);
            }

            used = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}