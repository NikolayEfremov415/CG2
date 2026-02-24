using UnityEngine;

public class MenuControler : MonoBehaviour
{
    public GameObject menuCanvas; // Reference to the menu canvas
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false); // Ensure the menu is hidden at the start
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Check if the Tab key is pressed
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf); // Toggle the menu visibility
        }
    }
}
