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
        animator.SetTrigger("FadeOut");
    }

    public void QuitGame()
    {
        Debug.Log("quit!");
        Application.Quit(); 
    }

}
