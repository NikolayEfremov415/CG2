using UnityEngine;

public class ArrowFloat : MonoBehaviour
{
    public float floatSpeed = 2f;     // скорост
    public float floatHeight = 0.25f; // височина на движението

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = startPos + new Vector3(0, newY, 0);
    }
}