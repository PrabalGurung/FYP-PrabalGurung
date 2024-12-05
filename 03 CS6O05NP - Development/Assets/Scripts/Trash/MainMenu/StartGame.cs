
/* 
Works Remaining:        - NA     
Problems:               - NA
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Animator transition;

    // Initiate scene management
    public void SceneManagement()
    {
        StartCoroutine(LoadLevel(1)); // Start coroutine to load level 
    }

    // Starts load level transition for smooth animation
    public IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f); // Suspends code for 1 second

        SceneManager.LoadScene(levelIndex);
    }
}
