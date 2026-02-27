using UnityEngine;

public class ShowArrowOnNear2D : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private string playerTag = "Player";

    private void Awake()
    {
        if (arrow != null) arrow.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            arrow.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            arrow.SetActive(false);
    }
}