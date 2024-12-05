/* 
Works Remaining:        - Transition   
Problems:               - NA
 */

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindEnemy : MonoBehaviour
{
    public Animator transition;
    public GameObject Controller;
    Save save = new Save();

    // Loads TurnBased Battle scene on call
    public void SceneManagement()
    {
        save.OnButtonClick();
        DontDestroyOnLoad(Controller);
        SceneManager.LoadScene(2);
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f); // Suspends code for 1 second

        SceneManager.LoadScene(levelIndex);
    }
}
