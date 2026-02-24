using UnityEngine;
using UnityEngine.UI;

public class TabControl : MonoBehaviour
{
    public Image[] tabImages; // Array of Image components for the tabs
    public GameObject[] pages; // Array of GameObjects for the pages
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       ActivateEscape(0); // Activate the first tab and page by default 
    }

    public void ActivateEscape(int tabNo)
    { 
        for(int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false); 
            tabImages[i].color = Color.grey; // Set all tabs to white
        }
        pages[tabNo].SetActive(true); // Activate the selected page
        tabImages[tabNo].color = Color.white; // Set the selected tab to grey
    }
}
