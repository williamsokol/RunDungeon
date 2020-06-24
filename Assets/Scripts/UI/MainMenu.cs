using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    
    public Animator animator;

    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
   
    public void FadeToLevel()
    {
        animator.SetBool("FadeOut", true);
    }

    public void QuitGame()
    {
        Debug.Log("quit!");
        Application.Quit(); 
    }

    public void NewFloor()
    {
        GameObject.Find("LevelBuilder").GetComponent<LevelBuilder>().ResetLevelGenerator();
        Time.timeScale = 1f;
        animator.SetBool("FadeOut", false);
    }
}
