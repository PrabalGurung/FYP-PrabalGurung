/* 
Changes menu according to the tabs pressed

Works Remaining:        - NA     
Problems:               - NA
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages;

    // Start is called before the first frame update
    void Start()
    {
        if (pages.Length > 0)
        {
            ActivateTab(0);
        }
    }

    // Changes tab according to tab pressed in game
    public void ActivateTab(int tabNo)
    {
        // Ensure tabNo is within bounds
        if (tabNo < 0 || tabNo >= pages.Length)
        {
            Debug.LogError($"Invalid tab number: {tabNo}. It must be between 0 and {pages.Length - 1}.");
            return;  // Exit the method if tabNo is out of bounds
        }

        // Deactivate all pages and set tab images to grey
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            if (i < tabImages.Length)
            {
                tabImages[i].color = Color.grey;
            }
        }

        // Activate the selected page and set its corresponding tab image to white
        pages[tabNo].SetActive(true);
        tabImages[tabNo].color = Color.white;
    }
}
